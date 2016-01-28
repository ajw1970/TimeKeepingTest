using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeltyAutomation.M2M.QueryData;

namespace ClearstreamWeb.Models {
    public interface ITimeKeepingRepository {
        IQueryable<WacsTimeKeepingProject> Projects { get; }
        WacsTimeKeepingProject AddProject(WacsTimeKeepingProject newProject);
        bool UpdateProject(WacsTimeKeepingProject updatedProject);
        bool DeleteProject(int id);

        IQueryable<WacsTimeKeepingUser> Users { get; }
        WacsTimeKeepingUser GetUserByName(string login);
        WacsTimeKeepingUser GetAdminUserByName(string login);
        WacsTimeKeepingUser AddUser(WacsTimeKeepingUser newUser);
        bool UpdateUser(WacsTimeKeepingUser updatedUser);
        bool DeleteUser(int id);

        IQueryable<WacsTimeKeepingEntry> Entries { get; }
        WacsTimeKeepingEntry AddEntry(WacsTimeKeepingEntry newEntry);
        bool UpdateEntry(WacsTimeKeepingEntry updatedEntry);
        bool DeleteEntry(int id);
    }

    public class TestTimeKeepingRepository : ITimeKeepingRepository {
        private static TestTimeKeepingRepository instance;
        private List<WacsTimeKeepingUser> users;
        private List<WacsTimeKeepingProject> projects;
        private List<WacsTimeKeepingEntry> entries;

        private TestTimeKeepingRepository() {
            initialize();
        }

        public static TestTimeKeepingRepository Instance {
            get {
                if (instance == null) {
                    instance = new TestTimeKeepingRepository();
                }
                return instance;
            }
        }

        private void initialize() {
            users = new List<WacsTimeKeepingUser> {
                    new WacsTimeKeepingUser {
                    Id = 1,
                    DisplayName = "Test Admin",
                    Department = "Test",
                    IsDepartmentAdmin = true
                }
            };

            projects = new List<WacsTimeKeepingProject> {
                new WacsTimeKeepingProject {
                    Id = 1,
                    SaleNo = "0123",
                    Description = "Project 1",
                    CreatedByUserId = 1,
                    Active = false
                },
                new WacsTimeKeepingProject {
                    Id = 2,
                    SaleNo = "0124",
                    Description = "Project 2",
                    CreatedByUserId = 1,
                    Active = true
                },
                new WacsTimeKeepingProject {
                    Id = 3,
                    SaleNo = "0125",
                    Description = "Project 3",
                    CreatedByUserId = 1,
                    Active = true
                },
            };

            entries = new List<WacsTimeKeepingEntry>();
        }

        public WacsTimeKeepingEntry AddEntry(WacsTimeKeepingEntry newEntry) {
            int lastId = 0;
            if (entries.Count > 0) {
                lastId = entries.Max(e => e.Id);
            }
            newEntry.Id = lastId + 1;
            entries.Add(newEntry);
            return newEntry;
        }

        public WacsTimeKeepingProject AddProject(WacsTimeKeepingProject newProject) {
            throw new NotImplementedException();
        }

        public WacsTimeKeepingUser AddUser(WacsTimeKeepingUser newUser) {
            throw new NotImplementedException();
        }

        public bool DeleteEntry(int id) {
            var entry = entries.Where(e => e.Id == id).FirstOrDefault();
            if (entry == null) {
                return false;        
            }
            entries.Remove(entry);
            return true;
        }

        public bool DeleteProject(int id) {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id) {
            throw new NotImplementedException();
        }

        public IQueryable<WacsTimeKeepingProject> Projects {
            get {
                return projects.AsQueryable();
            }
        }

        public IQueryable<WacsTimeKeepingUser> Users {
            get {
                return users.AsQueryable();
            }
        }

        public WacsTimeKeepingUser GetUserByName(string login) {
            return users.FirstOrDefault();
        }

        public WacsTimeKeepingUser GetAdminUserByName(string login) {
            return users.FirstOrDefault();
        }

        public IQueryable<WacsTimeKeepingEntry> Entries {
            get {
                return entries.AsQueryable();
            }
        }

        public bool UpdateEntry(WacsTimeKeepingEntry updatedEntry) {
            var entry = entries.Where(e => e.Id == updatedEntry.Id).FirstOrDefault();
            if (entry == null) {
                return false;
            }
            entry.ProjectId = updatedEntry.ProjectId;
            entry.Started = updatedEntry.Started;
            entry.Ended = updatedEntry.Ended;
            return true;
        }

        public bool UpdateProject(WacsTimeKeepingProject updatedProject) {
            throw new NotImplementedException();
        }

        public bool UpdateUser(WacsTimeKeepingUser updatedUser) {
            throw new NotImplementedException();
        }
    }
}