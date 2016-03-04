using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeltyAutomation.TimeKeeping.Entities;

namespace WeltyAutomation.TimeKeeping.Interfaces
{
    public interface ITimeKeepingApplication
    {
        IEnumerable<WacsTimeKeepingEntry> GetEntriesForUser(string login);
        WacsTimeKeepingEntry AddEntryForUser(string name, WacsTimeKeepingEntry postedEntry);
        bool UpdateEntry(string login, int entryId, WacsTimeKeepingEntry updatedEntry);
        bool DeleteEntry(int id);
        IEnumerable<TimeEntry> GetMyDepartmentSummary(string login);
        IEnumerable<WacsTimeKeepingProject> GetProjectList(string login);
        WacsTimeKeepingProject AddProject(string login, WacsTimeKeepingProject project);
        IEnumerable<WacsTimeKeepingUser> GetMyDepartmentUsers(string login);
        WacsTimeKeepingProject GetProject(string login, int id);
        bool UpdateProject(string login, int projectId, WacsTimeKeepingProject project);
        WacsTimeKeepingUser GetUser(string login, int userId);
        bool DeleteProject(string login, int projectId);
        WacsTimeKeepingUser AddUser(string login, WacsTimeKeepingUser user);
        bool UpdateUser(string login, int userId, WacsTimeKeepingUser user);
        bool DeleteUser(string login, int userId);
    }
}
