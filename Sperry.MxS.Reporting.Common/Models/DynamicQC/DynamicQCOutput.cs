using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.DynamicQC
{
    [Serializable]
    public class DynamicQCOutput
    {
        public double BtDipRssDynamicLimit { get; set; }

        public double BtRssDynamicLimit { get; set; }

        public double DipRssDynamicLimit { get; set; }

        public double AzRssDynamicLimit { get; set; }
    }

}
