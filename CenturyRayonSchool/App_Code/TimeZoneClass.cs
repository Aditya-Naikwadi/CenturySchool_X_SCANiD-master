using System;

namespace CenturyRayonSchool.Model
{
    public static class TimeZoneClass
    {
        private static TimeZoneInfo India_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");



        public static DateTime getIndianTimeZoneValues()
        {
            try
            {
                DateTime dateTime_Indian = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Standard_Time);
                return dateTime_Indian;
            }
            catch (Exception ex)
            {
                Log.Error("TimeZoneClass.getIndianTimeZoneValues", ex);
                return DateTime.UtcNow;
            }
        }



    }
}