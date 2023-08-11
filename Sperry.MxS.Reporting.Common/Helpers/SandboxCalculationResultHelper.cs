using Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base;
using Sperry.MxS.Core.Common.Models.Workbench.Cazandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Models.Workbench.Icarus;

namespace Sperry.MxS.Core.Infrastructure.Helper
{
    internal class SandboxCalculationResultHelper
    {
        internal void SandboxCalculationResultAngle(CazandraCentreData item, double factor)
        {
            item.Inc *= factor;
        }

        internal void SandboxCalculationResultAngle(CazandraDiyData item, double factor)
        {
            item.AzLc *= factor;
            item.AzLcMAv *= factor;
            item.AzLcMavTrue *= factor;
            item.AzLcTrue *= factor;
            item.AzSc *= factor;
            item.AzScTrue *= factor;
            item.Dipc *= factor;
            item.DipcMav *= factor;
            item.dDip *= factor;
            item.dDipMav *= factor;

            item.BGm *= factor;
            item.BGmMav *= factor;
            item.Sxy *= factor;
            item.SxyMav *= factor;
        }

        internal void SandboxCalculationResultAngle(CazandraRawData item, double factor)
        {
            item.AzLc *= factor;
            item.AzLcMAv *= factor;
            item.AzLcMavTrue *= factor;
            item.AzLcTrue *= factor;
            item.AzSc *= factor;
            item.AzScTrue *= factor;
            item.Inclination *= factor;
            item.Dipc *= factor;
            item.DipcMav *= factor;
            item.dDip *= factor;
            item.dDipMav *= factor;

            item.Highside *= factor;
            item.BGmMav *= factor;
            item.Sxy *= factor;
            item.SxyMav *= factor;
            item.HsSpread *= factor;
        }

        internal void SandboxCalculationResultAngle(CazandraSfeData item, double factor)
        {
            item.AzLc *= factor;
            item.AzLcMAv *= factor;
            item.AzLcMavTrue *= factor;
            item.AzLcTrue *= factor;
            item.AzSc *= factor;
            item.AzScTrue *= factor;
            item.Dipc *= factor;
            item.DipcMav *= factor;
            item.dDip *= factor;
            item.dDipMav *= factor;

            item.BGm *= factor;
            item.BGmMav *= factor;
        }

        internal void SandboxCalculationResultAngle(CazandraTfcData item, double factor)
        {
            item.AzLc *= factor;
            item.AzLcMAv *= factor;
            item.AzLcMavTrue *= factor;
            item.AzLcTrue *= factor;
            item.AzSc *= factor;
            item.AzScTrue *= factor;
            item.Dipc *= factor;
            item.DipcMav *= factor;
            item.dDip *= factor;
            item.dDipMav *= factor;

            item.BGm *= factor;
            item.BGmMav *= factor;
            item.Sxy *= factor;
            item.SxyMav *= factor;
        }

        internal void SandboxCalculationResultAngle(CazandraTxyData item, double factor)
        {
            item.AzLc *= factor;
            item.AzLcMAv *= factor;
            item.AzLcMavTrue *= factor;
            item.AzLcTrue *= factor;
            item.AzSc *= factor;
            item.AzScTrue *= factor;
            item.Dipc *= factor;
            item.DipcMav *= factor;
            item.dDip *= factor;
            item.dDipMav *= factor;

            item.BGmMav *= factor;
            item.Sxy *= factor;
            item.SxyMav *= factor;
        }

        internal void SandboxCalculationResultAngle(IcarusCentreData item, double factor)
        {
            item.Inc *= factor;
            item.IncCor *= factor;
            item.MisAng *= factor;
        }

        internal void SandboxCalculationResultAngle(IcarusDiyData item, double factor)
        {
            item.Inc *= factor;

            item.Hsd *= factor;
        }

        internal void SandboxCalculationResultAngle(IcarusRawData item, double factor)
        {
            item.GxyInc *= factor;
            item.GzInc *= factor;
            item.Inc *= factor;

            item.Hsd *= factor;
            item.HsdSpread *= factor;
        }

        internal void SandboxCalculationResultAngle(IcarusSfeData item, double factor)
        {
            item.Inc *= factor;

            item.Hsd *= factor;
        }

        internal void SandboxCalculationResultAngle(IcarusTfcData item, double factor)
        {
            item.Inc *= factor;

            item.Hsd *= factor;
        }

        internal void SandboxCalculationResultAngle(IcarusTxyData item, double factor)
        {
            item.Inc *= factor;

            item.Hsd *= factor;
        }

        internal void SandboxCalculationResultAngle(CazandraRawAveData item, double factor)
        {
            item.SxyAve *= factor;
            SandboxCalculationResultAngle(item as CazandraAveBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraRawStdData item, double factor)
        {
            item.SxyStd *= factor;
            SandboxCalculationResultAngle(item as CazandraStdBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraTfcAveData item, double factor)
        {
            item.SxyAve *= factor;
            SandboxCalculationResultAngle(item as CazandraAveBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraTfcStdData item, double factor)
        {
            item.SxyStd *= factor;
            SandboxCalculationResultAngle(item as CazandraStdBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraTxyAveData item, double factor)
        {
            item.SxyAve *= factor;
            SandboxCalculationResultAngle(item as CazandraAveBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraTxyStdData item, double factor)
        {
            item.SxyStd *= factor;
            SandboxCalculationResultAngle(item as CazandraStdBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraSfeAveData item, double factor)
        {
            SandboxCalculationResultAngle(item as CazandraAveBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraSfeStdData item, double factor)
        {
            SandboxCalculationResultAngle(item as CazandraStdBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraDiyAveData item, double factor)
        {
            item.SxyAve *= factor;
            SandboxCalculationResultAngle(item as CazandraAveBase, factor);
        }

        internal void SandboxCalculationResultAngle(CazandraDiyStdData item, double factor)
        {
            item.SxyStd *= factor;
            SandboxCalculationResultAngle(item as CazandraStdBase, factor);
        }

        private void SandboxCalculationResultAngle(CazandraStdBase item, double factor)
        {
            item.DipcStd *= factor;
            item.DipcMavStd *= factor;
            item.dDipStd *= factor;
            item.dDipMavStd *= factor;
            item.BGmStd *= factor;
            item.BGmMavStd *= factor;
        }

        private void SandboxCalculationResultAngle(CazandraAveBase item, double factor)
        {
            item.DipcAve *= factor;
            item.DipcMavAve *= factor;
            item.dDipAve *= factor;
            item.dDipMavAve *= factor;
            item.BGmAve *= factor;
            item.BGmMavAve *= factor;
        }
    }
}
