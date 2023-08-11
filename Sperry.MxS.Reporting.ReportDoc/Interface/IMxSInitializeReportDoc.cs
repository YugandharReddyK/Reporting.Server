using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Interface
{
    public interface IMxSInitializeReportDoc : IMxSReportGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatepath"></param>
        /// <param name="savepath"></param>
        /// <param name="enableLog"></param>
        /// <param name="docType"></param>
        void InitializeReportDoc(string templatepath, string savepath, bool enableLog, MxSFileFormatType docType = MxSFileFormatType.Docx);

    }
}
