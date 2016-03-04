using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeltyAutomation.TimeKeeping.Entities;
using WeltyAutomation.TimeKeeping.Interfaces;

namespace ClearstreamWeb.Controllers {
    [System.Web.Mvc.OutputCache(Duration = 0)]
    public class TimeKeepingProjectsController : ApiController {
        private readonly ITimeKeepingApplication app;

        public TimeKeepingProjectsController(ITimeKeepingApplication app)
        {
            this.app = app;
        }

        [HttpGet]
        [Route("api/TimeKeepingProjects/MyDepartmentSummary")]
        [ResponseType(typeof(IEnumerable<TimeEntry>))]
        public async Task<IHttpActionResult> GetMyDepartmentSummary()
        {
            var summary = app.GetMyDepartmentSummary(User.Identity.Name);
            if (summary == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(summary);
            }
        }

        // GET api/TimeKeepingProjects
        public IEnumerable<WacsTimeKeepingProject> Get()
        {
            return app.GetProjectList(User.Identity.Name);
        }

        // GET api/TimeKeepingProjects/5
        [HttpGet]
        public WacsTimeKeepingProject Get(int id)
        {
            return app.GetProject(User.Identity.Name, id);
        }

        // POST api/TimeKeepingProjects
        [HttpPost]
        [ActionName("TimeKeepingProjects")]
        [ResponseType(typeof(WacsTimeKeepingProject))]
        public async Task<IHttpActionResult> Post([FromBody]WacsTimeKeepingProject postedProject)
        {
            var newProject = app.AddProject(User.Identity.Name, postedProject);

            if (newProject == null)
            {
                return InternalServerError();
            }

            return Ok(newProject);
        }

        // PUT api/TimeKeepingProjects/5
        [HttpPut]
        [ActionName("TimeKeepingProjects")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]WacsTimeKeepingProject model)
        {
            var updatedOk = app.UpdateProject(User.Identity.Name, id, model);

            if (updatedOk)
            {
                return Ok();
            }

            return InternalServerError();
        }

        // DELETE api/TimeKeepingProjects/5
        [HttpDelete]
        [ActionName("TimeKeepingProjects")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var deleted = app.DeleteProject(User.Identity.Name, id);

            if (deleted)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}
