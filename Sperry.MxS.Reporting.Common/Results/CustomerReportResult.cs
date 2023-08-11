using Sperry.MxS.Core.Common.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Results
{
    public class CustomerReportResult<T> : ResultObject<T>
    {
        public bool HasMissingImages => MissingImages != null && MissingImages.Any();

        public Dictionary<string, string> MissingImages { get; private set; }

        public void AddMissingImageError(Dictionary<string, string> images)
        {
            if (images?.Any() ?? false)
            {
                MissingImages = images;
                AddError("ErrorType.MissingImage");
            }
        }
    }
    
}
