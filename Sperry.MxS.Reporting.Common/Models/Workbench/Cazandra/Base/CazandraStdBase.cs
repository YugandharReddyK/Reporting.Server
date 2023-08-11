using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base
{
    public abstract class CazandraStdBase
    {
        public double BcStd { get; set; }

        public double DipcStd { get; set; }

        public double BcMavStd { get; set; }

        public double DipcMavStd { get; set; }

        public double dBStd { get; set; }

        public double dDipStd { get; set; }

        public double BtDipStd { get; set; }

        public double dBMavStd { get; set; }

        public double dDipMavStd { get; set; }

        public double BtDipMavStd { get; set; }

        public double BGmStd { get; set; }

        public double BGmMavStd { get; set; }

        public double dBzSCStd { get; set; }

        public double dBzMavStd { get; set; }
    }
}
