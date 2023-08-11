using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    public class Uncert : DataModelBase
    {

        #region Properties


        public double Depth { get; set; }
        public double SigmaN { get; set; }
        public double SigmaE { get; set; }
        public double SigmaV { get; set; }
        public double BiasN { get; set; }
        public double BiasE { get; set; }
        public double BiasV { get; set; }
        public double SigmaH { get; set; }
        public double SigmaL { get; set; }
        public double SigmaA { get; set; }
        public double BiasH { get; set; }
        public double BiasL { get; set; }
        public double BiasA { get; set; }
        public double CorrHL { get; set; }
        public double CorrHA { get; set; }
        public double CorrLA { get; set; }
        public double HMajSA { get; set; }
        public double HMinSA { get; set; }
        public double RotAng { get; set; }
        public double SemiAx1 { get; set; }
        public double SemiAx2 { get; set; }
        public double SemiAx3 { get; set; }
        public double CovNN { get; set; }
        public double CovNE { get; set; }
        public double CovNV { get; set; }
        public double CovEE { get; set; }
        public double CovEV { get; set; }
        public double CovVV { get; set; }



        #endregion
    }
}
