using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Template;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class ListHelper
    {
        public static List<string[]> ExportDataToExcel(List<object> rows, ColumnOptions columnOptions,
            MxSUnitSystemEnum unitSystem,
            string rigTimeZone, Dictionary<string, MxSFormatType> formatTypes,
            ExportTemplateConfiguration template, bool isFormatPrecision = true, bool isShowError = true)
        {
            var definedColumns = ExportTemplateConfigurationExtension.ConverToList(template.TemplateContent);
            var result = new List<string[]>();
            var columns = UpdateColumnsByDefinition(columnOptions, definedColumns);
            UpdateColumnByUnitSystem(columns, unitSystem);
            BuildColumnResult(columns, result, template.TimeZoneType.ToString(), rigTimeZone);
            rows.ForEach(row => BuildRowData(result, row, columns, formatTypes, unitSystem, isFormatPrecision));
            BuildTimeZoneData(result, template.TimeZoneType.ToString(), rigTimeZone);
            return result;
        }

        private static void BuildTimeZoneData(List<string[]> result, string timeZoneType, string rigTimeZone)
        {
            if (result.Count <= 1)
                return;
            var utcDateTimeCols =
                result.First()
                    .Where(col => col.Contains(TimeConstants.UtcPrefix) && !col.StartsWith(TimeConstants.DateTimeCol));
            ConvertDateTimeByTimeZone(result, timeZoneType, rigTimeZone, utcDateTimeCols, MxSTimeZoneTypes.UTCTimeZone);
            var rigDateTimeCols = result.First().Where(col => col.StartsWith(TimeConstants.DateTimeCol));
            ConvertDateTimeByTimeZone(result, timeZoneType, rigTimeZone, rigDateTimeCols, MxSTimeZoneTypes.RigTimeZone);
        }

        private static void ConvertDateTimeByTimeZone(List<string[]> result, string timeZoneType, string rigTimeZone,
            IEnumerable<string> cols, MxSTimeZoneTypes sourceTimeZone)
        {
            if (cols != null && cols.Count() > 0)
            {
                var indexs = cols.Select<string, int>(col => result.First().ToList().IndexOf(col));
                for (int i = 1; i < result.Count; i++)
                {
                    indexs.ToList().ForEach(index =>
                    {
                        DateTime datetime;
                        if (DateTime.TryParse(result[i][index], out datetime))
                            result[i][index] =
                                TimeHelper.GetDateTimeByTimeZone(datetime, timeZoneType, rigTimeZone, sourceTimeZone)
                                    .ToString(MxSConstants.DefaultDateTimeFormat);
                    });
                }
            }
        }

        private static void BuildRowData(List<string[]> result, object row, List<ColumnItem> columns,
            Dictionary<string, MxSFormatType> formats, MxSUnitSystemEnum unitSystem, bool isFormatPrecision)
        {
            var current = new List<string>();
            columns.ForEach(columnOption =>
                current.Add(FormatNumberHelper.ConvertAndFormatByTypeWhenNumber(
                    FormatNumberHelper.GetValueByPropetyName(row, columnOption.Variable),
                    columnOption, unitSystem, isFormatPrecision)));
            result.Add(current.ToArray());
        }

        private static void UpdateColumnByUnitSystem(List<ColumnItem> columns, MxSUnitSystemEnum unitSystem)
        {
            columns.ForEach(item =>
            {
                var variable = item.Variable;
                if (item.FormatType != null)
                {
                    item.UnitName = FormatHelper.GetPrecision((MxSFormatType)item.FormatType, unitSystem).UnitName;
                }
            });
        }

        private static void BuildColumnResult(List<ColumnItem> columns, List<string[]> result, string timeZoneType, string rigTimeZone)
        {
            var current = new List<string>();
            columns.ForEach(
                columnOption =>
                    current.Add(TimeHelper.GetTimeZoneHeader(BuildColumnData(columnOption), timeZoneType, rigTimeZone)));
            result.Add(current.ToArray());
        }

        internal const char Separator = MxSConstant.Separator;

        private static string BuildColumnData(ColumnItem columnOption)
        {
            return string.Format("{0}{1}",
                columnOption.FriendlyName,
                string.IsNullOrEmpty(columnOption.UnitName)
                    ? string.Empty
                    : string.Format("({0})", columnOption.UnitName));
        }

        private static List<ColumnItem> UpdateColumnsByDefinition(ColumnOptions columns,
            IEnumerable<string> definedColumns)
        {
            if (definedColumns == null || !definedColumns.Any()) return columns;
            return (from item in definedColumns
                    where columns.FirstOrDefault(col => item == col.Variable) != null
                    select columns.First(col => item == col.Variable)).ToList();
        }
    }
}
