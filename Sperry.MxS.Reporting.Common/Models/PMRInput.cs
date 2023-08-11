using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class PMRInput
    {
        public double Depth { get; set; }

        public double? Gx { get; set; }

        public double? Gy { get; set; }

        public double? Gz { get; set; }

        public double? Bx { get; set; }

        public double? By { get; set; }

        public double? Bz { get; set; }
    }
}
