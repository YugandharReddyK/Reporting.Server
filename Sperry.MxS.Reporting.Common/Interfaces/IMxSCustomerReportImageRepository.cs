using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Interfaces
{
    public interface IMxSCustomerReportImageRepository
    {
        //bool Add(string imageToSave, string id);

        //bool Remove(string imageToRemove, string storeId);

        //FileInfo Get(string id);

        IEnumerable<FileInfo> GetAll();

        //void Clear();

        void Clear(Guid storeId);

        //DirectoryInfo LocalStore { get; }

        //string StoreId { get; }

        bool Initialize(string storeId, string storePath = null);
    }
}
