using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    public class TraceLog
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}
