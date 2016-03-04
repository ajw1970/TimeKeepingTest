using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeltyAutomation.TimeKeeping.Utilities
{
    public static class TimeKeepingExtenstions
    {
        public static DateRange WeekRange(this DateTimeOffset date)
        {
            var dayOfWeek = (int) date.DayOfWeek;

            var dateOnly = date.DateOnly();

            var from = dateOnly.AddDays(dayOfWeek * -1);
            var to = dateOnly.AddDays(7 - dayOfWeek - 1);

            return new DateRange(from, to);
        }

        public static DateRange MonthRange(this DateTimeOffset date)
        {
            var dateOnly = date.DateOnly();

            var from = dateOnly.AddDays((dateOnly.Day * -1) + 1);
            var to = from.AddMonths(1).AddDays(-1);

            return new DateRange(from, to);
        }

        public static DateTimeOffset DateOnly(this DateTimeOffset date)
        {
            return date.AddHours(date.Hour * -1)
                .AddMinutes(date.Minute * -1)
                .AddSeconds(date.Second * -1);
        }
    }
}