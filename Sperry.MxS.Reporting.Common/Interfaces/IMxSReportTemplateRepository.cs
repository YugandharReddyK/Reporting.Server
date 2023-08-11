using Sperry.MxS.Core.Common.Models.CustomerReport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Interfaces
{
    public interface IMxSReportTemplateRepository
    {
        bool Add(string imageToSave, string id);

        bool Remove(string imageToRemove, string storeId);

        //FileInfo Get(Guid id);

        IEnumerable<FileInfo> GetAll();

        void Clear();

        void Clear(Guid storeId);

        DirectoryInfo LocalStore { get; }

        bool Initialize(string storePath = null);

        #region MyRegion

        FileInfo Get(CustomerReportTemplateData templateData);

        #endregion
    }
}
