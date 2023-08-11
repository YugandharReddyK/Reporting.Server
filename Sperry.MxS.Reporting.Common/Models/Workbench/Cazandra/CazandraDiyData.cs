using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CazandraDiyData : CazandraDataBase
    {
        public CazandraDiyData()
        {
            DataGridName = "WbCazDiyGrid";
        }

        public double? BGm { get; set; }

        public double? BGmMav { get; set; }

        public double? BzMisAng { get; set; }

        public double? BzMisDir { get; set; }

        public double? dBx { get; set; }

        public double? dBy { get; set; }

        public double? dBz { get; set; }

        public double? dBzMAv { get; set; }

        public double? dBzSc { get; set; }

        public double? Sx { get; set; }

        public double? Sxy { get; set; }

        public double? SxyMav { get; set; }

        public double? Sy { get; set; }
    }
}
