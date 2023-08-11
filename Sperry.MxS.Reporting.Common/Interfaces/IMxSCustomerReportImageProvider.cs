using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Interfaces
{
    public interface IMxSCustomerReportImageProvider
    {
        IEnumerable<FileInfo> GetAll(Guid storeId);

        void ResetImageCacheForWell(Guid wellId);
    }
}
