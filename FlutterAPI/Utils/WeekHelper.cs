using System.Globalization;
using System.Text.RegularExpressions;

namespace FlutterAPI.Utils
{
    public static class WeekHelper
    {
        private static readonly CultureInfo cultureInfo = CultureInfo.CurrentCulture; // Use current culture for week calculations

        /// <summary>
        /// Converts a week string in the format "yyyy-Www" to the first day of that week.
        /// </summary>
        /// <param name="weekString">The week string in the specified format.</param>
        /// <returns>The first day of the specified week.</returns>
        /// <exception cref="ArgumentException">If the week string format is invalid.</exception>
        public static DateTime ToFirstDay(string? weekString)
        {
            if (weekString == null) return new DateTime(1, 1, 1);

            var match = Regex.Match(weekString, @"^(\d{4})-W(\d{2})$");

            if (match.Success)
            {
                int year = int.Parse(match.Groups[1].Value);
                int weekNumber = int.Parse(match.Groups[2].Value);

                // Handle edge cases for week 1 and 52/53:
                /*if (weekNumber == 1)
                {
                    // Week 1 might start in the previous year:
                    var startDate = new DateTime(year, 1, 1);
                    if (cultureInfo.DateTimeFormat.FirstDayOfWeek > startDate.DayOfWeek)
                    {
                        year--;
                    }
                }
                else if (weekNumber == 52 || weekNumber == 53)
                {
                    // Week 52/53 might start in the next year:
                    var startDate = new DateTime(year, 12, 31);
                    if (cultureInfo.DateTimeFormat.FirstDayOfWeek <= startDate.DayOfWeek)
                    {
                        year++;
                    }
                }*/

                // Calculate the offset from the year's start date:
                int daysToAdd = ((weekNumber - 1) * 7);//+ (int)cultureInfo.DateTimeFormat.FirstDayOfWeek;

                // Return the starting date of the week:
                return new DateTime(year, 1, 1).AddDays(daysToAdd);
            }
            else
            {
                throw new ArgumentException("Invalid week string format. Expected 'yyyy-Www'.");
            }
        }

        /// <summary>
        /// Converts a date to a week string in the format "yyyy-Wwww".
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>The week string for the given date.</returns>
        public static string ToWeekString(DateTime date)
        {
            // Calculate the ISO week number and year:
            var isoWeek = cultureInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var isoYear = cultureInfo.Calendar.GetYear(date);

            // Handle edge cases for weeks 52/53:
            /*if (isoWeek == 1)
            {
                var prevYearLastDate = new DateTime(isoYear - 1, 12, 31);
                if (cultureInfo.Calendar.GetWeekOfYear(prevYearLastDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) > 1)
                {
                    isoYear--;
                }
            }
            else if (isoWeek == 52 || isoWeek == 53)
            {
                var nextYearFirstDate = new DateTime(isoYear + 1, 1, 1);
                if (cultureInfo.Calendar.GetWeekOfYear(nextYearFirstDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) == 1)
                {
                    isoYear++;
                }
            }*/

            return $"{isoYear}-W{isoWeek:00}";
        }
    }

}
