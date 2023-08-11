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
    public class ExcelImportTemplateValidationBase //: NotificationObject to do
    {
        public string HeaderName { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public string BackgroundColor { get; set; } = null;

        public string ForegroundColor { get; set; } = null;

    }
}
