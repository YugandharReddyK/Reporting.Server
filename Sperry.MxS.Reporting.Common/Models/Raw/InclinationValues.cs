using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Raw
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class InclinationValues
    {
        public double GxyzInclination { get; set; }
        
        public double GxyInclination { get; set; }
        
        public double GzInclination { get; set; }
        
        public double GTotal { get; set; }
        
        public double Goxy { get; set; }
       
        public double GravityToolFace { get; set; }
    }
}
