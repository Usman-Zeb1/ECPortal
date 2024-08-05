namespace Pk.Com.Jazz.ECP.Utilities
{
    public static class DateFormatter
    {
        public static string GetFormattedDate(DateTime date)
        {
            string day = date.Day.ToString();
            string suffix = GetDaySuffix(date.Day);
            string month = date.ToString("MMMM");
            string year = date.Year.ToString();
            return $"{day}{suffix} {month}, {year}";
        }

        private static string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
