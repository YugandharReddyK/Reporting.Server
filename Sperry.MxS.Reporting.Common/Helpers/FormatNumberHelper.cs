using System.Globalization;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Utilities;
using Sperry.MxS.Core.Common.Interfaces.Templates;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Workbench;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.Models.CoordSys;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class FormatNumberHelper
    {
        public static string ConvertAndFormatByTypeWhenNumber(string cellText,
            ColumnItem columnConfig, MxSUnitSystemEnum unitSystem, bool isFormatPrecision)
        {
            if (columnConfig.FormatType == null) return cellText;
            var precision = FormatHelper.GetPrecision((MxSFormatType)columnConfig.FormatType, unitSystem).Precision;
            double number;
            if (double.TryParse(cellText, out number))
            {
                if (unitSystem == MxSUnitSystemEnum.Metric)
                {
                    number = UnitConverter.Convert(MxSUnitSystemEnum.English, MxSUnitSystemEnum.Metric, number,
                        (MxSFormatType)columnConfig.FormatType);
                }
                return isFormatPrecision
                    ? FormatHelper.GetRoundValueByPrecision(number, precision).ToString("0.".PadRight(precision + 2, '0'))
                    : number.ToString(CultureInfo.InvariantCulture);
            }
            return cellText;
        }

        public static string ConvertAndFormatByTypeWhenNumber(string cellText, string variable,
            IReadOnlyDictionary<string, MxSFormatType> formats, MxSUnitSystemEnum unitSystem, bool isFormatPrecision)
        {
            if (!formats.ContainsKey(variable))
            {
                return cellText;
            }
            var precision = FormatHelper.GetPrecision(formats[variable], unitSystem).Precision;
            double number;
            if (double.TryParse(cellText, out number))
            {
                if (unitSystem == MxSUnitSystemEnum.Metric)
                {
                    number = UnitConverter.Convert(MxSUnitSystemEnum.English, MxSUnitSystemEnum.Metric, number, formats[variable]);
                }
                return isFormatPrecision
                    ? FormatHelper.GetRoundValueByPrecision(number, precision).ToString("0.".PadRight(precision + 2, '0'))
                    : number.ToString(CultureInfo.InvariantCulture);
            }
            return cellText;
        }

        public static string GetValueByPropetyName(object item, string propertyName, List<Tuple<string, int>> selectedCells = null, IMxSExportImportTemplateServerListManager serverTemplateListManager = null)
        {
            if (propertyName == "MsaState" && item is WorkBenchDataModel)
            {
                WorkBenchDataModel workBenchDataModel = (WorkBenchDataModel)item;
                if (workBenchDataModel != null && workBenchDataModel.ShowStaticMSAState)
                {
                    return workBenchDataModel.MSAStaticState;
                }
            }

            if (propertyName.IndexOf(PropertySeparator) == -1)
            {
                return GetValueBySinglePropetyName(item, propertyName, selectedCells, serverTemplateListManager).ToString();
            }
            foreach (var pn in propertyName.Split(PropertySeparator))
            {
                var value = GetValueBySinglePropetyName(item, pn);
                if (value is CorrectedSurvey)
                {
                    CorrectedSurvey correctedSurvey = (CorrectedSurvey)value;
                    if (propertyName.Equals("CorrectedSurvey.Latitude") && correctedSurvey.Latitude != null)
                    {
                        return CoordService.ToGeoLatitudeString((double)correctedSurvey.Latitude, true);
                    }
                    if (propertyName.Equals("CorrectedSurvey.Longitude") && correctedSurvey.Longitude != null)
                    {
                        return CoordService.ToGeoLongitudeString((double)correctedSurvey.Longitude, true);
                    }
                    if (propertyName.Equals("CorrectedSurvey.SurveyReturnTime"))
                    {
                        return TimeHelper.GetTimeSpanString(correctedSurvey.SurveyReturnTime);
                    }
                    else if (propertyName.Equals("CorrectedSurvey.SurveyProcessTime"))
                    {
                        return TimeHelper.GetTimeSpanString(correctedSurvey.SurveyProcessTime);
                    }
                    else if (propertyName.Equals("CorrectedSurvey.SurveyTotalTime"))
                    {
                        return TimeHelper.GetTimeSpanString(correctedSurvey.SurveyTotalTime);
                    }
                }
                if (value == null || string.IsNullOrEmpty(value.ToString())) return string.Empty;
                item = value;
                if (selectedCells != null && !selectedCells.Any(x => x.Item1.Contains(pn)))
                {
                    return string.Empty;
                }
            }
            return item.ToString();
        }

        private const char PropertySeparator = '.';
        private static object GetValueBySinglePropetyName(object item, string propetyName, List<Tuple<string, int>> selectedCells = null, IMxSExportImportTemplateServerListManager serverTemplateListManager = null)
        {
            var properties = item.GetType().GetProperties();
            var property = properties.FirstOrDefault(prop => prop.Name == propetyName);
            if (property != null)
            {
                if (selectedCells != null)
                {
                    var check = selectedCells.FirstOrDefault(x => x.Item1 == propetyName);
                    if (check != null)
                    {
                        var value = property.GetValue(item);
                        if (property.PropertyType == typeof(string))
                            value = ((string)value);
                        else
                        if (property.PropertyType == typeof(DateTime))
                            value = ((DateTime)value).ToString();
                        if (value != null) return value;
                    }
                }
                else
                {
                    var value = property.GetValue(item);
                    if (serverTemplateListManager != null && item is Well)
                    {
                        if (propetyName.Equals("ExportTemplateId"))
                        {
                            value = serverTemplateListManager.GetTemplateName((Guid)value, MxSTemplateType.Export);
                        }
                        else if (propetyName.Equals("ImportTemplateId"))
                        {
                            value = serverTemplateListManager.GetTemplateName((Guid)value, MxSTemplateType.Import);
                        }
                    }
                    if (property.PropertyType == typeof(string))
                        value = ((string)value);
                    else
                    if (property.PropertyType == typeof(DateTime))
                        value = ((DateTime)value).ToString();
                    if (value != null) return value;
                }
            }
            return string.Empty;
        }
    }
}
