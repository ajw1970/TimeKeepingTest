using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeltyAutomation.TimeKeeping.Entities;
using WeltyAutomation.TimeKeeping.Interfaces;

namespace WeltyAutomation.TimeKeeping.Application
{
    public class TimeKeepingApplication : ITimeKeepingApplication
    {
        ITimeKeepingRepository db;

        public TimeKeepingApplication(ITimeKeepingRepository db)
        {
            this.db = db;
        }

        private WacsTimeKeepingUser getAdminUserByLogin(string login)
        {
            var user = getUserByLogin(login);

            var admin = db.Users
                    .Where(u => u.DepartmentId == user.DepartmentId && u.IsDepartmentAdmin)
                    .FirstOrDefault();

            return admin ?? new WacsTimeKeepingUser();
        }

        private WacsTimeKeepingUser getUserByLogin(string login)
        {
            var user = db.Users
                    .Where(u => u.Login == login)
                    .FirstOrDefault();

            if (user == null)
            {
                return new WacsTimeKeepingUser();
            }

            return user;
        }

        public IEnumerable<WacsTimeKeepingEntry> GetEntriesForUser(string login)
        {
            var user = getUserByLogin(login);

            return db.Entries
                     .Where(e => e.UserId == user.Id)
                     .OrderBy(e => e.Started)
                     .ToList();
        }

        public WacsTimeKeepingEntry AddEntryForUser(string login, WacsTimeKeepingEntry postedEntry)
        {
            var user = getUserByLogin(login);

            var existingProject = db.Projects
                .Where(p =>
                         p.DepartmentId == user.DepartmentId &&
                         p.Id == postedEntry.ProjectId)
                .FirstOrDefault();
            if (existingProject == null)
            {
                return null;
            }

            var newEntry = new WacsTimeKeepingEntry
            {
                Id = 0,
                UserId = user.Id,
                ProjectId = postedEntry.ProjectId,
                Started = postedEntry.Started,
                Ended = postedEntry.Ended
            };

            db.AddEntry(newEntry);

            return newEntry;
        }

        public bool UpdateEntry(string login, int entryId, WacsTimeKeepingEntry updatedEntry)
        {
            var user = getUserByLogin(login);

            updatedEntry.Id = entryId;
            updatedEntry.UserId = user.Id;

            return db.UpdateEntry(updatedEntry);
        }

        public bool DeleteEntry(int id)
        {
            return db.DeleteEntry(id);
        }

        public IEnumerable<TimeEntry> GetMyDepartmentSummary(string login)
        {
            var user = getUserByLogin(login);

            if (user.IsDepartmentAdmin == false)
            {
                return null;
            }

            var query = from p in db.Projects.Where(p => p.DepartmentId == user.DepartmentId)
                        join e in db.Entries on p.Id equals e.ProjectId
                        join u in db.Users on e.UserId equals u.Id
                        select new
                        {
                            project = p,
                            entry = e,
                            user = u
                        };

            var list = query.ToList();

            var entries = list.Select(d => new
            {
                Year = d.entry.Started.Year,
                MonthNo = d.entry.Started.Month,
                Month = string.Format("{0:MMMM}", d.entry.Started),
                ProjectDescription = d.project.Description,
                Project = formattedProjectDescription(d.project.SaleNo, d.project.Description, d.project.Active),
                UserName = d.user.DisplayName,
                Hours = d.entry.Ended == null ? 0.25 : ((DateTimeOffset) d.entry.Ended).Subtract(d.entry.Started).TotalHours
            });

            var summary = from e in entries
                          orderby e.Year descending, e.MonthNo descending, e.ProjectDescription, e.UserName
                          group e by new { e.Year, e.Month, e.Project, e.UserName } into eg
                          select new TimeEntry
                          {
                              Year = eg.Key.Year,
                              Month = eg.Key.Month,
                              Project = eg.Key.Project,
                              UserName = eg.Key.UserName,
                              Hours = eg.Sum(e => e.Hours)
                          };

            return summary;
        }

        private string formattedProjectDescription(string saleNo, string description, bool active)
        {
            var sb = new StringBuilder(saleNo.TrimEnd());
            if (sb.Length > 0)
            {
                sb.Append(": ");
            }
            sb.Append(description.TrimEnd());
            if (!active)
            {
                sb.Append((" (Inactive)"));
            }
            return sb.ToString();
        }

        public IEnumerable<WacsTimeKeepingProject> GetProjectList(string login)
        {
            var user = getUserByLogin(login);

            var query = db.Projects
                .Where(p => p.DepartmentId == user.DepartmentId)
                .OrderByDescending(p => p.Active)
                .ThenBy(p => p.Description);
            return query.ToList();
        }

        public WacsTimeKeepingProject GetProject(string login, int projectId)
        {
            var user = getUserByLogin(login);

            return db.Projects
                .Where(p => p.DepartmentId == user.DepartmentId && p.Id.Equals(projectId))
                .FirstOrDefault();
        }

        public WacsTimeKeepingProject AddProject(string login, WacsTimeKeepingProject postedProject)
        {
            if (postedProject.Id != 0)
            {
                return null;
            }

            var user = getUserByLogin(login);

            var existingProject = db.Projects
                .Where(p =>
                         p.DepartmentId == user.Id &&
                         String.IsNullOrEmpty(p.SaleNo) ?
                            p.Description == postedProject.Description :
                            p.SaleNo == postedProject.SaleNo && p.Description == postedProject.Description)
                .FirstOrDefault();

            if (existingProject != null)
            {
                return null;
            }

            var newProject = new WacsTimeKeepingProject
            {
                Id = 0,
                SaleNo = postedProject.SaleNo,
                Description = postedProject.Description,
                DepartmentId = user.DepartmentId,
                Active = postedProject.Active,
                OpeningHours = postedProject.OpeningHours,
                NpdHours = postedProject.NpdHours,
            };
            return db.AddProject(newProject);
        }

        public bool UpdateProject(string login, int projectId, WacsTimeKeepingProject project)
        {
            var user = getUserByLogin(login);

            var existingProject = db.Projects
                .Where(p => p.DepartmentId == user.DepartmentId && p.Id == projectId)
                .FirstOrDefault();

            if (existingProject == null)
            {
                return false;
            }

            project.Id = projectId;

            return db.UpdateProject(project);
        }

        public bool DeleteProject(string login, int projectId)
        {
            var user = getUserByLogin(login);

            var existingProject = db.Projects
                .Where(p => p.DepartmentId == user.DepartmentId && p.Id == projectId)
                .FirstOrDefault();

            if (existingProject == null)
            {
                return false;
            }

            return db.DeleteProject(projectId);
        }

        public IEnumerable<WacsTimeKeepingUser> GetMyDepartmentUsers(string login)
        {
            var user = getUserByLogin(login);

            if (user.IsDepartmentAdmin == false)
            {
                return new List<WacsTimeKeepingUser>();
            }

            var query = db.Users
                .Where(u => u.DepartmentId == user.DepartmentId)
                .OrderBy(u => u.DisplayName);
            return query.ToList();
        }

        public WacsTimeKeepingUser GetUser(string login, int userId)
        {
            var user = getUserByLogin(login);

            if (user.Id != userId && user.IsDepartmentAdmin == false)
            {
                return new WacsTimeKeepingUser();
            }

            return db.Users
                .Where(u => u.DepartmentId == user.DepartmentId && u.Id.Equals(userId))
                .FirstOrDefault();
        }

        public WacsTimeKeepingUser AddUser(string login, WacsTimeKeepingUser postedUser)
        {
            if(postedUser.Id != 0)
            {
                return null;
            }

            var my = getUserByLogin(login);

            var newUser = new WacsTimeKeepingUser
            {
                Id = 0,
                Login = postedUser.Login,
                DisplayName = postedUser.DisplayName,
                DepartmentId = my.DepartmentId, 
                IsDepartmentAdmin = false
            };
            return db.AddUser(newUser);
        }

        public bool UpdateUser(string login, int userId, WacsTimeKeepingUser user)
        {
            var my = getUserByLogin(login);

            var existingUser = db.Users
                .Where(p => p.DepartmentId == my.DepartmentId && p.Id == userId)
                .FirstOrDefault();

            if (existingUser == null)
            {
                return false;
            }

            return db.UpdateUser(user);
        }

        public bool DeleteUser(string login, int userId)
        {
            var my = getUserByLogin(login);

            var existingUser = db.Users
                .Where(p => p.DepartmentId == my.DepartmentId && p.Id == userId)
                .FirstOrDefault();

            if (existingUser == null)
            {
                return false;
            }

            return db.DeleteUser(userId);
        }
    }
}
