using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    public class HealthState
    {
        public string Name { get; set; }

        public MxSHealthStatusEnum Status { get; set; } = MxSHealthStatusEnum.Pass;

        public List<string> Message { get; set; } = new List<string>();
    }
}
