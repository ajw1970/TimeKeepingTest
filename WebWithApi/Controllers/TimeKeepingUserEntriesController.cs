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
    public class TimeKeepingUserEntriesController : ApiController {
        private readonly ITimeKeepingApplication app;

        public TimeKeepingUserEntriesController(ITimeKeepingApplication app)
        {
            this.app = app;
        }

        // GET api/TimeKeepingUserEntries
        public IEnumerable<WacsTimeKeepingEntry> Get() {
            return app.GetEntriesForUser(User.Identity.Name);
        }

        //// GET api/TimeKeepingEntries/5
        //[HttpGet]
        //public WacsTimeKeepingEntry Get(int id) {
        //    return db.WacsTimeKeepingProjects
        //        .Where(p => p.CreatedByUserId == departmentAdminId && p.Id.Equals(id))
        //        .FirstOrDefault();
        //}

        // POST api/TimeKeepingUserEntries
        [HttpPost]
        [ActionName("TimeKeepingUserEntries")]
        [ResponseType(typeof(WacsTimeKeepingEntry))]
        public async Task<IHttpActionResult> Post([FromBody]WacsTimeKeepingEntry postedEntry) {
            var newEntry = app.AddEntryForUser(User.Identity.Name, postedEntry);
            if (newEntry == null)
            {
                return InternalServerError();
            }
            return Ok(newEntry);
        }

        // PUT api/TimeKeepingUserEntries/5
        [HttpPut]
        [ActionName("TimeKeepingUserEntries")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]WacsTimeKeepingEntry updatedEntry) {
            if (app.UpdateEntry(User.Identity.Name, id, updatedEntry)) { 
                return Ok();
            } else
            {
                return InternalServerError();
            }
        }

        // DELETE api/TimeKeepingUserEntries/5
        [HttpDelete]
        [ActionName("TimeKeepingUserEntries")]
        public async Task<IHttpActionResult> Delete(int id) {
            if (app.DeleteEntry(id))
            {
                return Ok();
            } else
            {
                return InternalServerError();
            }
        }
    }
}
