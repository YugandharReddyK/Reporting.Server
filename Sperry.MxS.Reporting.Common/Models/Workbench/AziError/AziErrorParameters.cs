using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.AziError
{
    [Serializable]
    public class AziErrorParameters
    {
        public double AziErrordDipe { get; set; }

        public double AziErrordBe { get; set; }

        public double AziErrordBz { get; set; }

        public double AziErrorSxy { get; set; }

        public double AziErrorBGm { get; set; }
    }

}
