using System.Globalization;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// DateTimeUtility
    /// </summary>
    public static class DateTimeUtility
    {
        private static readonly DateTime DATETIME_MIN_VALUE = DateTime.MinValue;


        /// <summary>
        /// Get Sql DateTime yyyy-MM-dd
        /// </summary>
        /// <param name="dt">date time</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S4136:Method overloads should be grouped together", Justification = "")]
        public static string GetSqlDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// Get Full Sql DateTime yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dt">date time</param>
        /// <returns></returns>
        public static string GetFullSqlDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// Get SqlDate Time
        /// </summary>
        /// <param name="date">date string</param>
        /// <returns></returns>
        public static string GetSqlDateTime(string date)
        {
            string[] item = date.Split(new char[] { '/' });
            if (item.Length < 3)
            {
                return GetSqlDateTime(DateTime.Now);
            }
            return string.Join("-", new string[] { item[2], item[1], item[0] });
        }
        /// <summary>
        /// Parse DateTime
        /// </summary>
        /// <param name="value">date string</param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string value)
        {
            DateTime result = DATETIME_MIN_VALUE;
            if (value.Trim() != string.Empty)
            {
                try
                {
                    result = DateTime.Parse(value);
                }
                catch
                {
                    try
                    {
                        result = DateTime.Parse(value);
                    }
                    catch
                    {
                        result = DATETIME_MIN_VALUE;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// ParseDateTimeVN
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ParseDateTimeVN(string value)
        {
            DateTime result = DATETIME_MIN_VALUE;
            if (value.Trim() != string.Empty)
            {
                IFormatProvider culture = new CultureInfo("vi-VN");
                try
                {
                    result = DateTime.Parse(value, culture, DateTimeStyles.NoCurrentDateDefault);
                }
                catch
                {
                    try
                    {
                        culture = new CultureInfo("en-US");
                        result = DateTime.Parse(value, culture, DateTimeStyles.NoCurrentDateDefault);
                    }
                    catch
                    {
                        result = DATETIME_MIN_VALUE;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// ConvertIntTimeToHoursDateTime
        /// </summary>
        /// <param name="intTime">int time</param>
        /// <returns></returns>
        public static DateTime ConvertIntTimeToHoursDateTime(int intTime)
        {
            int hour = (intTime / 60);
            int minute = (intTime % 60);
            DateTime date = new DateTime();
            date = date.Add(new TimeSpan(hour, minute, 0));
            return date;
        }
        /// <summary>
        /// ParseOADateTime
        /// </summary>
        /// <param name="excelDateTimeAsString">excel DateTime As String</param>
        /// <returns></returns>
        public static DateTime ParseOADateTime(string excelDateTimeAsString)
        {
            try
            {
                double oaDateAsDouble = double.Parse(excelDateTimeAsString, CultureInfo.InvariantCulture);
                return DateTime.FromOADate(oaDateAsDouble);
            }
            catch { return DATETIME_MIN_VALUE; }
        }
    }
}
