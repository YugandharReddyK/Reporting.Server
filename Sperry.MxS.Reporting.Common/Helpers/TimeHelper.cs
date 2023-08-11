using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public class TimeHelper
    {
        private static string _timePartFormat = "h\\:mm\\:ss";

        private static string _dayPartFormat = "d\\.";

        private static readonly string UtcDisplayName = TimeZoneInfo.Utc.DisplayName;

        public static string GetTimeSpanString(long? ticks)
        {
            if (ticks == null)
                return null;
            string returnString = (ticks > 0 ? "-" : "+");
            TimeSpan timeSpan = -TimeSpan.FromTicks(ticks.Value);
            if (timeSpan.TotalDays >= 1 || timeSpan.TotalDays <= -1)
            {
                returnString += timeSpan.ToString(_dayPartFormat + _timePartFormat);
            }
            else
            {
                returnString += timeSpan.ToString(_timePartFormat);
            }
            return returnString;
        }
        public static string GetLocalTimeZone()
        {
            TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
            return (utcOffset >= TimeSpan.Zero) ? ("+" + utcOffset.ToString("hh\\:mm")) : ("-" + utcOffset.ToString("hh\\:mm"));
        }
        public static DateTime GetDateTimeByTimeZone(DateTime source, string timeZoneType, string rigTimeZone, MxSTimeZoneTypes sourceTimeZone = MxSTimeZoneTypes.RigTimeZone)
        {
            TimeZoneInfo timeZoneByDisplayName = rigTimeZone.GetTimeZoneByDisplayName(defaultToLocalTimeZone: false);
            if (sourceTimeZone == MxSTimeZoneTypes.RigTimeZone)
            {
                DateTime result = timeZoneByDisplayName.ConvertToUtc(source);
                if (timeZoneType == MxSTimeZoneTypes.UTCTimeZone.ToString())
                {
                    return result;
                }

                if (timeZoneType == MxSTimeZoneTypes.ClientTimeZone.ToString())
                {
                    return result.ToLocalTime();
                }
            }

            if (sourceTimeZone == MxSTimeZoneTypes.UTCTimeZone)
            {
                if (timeZoneType == MxSTimeZoneTypes.ClientTimeZone.ToString())
                {
                    return source.ToLocalTime();
                }

                if (timeZoneType == MxSTimeZoneTypes.RigTimeZone.ToString())
                {
                    rigTimeZone.GetTimeZoneByDisplayName(defaultToLocalTimeZone: false);
                    return timeZoneByDisplayName.ConvertFromUtc(source);
                }
            }
            return source;
        }
        public static string GetTimeZoneHeader(string header, string timeZoneType, string rigTimeZone)
        {
            if (string.IsNullOrEmpty(header) || (!header.Contains("[UTC") && !header.StartsWith("Time&Date")))
            {
                return header;
            }

            if (header == "Time&Date")
            {
                header += $" [UTC {GetLocalTimeZone()}]";
            }

            if (timeZoneType == MxSTimeZoneTypes.UTCTimeZone.ToString())
            {
                return ChangeTimeZoneInfo(header, string.Empty);
            }

            if (timeZoneType == MxSTimeZoneTypes.RigTimeZone.ToString())
            {
                return ChangeTimeZoneInfo(header, $" {GetRigTimeZoneOffset(rigTimeZone)}");
            }

            return header;
        }
        public static string ChangeTimeZoneInfo(string header, string offset)
        {
            int num = header.IndexOf("[UTC") + 4;
            int num2 = header.IndexOf("]");
            string oldValue = header.Substring(num, num2 - num);
            return header.Replace(oldValue, offset.Trim().Equals(UtcDisplayName) ? string.Empty : offset);
        }
        public static string GetRigTimeZoneOffset(string rigTimeZone)
        {
            if (rigTimeZone == UtcDisplayName)
            {
                return UtcDisplayName;
            }

            if (string.IsNullOrEmpty(rigTimeZone))
            {
                return string.Empty;
            }

            int num = rigTimeZone.IndexOf("+");
            if (num <= 0)
            {
                num = rigTimeZone.IndexOf("-");
            }

            if (num < 0)
            {
                return UtcDisplayName;
            }

            int num2 = rigTimeZone.IndexOf(")");
            return rigTimeZone.Substring(num, num2 - num);
        }
        public static string GetTimeSpanString(TimeSpan? timeSpan)
        {
            if (!timeSpan.HasValue)
            {
                return null;
            }

            string text = "";
            if (timeSpan.HasValue && timeSpan.GetValueOrDefault().Days >= 1)
            {
                text = text + " " + timeSpan?.Days + "d";
            }

            if (timeSpan.HasValue && timeSpan.GetValueOrDefault().Hours >= 1)
            {
                text = text + " " + timeSpan?.Hours + "h";
            }

            if (timeSpan.HasValue && timeSpan.GetValueOrDefault().Minutes >= 1)
            {
                text = text + " " + timeSpan?.Minutes + "m";
            }

            if (timeSpan.HasValue && timeSpan.GetValueOrDefault().Seconds >= 1)
            {
                text = text + " " + timeSpan?.Seconds + "s";
            }

            if (timeSpan.HasValue && timeSpan.GetValueOrDefault().Milliseconds >= 1)
            {
                text = text + " " + timeSpan?.Milliseconds + "ms";
            }

            return text;
        }
    }
}
