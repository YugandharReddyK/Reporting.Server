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
    public class DPAPoleModel
    {
        public double Casing { get; set; }
        
        public double Steel { get; set; }
        
        public double CasingDiameter { get; set; }
        
        public double RelativePermiablity { get; set; }
        
        public double CasingMagneticWavenumber { get; set; }
        
        public double MaxCumulative { get; set; }
    }
}
