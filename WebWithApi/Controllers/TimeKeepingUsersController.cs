using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeltyAutomation.TimeKeeping.Entities;
using WeltyAutomation.TimeKeeping.Interfaces;

namespace ClearstreamWeb.Controllers
{
    [System.Web.Mvc.OutputCache(Duration = 0)]
    public class TimeKeepingUsersController : ApiController
    {
        private readonly ITimeKeepingApplication app;

        public TimeKeepingUsersController(ITimeKeepingApplication app)
        {
            this.app = app;
        }

        // GET api/TimeKeepingUsers
        public IEnumerable<WacsTimeKeepingUser> Get()
        {
            return app.GetMyDepartmentUsers(User.Identity.Name)
                .OrderBy(u => u.DisplayName);
        }

        // GET api/TimeKeepingUsers/5
        [HttpGet]
        public WacsTimeKeepingUser Get(int id)
        {
            return app.GetUser(User.Identity.Name, userId: id);
        }

        // POST api/TimeKeepingUsers
        [HttpPost]
        [ActionName("TimeKeepingUsers")]
        [ResponseType(typeof(WacsTimeKeepingUser))]
        public async Task<IHttpActionResult> Post([FromBody]WacsTimeKeepingUser postedUser)
        {
            var newUser = app.AddUser(User.Identity.Name, postedUser);

            if (newUser == null)
            {
                return InternalServerError();
            }

            return Ok(newUser);
        }

        // PUT api/TimeKeepingUsers/5
        [HttpPut]
        [ActionName("TimeKeepingUsers")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]WacsTimeKeepingUser updatedUser)
        {
            var updatedOk = app.UpdateUser(User.Identity.Name, id, updatedUser);

            if (updatedOk)
            {
                return Ok();
            }

            return InternalServerError();
        }

        // DELETE api/TimeKeepingUsers/5
        [HttpDelete]
        [ActionName("TimeKeepingUsers")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            bool deleted = app.DeleteUser(User.Identity.Name, id);

            if (deleted)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}
