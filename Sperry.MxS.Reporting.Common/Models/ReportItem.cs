﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ReportItem
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }
    }
}
