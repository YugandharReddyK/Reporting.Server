using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
   // [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ColumnItem
    {
        public int Order { get; set; }

        public string FriendlyName { get; set; }

        public bool Visible { get; set; }

        public string Variable { get; set; }

        public string UnitName { get; set; }

        public int? FormatType { get; set; }

        public bool IsFrozen { get; set; }

        public double ColumnWidth { get; set; }
    }
}
