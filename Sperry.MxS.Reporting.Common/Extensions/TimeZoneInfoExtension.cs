using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class TimeZoneInfoExtension
    {
        private static readonly ReadOnlyCollection<TimeZoneInfo> TimeZoneInfos = TimeZoneInfo.GetSystemTimeZones();
        public static DateTime ConvertFromUtc(this TimeZoneInfo timeZoneInfo, DateTime dateTime)
        {
            DateTime dateTime2 = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
            return dateTime2 + timeZoneInfo.BaseUtcOffset;
        }
        public static DateTime ConvertToUtc(this TimeZoneInfo timeZoneInfo, DateTime dateTime)
        {
            DateTime dateTime2 = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
            return dateTime2 - timeZoneInfo.BaseUtcOffset;
        }
        private static TimeZoneInfo GetUtcTimeZone()
        {
            return TimeZoneInfos.FirstOrDefault((TimeZoneInfo item) => item.DisplayName == "(UTC) Coordinated Universal Time");
        }
        public static TimeZoneInfo GetTimeZoneByDisplayName(this string timeZoneName, bool defaultToLocalTimeZone = true)
        {
            TimeZoneInfo timeZoneInfo = (defaultToLocalTimeZone ? TimeZoneInfo.Local : GetUtcTimeZone());
            if (string.IsNullOrWhiteSpace(timeZoneName))
            {
                return timeZoneInfo;
            }

            TimeZoneInfo timeZoneInfo2 = TimeZoneInfos.FirstOrDefault((TimeZoneInfo item) => item.DisplayName == timeZoneName);
            return timeZoneInfo2 ?? timeZoneInfo;
        }
    }
}
