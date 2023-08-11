using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base
{
    public abstract class CazandraAveBase
    {
        public double BcAve { get; set; }

        public double DipcAve { get; set; }

        public double BcMavAve { get; set; }

        public double DipcMavAve { get; set; }

        public double dBAve { get; set; }

        public double dDipAve { get; set; }

        public double BtDipAve { get; set; }

        public double dBMavAve { get; set; }

        public double dDipMavAve { get; set; }

        public double BtDipMavAve { get; set; }

        public double BGmAve { get; set; }

        public double BGmMavAve { get; set; }

        public double dBzSCAve { get; set; }

        public double dBzMavAve { get; set; }
    }
}
