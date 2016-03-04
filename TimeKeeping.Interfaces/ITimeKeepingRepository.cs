using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeltyAutomation.TimeKeeping.Entities;

namespace WeltyAutomation.TimeKeeping.Interfaces
{
    public interface ITimeKeepingRepository
    {
        IQueryable<WacsTimeKeepingProject> Projects { get; }
        WacsTimeKeepingProject AddProject(WacsTimeKeepingProject newProject);
        bool UpdateProject(WacsTimeKeepingProject updatedProject);
        bool DeleteProject(int id);

        IQueryable<WacsTimeKeepingUser> Users { get; }

        WacsTimeKeepingUser AddUser(WacsTimeKeepingUser newUser);
        bool UpdateUser(WacsTimeKeepingUser updatedUser);
        bool DeleteUser(int id);

        IQueryable<WacsTimeKeepingEntry> Entries { get; }
        WacsTimeKeepingEntry AddEntry(WacsTimeKeepingEntry newEntry);
        bool UpdateEntry(WacsTimeKeepingEntry updatedEntry);
        bool DeleteEntry(int id);
    }
}
