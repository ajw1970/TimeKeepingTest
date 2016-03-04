using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeltyAutomation.TimeKeeping.Interfaces
{
    public class TimeEntry
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public string Project { get; set; }
        public string UserName { get; set; }
        public double Hours { get; set; }
    }
}
