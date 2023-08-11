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
    public class PreviewCellModel  // : NotificationObject  //To do 
    {
        public object CellValue { get; set; } = "";

        public string CellBackground { get; set; } = null;

        public string CellToolTip { get; set; } = "";

        public string CellForeground { get; set; } = null;

        public bool HasError { get; set; } = false;

    }
}
