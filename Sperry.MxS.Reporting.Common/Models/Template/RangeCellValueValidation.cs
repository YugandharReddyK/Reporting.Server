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
    public class RangeCellValueValidation : ExcelImportTemplateValidationBase
    {
        public float StartValue { get; set; } = float.MinValue;

        public float EndValue { get; set; } = float.MinValue;
    }
}
