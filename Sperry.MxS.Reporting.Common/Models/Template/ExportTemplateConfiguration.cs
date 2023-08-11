using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    //  [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ExportTemplateConfiguration : TemplateConfigurationBase
    {
        public string OutputFolder { get; set; }

        public MxSTimeZoneTypes TimeZoneType { get; set; }

        public string TemplateContent { get; set; }

        public string ColumnWidths { get; set; }

        public bool ExportNonDefinitiveSurveys { get; set; } = false;

        public static ExportTemplateConfiguration Create()
        {
            return new ExportTemplateConfiguration { IsActive = true, TimeZoneType = MxSTimeZoneTypes.RigTimeZone };
        }

        private const char Separator = 'á';

        public static string[] ConverToList(string variables)
        {
            if (string.IsNullOrEmpty(variables)) return new string[0];
            return variables.Split(Separator);
        }

        public static string ConverToString(string[] variables)
        {
            var result = string.Empty;
            if (variables != null && variables.Length > 0)
            {
                result = variables.Aggregate(result, (current, item) => current + (Separator + item));
                result = result.Substring(1);
            }
            return result;
        }
    }
}