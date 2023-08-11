using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    public class ObservatoryUpdateInformation
    {
        public DateTime? EndDate { get; set; }
        
        public string Name { get; set; }
        
        public Guid ObservatoryId { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public Guid BgsWebsiteId { get; set; }
    }
}
