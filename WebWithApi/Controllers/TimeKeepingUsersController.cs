using ClearstreamWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeltyAutomation.M2M.QueryData;

namespace ClearstreamWeb.Controllers {
    public class TimeKeepingUsersController : ApiController {
        ITimeKeepingRepository db = TestTimeKeepingRepository.Instance;

        private string adminDepartment {
            get {
                var admin = db.GetAdminUserByName(User.Identity.Name);

                if (admin == null) {
                    return null;
                }

                return admin.Department;
            }
        }

        // GET api/TimeKeepingUsers
        public IEnumerable<WacsTimeKeepingUser> Get() {
            var query = db.Users
                .Where(u => u.Department == adminDepartment)
                .OrderBy(u => u.DisplayName);
            return query.ToList();
        }

        // GET api/TimeKeepingUsers/5
        [HttpGet]
        public WacsTimeKeepingUser Get(int id) {
            return db.Users
                .Where(u => u.Department == adminDepartment && u.Id.Equals(id))
                .FirstOrDefault();
        }

        // POST api/TimeKeepingUsers
        [HttpPost]
        [ActionName("TimeKeepingUsers")]
        public WacsTimeKeepingUser Post([FromBody]WacsTimeKeepingUser postedUser) {
            var adminDepartment = this.adminDepartment;
            if (adminDepartment == null) {
                return null;
            }

            var existingUser = db.Users
                .Where(u => u.Login.Equals(postedUser.Login))
                .FirstOrDefault();

            if (existingUser != null) {
                return null;
            }

            var newUser = new WacsTimeKeepingUser {
                Id = 0,
                Login = postedUser.Login,
                DisplayName = postedUser.DisplayName,
                Department = adminDepartment,
                IsDepartmentAdmin = false
            };
            db.AddUser(newUser);

            return newUser;
        }

        // PUT api/TimeKeepingUsers/5
        [HttpPut]
        [ActionName("TimeKeepingUsers")]
        public void Put([FromBody]WacsTimeKeepingUser updatedUser) {
            var existingUser = db.Users
                .Where(u => u.Department == adminDepartment && u.Id == updatedUser.Id)
                .FirstOrDefault();

            if (existingUser == null) {
                return;
            }

            existingUser.Login = updatedUser.Login;
            existingUser.DisplayName = updatedUser.DisplayName;
            db.UpdateUser(existingUser);
        }

        // DELETE api/TimeKeepingUsers/5
        [HttpDelete]
        [ActionName("TimeKeepingUsers")]
        public void Delete(int id) {
            var existingUser = db.Users
                .Where(u => u.Department == adminDepartment && u.Id == id)
                .FirstOrDefault();

            if (existingUser != null) {
                db.DeleteUser(existingUser.Id);
            }
        }
    }
}
