using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Models.Results;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSBgsHyperCubeService
    {
        ResultObject<BGSDataPoint> GetHyperCubePoint(string hyperCubeId, BGSDataPoint requestedBgsDataPoint);
        
        ResultObject<BGSDataPoint> GetHyperCubePoint(BgsHyperCubeRequestHeader bgsHyperCubeRequestHeader);
        
        ResultObject<List<ResultObject<BGSDataPoint>>> GetBgsDataForSurveys(string hyperCubeId, IEnumerable<BGSDataPoint> requestedBgsDataPoints);

        ResultObject<List<HypercubeWebsiteInfo>> GetHypercubeWebSites(bool showActiveOnly = false);

        ResultObject AddHypercubeWebSite(HypercubeWebsiteInfo hypercubeWebsite);

        ResultObject DeleteHypercubeWebSite(Guid hyperCubeId);
    }
}
