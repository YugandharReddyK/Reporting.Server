using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.DTOModel
{
    public class WellUpdateResponse
    {
        public Well EditedWell { get; set; }
        
        public WellResponse wellResponse { get; set; }
        
        public string ClientName { get; set; }
    }
}
