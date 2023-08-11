using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    [XmlInclude(typeof(ExportTemplateConfiguration))]
    [XmlInclude(typeof(ImportTemplateConfiguration))]
    public class TemplateConfigurationBase //: NotificationObject  // To do
    {
        private string DefaultOption = Enum.GetName(typeof(MxSROCOptions), MxSROCOptions.Other);

        public virtual string UnitSystem { get; set; }

        public Guid Id { get; set; }

        public Guid WellId { get; set; }

        public string Description { get; set; }

        public string TemplateName { get; set; }

        public string ImportFileName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; } = false;

        public string ChangesMade { get; set; } = string.Empty;

        private MxSROCOptions? _roc = null;

        public string ROC
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                MxSROCOptions roc;
                if (!Enum.TryParse(value, out roc))
                {
                    _roc = null;
                    return;
                }
                _roc = roc;
            }
            get { return !_roc.HasValue ? DefaultOption : _roc.ToString(); }
        }

        public MxSTemplateType TemplateType { get; set; }

    }
}
