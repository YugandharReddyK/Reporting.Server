using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Workbench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CazandraCentreData : WorkBenchDataModel
    {
        public double Bx { get; set; }

        public double By { get; set; }

        public double Bz { get; set; }

        public double Tfc { get; set; }

        public double Inc { get; set; }

        public double Fld { get; set; }

        public double dX { get; set; }

        public double dY { get; set; }

        public double MisAng { get; set; }

        public double MisDir { get; set; }

        public double BxCor { get; set; }

        public double ByCor { get; set; }

        public double BzCor { get; set; }

        public double TfcCor { get; set; }

        public double IncCor { get; set; }

        public double FldCor { get; set; }
    }

}
