using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    public class Uncertainty : DataModelBase
    {
        private double? _sigmaN;
        private double? _sigmaE;
        private double? _sigmaV;
        private double? _sigmaL;
        private double? _sigmaH;
        private double? _sigmaA;
        private double? _corrHL;
        private double? _corrHA;
        private double? _corrLA;
        private double? _hMajSA;
        private double? _hMinSA;
        private double? _rotAng;
        private double? _semiAx1;
        private double? _semiAx2;
        private double? _semiAx3;
        private double? _covNN;
        private double? _covNV;
        private double? _covNE;
        private double? _covEE;
        private double? _covEV;
        private double? _covVV;

        public Uncertainty()
        {

        }

        public Uncertainty(Uncertainty uncertainty) : this()
        {
            _sigmaN = uncertainty.SigmaN;
            _sigmaE = uncertainty.SigmaE;
            _sigmaV = uncertainty.SigmaV;
            _sigmaL = uncertainty.SigmaL;
            _sigmaH = uncertainty.SigmaH;
            _sigmaA = uncertainty.SigmaA;
            BiasN = uncertainty.BiasN;
            BiasE = uncertainty.BiasE;
            BiasV = uncertainty.BiasV;
            BiasH = uncertainty.BiasH;
            BiasL = uncertainty.BiasL;
            BiasA = uncertainty.BiasA;
            _corrHL = uncertainty.CorrHL;
            _corrHA = uncertainty.CorrHA;
            _corrLA = uncertainty.CorrLA;
            _hMajSA = uncertainty.HMajSA;
            _hMinSA = uncertainty.HMinSA;
            _rotAng = uncertainty.RotAng;
            _semiAx1 = uncertainty.SemiAx1;
            _semiAx2 = uncertainty.SemiAx1;
            _semiAx3 = uncertainty.SemiAx1;
            _covNN = uncertainty.CovNN;
            _covNE = uncertainty.CovNE;
            _covNV = uncertainty.CovNV;
            _covEE = uncertainty.CovEE;
            _covEV = uncertainty.CovEV;
            _covVV = uncertainty.CovVV;
            ToolCode = uncertainty.ToolCode;
        }

        [JsonIgnore]
        public Guid CorrectedSurveyId { get; set; }

        [JsonIgnore]
        public CorrectedSurvey CorrectedSurvey { get; set; }

        [JsonProperty]
        public double? SigmaN
        {
            get { return _sigmaN; }
            set
            {
                if (_sigmaN != value && value != null && !value.IsNaN())
                {
                    _sigmaN = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaE
        {
            get { return _sigmaE; }
            set
            {
                if (_sigmaE != value && value != null && !value.IsNaN())
                {
                    _sigmaE = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaV
        {
            get { return _sigmaV; }
            set
            {
                if (_sigmaV != value && value != null && !value.IsNaN())
                {
                    _sigmaV = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaL
        {
            get { return _sigmaL; }
            set
            {
                if (_sigmaL != value && value != null && !value.IsNaN())
                {
                    _sigmaL = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaH
        {
            get { return _sigmaH; }
            set
            {
                if (_sigmaH != value && value != null && !value.IsNaN())
                {
                    _sigmaH = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaA
        {
            get { return _sigmaA; }
            set
            {
                if (_sigmaA != value && value != null && !value.IsNaN())
                {
                    _sigmaA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? BiasN { get; set; }

        [JsonProperty]
        public double? BiasE { get; set; }

        [JsonProperty]
        public double? BiasV { get; set; }

        [JsonProperty]
        public double? BiasH { get; set; }

        [JsonProperty]
        public double? BiasL { get; set; }

        [JsonProperty]
        public double? BiasA { get; set; }

        [JsonProperty]
        public double? CorrHL
        {
            get { return _corrHL; }
            set
            {
                if (_corrHL != value && value != null && !value.IsNaN())
                {
                    _corrHL = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CorrHA
        {
            get { return _corrHA; }
            set
            {
                if (_corrHA != value && value != null && !value.IsNaN())
                {
                    _corrHA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CorrLA
        {
            get { return _corrLA; }
            set
            {
                if (_corrLA != value && value != null && !value.IsNaN())
                {
                    _corrLA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? HMajSA
        {
            get { return _hMajSA; }
            set
            {
                if (_hMajSA != value && value != null && !value.IsNaN())
                {
                    _hMajSA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? HMinSA
        {
            get { return _hMinSA; }
            set
            {
                if (_hMinSA != value && value != null && !value.IsNaN())
                {
                    _hMinSA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? RotAng
        {
            get { return _rotAng; }
            set
            {
                if (_rotAng != value && value != null && !value.IsNaN())
                {
                    _rotAng = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SemiAx1
        {
            get { return _semiAx1; }
            set
            {
                if (_semiAx1 != value && value != null && !value.IsNaN())
                {
                    _semiAx1 = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SemiAx2
        {
            get { return _semiAx2; }
            set
            {
                if (_semiAx2 != value && value != null && !value.IsNaN())
                {
                    _semiAx2 = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SemiAx3
        {
            get { return _semiAx3; }
            set
            {
                if (_semiAx3 != value && value != null && !value.IsNaN())
                {
                    _semiAx3 = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovNN
        {
            get { return _covNN; }
            set
            {
                if (_covNN != value && value != null && !value.IsNaN())
                {
                    _covNN = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovNE
        {
            get { return _covNE; }
            set
            {
                if (_covNE != value && value != null && !value.IsNaN())
                {
                    _covNE = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovNV
        {
            get { return _covNV; }
            set
            {
                if (_covNV != value && value != null && !value.IsNaN())
                {
                    _covNV = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovEE
        {
            get { return _covEE; }
            set
            {
                if (_covEE != value && value != null && !value.IsNaN())
                {
                    _covEE = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovEV
        {
            get { return _covEV; }
            set
            {
                if (_covEV != value && value != null && !value.IsNaN())
                {
                    _covEV = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovVV
        {
            get { return _covVV; }
            set
            {
                if (_covVV != value && value != null && !value.IsNaN())
                {
                    _covVV = value.Value;
                }
            }
        }

        [JsonProperty]
        public string ToolCode { get; set; }

        public void Reset()
        {
            _sigmaN = null;
            _sigmaE = null;
            _sigmaV = null;
            _sigmaL = null;
            _sigmaH = null;
            _sigmaA = null;
            BiasN = null;
            BiasE = null;
            BiasV = null;
            BiasH = null;
            BiasL = null;
            BiasA = null;
            _corrHL = null;
            _corrHA = null;
            _corrLA = null;
            _hMajSA = null;
            _hMinSA = null;
            _rotAng = null;
            _semiAx1 = null;
            _semiAx2 = null;
            _semiAx3 = null;
            _covNN = null;
            _covNE = null;
            _covNV = null;
            _covEE = null;
            _covEV = null;
            _covVV = null;
            ToolCode = string.Empty;
        }
    }
}
