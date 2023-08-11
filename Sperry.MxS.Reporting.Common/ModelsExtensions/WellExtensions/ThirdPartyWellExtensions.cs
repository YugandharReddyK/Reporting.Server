using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models
{
    public static class ThirdPartyWellExtensions
    {
        public static void UpdateRawSurveyColumnNames(this ThirdPartyWell thirdPartyWell, Well well, List<string> mappedColumnNames)
        {
            string depthDisplayUnitName = FormatHelper.GetUnitName(MxSFormatType.MeasuredDepthDistance, well.UnitSystem);
            string temperatureDisplayUnitName = FormatHelper.GetUnitName(MxSFormatType.Temperature, well.UnitSystem);
            thirdPartyWell.RawSurveys.Columns = new List<string>()
            {
                "Run",
                "Time & Date",
                $"Depth ({depthDisplayUnitName})",
                "Gx Raw (g)",
                "Gy Raw (g)",
                "Gz Raw (g)",
                "Bx Raw (nT)",
                "By Raw (nT)",
                "Bz Raw (nT)",
            };
            if (mappedColumnNames.Contains("MWD Inc"))
                thirdPartyWell.RawSurveys.Columns.Add("MWD Inc (°)");
            if (mappedColumnNames.Contains("Sag Inc"))
                thirdPartyWell.RawSurveys.Columns.Add("Sag Inc (°)");
            if (mappedColumnNames.Contains("MWD SC"))
                thirdPartyWell.RawSurveys.Columns.Add("MWD SC (°)");
            if (mappedColumnNames.Contains("MWD LC"))
                thirdPartyWell.RawSurveys.Columns.Add("MWD LC (°)");
            if (mappedColumnNames.Contains("Pump Status"))
                thirdPartyWell.RawSurveys.Columns.Add("Pump Status");
            if (mappedColumnNames.Contains("Serial Number"))
                thirdPartyWell.RawSurveys.Columns.Add("Serial Number");
            if (mappedColumnNames.Contains("Azimuth Type"))
                thirdPartyWell.RawSurveys.Columns.Add("Azimuth Type");
            if (mappedColumnNames.Contains("Temperature"))
                thirdPartyWell.RawSurveys.Columns.Add($"Temperature ({temperatureDisplayUnitName})");
        }
    }
}
