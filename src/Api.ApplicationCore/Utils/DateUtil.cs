using System;
using System.Collections.Generic;
using System.Text;

namespace Api.ApplicationCore.Utils
{
    public class DateUtil
    {
        public static DateTime GetCurrentDate()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);
        }
    }
}
