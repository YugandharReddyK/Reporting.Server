using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.DPA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class DPAResults
    {
        public double Depth { get; set; }
        
        public double Range { get; set; }
        
        public double Az2Tg { get; set; }
        
        public double Hs2Tg { get; set; }
        
        public double Nshift { get; set; }
        
        public double Eshift { get; set; }
        
        public double SvMd { get; set; }
        
        public double SvInc { get; set; }
        
        public double SvAz { get; set; }

        public DPAParameters Parameters { get; set; }
        
        public DPAPickResult PickData { get; set; }
    }
}
