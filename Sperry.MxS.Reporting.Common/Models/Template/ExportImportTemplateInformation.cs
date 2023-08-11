using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ExportImportTemplateInformation : TemplateDisplayInformation
    {
        public ExportImportTemplateInformation() 
        { }

        public ExportImportTemplateInformation(TemplateConfigurationBase configuration)
        {
            TemplateName = configuration.TemplateName;
            Description = configuration.Description;
            ImportFileName = configuration.ImportFileName;
            IsDefault = configuration.IsDefault;
            TemplateType = configuration.TemplateType;
            ROC = configuration.ROC;
        }

        [JsonProperty]
        public byte[] Data { get; set; }
        
    }
}
