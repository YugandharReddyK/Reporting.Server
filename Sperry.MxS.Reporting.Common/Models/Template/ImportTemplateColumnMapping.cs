using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System.Xml.Serialization;
using Sperry.MxS.Core.Common.Models.Units;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
   // [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ImportTemplateColumnMapping //: NotificationObject // to do
    {
        [NonSerialized]
        private UnitSystem _unitSystem;
        private bool _isUsableColumn = false;
        private string _unit = string.Empty;
        private TemplateKeyValuePair<string, string> _variable = new TemplateKeyValuePair<string, string>()
        {
            Key = string.Empty,
            Value = string.Empty
        };
        [JsonIgnore]
        public string CurrentUnitSystem { get; set; }

        [XmlIgnore]
        public List<string> AvaibleUnits { get; set; }

        public string Conversion { get; set; } = string.Empty;

        public TemplateKeyValuePair<string, string> Variable
        {
            get { return _variable; }
            set
            {
                _variable = value;
                SyncRelatedUnits();

                if (!string.IsNullOrEmpty(_variable.Key) && !string.IsNullOrEmpty(Unit))
                {
                    _isUsableColumn = true;
                }
            }
        }

        public string ExcelVariable { get; set; } = string.Empty;

        public string Unit
        {
            get { return _unit; }
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    //Ghanshyam:This is required as multidattrigger is not setting the value back to binding
                    //Need to be fixed
                    if (!string.IsNullOrEmpty(_variable.Key) && !string.IsNullOrEmpty(Unit))
                    {
                        _isUsableColumn = true;
                    }
                }
            }
        }

        public bool IsUsableColumn { get; set; }

        public bool HyperLinkEnabled { get; set; }

        public string ColumnName { get; set; } = string.Empty;

        public MxSScaleAndOffsetType ScaleandOffsetType { get; set; }

        public string Scale { get; set; }

        public string Offset { get; set; }

        public ImportTemplateColumnMapping()
        {
            _unitSystem = new UnitSystem(new MxSMapInformation()); 
            AvaibleUnits = new List<string>();
        }

        private void SyncRelatedUnits()
        {
            var original = _unit;
            if (!string.IsNullOrEmpty(_variable.Value))
            {
                AvaibleUnits = _unitSystem.GetUnitsByVariable(_variable.Value, true).Cast<string>().ToList();
                _unit = AvaibleUnits == null || AvaibleUnits.Contains(original)
                    ? original
                    : _unitSystem.GetDefaultUnitByVariable(_variable.Value, CurrentUnitSystem, true);
            }
        }
    }
}
