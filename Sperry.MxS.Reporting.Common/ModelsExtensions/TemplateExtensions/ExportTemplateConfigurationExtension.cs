using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Template
{
    public static class ExportTemplateConfigurationExtension
    {
        public static ExportTemplateConfiguration Create()
        {
            return new ExportTemplateConfiguration { IsActive = true, TimeZoneType = MxSTimeZoneTypes.RigTimeZone };
        }

        public static string[] ConverToList(string variables)
        {
            if (string.IsNullOrEmpty(variables)) return new string[0];
            return variables.Split(MxSTemplateConstant.Separator);
        }

        public static string ConverToString(string[] variables)
        {
            var result = string.Empty;
            if (variables != null && variables.Length > 0)
            {
                result = variables.Aggregate(result, (current, item) => current + (MxSTemplateConstant.Separator + item));
                result = result.Substring(1);
            }
            return result;
        }
    }
}
