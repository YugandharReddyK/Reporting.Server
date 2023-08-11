using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base
{
    public abstract class CazandraDataBase : WorkBenchDataModel
    {
        public double? Bc { get; set; }

        public double? BcMav { get; set; }

        public MxSWorkBenchSpecialDataStatus BcMavStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus BcStatus { get; set; }

        public double? BtDip { get; set; }

        public double? BtDipMav { get; set; }

        public MxSWorkBenchSpecialDataStatus BtDipMavStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus BtDipStatus { get; set; }

        public double? dB { get; set; }

        public double? dBMav { get; set; }

        public MxSWorkBenchSpecialDataStatus dBMavStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dBStatus { get; set; }

        public double? dDip { get; set; }

        public double? dDipMav { get; set; }

        public MxSWorkBenchSpecialDataStatus dDipMavStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dDipStatus { get; set; }

        public double? Dipc { get; set; }

        public double? DipcMav { get; set; }

        public MxSWorkBenchSpecialDataStatus DipcMavStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus DipcStatus { get; set; }
    }
}
