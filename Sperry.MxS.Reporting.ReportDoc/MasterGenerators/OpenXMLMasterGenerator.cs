// ***********************************************************************
// Assembly         : Hal.Core.ReportDoc
// Author           : H122101
// Created          : 06-18-2013
//
// Last Modified By : H122101
// Last Modified On : 06-24-2013
// ***********************************************************************
// <copyright file="WordMasterGenerator.cs" company="Halliburton">
//     Copyright \u00a9 Landmark 2014-2015. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using XmlDrawing = DocumentFormat.OpenXml.Drawing;
using XmlPictures = DocumentFormat.OpenXml.Drawing.Pictures;
using Sperry.MxS.Reporting.ReportDoc.Interface;
using Sperry.MxS.Reporting.ReportDoc;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.ReportDoc.Lib;

namespace Hal.Core.ReportDoc.MasterGenerators
{
    /// <summary>
    /// WordMasterGenerator - Master Report Generator for the reporting Engine
    /// </summary>
    /// <remarks>Master Report Generator uses Word to Combine Multiple word documents into a single report</remarks>
    public class OpenXMLMasterGenerator : IMxSMasterGenerator
    {
        // ************************************* STRUCTURES **************************************

        #region "Structures"

        #endregion

        // *************************************** ENUMS *****************************************

        #region "Enums"

        #endregion

        // ************************************ DLL IMPORTS **************************************

        #region "DLL Imports"

        #endregion

        // ********************************** PRIVATE VARIABLES **********************************

        #region "Private Variables"


        /// <summary>
        /// The doc
        /// </summary>
        private WordprocessingDocument doc;

        /// <summary>
        /// The sub report document
        /// </summary>
        private Document subReportDocument;

        /// <summary>
        /// The return report
        /// </summary>
        private HalReport returnReport;

        /// <summary>
        /// The report header
        /// </summary>
        private Uri reportHeader;

        /// <summary>
        /// The report footer
        /// </summary>
        private Uri reportFooter;

        /// <summary>
        /// The report toc
        /// </summary>
        private Uri reportToc;

        /// <summary>
        /// The page header
        /// </summary>
        private Uri pageHeader;

        /// <summary>
        /// The page footer
        /// </summary>
        private Uri pageFooter;

        /// <summary>
        /// The sub reports
        /// </summary>
        private IEnumerable<Uri> subReports;

        /// <summary>
        /// The file format type
        /// </summary>
        private MxSFileFormatType fileFormatType;

        /// <summary>
        /// The save location
        /// </summary>
        private string saveLocation;

        /// <summary>
        /// The save location
        /// </summary>
        private string newFileLocation;

        /// <summary>
        /// Used for the unique id of the AltChunk
        /// </summary>
        private int cnt;

        private Boolean hasLoadedHeader;


        #endregion

        // ********************************* PROTECTED VARIABLES *********************************

        #region "Protected Variables"

        #endregion

        // ************************************** EVENTS *****************************************

        #region "Events"

        #endregion

        // ************************************* DELEGATES ***************************************

        #region "Delegates"

        #endregion

        // ************************************* PROPERTIES **************************************

        #region "Properties"

        /// <summary>
        /// ReportHeader Property - Gets or sets the report header Uri.
        /// </summary>
        /// <remarks>Gets or sets the report header Uri.</remarks>
        /// <returns>Uri</returns>
        public Uri ReportHeader
        {
            get { return reportHeader; }
            set { reportHeader = value; }
        }

        /// <summary>
        /// ReportFooter Property - Gets or sets the report footer Uri.
        /// </summary>
        /// <remarks>Gets or sets the report footer Uri.</remarks>
        /// <returns>Uri</returns>
        public Uri ReportFooter
        {
            get { return reportFooter; }
            set { reportFooter = value; }
        }

        /// <summary>
        /// ReportTOC Property - Gets or sets the report table of Contents Uri.
        /// </summary>
        /// <remarks>Gets or sets the report table of Contents Uri.</remarks>
        /// <returns>Uri</returns>
        public Uri ReportToc
        {
            get { return reportToc; }
            set { reportToc = value; }
        }

        /// <summary>
        /// PageHeader Property - Gets or sets the page header Uri.
        /// </summary>
        /// <remarks>Gets or sets the page header Uri.</remarks>
        /// <returns>Uri</returns>
        public Uri PageHeader
        {
            get { return pageHeader; }
            set { pageHeader = value; }
        }

        /// <summary>
        /// PageFooter Property - Gets or sets the page footer Uri.
        /// </summary>
        /// <remarks>Gets or sets the page footer Uri.</remarks>
        /// <returns>Uri</returns>
        public Uri PageFooter
        {
            get { return pageFooter; }
            set { pageFooter = value; }
        }

        /// <summary>
        /// SubReports Property - Gets or sets the sub report's Uri.
        /// </summary>
        /// <remarks>Gets or sets the sub report's Uri.</remarks>
        /// <returns>IEnumerable&lt;Uri&gt;</returns>
        public IEnumerable<Uri> SubReports
        {
            get { return subReports; }
            set { subReports = value; }
        }

        /// <summary>
        /// Exported File Format Type
        /// </summary>
        /// <remarks>Exported File Format Type</remarks>
        /// <returns>HalReport.FileFormatType</returns>
        public MxSFileFormatType FileFormatType
        {
            get { return fileFormatType; }
            set { fileFormatType = value; }
        }

        /// <summary>
        /// SaveLocation - Location/Path of the Saved Document
        /// </summary>
        /// <remarks>Location/Path of the Saved Document</remarks>
        /// <returns>string</returns>
        public string SaveLocation
        {
            get { return saveLocation; }
            set { saveLocation = value; }
        }

        /// <summary>
        /// ReturnReport - Returns a HalReport with ResultMessage, ErrorMessage, ReturnUri
        /// </summary>
        /// <remarks>Returns a HalReport with ResultMessage, ErrorMessage, ReturnUri</remarks>
        /// <returns>HalReport</returns>
        public HalReport ReturnReport
        {
            get { return returnReport; }
            set { returnReport = value; }
        }

        /// <summary>
        /// IsDocumentProtected - Boolean Operator indicating if the current document is to be Locked from edits
        /// </summary>
        public bool IsDocumentProtected { get; set; }

        /// <summary>
        /// DocumentProtectedPassword - Password used to protect the current document from edits
        /// </summary>
        public string DocumentProtectedPassword { get; set; }
        #endregion

        // ************************************ CONSTRUCTORS *************************************

        #region "Constructors"

        /// <summary>
        /// Report Constructor - Initializes a new instance of the <see cref="OpenXMLMasterGenerator" /> class.
        /// </summary>
        /// <remarks>Initializes a new instance of the <see cref="OpenXMLMasterGenerator" /> class.</remarks>
        public OpenXMLMasterGenerator()
        {
        }

        /// <summary>
        /// Report Constructor - Initializes a new instance of the <see cref="OpenXMLMasterGenerator" /> class.
        /// </summary>
        /// <param name="reportheader">Uri - A Uniform Resource Identifier containing the MasterTemplate information.</param>
        /// <param name="reportfooter">Uri - A Uniform Resource Identifier containing the MasterTemplate information.</param>
        /// <param name="pageheader">Uri - A Uniform Resource Identifier containing the MasterTemplate information.</param>
        /// <param name="pagefooter">Uri - A Uniform Resource Identifier containing the MasterTemplate information.</param>
        /// <param name="subreports">IEnumerable - An object which implements the IEnumerable Interface, containing one or more Sub-Report Uri's</param>
        /// <remarks>Initializes a new instance of the <see cref="OpenXMLMasterGenerator" /> class</remarks>
        public OpenXMLMasterGenerator(Uri reportheader, Uri reportfooter, Uri pageheader, Uri pagefooter, IEnumerable<Uri> subreports)
        {
            ReportHeader = reportheader;
            ReportFooter = reportfooter;
            PageHeader = pageheader;
            PageFooter = pagefooter;
            SubReports = subreports;
        }

        #endregion

        // ************************************ DESTRUCTORS **************************************

        #region "Destructors"

        /// <summary>
        /// ~Report Destructor - Finalizes an instance of the <see cref="OpenXMLMasterGenerator" /> class.
        /// </summary>
        /// <remarks>Finalizes an instance of the <see cref="OpenXMLMasterGenerator" /> class.</remarks>
        ~OpenXMLMasterGenerator()
        {
            Dispose();
        }

        #endregion

        // *********************************** PUBLIC METHODS ************************************

        #region "Public Methods"

        /// <summary>
        /// Dispose Method - Used to Dispose of this object
        /// </summary>
        /// <remarks>Used to Dispose of this object</remarks>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enableLog"></param>
        public void Dispose(bool enableLog)
        {
            try
            {
                if (doc != null)
                {
                    doc.Close();
                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                doc = null;
            }
        }

        /// <summary>
        /// Creates and Saves a Master Report
        /// </summary>
        /// <returns>HalReport</returns>
        /// <remarks>Creates and Saves a Master Report</remarks>
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
        public HalReport CreateMasterReport(bool enableLog)
        {
            AddinBase.ErrorMessages = new List<string>();
            ReturnReport = new HalReport { ResultMessage = MxSReportResultMessage.NoErrors };

            try
            {
                InsertSubReports();

                if (string.IsNullOrEmpty(SaveLocation))
                {
                    if (SubReports.Any())
                        SaveLocation = WinFileSystem.GetDirectoryName(SubReports.ToList()[0].LocalPath) + @"\ExecuteMasterReport.docx";
                    else
                        throw new Exception("Unable to find Save Location");
                }

                SaveReportFormat(enableLog);
                ReturnReport.ReturnUri = new Uri(SaveLocation);
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                ReturnReport.ErrorMessages.Add(exception.InnerException.ToString());
                ReturnReport.ResultMessage = MxSReportResultMessage.SavedFile;
            }

            return ReturnReport;
        }

        private void GetDocument(string path)
        {
            newFileLocation = WinFileSystem.Copy(path, WinFileSystem.GetTempFileName(path));
            doc = WordprocessingDocument.Open(newFileLocation, true);

        }

        /// <summary>
        /// Executes the Master Report from individual Parts
        /// </summary>
        /// <returns>HalReport.</returns>
        /// <remarks>Executes the Master Report from individual Parts</remarks>
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
        ///        ReportToc = new Uri(templatePath + @"\ReportDoc-ReportToc.docx"),
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
        public HalReport ExecuteMasterReport(bool enableLog)
        {
            ReturnReport = new HalReport { ResultMessage = MxSReportResultMessage.NoErrors };
            AddinBase.ErrorMessages = new List<string>();

            try
            {
                foreach (var subReport in SubReports)
                {
                    AddSectionsToReport(subReport);
                }

                if (doc == null)
                    throw new Exception("SubReport not added properly.  Please check the report");

                //<Delete main docs header/footer properties>
                MainDocumentPart mainPart = doc.MainDocumentPart;
                mainPart.DeleteParts(mainPart.HeaderParts);

                IEnumerable<SectionProperties> sectPrs = mainPart.Document.Body.Elements<SectionProperties>();
                foreach (var sectPr in sectPrs)
                {
                    // Delete existing references to headers.
                    sectPr.RemoveAllChildren<HeaderReference>();
                    sectPr.RemoveAllChildren<FooterReference>();
                }
                //</Delete main docs header/footer properties>

                // subReportDocument = new Document();
                AddHeaderSectionToReport(ReportHeader);
                AddFooterSectionToReport(ReportFooter);
                //AddSectionsToReport(ReportHeader);
                /*AddSectionsToReport(ReportToc);*/
                AddHeaderSectionToReport(PageHeader);
                //AddSectionsToReport(PageHeader);



                //AddSectionsToReport(PageFooter);
                AddFooterSectionToReport(PageFooter);
                //AddSectionsToReport(ReportFooter);



                //// subReportDocument = new Document();
                //AddSectionsToReport(ReportHeader);
                //AddSectionsToReport(ReportToc);
                //AddSectionsToReport(PageHeader);

                //foreach (var subReport in SubReports)
                //{
                //    AddSectionsToReport(subReport);
                //}

                //AddSectionsToReport(PageFooter);
                //AddSectionsToReport(ReportFooter);


                //var docSectionProperties = doc.MainDocumentPart.Parts;
                //XDocument xmlXdocument = XDocument.Parse(doc.MainDocumentPart.Document.InnerXml);
                //IEnumerable<XElement> xmlelement = xmlXdocument.Descendants(ns + "sdtContent");
                //XElement tocRefNode = xmlelement.First();
                //GenerateToc(xmlXdocument, tocRefNode);

                //doc.MainDocumentPart.Document.InnerXml = xmlXdocument.ToString();

                if (string.IsNullOrEmpty(SaveLocation))
                {
                    SaveLocation = WinFileSystem.GetDirectoryName(ReportHeader.LocalPath) + @"\ExecuteMasterReport.docx";
                }

                if (IsDocumentProtected)
                {
                    var docProtection = new ReportDocProtection();
                    docProtection.ApplyDocumentProtection(doc, DocumentProtectedPassword);
                }


                SaveReportFormat(enableLog);

                ReturnReport.ReturnUri = new Uri(SaveLocation);
            }
            catch (Exception ex)
            {
                //LoggingSingleton.Instance.LogMessage(ex, enableLog);

                ReturnReport.ErrorMessages.Add("Error Loading Reports into Master -- " + ex.Message);
                ReturnReport.ResultMessage = MxSReportResultMessage.SavedFile;
            }

            return ReturnReport;
        }
        static readonly XNamespace ns = "http://schemas.openxmlformats.org/wordprocessingml/2006/main/";

        #endregion

        // *********************************** PRIVATE METHODS ***********************************

        #region "Private Methods"

        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <param name="reports">The reports.</param>
        private void GetDocument(Uri reports)
        {
            doc = WordprocessingDocument.Open(reports.LocalPath, true);
        }

        /// <summary>
        /// Saves the report format.
        /// </summary>
        private void SaveReportFormat(bool enableLog)
        {
            //string fileName = FileSystem.WinFileSystem.GetFileName(SaveLocation);
            //string extension = FileSystem.WinFileSystem.GetExtension(SaveLocation);

            //if (fileName != null)
            //{
            //    if (extension != null)
            //    {
            //        SaveLocation = SaveLocation.Replace(fileName, fileName.Replace(extension, "." + FileFormatType));
            //    }
            //}

            //WdSaveFormat saveFormat = WdSaveFormat.wdFormatDocumentDefault;

            //switch (FileFormatType)
            //{
            //    case HalReport.FileFormatType.Pdf:
            //        saveFormat = WdSaveFormat.wdFormatPDF;
            //        break;
            //    case HalReport.FileFormatType.Html:
            //        //Using HTMLExport for conversion
            //        saveFormat = WdSaveFormat.wdFormatHTML;
            //        break;
            //    case HalReport.FileFormatType.Rtf:
            //        saveFormat = WdSaveFormat.wdFormatRTF;
            //        break;
            //    case HalReport.FileFormatType.Docx:
            //        saveFormat = WdSaveFormat.wdFormatDocumentDefault;
            //        break;
            //}


            DocumentSaveAs(SaveLocation, enableLog);

            if (String.IsNullOrEmpty(SaveLocation) || !WinFileSystem.Exists(SaveLocation))
            {
                ReturnReport.ResultMessage = MxSReportResultMessage.SavedFile;
                ReturnReport.ErrorMessages.Add("Error Saving file to Location.");
            }
        }

        /// <summary>
        /// Documents the save as.
        /// </summary>
        /// <param name="sFileName">Name of the s file.</param>
        /// <param name="enableLog"></param>
        private void DocumentSaveAs(string sFileName, bool enableLog)
        {
            try
            {
                //ToDo - Find a way to save as HTML, PDF and RTF
                doc.MainDocumentPart.Document.Save();

                //if (bHasDirtyField)
                // Bug 4236 - Warning message is displayed because of adding "<w:updateFields w:val="true"/>" in the xml.
                // Commenting this code - doesn't display warning message and output is verified.
                //SetDirty(doc.MainDocumentPart);

                doc.Close();


                //GetDocument(newFileLocation);
                //doc.MainDocumentPart.Document.Save();
                //doc.Close();

                try
                {
                    File.Delete(SaveLocation);
                    File.Move(newFileLocation, SaveLocation);
                }
                catch (Exception exception)
                {
                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                    if (ReturnReport == null)
                    {
                        ReturnReport = new HalReport();
                    }

                    ReturnReport.ResultMessage = MxSReportResultMessage.SavedFile;
                    ReturnReport.ErrorMessages.Add("Error Saving file to Location.");
                }

            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                if (ReturnReport == null)
                {
                    ReturnReport = new HalReport();
                }

                ReturnReport.ResultMessage = MxSReportResultMessage.SavedFile;
                ReturnReport.ErrorMessages.Add("Error Saving file to Location.");
            }
        }

        /// <summary>
        /// Inserts the sub reports.
        /// </summary>
        private void InsertSubReports()
        {
            object missing = Type.Missing;
            object visible = false;


            subReportDocument = new Document();

            foreach (Uri reports in SubReports)
            {
                AddSectionsToReport(reports);
            }



            // ToDo - Fix issue with Table of contents if exists
            //foreach (TableOfContents toc in doc.TablesOfContents)
            //{
            //    toc.Update();
            //}
        }

        /// <summary>
        /// Adds the sections to report.
        /// </summary>
        /// <param name="uri">The URI.</param>
        private void AddSectionsToReport(Uri uri)
        {
            object missing = Type.Missing;
            object visible = false;
            cnt++;
            if (uri != null)
            {
                if (doc == null)
                {
                    GetDocument(uri.LocalPath);
                }
                else
                {
                    MainDocumentPart mainPart = doc.MainDocumentPart;

                    using (WordprocessingDocument subDocument = WordprocessingDocument.Open(uri.LocalPath, true))
                    {
                        // Want to look thought Simple fields for necessary updates and set document to dirty
                        List<SimpleField> simpleFields = subDocument.MainDocumentPart.Document.Body.Descendants<SimpleField>().ToList();
                        foreach (SimpleField simpleField in simpleFields)
                        {
                            if (simpleField.Dirty == null)
                                continue;
                        }

                        if (pageHeader == null || uri.LocalPath == pageHeader.LocalPath || reportHeader == null || uri.LocalPath == reportHeader.LocalPath)
                        {
                            HeaderFooterValues headerType = HeaderFooterValues.Default;
                            if (reportHeader != null)
                                headerType = uri.LocalPath == reportHeader.LocalPath ? HeaderFooterValues.First : HeaderFooterValues.Default;

                            // Header
                            IEnumerable<HeaderPart> partsHeader = subDocument.MainDocumentPart.GetPartsOfType<HeaderPart>();
                            HeaderPart[] headerParts = partsHeader as HeaderPart[] ?? partsHeader.ToArray();

                            Boolean hasHeader = headerParts.Any(headerPart => !string.IsNullOrEmpty(headerPart.Header.InnerText));

                            bool any = false;

                            any = headerParts.Any();

                            /*foreach (HeaderPart part in headerParts)
                            {
                                any = true;
                                break;
                            }*/
                            if (!hasLoadedHeader && any && hasHeader)
                            {
                                if (pageHeader == null)
                                    pageHeader = uri;

                                CopyHeaderHandler(subDocument, headerType);
                            }

                        }

                        if (pageFooter == null || uri.LocalPath == pageFooter.LocalPath || reportFooter == null || uri.LocalPath == reportFooter.LocalPath)
                        {
                            HeaderFooterValues footerType = HeaderFooterValues.Default;
                            if (reportFooter != null)
                                footerType = uri.LocalPath == reportFooter.LocalPath ? HeaderFooterValues.First : HeaderFooterValues.Default;

                            // Footer
                            IEnumerable<FooterPart> partsFooter = subDocument.MainDocumentPart.GetPartsOfType<FooterPart>();
                            FooterPart[] footerParts = partsFooter as FooterPart[] ?? partsFooter.ToArray();

                            Boolean hasFooter = footerParts.Any(footerPart => !string.IsNullOrEmpty(footerPart.Footer.InnerText));
                            //if (!hasLoadedFooter && footerParts.Any() && hasFooter)
                            if (footerParts.Any() && hasFooter)
                            {
                                if (pageFooter == null)
                                    pageFooter = uri;

                                CopyFooterHandler(subDocument, footerType);
                            }
                        }

                    }

                    AlternativeFormatImportPart chunk = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.WordprocessingML);
                    string altChunkId = mainPart.GetIdOfPart(chunk);

                    using (FileStream fileStream = File.Open(uri.LocalPath, FileMode.Open))
                    {
                        chunk.FeedData(fileStream);
                    }

                    AltChunk altChunk = new AltChunk { Id = altChunkId };
                    mainPart.Document.Body.AppendChild(altChunk);

                }

            }
        }

        private void AddHeaderSectionToReport(Uri uri)
        {
            cnt++;
            if (uri != null)
            {
                if (doc == null)
                {
                    GetDocument(uri.LocalPath);
                }
                else
                {
                    using (WordprocessingDocument subDocument = WordprocessingDocument.Open(uri.LocalPath, true))
                    {
                        if ((PageHeader != null && uri.LocalPath == pageHeader.LocalPath) ||
                            (reportHeader != null && uri.LocalPath == reportHeader.LocalPath))
                        {
                            HeaderFooterValues headerType = reportHeader != null && uri.LocalPath == reportHeader.LocalPath ? HeaderFooterValues.First : HeaderFooterValues.Default;

                            // Header
                            IEnumerable<HeaderPart> partsHeader = subDocument.MainDocumentPart.GetPartsOfType<HeaderPart>();
                            HeaderPart[] headerParts = partsHeader as HeaderPart[] ?? partsHeader.ToArray();

                            //Boolean hasHeader = headerParts.Any(headerPart => !string.IsNullOrEmpty(headerPart.Header.InnerText));

                            //if (!hasLoadedHeader && headerParts.Any() && hasHeader)
                            if (headerParts.Any())
                            {
                                if (pageHeader == null)
                                    pageHeader = uri;

                                //CopyHeaderHandler(subDocument, headerType);
                                AddHeaderFromTo(subDocument, headerType);
                            }

                        }
                    }
                }
            }
        }


        private void AddFooterSectionToReport(Uri uri)
        {
            cnt++;
            if (uri != null)
            {
                if (doc == null)
                {
                    GetDocument(uri.LocalPath);
                }
                else
                {
                    using (WordprocessingDocument subDocument = WordprocessingDocument.Open(uri.LocalPath, true))
                    {
                        if (pageFooter == null || uri.LocalPath == pageFooter.LocalPath || uri.LocalPath == reportFooter.LocalPath)
                        {
                            HeaderFooterValues footerType = reportFooter != null && uri.LocalPath == reportFooter.LocalPath ? HeaderFooterValues.First : HeaderFooterValues.Default;

                            // Footer
                            IEnumerable<FooterPart> partsFooter = subDocument.MainDocumentPart.GetPartsOfType<FooterPart>();
                            FooterPart[] footerParts = partsFooter as FooterPart[] ?? partsFooter.ToArray();

                            //Boolean hasFooter = footerParts.Any(footerPart => !string.IsNullOrEmpty(footerPart.Footer.InnerText));

                            if (footerParts.Any())
                            {
                                if (pageFooter == null)
                                    pageFooter = uri;

                                //CopyFooterHandler(subDocument);
                                AddFooterFromTo(subDocument, footerType);
                            }
                        }

                    }
                }
            }
        }

        private void CopyHeaderHandler(WordprocessingDocument subDoc, HeaderFooterValues headerType)
        {
            //if (PageHeader != null)
            //{
            //    if (PageHeader != subDoc.MainDocumentPart.Uri)
            //        return;
            //}

            hasLoadedHeader = true;
            MainDocumentPart mainPart = doc.MainDocumentPart;
            SectionProperties docSectionProperties = mainPart.Document.Descendants<SectionProperties>().FirstOrDefault();
            SectionProperties subDocSectionProperties = subDoc.MainDocumentPart.Document.Descendants<SectionProperties>().FirstOrDefault();

            if (docSectionProperties != null && subDocSectionProperties != null)
            {

                HeaderReference headerReference = docSectionProperties.Descendants<HeaderReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.First);
                HeaderReference subReference = subDocSectionProperties.Descendants<HeaderReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.First);

                if (headerReference != null && subReference != null)
                {
                    TitlePage tabItems = subDocSectionProperties.Descendants<TitlePage>().FirstOrDefault();
                    if (tabItems != null)
                        docSectionProperties.InsertBefore(new TitlePage(), docSectionProperties.Descendants<PageSize>().FirstOrDefault());

                    // declare the elements of the body 
                    List<OpenXmlElement> tabItems2 = docSectionProperties.ToList();

                    HeaderPart header = doc.MainDocumentPart.HeaderParts.FirstOrDefault();

                    HeaderPart olderHeader = mainPart.GetPartById(headerReference.Id) as HeaderPart;
                    HeaderPart newHeader = NewHeader(subDoc.MainDocumentPart.GetPartById(subReference.Id) as HeaderPart);

                    headerReference.Id = mainPart.GetIdOfPart(newHeader);
                    headerReference.Type = headerType;

                    mainPart.DeletePart(olderHeader);

                    mainPart.AddPart(newHeader);
                    mainPart.Document.Save();
                }

                headerReference = docSectionProperties.Descendants<HeaderReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Default);
                subReference = subDocSectionProperties.Descendants<HeaderReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Default);

                if (headerReference != null && subReference != null)
                {

                    HeaderPart olderHeader = mainPart.GetPartById(headerReference.Id) as HeaderPart;
                    HeaderPart newHeader = NewHeader(subDoc.MainDocumentPart.GetPartById(subReference.Id) as HeaderPart);
                    headerReference.Id = mainPart.GetIdOfPart(newHeader);

                    mainPart.DeletePart(olderHeader);
                    mainPart.AddPart(newHeader);
                    mainPart.Document.Save();
                }

                headerReference = docSectionProperties.Descendants<HeaderReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Even);
                subReference = subDocSectionProperties.Descendants<HeaderReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Even);

                if (headerReference != null && subReference != null)
                {
                    HeaderPart olderHeader = mainPart.GetPartById(headerReference.Id) as HeaderPart;
                    HeaderPart newHeader = NewHeader(subDoc.MainDocumentPart.GetPartById(subReference.Id) as HeaderPart);
                    headerReference.Id = mainPart.GetIdOfPart(newHeader);

                    mainPart.DeletePart(olderHeader);
                    mainPart.AddPart(newHeader);
                    mainPart.Document.Save();
                }

            }
        }

        private void AddHeaderFromTo(WordprocessingDocument subDoc, HeaderFooterValues headerType)
        {
            // Replace header in target document with header of source document.
            WordprocessingDocument wdDoc = doc;

            MainDocumentPart mainPart = wdDoc.MainDocumentPart;

            //// Delete the existing header part.
            //mainPart.DeleteParts(mainPart.HeaderParts);

            // Create a new header part.
            HeaderPart headerPart = mainPart.AddNewPart<HeaderPart>();

            // Get Id of the headerPart.
            string rId = mainPart.GetIdOfPart(headerPart);

            // Feed target headerPart with source headerPart.
            HeaderPart subReportHeader = null;


            SectionProperties docSectionProperties = subDoc.MainDocumentPart.Document.Descendants<SectionProperties>().FirstOrDefault();

            if (headerType == HeaderFooterValues.First && docSectionProperties != null)
            {
                HeaderReference headerReference = docSectionProperties.Descendants<HeaderReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.First);

                if (headerReference != null)
                {
                    subReportHeader = subDoc.MainDocumentPart.GetPartById(headerReference.Id) as HeaderPart;
                }
            }

            if (subReportHeader == null)
            {
                subReportHeader = subDoc.MainDocumentPart.HeaderParts.FirstOrDefault();
            }

            if (subReportHeader != null)
            {
                headerPart.FeedData(subReportHeader.GetStream());
            }

            foreach (var subReportImagePart in subReportHeader.Header.HeaderPart.ImageParts)
            {
                ImagePart imagePart = headerPart.AddImagePart(subReportImagePart.ContentType);
                imagePart.FeedData(subReportImagePart.GetStream());
                string imagePartId = headerPart.GetIdOfPart(imagePart);

                var headerParagraphs = headerPart.Header.Descendants<Paragraph>();

                foreach (var paragraph in headerParagraphs)
                {
                    //Run run = paragraph.Descendants<Run>().First();
                    //Drawing drawing = run.Descendants<Drawing>().First();
                    //XmlDrawing.GraphicData graphicData = drawing.Inline.Graphic.GraphicData;
                    //XmlPictures.Picture pic = graphicData.Descendants<XmlPictures.Picture>().FirstOrDefault();
                    //if (pic != null)
                    //{
                    //    XmlPictures.BlipFill blipFill = pic.Descendants<XmlPictures.BlipFill>().FirstOrDefault();
                    //    if (blipFill != null)
                    //        blipFill.Blip.Embed = imagePartId;
                    //}

                    Run[] runcollection = paragraph.Descendants<Run>().ToArray();
                    foreach (Run run in runcollection)
                    {
                        if (run.Descendants<Drawing>().Any())
                        {
                            Drawing[] drawingcollection = run.Descendants<Drawing>().ToArray();
                            foreach (Drawing drawing in drawingcollection)
                            {
                                XmlDrawing.GraphicData graphicData = drawing.Inline.Graphic.GraphicData;
                                if (graphicData.Descendants<XmlPictures.Picture>().Any())
                                {
                                    XmlPictures.Picture[] picCollection = graphicData.Descendants<XmlPictures.Picture>().ToArray();

                                    foreach (XmlPictures.Picture pic in picCollection)
                                    {
                                        if (pic != null)
                                        {
                                            XmlPictures.BlipFill[] blipFillCollection = pic.Descendants<XmlPictures.BlipFill>().ToArray();
                                            foreach (XmlPictures.BlipFill blipFill in blipFillCollection)
                                            {
                                                if (blipFill != null)
                                                    blipFill.Blip.Embed = imagePartId;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            // Get SectionProperties and Replace HeaderReference with new Id.
            IEnumerable<SectionProperties> sectPrs = mainPart.Document.Body.Elements<SectionProperties>();
            foreach (var sectPr in sectPrs)
            {
                // Delete existing references to headers.
                //sectPr.RemoveAllChildren<HeaderReference>();
                // Create the new header reference node.
                sectPr.PrependChild<HeaderReference>(new HeaderReference()
                {
                    Id = rId,
                    Type = headerType
                });
            }

            headerPart.Header.Save();
        }

        private void AddFooterFromTo(WordprocessingDocument subDoc, HeaderFooterValues footerType)
        {
            // Replace header in target document with header of source document.
            WordprocessingDocument wdDoc = doc;

            MainDocumentPart mainPart = wdDoc.MainDocumentPart;

            //// Delete the existing header part.
            //mainPart.DeleteParts(mainPart.HeaderParts);

            // Create a new header part.
            FooterPart footerPart = mainPart.AddNewPart<FooterPart>();

            // Get Id of the headerPart.
            string rId = mainPart.GetIdOfPart(footerPart);

            // Feed target headerPart with source headerPart.
            FooterPart subReportFooter = null;


            SectionProperties docSectionProperties = subDoc.MainDocumentPart.Document.Descendants<SectionProperties>().FirstOrDefault();

            if (footerType == HeaderFooterValues.First && docSectionProperties != null)
            {
                FooterReference footerReference = docSectionProperties.Descendants<FooterReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.First);

                if (footerReference != null)
                {
                    subReportFooter = subDoc.MainDocumentPart.GetPartById(footerReference.Id) as FooterPart;
                }
            }

            if (subReportFooter == null)
            {
                subReportFooter = subDoc.MainDocumentPart.FooterParts.FirstOrDefault();
            }

            if (subReportFooter != null)
            {
                footerPart.FeedData(subReportFooter.GetStream());
            }


            foreach (var subReportImagePart in subReportFooter.Footer.FooterPart.ImageParts)
            {
                ImagePart imagePart = footerPart.AddImagePart(subReportImagePart.ContentType);
                imagePart.FeedData(subReportImagePart.GetStream());
                string imagePartId = footerPart.GetIdOfPart(imagePart);

                var headerParagraphs = footerPart.Footer.Descendants<Paragraph>();

                foreach (var paragraph in headerParagraphs)
                {
                    //Run run = paragraph.Descendants<Run>().First();
                    //Drawing drawing = run.Descendants<Drawing>().First();
                    //XmlDrawing.GraphicData graphicData = drawing.Inline.Graphic.GraphicData;
                    //XmlPictures.Picture pic = graphicData.Descendants<XmlPictures.Picture>().FirstOrDefault();
                    //if (pic != null)
                    //{
                    //    XmlPictures.BlipFill blipFill = pic.Descendants<XmlPictures.BlipFill>().FirstOrDefault();
                    //    if (blipFill != null)
                    //        blipFill.Blip.Embed = imagePartId;
                    //}
                    Run[] runcollection = paragraph.Descendants<Run>().ToArray();
                    foreach (Run run in runcollection)
                    {
                        if (run.Descendants<Drawing>().Any())
                        {
                            Drawing[] drawingcollection = run.Descendants<Drawing>().ToArray();
                            foreach (Drawing drawing in drawingcollection)
                            {
                                XmlDrawing.GraphicData graphicData = drawing.Inline.Graphic.GraphicData;
                                if (graphicData.Descendants<XmlPictures.Picture>().Any())
                                {
                                    XmlPictures.Picture[] picCollection = graphicData.Descendants<XmlPictures.Picture>().ToArray();

                                    foreach (XmlPictures.Picture pic in picCollection)
                                    {
                                        if (pic != null)
                                        {
                                            XmlPictures.BlipFill[] blipFillCollection = pic.Descendants<XmlPictures.BlipFill>().ToArray();
                                            foreach (XmlPictures.BlipFill blipFill in blipFillCollection)
                                            {
                                                if (blipFill != null)
                                                    blipFill.Blip.Embed = imagePartId;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Get SectionProperties and Replace HeaderReference with new Id.
            IEnumerable<SectionProperties> sectPrs = mainPart.Document.Body.Elements<SectionProperties>();
            foreach (var sectPr in sectPrs)
            {
                // Delete existing references to headers.
                //sectPr.RemoveAllChildren<HeaderReference>();
                // Create the new header reference node.
                sectPr.PrependChild<FooterReference>(new FooterReference()
                {
                    Id = rId,
                    Type = footerType
                });
            }

            footerPart.Footer.Save();
        }

        private HeaderPart NewHeader(HeaderPart headerPart)
        {

            string ImageId = null;
            ImagePart image = headerPart.ImageParts.FirstOrDefault();

            HeaderPart newHeader = doc.MainDocumentPart.AddNewPart<HeaderPart>();

            if (headerPart != null)
            {
                newHeader.FeedData(headerPart.GetStream());
                newHeader.Header.Load(headerPart.Header.HeaderPart);

                if (image != null)
                {
                    ImagePart imagePart = newHeader.AddImagePart(ImagePartType.Png);
                    imagePart.FeedData(image.GetStream());
                    ImageId = newHeader.GetIdOfPart(imagePart);
                }
                Header header = newHeader.Header;
                Paragraph paragraph = header.Descendants<Paragraph>().FirstOrDefault();
                if (ImageId != null && paragraph != null)
                {
                    Run run = paragraph.Descendants<Run>().First();
                    Drawing drawing = run.Descendants<Drawing>().First();
                    XmlDrawing.GraphicData graphicData = drawing.Inline.Graphic.GraphicData;
                    XmlPictures.Picture pic = graphicData.Descendants<XmlPictures.Picture>().FirstOrDefault();
                    if (pic != null)
                    {
                        XmlPictures.BlipFill blipFill = pic.Descendants<XmlPictures.BlipFill>().FirstOrDefault();
                        if (blipFill != null)
                            blipFill.Blip.Embed = ImageId;
                    }
                }
                newHeader.Header.Save();
            }
            return newHeader;
        }

        private void CopyFooterHandler(WordprocessingDocument subDoc, HeaderFooterValues footerType)
        {
            MainDocumentPart mainPart = doc.MainDocumentPart;
            SectionProperties docSectionProperties = mainPart.Document.Descendants<SectionProperties>().FirstOrDefault();
            SectionProperties subDocSectionProperties = subDoc.MainDocumentPart.Document.Descendants<SectionProperties>().FirstOrDefault();

            if (docSectionProperties != null)
            {

                FooterReference FooterReference = docSectionProperties.Descendants<FooterReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Default);
                FooterReference subReference = subDocSectionProperties.Descendants<FooterReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Default);

                if (FooterReference != null && subReference != null)
                {
                    TitlePage tabItems = subDocSectionProperties.Descendants<TitlePage>().FirstOrDefault();
                    if (tabItems != null)
                        docSectionProperties.InsertBefore(new TitlePage(), docSectionProperties.Descendants<PageSize>().FirstOrDefault());


                    FooterPart olderFooter = mainPart.GetPartById(FooterReference.Id) as FooterPart;
                    FooterPart newFooter = NewFooter(subDoc.MainDocumentPart.GetPartById(subReference.Id) as FooterPart);
                    FooterReference.Id = mainPart.GetIdOfPart(newFooter);
                    FooterReference.Type = footerType;

                    mainPart.DeletePart(olderFooter);
                    mainPart.AddPart(newFooter);
                }


                FooterReference = docSectionProperties.Descendants<FooterReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Even);
                subReference = subDocSectionProperties.Descendants<FooterReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.Even);

                if (FooterReference != null && subReference != null)
                {
                    FooterPart olderFooter = mainPart.GetPartById(FooterReference.Id) as FooterPart;
                    FooterPart newFooter = NewFooter(subDoc.MainDocumentPart.GetPartById(subReference.Id) as FooterPart);
                    FooterReference.Id = mainPart.GetIdOfPart(newFooter);

                    mainPart.DeletePart(olderFooter);
                    mainPart.AddPart(newFooter);
                }

                FooterReference = docSectionProperties.Descendants<FooterReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.First);
                subReference = subDocSectionProperties.Descendants<FooterReference>().FirstOrDefault(h => h.Type == HeaderFooterValues.First);

                if (FooterReference != null && subReference != null)
                {
                    FooterPart olderFooter = mainPart.GetPartById(FooterReference.Id) as FooterPart;
                    FooterPart newFooter = NewFooter(subDoc.MainDocumentPart.GetPartById(subReference.Id) as FooterPart);
                    FooterReference.Id = mainPart.GetIdOfPart(newFooter);

                    mainPart.DeletePart(olderFooter);
                    mainPart.AddPart(newFooter);
                }

            }
            mainPart.Document.Save();
        }
        private FooterPart NewFooter(FooterPart footerPart)
        {

            string ImageId = null;
            ImagePart image = footerPart.ImageParts.FirstOrDefault();

            FooterPart newFooter = doc.MainDocumentPart.AddNewPart<FooterPart>();

            if (footerPart != null)
            {
                newFooter.FeedData(footerPart.GetStream());
                newFooter.Footer.Load(footerPart.Footer.FooterPart);

                if (image != null)
                {
                    ImagePart imagePart = newFooter.AddImagePart(ImagePartType.Png);
                    imagePart.FeedData(image.GetStream());
                    ImageId = newFooter.GetIdOfPart(imagePart);
                }
                Footer footer = newFooter.Footer;
                Paragraph paragraph = footer.Descendants<Paragraph>().FirstOrDefault();
                if (ImageId != null && paragraph != null)
                {
                    Run run = paragraph.Descendants<Run>().First();
                    Drawing drawing = run.Descendants<Drawing>().First();
                    XmlDrawing.GraphicData graphicData = drawing.Inline.Graphic.GraphicData;
                    XmlPictures.Picture pic = graphicData.Descendants<XmlPictures.Picture>().FirstOrDefault();
                    if (pic != null)
                    {
                        XmlPictures.BlipFill blipFill = pic.Descendants<XmlPictures.BlipFill>().FirstOrDefault();
                        if (blipFill != null)
                            blipFill.Blip.Embed = ImageId;
                    }
                }
                newFooter.Footer.Save();
            }
            return newFooter;
        }




        private static void SetDirty(MainDocumentPart mainPart)
        {
            DocumentSettingsPart documentSettingsPart = mainPart.GetPartsOfType<DocumentSettingsPart>().FirstOrDefault();

            if (documentSettingsPart != null)
            {
                documentSettingsPart.Settings.AppendChild(new UpdateFieldsOnOpen() { Val = true });
            }
        }
        #endregion

        // ********************************** PROTECTED METHODS **********************************

        #region "Protected Methods"

        #endregion

        // ********************************** INTERNAL METHODS ***********************************

        #region "Internal Methods"

        #endregion

        // ********************************** INTERNAL CLASSES ***********************************

        #region "Internal Classes"

        #endregion

    }
}





