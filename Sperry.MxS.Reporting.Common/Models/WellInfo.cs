using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    public class WellInfo
    {
        public Guid WellId { get; set; }

        public byte[] Data { get; set; }
    }
}
