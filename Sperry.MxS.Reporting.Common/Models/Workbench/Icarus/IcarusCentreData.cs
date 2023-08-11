using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Icarus
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class IcarusCentreData : WorkBenchDataModel
    {
        public double dX { get; set; }

        public double dY { get; set; }

        public double Fld { get; set; }

        public double Fld2 { get; set; }

        public double FldCor { get; set; }

        public double Gx { get; set; }

        public double GxCor { get; set; }

        public double Gy { get; set; }

        public double GyCor { get; set; }

        public double Gz { get; set; }

        public double GzCor { get; set; }

        public double Inc { get; set; }

        public double IncCor { get; set; }

        public double MisAng { get; set; }

        public double MisDir { get; set; }

        public double Tfc { get; set; }

        public double TfcCor { get; set; }
    }
}
