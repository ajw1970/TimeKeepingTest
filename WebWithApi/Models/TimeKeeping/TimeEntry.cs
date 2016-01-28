using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClearstreamWeb.Models.TimeKeeping {
    public class TimeEntry {
        public int Year { get; set; }
        public string Month { get; set; }
        public string Project { get; set; }
        public string UserName { get; set; }
        public double Hours { get; set; }
    }
}