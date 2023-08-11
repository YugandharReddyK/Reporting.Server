using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.DynamicQC
{
    [Serializable]
    public class DynamicQCSurveyInput
    {
        public double? Depth { get; set; }

        public double? SolAzm { get; set; }

        public double? SolInc { get; set; }

        public double NomDip { get; set; }

        public double NomBt { get; set; }

        public double NomGrid { get; set; }

        public double NomDeclination { get; set; }
    }
}
