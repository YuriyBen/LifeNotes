using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Helpers
{
    public static class DayOfWeek
    {
        public static string DayName(this DateTime date)
        {
            string dayName = null;
            if (date.DayOfWeek == DateTime.Now.DayOfWeek && date.ToShortDateString()==DateTime.Now.ToShortDateString())
            {
                dayName = "Today";
            }
            else
            {
                dayName = date.DayOfWeek.ToString();
            }
            return dayName;
        }
    }
}
