﻿using System;
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
    public class CazandraTfcStdData : CazandraStdBase
    {
        public double dBxStd { get; set; }

        public double dByStd { get; set; }

        public double dBzStd { get; set; }

        public double SxyStd { get; set; }
    }
}
