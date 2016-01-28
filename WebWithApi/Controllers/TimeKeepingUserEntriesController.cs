using ClearstreamWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeltyAutomation.M2M.QueryData;

namespace ClearstreamWeb.Controllers {
    public class TimeKeepingUserEntriesController : ApiController {
        ITimeKeepingRepository db = TestTimeKeepingRepository.Instance;

        private int departmentAdminId {
            get {
                var admin = db.GetAdminUserByName(User.Identity.Name);

                if (admin == null) {
                    return 0;
                }

                return admin.Id;
            }
        }
        private string userDepartment {
            get {
                var user = db.GetUserByName(User.Identity.Name);

                if (user == null) {
                    return null;
                }

                return user.Department;
            }
        }

        private int userId {
            get {
                var user = db.GetUserByName(User.Identity.Name);

                if (user == null) {
                    return 0;
                }

                return user.Id;
            }
        }

        // GET api/TimeKeepingUserEntries
        public IEnumerable<WacsTimeKeepingEntry> Get() {
            return db.Entries
                 .Where(e => e.UserId == userId)
                 .OrderBy(e => e.Started)
                 .ToList();
        }

        // POST api/TimeKeepingUserEntries
        [HttpPost]
        [ActionName("TimeKeepingUserEntries")]
        [ResponseType(typeof(WacsTimeKeepingEntry))]
        public async Task<IHttpActionResult> Post([FromBody]WacsTimeKeepingEntry postedEntry) {
            var departmentAdminId = this.departmentAdminId;
            if (departmentAdminId == 0) {
                return InternalServerError();
            }

            var existingProject = db.Projects
                .Where(p =>
                         p.CreatedByUserId == departmentAdminId &&
                         p.Id == postedEntry.ProjectId)
                .FirstOrDefault();
            if (existingProject == null) {
                return InternalServerError();
            }

            var newEntry = new WacsTimeKeepingEntry {
                Id = 0,
                UserId = userId,
                ProjectId = postedEntry.ProjectId,
                Started = postedEntry.Started,
                Ended = postedEntry.Ended
            };
            db.AddEntry(newEntry);

            return Ok(newEntry);
        }

        // PUT api/TimeKeepingUserEntries/5
        [HttpPut]
        [ActionName("TimeKeepingUserEntries")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]WacsTimeKeepingEntry updatedEntry) {
            var existingEntry = db.Entries
                .Where(e => e.Id == id && e.UserId == userId)
                .FirstOrDefault();

            if (existingEntry == null) {
                return InternalServerError();
            }

            existingEntry.Started = updatedEntry.Started;
            existingEntry.Ended = updatedEntry.Ended;
            existingEntry.ProjectId = updatedEntry.ProjectId;
            db.UpdateEntry(existingEntry);
            return Ok();
        }

        // DELETE api/TimeKeepingUserEntries/5
        [HttpDelete]
        [ActionName("TimeKeepingUserEntries")]
        public async Task<IHttpActionResult> Delete(int id) {
            var existingEntry = db.Entries
                .Where(e => e.Id == id && e.UserId == userId)
                .FirstOrDefault();

            if (existingEntry == null) {
                return InternalServerError();
            }

            db.DeleteEntry(existingEntry.Id);

            return Ok();
        }
    }
}
