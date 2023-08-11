using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    //[JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ImportTemplateConfiguration : TemplateConfigurationBase
    {
        public ImportTemplateConfiguration()
        { }
        public string InputFolder { get; set; }

        public string SelectedSheet { get; set; } = string.Empty;

        public int StartRow { get; set; } = 0;

        public string ExcelFileNameSearchPattern { get; set; } = "";

        //commented by asha 
        //[NonSerialized] // to do
        [NotMapped]
        public double TotalCorrection { get; set; }

        //[NonSerialized]
        [NotMapped]
        public bool SkipPreview { get; set; } = false;

        public string RunNumber { get; set; }

        public MxSAzimuthTypeEnum AzimuthType { get; set; } = MxSAzimuthTypeEnum.LongCollar;

        public MxSRunOption RunOption { get; set; }

        public string SerialNumber { get; set; }


        private List<ImportTemplateColumnMapping> _mappingConfigurations =
             new List<ImportTemplateColumnMapping>();

        public List<ImportTemplateColumnMapping> MappingConfigurations
        {
            get { return _mappingConfigurations; }
            set
            {
                _mappingConfigurations = value;
                this.SyncConfigurationUnitSystem();
            }
        }
        public List<string> AllowedNullValues { get; set; } = new List<string>();

        public override string UnitSystem
        {
            get { return base.UnitSystem; }
            set
            {
                base.UnitSystem = value;
                this.SyncConfigurationUnitSystem();
            }
        }
        
    }
}
