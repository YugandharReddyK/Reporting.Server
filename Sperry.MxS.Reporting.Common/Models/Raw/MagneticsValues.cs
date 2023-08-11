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
    public class MagneticsValues
    {
        public double BTotalMeasured { get; set; }
       
        public double DipMeasured { get; set; }
        
        public double BvMeasured { get; set; }
        
        public double BhMeasured { get; set; }
    }
}
