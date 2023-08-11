using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    //[JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class PreviewTemplateValidationModel //: NotificationObject  //To do
    {
        public List<RangeCellValueValidation> RangeValidation { get; set; } = new List<RangeCellValueValidation>();

        public List<OptionListCellValueValidation> OptionListValidation { get; set; } = new List<OptionListCellValueValidation>();

        public List<DuplicateCellValueValidation> DuplicateValidation { get; set; } = new List<DuplicateCellValueValidation>();

        public List<TypeCheckCellValueValidation> TypeCheckValidation { get; set; } = new List<TypeCheckCellValueValidation>();

        public List<CellValueChangedValidation> ValueChangedValidation { get; set; } = new List<CellValueChangedValidation>();
    }
}

    
