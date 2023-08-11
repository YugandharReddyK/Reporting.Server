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
    public class ExcelAllowedOption //: NotificationObject // to do 
    {
        public string HeaderName { get; set; }

        public string OptionType { get; set; }

        public List<string> ListOptions { get; set; } = new List<string>();

    }
}
