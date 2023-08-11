using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models.DPA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class DPAPickResult
    {
        public double Hss { get; set; }
        
        public double Rss { get; set; }

        public double[] Depths { get; set; }
        
        public double[] Ranges { get; set; }
        
        public double[] Az2Tgs { get; set; }
        
        public double[] Hs2Tgs { get; set; }
        
        public double Objective { get; set; }
        
        public double ObjectiveQ { get; set; }
        
        public double[] TrajectoryAxialBx { get; set; }
        
        public double[] TrajectoryAxialBy { get; set; }
        
        public double[] TrajectoryAxialRx { get; set; }
        
        public double[] TrajectoryAxialRy { get; set; }
        
        public double[] PoleDistributionR { get; set; }
        
        public double[] PoleDistributionG { get; set; }
        
        public double[] PoleDistributionMdq { get; set; }
    }
}
