using ClearstreamWeb.Models;
using ClearstreamWeb.Models.TimeKeeping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeltyAutomation.M2M.QueryData;

namespace ClearstreamWeb.Controllers {
    [Authorize]
    public class TimeKeepingProjectsController : ApiController {
        ITimeKeepingRepository db = TestTimeKeepingRepository.Instance;

        private int departmentAdminId {
            get {
                var admin = db.Users
                    .Where(u => u.Department == user.Department && u.IsDepartmentAdmin)
                    .OrderBy(u => u.Id)
                    .FirstOrDefault();

                if (admin == null) {
                    return 0;
                }

                return admin.Id;
            }
        }

        private WacsTimeKeepingUser user {
            get {
                var user = db.GetUserByName(User.Identity.Name);

                if (user == null) {
                    return new WacsTimeKeepingUser();
                }

                return user;
            }
        }

        [HttpGet]
        [Route("api/TimeKeepingProjects/MyDepartmentSummary")]
        [ResponseType(typeof(IEnumerable<TimeEntry>))]
        public async Task<IHttpActionResult> GetMyDepartmentSummary() {
            if (user.IsDepartmentAdmin == false) {
                return BadRequest("Restricted to Time Keeping Admins");
            }

            var query = from p in db.Projects.Where(p => p.CreatedByUserId == departmentAdminId)
                        join e in db.Entries on p.Id equals e.ProjectId
                        join u in db.Users on e.UserId equals u.Id
                        select new {
                            project = p,
                            entry = e,
                            user = u
                        };

            var list = query.ToList();

            var entries = list.Select(d => new {
                Year = d.entry.Started.Year,
                MonthNo = d.entry.Started.Month,
                Month = string.Format("{0:MMMM}", d.entry.Started),
                ProjectDescription = d.project.Description,
                Project = formattedProjectDescription(d.project.SaleNo, d.project.Description, d.project.Active),
                UserName = d.user.DisplayName,
                Hours = d.entry.Ended == null ? 0.25 : ((DateTimeOffset)d.entry.Ended).Subtract(d.entry.Started).TotalHours
            });

            var summary = from e in entries
                          orderby e.Year descending, e.MonthNo descending, e.ProjectDescription, e.UserName
                          group e by new { e.Year, e.Month, e.Project, e.UserName } into eg
                          select new TimeEntry {
                              Year = eg.Key.Year,
                              Month = eg.Key.Month,
                              Project = eg.Key.Project,
                              UserName = eg.Key.UserName,
                              Hours = eg.Sum(e => e.Hours)
                          };

            return Ok(summary);
        }

        private string formattedProjectDescription(string saleNo, string description, bool active) {
            var sb = new StringBuilder(saleNo.TrimEnd());
            if (sb.Length > 0) {
                sb.Append(": ");
            }
            sb.Append(description.TrimEnd());
            if (!active) {
                sb.Append((" (Inactive)"));
            }
            return sb.ToString();
        }

        // GET api/TimeKeepingProjects
        public IEnumerable<WacsTimeKeepingProject> Get() {
            var query = db.Projects
                .Where(p => p.CreatedByUserId == departmentAdminId)
                .OrderByDescending(p => p.Active)
                .ThenBy(p => p.Description);
            return query.ToList();
        }

        // GET api/TimeKeepingProjects/5
        [HttpGet]
        public WacsTimeKeepingProject Get(int id) {
            return db.Projects
                .Where(p => p.CreatedByUserId == departmentAdminId && p.Id.Equals(id))
                .FirstOrDefault();
        }

        // POST api/TimeKeepingProjects
        [HttpPost]
        [ActionName("TimeKeepingProjects")]
        [ResponseType(typeof(WacsTimeKeepingProject))]
        public async Task<IHttpActionResult> Post([FromBody]WacsTimeKeepingProject postedProject) {
            var departmentAdminId = this.departmentAdminId;
            if (departmentAdminId == 0) {
                return InternalServerError();
            }

            if (postedProject.Id != 0) {
                return InternalServerError();
            }

            var existingProject = db.Projects
                .Where(p =>
                         p.CreatedByUserId == departmentAdminId &&
                         String.IsNullOrEmpty(p.SaleNo) ?
                            p.Description == postedProject.Description :
                            p.SaleNo == postedProject.SaleNo && p.Description == postedProject.Description)
                .FirstOrDefault();

            if (existingProject != null) {
                return InternalServerError();
            }

            var newProject = new WacsTimeKeepingProject {
                Id = 0,
                SaleNo = postedProject.SaleNo,
                Description = postedProject.Description,
                CreatedByUserId = departmentAdminId,
                Active = postedProject.Active
            };
            db.AddProject(newProject);

            return Ok(newProject);
        }

        // PUT api/TimeKeepingProjects/5
        [HttpPut]
        [ActionName("TimeKeepingProjects")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]WacsTimeKeepingProject updatedProject) {
            var existingProject = db.Projects
                .Where(p => p.CreatedByUserId == departmentAdminId && p.Id == id)
                .FirstOrDefault();

            if (existingProject == null) {
                return NotFound();
            }

            existingProject.SaleNo = updatedProject.SaleNo;
            existingProject.Description = updatedProject.Description;
            existingProject.Active = updatedProject.Active;
            db.UpdateProject(existingProject);

            return Ok();
        }

        // DELETE api/TimeKeepingProjects/5
        [HttpDelete]
        [ActionName("TimeKeepingProjects")]
        public async Task<IHttpActionResult> Delete(int id) {
            var existingProject = db.Projects
                .Where(p => p.CreatedByUserId == departmentAdminId && p.Id == id)
                .FirstOrDefault();

            if (existingProject != null) {
                db.DeleteProject(existingProject.Id);
                return Ok();
            }
            return NotFound();
        }
    }
}
