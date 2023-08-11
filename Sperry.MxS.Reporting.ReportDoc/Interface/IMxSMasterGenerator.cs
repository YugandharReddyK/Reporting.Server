using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Interface
{
    public interface IMxSMasterGenerator : IDisposable
    {
        #region "Properties"
      
        Uri ReportHeader { get; set; }
        
        Uri ReportFooter { get; set; }

        Uri PageHeader { get; set; }

        Uri PageFooter { get; set; }
      
        IEnumerable<Uri> SubReports { get; set; }

        MxSFileFormatType FileFormatType { get; set; }
     
        string SaveLocation { get; set; }
       
        HalReport ReturnReport { get; set; }

        bool IsDocumentProtected { get; set; }

        string DocumentProtectedPassword { get; set; }
        #endregion
      
        #region "Public Methods"

        /// <summary>
        /// CreateMasterReport - Initializes generator, adds Subreports to Document, Exports to proper format 
        /// </summary>
        /// <remarks>Creates and Saves a Master Report</remarks>
        /// <returns>HalReport</returns>
        /// <example>
        /// <code>
        ///    HalReport.FileFormatType formatType = HalReport.FileFormatType.Docx;
        ///    string templatePath = @"C:\HAL_Reports\Templates";
        ///    string saveLocation = @"C:\HAL_Reports\MasterTemplate.docx";
        ///    HalReport returnLocation = new HalReport();
        ///
        ///    List<![CDATA[<]]>Uri<![CDATA[>]]> subReports = new List<![CDATA[<]]>Uri<![CDATA[>]]>
        ///                {
        ///                    new Uri(templatePath + @"\ReportDoc-ReportHeader.docx"),
        ///                    new Uri(templatePath + @"\ReportDoc-PageHeader.docx"),
        ///                    new Uri(templatePath + @"\ReportDoc-PageFooter.docx"),
        ///                    new Uri(templatePath + @"\ReportDoc-TOC.docx"),
        ///                    new Uri(templatePath + @"\SubReports\ReportSort.docx"),
        ///                    new Uri(templatePath + @"\SubReports\ReportFilter.docx"),
        ///                    new Uri(templatePath + @"\SubReports\ReportTableJoin.docx"),
        ///                    new Uri(templatePath + @"\ReportDoc-ReportFooter.dotx")
        ///                };
        ///    
        ///    using (IMasterGenerator masterReport = new WordMasterGenerator)
        ///    {
        ///        SaveLocation = saveLocation,
        ///        SubReports = subReports,
        ///        FileFormatType = formatType
        ///        returnLocation = masterReport.CreateMasterReport();
        ///    }
        ///	
        ///    if (returnLocation.ResultMessage != HalReport.ReportResultMessage.NoErrors)
        ///        MessageBox.Show(returnLocation.ErrorMessage, @"Error Running Report", MessageBoxButtons.OK);
        ///    else
        ///        Process.Start(returnLocation.ReturnUri.LocalPath);
        /// </code>
        /// </example>        
        HalReport CreateMasterReport(bool enableLog);

        /// <summary>
        ///  ExecuteMasterReport - Initializes generator, executes master report parts ,adds Subreports to Document, Exports to proper format  
        /// </summary>
        /// <remarks>Executes the Master Report from individual Parts</remarks>
        /// <returns>HalReport</returns>
        /// <seealso cref="HalReport">
        /// HalReport </seealso>
        /// <example>
        /// <code>
        ///    HalReport.FileFormatType formatType = HalReport.FileFormatType.Docx;
        ///    string templatePath = @"C:\HAL_Reports\Templates\";
        ///    string saveLocation = @"C:\HAL_Reports\MasterTemplate.docx";
        ///    HalReport returnLocation = new HalReport();
        ///
        ///     List<![CDATA[<]]>Uri<![CDATA[>]]> subReports = new List<![CDATA[<]]>Uri<![CDATA[>]]>
        ///                {
        ///                    new Uri(templatePath + @"\SubReports\ReportSort.docx"),
        ///                    new Uri(templatePath + @"\SubReports\ReportFilter.docx"),
        ///                    new Uri(templatePath + @"\SubReports\ReportTableJoin.docx")  
        ///                 };
        ///
        ///    using (IMasterGenerator masterReport = new WordMasterGenerator)
        ///    {
        ///        SaveLocation = saveLocation,
        ///        FileFormatType = formatType,
        ///        ReportHeader = new Uri(templatePath + @"\ReportDoc-ReportHeader.docx"),
        ///        PageHeader = new Uri(templatePath + @"\ReportDoc-PageHeader.docx"),
        ///        PageFooter = new Uri(templatePath + @"\ReportDoc-PageFooter.docx"),
        ///        SubReports = subReports,
        ///        ReportFooter = new Uri(templatePath + @"\ReportDoc-ReportFooter.docx"),
        ///        returnLocation = masterReport.CreateMasterReport();
        ///    }
        ///	
        ///    if (returnLocation.ResultMessage != HalReport.ReportResultMessage.NoErrors)
        ///        MessageBox.Show(returnLocation.ErrorMessage, @"Error Running Report", MessageBoxButtons.OK);
        ///    else
        ///        Process.Start(returnLocation.ReturnUri.LocalPath);
        /// </code>
        /// </example>        
        HalReport ExecuteMasterReport(bool enableLog);

        #endregion

    }
}
