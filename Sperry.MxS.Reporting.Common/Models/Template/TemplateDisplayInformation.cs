using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class TemplateDisplayInformation : DataModelBase
    {
        public TemplateDisplayInformation()
        { }

        public TemplateDisplayInformation(ExportImportTemplateInformation dataToCopy)
        {
            TemplateName = dataToCopy.TemplateName;
            Description = dataToCopy.Description;
            ImportFileName = dataToCopy.ImportFileName;
            IsDefault = dataToCopy.IsDefault;
            TemplateType = dataToCopy.TemplateType;
            ROC = dataToCopy.ROC;
        }

        public TemplateDisplayInformation(TemplateConfigurationBase dataToCopy)
        {
            TemplateName = dataToCopy.TemplateName;
            Description = dataToCopy.Description;
            ImportFileName = dataToCopy.ImportFileName;
            IsDefault = dataToCopy.IsDefault;
            TemplateType = dataToCopy.TemplateType;
            ROC = dataToCopy.ROC;
        }

        [JsonProperty]
        public string TemplateName { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public string ImportFileName { get; set; }

        [JsonProperty]
        public bool IsDefault { get; set; } = false;

        [JsonProperty]
        public MxSTemplateType TemplateType { get; set; }

        [JsonProperty]
        public string ROC { get; set; }
   
    }
}
