using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeltyAutomation.TimeKeeping.Utilities
{

    public class DateRange
    {
        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }

        public DateRange(DateTimeOffset from, DateTimeOffset to)
        {
            From = from;
            To = to;
        }
    }
}
