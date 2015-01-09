using System;

namespace NetVietDev.Performance.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime GetToSecond(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second);
        }
    }
}
