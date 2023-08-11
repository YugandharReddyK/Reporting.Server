using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Requests
{
    public class WellSearchRequest
    {

        public WellSearchRequest(string wellName, MxSROCOptions? roc, bool? lockStatus, string lockedBy)
        {
            WellName = wellName;
            Roc = roc?.ToString() ?? string.Empty;
            LockStatus = lockStatus?.ToString() ?? string.Empty;
            LockedBy = string.IsNullOrEmpty(lockedBy) ? string.Empty : lockedBy;
        }

        public WellSearchRequest()
        {
            WellName = string.Empty;
            Roc = string.Empty;
            LockStatus = string.Empty;
            LockedBy = string.Empty;
        }

        public string WellName { get; set; }
        
        public string Roc { get; set; }
        
        public string LockStatus { get; set; }
        
        public string LockedBy { get; set; }
        
        public bool UpdateLastSearchResult { get; set; } = true;
        
        public bool? GetArchived { get; set; } = false;
    }
}
