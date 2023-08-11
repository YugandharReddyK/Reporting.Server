using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Hal.Core.StringExtensions;
using Hal.Core.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using Condition = Sperry.MxS.Reporting.ReportDoc.Lib.Condition;
using Bold = DocumentFormat.OpenXml.Wordprocessing.Bold;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;
using DataTable = System.Data.DataTable;
using Drawing = DocumentFormat.OpenXml.Wordprocessing.Drawing;
using FontSize = DocumentFormat.OpenXml.Wordprocessing.FontSize;
using Hyperlink = DocumentFormat.OpenXml.Wordprocessing.Hyperlink;
using Justification = DocumentFormat.OpenXml.Wordprocessing.Justification;
using JustificationValues = DocumentFormat.OpenXml.Wordprocessing.JustificationValues;
using OXmlDrawing = DocumentFormat.OpenXml.Drawing;
using OXmlPIC = DocumentFormat.OpenXml.Drawing.Pictures;
using OXmlTable = DocumentFormat.OpenXml.Wordprocessing.Table;
using OXmlWordProc = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using ParagraphProperties = DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties;
using Path = System.IO.Path;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using RunProperties = DocumentFormat.OpenXml.Wordprocessing.RunProperties;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.ReportDoc.Interface;
using Sperry.MxS.Reporting.ReportDoc.Lib;
using Sperry.MxS.Reporting.ReportDoc;
using Sperry.MxS.Reporting.ReportDoc.Extensions;
using System.Net.Mime;
using Sperry.MxS.Reporting.Control.Lib.Chart;
using Sperry.MxS.Reporting.Control.Lib;
using Sperry.MxS.Reporting.Control.Forms;
using Sperry.MxS.Reporting.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hal.Core.ReportDoc.ReportGenerator
{
    /// <summary>
    /// ReportDoc Class - Main Word Document Reporing Class for the Common Ground .NET Framework
    /// </summary>
    /// <remarks>Report Generator uses the creates a new word document using the Template and Datasource provided</remarks>
    public class OpenXMLReportGenerator : BaseReportGenerator, IMxSInitializeReportDoc
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
        private const decimal PIXEL_TO_EMU = 9525;

        private WordprocessingDocument _doc;
        private string _docText;
        private readonly static List<XmlNodeList> _reportDocPropertyList = new List<XmlNodeList>();

        static List<TableJoin> tableJoins = new List<TableJoin>();
        static List<TableGrp> tableGroups = new List<TableGrp>();
        private List<string> ConditionsRemoveList = new List<string>();

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
        /// OpenXMLReportGenerator - Open XML Report Generator Constructor
        /// </summary>
        public OpenXMLReportGenerator()
        {
        }

        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// OpenXMLReportGenerator - Open XML Report Generator Constructor
        /// </summary>
        /// <param name="templatepath">String path for the location of the Report Template</param>
        /// <param name="savepath">Save Location of Exported File,  If left null file will save in users appData\Local\temp folder</param>
        /// <param name="enableLog"></param>
        /// <param name="docType">HalReport.FileFormatType - defaults to 'Docx'</param>
        /// <remarks>Open XML Report Generator Constructor</remarks>
        public OpenXMLReportGenerator(string templatepath, string savepath, bool enableLog, MxSFileFormatType docType = MxSFileFormatType.Docx, ILoggerFactory loggerFactory = null)
        {
            if (loggerFactory != null)
            {
                _logger = loggerFactory.CreateLogger<OpenXMLReportGenerator>();
            }
            _loggerFactory = loggerFactory;
            InitializeReportDoc(templatepath, savepath, enableLog, docType);

        }
        #endregion

        // ************************************ DESTRUCTORS **************************************
        #region "Destructors"
        #endregion

        // *********************************** PUBLIC METHODS ************************************
        #region "Public Methods"

        /// <summary>
        /// Initialize Report Doc
        /// </summary>
        /// <param name="templatepath"></param>
        /// <param name="savepath"></param>
        /// <param name="docType"></param>
        /// <param name="enableLog"></param>
        public void InitializeReportDoc(string templatepath, string savepath, bool enableLog, MxSFileFormatType docType = MxSFileFormatType.Docx)
        {
            try
            {
                //bIsMasterReport = !string.IsNullOrEmpty(savepath);
                AddinBase.ErrorMessages = new List<string>();
                if (!WinFileSystem.Exists(templatepath))
                    throw new FileNotFoundException();

                string fileName = WinFileSystem.GetFileName(templatepath);
                Extension = WinFileSystem.GetExtension(templatepath);

                FileFormatType = docType;

                if (fileName != null && Extension != null)
                {
                    _logger.LogCritical($"Yug 5 - Reached code inside -- {fileName} - {Extension}");
                    fileName = fileName.Replace(" ", "_");

                    SaveLocation = string.IsNullOrEmpty(savepath) ? Path.Combine(SavePath, fileName.Replace(Extension, "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + Extension)) : savepath;

                    TemplatePath = templatepath;
                    SavePath = WinFileSystem.GetDirectoryName(string.IsNullOrEmpty(savepath) ? SaveLocation : savepath);

                    string randomName = Path.GetRandomFileName();
                    NewFileLocation = WinFileSystem.Copy(TemplatePath, Path.Combine(SavePath, randomName.Replace(WinFileSystem.GetExtension(randomName), Extension)));

                    //AddInUtil.RemoveDependency(NewFileLocation);
                    _logger.LogCritical($"Yug 6 - Reached code inside -- {SaveLocation} - {TemplatePath} - {SavePath} - {NewFileLocation}");
                    GetDocument(NewFileLocation);

                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"Yug 4 - {exception.Message}");
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                AddinBase.ErrorMessages.Add(exception.Message);
            }

        }


        /// <summary>
        /// CreateReport Method - Creates the report using the provided XML Data file
        /// </summary>
        /// <param name="xmlDataFile">String - Full Path and File name of XML File containing report data</param>
        /// <param name="chartInfo"></param>
        /// <remarks>Creates the report using the provided XML Data file</remarks>
        /// <returns>HalReport</returns>
        public HalReport CreateReport(string xmlDataFile, IEnumerable<ChartInfo> chartInfo)
        {
            _logger.LogCritical("Yug 13 -- Sandy HalReport CreateReport in OpenXMLReportGenerator");
            //return CreateReport(xmlDataFile, true, chartInfo);
            var data = CreateReport(xmlDataFile, true, chartInfo);
            _logger.LogCritical($"Yug 15 -- Sandy  {data}");
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDataFile"></param>
        /// <param name="enableLog"></param>
        /// <param name="chartInfo"></param>
        /// <returns></returns>
        public HalReport CreateReport(string xmlDataFile, bool enableLog, IEnumerable<ChartInfo> chartInfo)
        {
            ReturnValue = new HalReport { ResultMessage = MxSReportResultMessage.NoErrors };

            try
            {
                _logger.LogCritical($"Yug 16 -- Sandy IsNullOrEmpty(xmlDataFile) ");

                if (String.IsNullOrEmpty(xmlDataFile))
                {
                    ReturnValue.ResultMessage = MxSReportResultMessage.XMLFile;
                    ReturnValue.ErrorMessages.Add("The provided XML file path cannot be null or empty.");
                }

                _logger.LogCritical($"Yug 17 -- Sandy IsValidXML(xmlDataFile, enableLog) {enableLog}");
                if (!AddinBase.IsValidXML(xmlDataFile, enableLog))
                {
                    ReturnValue.ResultMessage = MxSReportResultMessage.XMLFile;
                    ReturnValue.ErrorMessages.Add("The XML file is Invalid.");
                }

                _logger.LogCritical($"Yug 18 -- Sandy ReturnValue.ResultMessage == MxSReportResultMessage.NoErrors) {ReturnValue.ResultMessage} {MxSReportResultMessage.NoErrors}");
                if (ReturnValue.ResultMessage == MxSReportResultMessage.NoErrors)
                {
                    using (var xml = new XMLHelper())
                    {
                        //_logger.LogCritical($"Yug 21 -- Sandy creayed object of XMLHelper");
                        xml.XmlFileName = xmlDataFile;
                        MainDataSet = xml.TransformXMLToDataSet(enableLog);
                       // _logger.LogCritical($"Yug 22 -- Sandy {JsonConvert.SerializeObject(MainDataSet.Namespace)}");
                        if (MainDataSet != null)
                        {
                            _logger.LogCritical($"Yug 23 -- Sandy inside if conduction ");
                            ProcessDocument(enableLog, chartInfo);
                            _logger.LogCritical($"Yug 23.1 -- Sandy inside if conduction ");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"Yug 19 -- Sandy {exception.Message}");
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                ReturnValue.ErrorMessages.Add(exception.ToString());
                _logger.LogCritical($"Yug 20 -- Sandy {exception.InnerException}");
            }
            finally
            {
                if (ReturnValue.ResultMessage == MxSReportResultMessage.NoErrors)
                {
                    ReturnValue.ReturnUri = new Uri(SaveLocation);
                }
            }
            _logger.LogCritical($"Yug 21 -- Sandy {ReturnValue}");
            return ReturnValue;
        }

        /// <summary>
        /// CreateReport Method - Create the report using the provided DataSet
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="chartInfo"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>Create the report using the provided DataSet</remarks>
        /// <returns>HalReport</returns>
        public HalReport CreateReport(DataSet dataSet, IEnumerable<ChartInfo> chartInfo)
        {
            return CreateReport(dataSet, true, chartInfo);
        }

        /// <summary>
        /// CreateReport Method - Create the report using the provided DataSet
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="enableLog"></param>
        /// <param name="chartInfo"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>Create the report using the provided DataSet</remarks>
        /// <returns>HalReport</returns>
        public HalReport CreateReport(DataSet dataSet, bool enableLog, IEnumerable<ChartInfo> chartInfo)
        {
            ReturnValue = new HalReport { ResultMessage = MxSReportResultMessage.NoErrors };
            try
            {
                if (dataSet == null)
                {
                    ReturnValue.ResultMessage = MxSReportResultMessage.DataLoading;
                    ReturnValue.ErrorMessages.Add("The provided DataSet cannot be null.");
                }

                if (dataSet != null)
                {
                    MainDataSet = dataSet;
                    ProcessDocument(enableLog, chartInfo);
                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
            }
            finally
            {
                if (ReturnValue.ResultMessage == MxSReportResultMessage.NoErrors)
                {
                    ReturnValue.ReturnUri = new Uri(SaveLocation);
                }
            }
            return ReturnValue;
        }

        /// <summary>
        /// CreateReport Method - Create the report using the provided DataTable
        /// <para>The Columns names in the DataTable must match the Field Names</para>
        /// </summary>
        /// <param name="dataTable">DataTable - DataTable containing the Field values to use in the report</param>
        /// <param name="enableLog"></param>
        /// <remarks>Create the report using the provided DataTable</remarks>
        /// <returns>HalReport</returns>
        public HalReport CreateReport(DataTable dataTable, bool enableLog)
        {
            ReturnValue = new HalReport { ResultMessage = MxSReportResultMessage.NoErrors };

            try
            {
                if (dataTable == null)
                {
                    ReturnValue.ResultMessage = MxSReportResultMessage.DataLoading;
                    ReturnValue.ErrorMessages.Add("The provided DataSet cannot be null.");
                }
                else
                {
                    MainDataSet.Tables.Add(dataTable);
                    ProcessDocument(enableLog, null);
                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                ReturnValue.ResultMessage = MxSReportResultMessage.DataLoading;
                ReturnValue.ErrorMessages.Add(exception.Message);
            }
            finally
            {
                if (ReturnValue.ResultMessage == MxSReportResultMessage.NoErrors)
                {
                    ReturnValue.ReturnUri = new Uri(SaveLocation);
                }
            }
            return ReturnValue;
        }
        /*
        /// <summary>
        /// CreateReport Method - Create the report using the provided DataView
        /// <para>The Columns names in the DataView must match the Field Names</para>
        /// </summary>
        /// <param name="dataView">DataView - DataView containing the Field values to use in the report</param>
        /// <remarks>Create the report using the provided DataView</remarks>
        /// <returns>HalReport</returns>
        public HalReport CreateReport(DataView dataView)
        {
            ReturnValue = new HalReport { ResultMessage = HalReport.ReportResultMessage.NoErrors };

            try
            {
                if (dataView == null)
                {
                    ReturnValue.ResultMessage = HalReport.ReportResultMessage.DataLoading;
                    ReturnValue.ErrorMessages.Add("The provided DataSet cannot be null.");
                }
                else
                {
                    DataTable dataTable = dataView.Table;
                    MainDataSet.Tables.Add(dataTable);
                    ProcessDocument();
                }
            }
            catch (Exception exception)
            {
                ReturnValue.ResultMessage = HalReport.ReportResultMessage.DataLoading;
                ReturnValue.ErrorMessages.Add(exception.Message);
            }
            finally
            {
                if (ReturnValue.ResultMessage == HalReport.ReportResultMessage.NoErrors)
                {
                    ReturnValue.ReturnUri = new Uri(SaveLocation);
                }
            }
            return ReturnValue;
        }

        /// <summary>
        /// CreateReport Method - Create the report using the provided IDataReader
        /// <para>The Columns names in the IDataReader must match the Field Names</para>
        /// </summary>
        /// <param name="dataReader">IDataReader - IDataReader containing the Field values to use in the report</param>
        /// <remarks>Create the report using the provided IDataReader</remarks>
        /// <returns>HalReport</returns>
        public HalReport CreateReport(IDataReader dataReader)
        {
            ReturnValue = new HalReport { ResultMessage = HalReport.ReportResultMessage.NoErrors };

            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                MainDataSet.Tables.Add(dataTable);
                ProcessDocument();
            }
            catch (Exception exception)
            {
                ReturnValue.ResultMessage = HalReport.ReportResultMessage.DataLoading;
                ReturnValue.ErrorMessages.Add(exception.Message);
            }
            finally
            {
                if (ReturnValue.ResultMessage == HalReport.ReportResultMessage.NoErrors)
                {
                    ReturnValue.ReturnUri = new Uri(SaveLocation);
                }
            }
            return ReturnValue;
        }
        */
        /// <summary>
        /// Dispose Method 
        /// </summary>
        /// <remarks>Dispose Method</remarks>
        public void Dispose()
        {
            _reportDocPropertyList.Clear();
            //Report.Close();

            if (WinFileSystem.Exists(NewFileLocation))
            {
                WinFileSystem.Delete(NewFileLocation);
            }

            //AddInUtil.RemoveDependency(SaveLocation);
        }

        /// <summary>
        /// FindTemplates - returns a list of all Templates that fit the criteria 
        /// </summary>
        /// <param name="templatePath">String path for initial location to search</param>
        /// <param name="searchCriteria">String search Criteria</param>
        /// <param name="searchSubDirectories">Boolean expresion used to search sub directories</param>
        /// <param name="enableLog"></param>
        /// <returns>List<![CDATA[<]]>string<![CDATA[>]]> of all Templates that fit the criteria </returns>
        /// <remarks>List of all Templates that fit the criteria </remarks>
        public static List<HalSearch> FindTemplates(string templatePath, string searchCriteria, bool enableLog, bool searchSubDirectories = false)
        {
            var returnList = new List<HalSearch>();
            var directoryInfo = new DirectoryInfo(templatePath);

            try
            {
                FileInfo[] files = directoryInfo.GetFiles("*.docx", searchSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    var returnSearch = new HalSearch();
                    try
                    {
                        returnSearch.ResultMessage = HalSearch.SearchResultMessage.Success;

                        using (WordprocessingDocument document = WordprocessingDocument.Open(file.FullName, false))
                        {
                            var keywordText = document.PackageProperties.Keywords;

                            if (!string.IsNullOrEmpty(keywordText))
                            {
                                List<string> keywords = keywordText.Split(';').ToList();

                                if (keywords.Any(x => x.Trim().ToUpper() == searchCriteria.ToUpper()))
                                {
                                    returnSearch.ReturnUri = new Uri(file.FullName);
                                    returnList.Add(returnSearch);
                                }
                            }

                            // Code removed here...
                        }

                    }
                    catch (Exception exception)
                    {
                        //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                        returnSearch.ResultMessage = HalSearch.SearchResultMessage.Failed;
                        returnSearch.Message = exception.Message;
                        returnList.Add(returnSearch);
                        //continue
                    }

                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Error, "OpenXMLReportGenerator", "FindTemplates - " + exception.Message + " | " + exception.StackTrace, enableLog);

                var returnSearch = new HalSearch
                {
                    ResultMessage = HalSearch.SearchResultMessage.Failed,
                    Message = exception.Message
                };
                returnList.Add(returnSearch);
            }
            return returnList;
        }

        /// <summary>
        /// Export DataSet to location as Excel document
        /// </summary>
        /// <param name="dataSet">Dataset to export</param>
        /// <param name="exportFileName">Export File Name</param>
        public static HalReport ExportDataSet(DataSet dataSet, string exportFileName)
        {
            var returnValue = new HalReport { ResultMessage = MxSReportResultMessage.NoErrors };


            var fileName = WinFileSystem.GetFileName(exportFileName);
            var extension = WinFileSystem.GetExtension(exportFileName);
            if (fileName == null || extension == null)
                return null;

            string savePath = Path.GetTempPath();
            fileName = fileName.Replace(" ", "_");

            var destination = Path.Combine(savePath, fileName.Replace(extension, "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension));

            var workbook = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook);

            using (workbook)
            {
                var workbookPart = workbook.AddWorkbookPart();

                workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();

                foreach (DataTable table in dataSet.Tables)
                {

                    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                    sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                    var sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                    string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                    uint sheetId = 1;
                    if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Any())
                    {
                        sheetId =
                            sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                    }

                    var sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = FormatTableName(table.TableName) };
                    sheets.Append(sheet);

                    var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    var metadataRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    var hasMetadata = false;

                    var columns = new List<string>();
                    foreach (DataColumn column in table.Columns)
                    {
                        columns.Add(column.ColumnName);

                        var cell = new DocumentFormat.OpenXml.Spreadsheet.Cell
                        {
                            DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString() { Text = new DocumentFormat.OpenXml.Spreadsheet.Text(column.ColumnName) }
                        };
                        headerRow.AppendChild(cell);

                        if (!string.IsNullOrEmpty(column.Namespace))
                            hasMetadata = true;

                        var metadata = new DocumentFormat.OpenXml.Spreadsheet.Cell
                        {
                            DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString() { Text = new DocumentFormat.OpenXml.Spreadsheet.Text(string.Format("({0})", column.Namespace)) }
                        };
                        metadataRow.AppendChild(metadata);

                    }

                    sheetData.AppendChild(headerRow);

                    if (hasMetadata)
                        sheetData.AppendChild(metadataRow);

                    foreach (DataRow dsrow in table.Rows)
                    {
                        var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                        foreach (String col in columns)
                        {
                            var cell = new DocumentFormat.OpenXml.Spreadsheet.Cell
                            {
                                DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.InlineString,
                                InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString() { Text = new DocumentFormat.OpenXml.Spreadsheet.Text(dsrow[col].ToString()) }
                            };
                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }

                }
                returnValue.ReturnUri = new Uri(destination);
            }

            return returnValue;
        }

        private static string FormatTableName(string tableName)
        {
            return tableName.Replace("\'", "").Replace("\"", "").Replace(">", "").Replace("<", "").Replace("&", "");
        }

        /// <summary>
        /// QueryTemplate - returns a list of all attributes used in the Template
        /// </summary>
        /// <param name="templatePath">String path for initial location to search</param>
        /// <param name="enableLog"></param>
        /// <returns>Dictionary<![CDATA[<]]>string<![CDATA[>]], ![CDATA[<]]>string<![CDATA[>]]> of all attributes used in the Template </returns>
        /// <remarks>List of all attributes used in the Template </remarks>
        public static HalQuery QueryTemplate(string templatePath, bool enableLog)
        {
            var queryTableColumns = new List<QueryTableColumn>();
            var queryImages = new List<QueryImage>();
            var conditionalExpressions = new List<QueryConditionalExpressions>();
            var subReports = new List<QuerySubReport>();

            try
            {
                var file = new Uri(templatePath);
                if (string.IsNullOrEmpty(file.AbsolutePath))
                    return null;

                using (var doc = WordprocessingDocument.Open(file.LocalPath, false))
                {
                    GetCustomXMLParts(doc, enableLog);

                    // Since MS Word 2010 the SimpleField element is not longer used. It has been replaced by a combination of
                    // Run elements and a FieldCode element. This method will convert the new format to the old SimpleField-compliant format.
                    foreach (var headerPart in doc.MainDocumentPart.HeaderParts)
                        ConvertFieldCodes(headerPart.Header);

                    foreach (var footerPart in doc.MainDocumentPart.FooterParts)
                        ConvertFieldCodes(footerPart.Footer);

                    ConvertFieldCodes(doc.MainDocumentPart.Document);

                    // Loads Conditional Expressions (Formulas and Conditions) 
                    // Also loads the 
                    conditionalExpressions = ProcessXMLParts(doc, enableLog);

                    // Finds all tables within the document and runs through each one looking for Simple fields and Images 
                    var tbls = doc.MainDocumentPart.Document.Body.Descendants<OXmlTable>().ToList();
                    foreach (var tbl in tbls)
                    {
                        var tableProperties = tbl.Descendants<TableProperties>().FirstOrDefault();

                        if (tableProperties != null && tableProperties.TableCaption != null)
                        {
                            if (tableProperties.TableCaption != null && string.IsNullOrEmpty(tableProperties.TableCaption.Val))
                                continue;

                            string tableTitle = tableProperties.TableCaption.Val.ToString();

                            if (tableTitle.Contains("|"))
                                tableTitle = tableTitle.Left("|");

                            // Gets all Simple Fields from the current table
                            var sFields = tbl.Descendants<SimpleField>().ToList();
                            foreach (var sField in sFields.Where(sField => !string.IsNullOrEmpty(sField.Instruction.Value)))
                            {
                                string columnName = sField.Instruction.Value.Replace(" MERGEFIELD ", string.Empty).Trim().Replace(@"\* MERGEFORMAT", string.Empty).Trim();

                                if (tableTitle.Contains("DynamicTable:"))
                                {
                                    columnName = tableTitle.Left(":");
                                    tableTitle = tableTitle.Right(":");
                                }

                                if (string.IsNullOrEmpty(columnName) || columnName.Contains('~'))
                                    continue;


                                if (conditionalExpressions.Any(x => x.TableName == tableTitle && x.ExpressionName == columnName))
                                    continue;

                                // Traverses down from the Group and Table Joins to get original table
                                var parentTable = FindParent(tableTitle, ref columnName);

                                var queryTableColumn = new QueryTableColumn
                                {
                                    TableName = parentTable,
                                    ColumnName = columnName
                                };

                                if (queryTableColumns.FirstOrDefault(x => x.TableName == queryTableColumn.TableName && x.ColumnName == queryTableColumn.ColumnName) == null)
                                    queryTableColumns.Add(queryTableColumn);
                            }


                            // Gets all Images from the current table
                            var imageList = tbl.Descendants<OXmlWordProc.Inline>().ToList();
                            foreach (var inline in imageList.Where(inline => !string.IsNullOrEmpty(inline.DocProperties.Title.Value)))
                            {
                                string imageName = inline.DocProperties.Title.Value.Replace("StandAlone:", string.Empty).Trim().Replace(@"Units:", string.Empty).Trim();
                                if (string.IsNullOrEmpty(imageName) || imageName.Contains('~'))
                                    continue;

                                // Traverses down from the Group and Table Joins to get original table
                                var parentTable = FindParent(tableTitle, ref imageName);

                                var queryImage = new QueryImage
                                {
                                    TableName = parentTable,
                                    ImageName = imageName,
                                    XSize = decimal.Round(Convert.ToDecimal(inline.Extent.Cx / 914400.0), 4),
                                    YSize = decimal.Round(Convert.ToDecimal(inline.Extent.Cy / 914400.0), 4)
                                };

                                if (queryImages.FirstOrDefault(x => x.TableName == queryImage.TableName && x.ImageName == queryImage.ImageName && x.XSize == queryImage.XSize && x.YSize == queryImage.YSize) == null)
                                    queryImages.Add(queryImage);

                            }
                        }
                    }


                    // List all Simple Fields from Main Document
                    var simpleFields = doc.MainDocumentPart.Document.Descendants<SimpleField>().ToList();
                    // Inline Image Tags
                    var imageList2 = doc.MainDocumentPart.Document.Descendants<OXmlWordProc.Inline>().ToList();

                    //<Header>
                    foreach (var headerPart in doc.MainDocumentPart.HeaderParts)
                    {
                        simpleFields.AddRange(headerPart.Header.Descendants<SimpleField>());
                        imageList2.AddRange(headerPart.Header.Descendants<OXmlWordProc.Inline>());
                    }
                    //</Header>

                    //<Footer>
                    foreach (var footerPart in doc.MainDocumentPart.FooterParts)
                    {
                        simpleFields.AddRange(footerPart.Footer.Descendants<SimpleField>());
                        imageList2.AddRange(footerPart.Footer.Descendants<OXmlWordProc.Inline>());
                    }
                    foreach (var sField in simpleFields.Where(sField => !string.IsNullOrEmpty(sField.Instruction.Value)))
                    {

                        string columnName = sField.Instruction.Value.Replace("StandAlone:", string.Empty).Trim().Replace(@"Units:", string.Empty).Trim();
                        columnName = columnName.Replace("MERGEFIELD", string.Empty).Trim().Replace(@"\* MERGEFORMAT", string.Empty).Trim();
                        if (string.IsNullOrEmpty(columnName))
                            continue;

                        if (columnName.StartsWith("SUBREPORT:"))
                        {
                            subReports.Add(GetSubReportFromName(columnName));
                            continue;
                        }

                        if (!columnName.Contains('~'))
                            continue;

                        string tableTitle = columnName.Left("~");

                        // Traverses down from the Group and Table Joins to get original table
                        var parentTable = FindParent(tableTitle, ref columnName);

                        var queryTableColumn = new QueryTableColumn
                        {
                            TableName = parentTable,
                            ColumnName = columnName.Right("~")
                        };
                        if (queryTableColumns.FirstOrDefault(x => x.TableName == queryTableColumn.TableName && x.ColumnName == queryTableColumn.ColumnName) == null)
                            queryTableColumns.Add(queryTableColumn);
                    }

                    foreach (var inline in imageList2.Where(inline => inline.DocProperties != null && inline.DocProperties.Title != null && !string.IsNullOrEmpty(inline.DocProperties.Title.Value)))
                    {
                        var queryImage = new QueryImage
                        {
                            XSize = decimal.Round(Convert.ToDecimal(inline.Extent.Cx / 914400.0), 4),
                            YSize = decimal.Round(Convert.ToDecimal(inline.Extent.Cy / 914400.0), 4)
                        };

                        string imageName = inline.DocProperties.Title.Value.Replace("SA:", string.Empty).Trim().Replace("StandAlone:", string.Empty).Trim().Replace(@"Units:", string.Empty).Trim();
                        if (string.IsNullOrEmpty(imageName) || imageName.Contains('~'))
                        {
                            string tableTitle = imageName.Left("~");
                            imageName = imageName.Right("~");
                            // Traverses down from the Group and Table Joins to get original table
                            var parentTable = FindParent(tableTitle, ref imageName);

                            queryImage.TableName = parentTable;
                            queryImage.ImageName = imageName.Right("~");
                        }
                        else
                        {
                            queryImage.ImageName = imageName;
                        }

                        if (queryImages.FirstOrDefault(x => x.ImageName == queryImage.ImageName && x.XSize == queryImage.XSize && x.YSize == queryImage.YSize) == null)
                            queryImages.Add(queryImage);
                    }
                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
            }

            return new HalQuery
            {
                ConditionalExpressions = conditionalExpressions,
                ImageDefinition = queryImages,
                TableDefinition = queryTableColumns,
                SubReports = subReports
            };
        }

        #endregion

        // *********************************** PRIVATE METHODS ***********************************
        #region "Private Methods"
        private void GetDocument(string path)
        {
            //_logger.LogCritical($"Yug 66 -- Sandy inside GetDocument start");
            _doc = WordprocessingDocument.Open(path, true);

            //_logger.LogCritical($"Yug 67 -- sandy _doc is Generated");
        }

        private void ReplaceMergeFieldInRow(SimpleField simpleField, string replaceText)
        {
            var someRun = new Run();

            var simpleFieldRuns = simpleField.Descendants<Run>().FirstOrDefault();

            if (simpleFieldRuns != null && simpleFieldRuns.Any())
            {
                if (simpleFieldRuns.RunProperties != null)
                {
                    someRun.RunProperties = (RunProperties)simpleFieldRuns.RunProperties.CloneNode(true);
                }
            }

            //someRun.Append(new Text(replaceText));
            ParseTextForBreaks(someRun, replaceText);

            OpenXmlElement parent = simpleField.Parent;
            parent.ReplaceChild(someRun, simpleField);
        }

        private void ParseTextForBreaks(Run run, string textualData)
        {
            if (string.IsNullOrEmpty(textualData))
            {
                return;
            }

            string[] newLineArray = { Environment.NewLine };
            string[] textArray = textualData.Split(newLineArray, StringSplitOptions.None);

            bool first = true;

            foreach (var line in textArray)
            {
                if (!first)
                {
                    run.Append(new Break());
                }

                first = false;

                var txt = new Text { Text = line };
                run.Append(txt);
            }
        }

        private void ReplaceUnknownFields(OpenXmlUnknownElement unknownElement, string replaceText)
        {
            var someRun = new Run();
            //someRun.Append(new Text(replaceText));
            ParseTextForBreaks(someRun, replaceText);
            var parent = unknownElement.Parent;
            parent.ReplaceChild(someRun, unknownElement);
        }

        private DataSet ProcessDataSet()
        {
            _logger.LogCritical($"Yug 40 -- Sandy inside the ProcessDataSet start");
            // Get a list of all the Group (Table) Names
            var grps = GetMergeGroupNames();

            //_logger.LogCritical($"Yug 43 -- Sandy {JsonConvert.SerializeObject(grps)}");

            foreach (var grp in grps)
            {
                // If the Table does not exist in the DataSet
                if (!MainDataSet.DataTableExists(grp))
                {
                    // Get the Base Table Name by removing the Pipe (|) and everything after it
                    var grpName = grp.Left("|");

                    // Make sure the Base table exists
                    if (MainDataSet.DataTableExists(grpName))
                    {
                        // Get the Base Table
                        var tbl = MainDataSet.GetDataTable(grpName);

                        // Copy the base Table
                        var newTable = tbl.Copy();

                        // Make the Table name match the Group name
                        newTable.TableName = grp;

                        // Add the new table to the DataSet
                        MainDataSet.Tables.Add(newTable);
                    }
                }
            }

            _logger.LogCritical($"Yug 44 -- Sandy {MainDataSet}");

            return MainDataSet;
        }

        private IEnumerable<string> GetMergeGroupNames()
        {
            var tblProps = _doc.MainDocumentPart.Document.Body.Descendants<TableProperties>().ToList();
            _logger.LogCritical($"Yug 41 -- Sandy {tblProps}");
            // return (from tblProp in tblProps where tblProp.TableCaption != null select tblProp.TableCaption.Val).Select(dummy => (string)dummy).ToList();
            var data = (from tblProp in tblProps where tblProp.TableCaption != null select tblProp.TableCaption.Val).Select(dummy => (string)dummy).ToList();

            _logger.LogCritical($"Yug 42 -- Sandy {data}");

            return data;
        }

        //private string StripMailMergeTags(string codeText)
        //{
        //    string sTempName = codeText.Replace("MERGEFIELD ", string.Empty);
        //    sTempName = sTempName.Replace("MERGEFORMAT ", string.Empty);
        //    sTempName = sTempName.Replace(@"\*", string.Empty);
        //    sTempName = sTempName.Right(":").Trim();

        //    return sTempName;
        //}

        private void ConvertPartToSimpleField()
        {
            _logger.LogCritical($"Yug 76.1 -- Sandy ConvertPartToSimpleField() start");

            foreach (var headerPart in _doc.MainDocumentPart.HeaderParts)
            {
                _logger.LogCritical($"Yug 76.2 -- Sandy inside foreach loop{headerPart}");

                ConvertFieldCodes(headerPart.Header);
                _logger.LogCritical($"Yug 76.3 -- Sandy inside foreach loop end");

                foreach (var footerPart in _doc.MainDocumentPart.FooterParts)
                {
                    _logger.LogCritical($"Yug 76.4 -- Sandy inside foreach loop{footerPart}");

                    ConvertFieldCodes(footerPart.Footer);
                    _logger.LogCritical($"Yug 76.5 -- Sandy inside foreach loop end");

                }

                if (ConvertFieldCodes(_doc.MainDocumentPart.Document))
                {
                    _logger.LogCritical($"Yug 76.6 -- Sandy inside the if condition ConvertFieldCodes(_doc.MainDocumentPart.Document) start");

                    SetDirty(_doc.MainDocumentPart);
                    _logger.LogCritical($"Yug 76.12 -- Sandy come back the SetDirty()");
                }

            }
        }

        private void ProcessDocument(bool enableLog, IEnumerable<ChartInfo> chartInfo)
        {
            try
            {
                _logger.LogCritical($"Yug 24 -- Sandy ProcessDocument {chartInfo} {enableLog}");
                ConditionsRemoveList = new List<string>();

                ConvertPartToSimpleField();

                if (String.IsNullOrEmpty(TemplatePath) || !WinFileSystem.Exists(TemplatePath))
                {
                    _logger.LogCritical($"Yug 25 -- Sandy ProcessDocument inside the if condition");
                    ReturnValue.ResultMessage = MxSReportResultMessage.TemplateFile;
                    ReturnValue.ErrorMessages.Add("The provided Template file path cannot be null or empty.");
                }

                GetCustomXMLParts(_doc, enableLog);
                //</Populate reportDocPropertyList>

                ReplaceToc();
                ProcessTables(enableLog);
                ProcessConditions(enableLog);

                //CreateFieldsFromData() called again because ReplaceRemainingFields() could reveal an image field 
                CreateFieldsFromData();
                InsertPictures();
                ReplaceImageObjects(enableLog, chartInfo);
                ReplaceRemainingFields(enableLog);

                ChangeCustomFilePropertiesPart();

                _doc.MainDocumentPart.Document.Save();

                _logger.LogCritical($"Yug 81 -- Sandy _doc is Save");


                if (IsDocumentProtected)
                {
                    _logger.LogCritical($"Yug 82 -- Sandy IsDocumentProtected ");
                    var docProtection = new ReportDocProtection();
                    docProtection.ApplyDocumentProtection(_doc, DocumentProtectedPassword);

                    _logger.LogCritical($"Yug 83 -- Sandy {docProtection}");
                }

                _doc.Close();

                _logger.LogCritical($"Yug 84 -- Sandy _doc is Closed");

                File.Delete(SaveLocation);
                File.Move(base.NewFileLocation, SaveLocation);
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                _logger.LogCritical($"Yug 85 -- sandy {exception.Message}");
                _logger.LogCritical($"Yug 86 -- sandy {exception.InnerException}");


                if (ReturnReport == null)
                {
                    ReturnReport = new HalReport();
                }

                ReturnReport.ResultMessage = MxSReportResultMessage.SavedFile;
                ReturnReport.ErrorMessages.Add(exception.Message);
            }
        }

        private static void GetCustomXMLParts(WordprocessingDocument doc, bool enableLog)
        {
            //<Populate reportDocPropertyList>
            using (var util = new OpenXmlAddinBase(doc))
            {
                List<Tuple<string, string, string>> allProperties = util.ReadAllCustomProperties(enableLog);

                if (allProperties != new List<Tuple<string, string, string>>())
                {
                    Tuple<string, string, string> reportDocProperty = allProperties.Find(x => x.Item1.ToUpper() == "REPORTDOC");

                    if (reportDocProperty != null)
                    {
                        var reportDocCustomProperties = AddinBase.DeserializeClass<ReportDocProperty>(reportDocProperty.Item3);

                        var xml = new XmlDocument();
                        XMLHelper.LoadXmlSafely(xml, reportDocCustomProperties.Value, enableLog);
                        _reportDocPropertyList.Clear();

                        foreach (XmlNode xNode in xml.ChildNodes)
                        {
                            if (xNode.Name != "ReportDoc")
                            {
                                continue;
                            }

                            _reportDocPropertyList.Add(xNode.ChildNodes);
                        }
                    }
                }
            }
        }

        private void ChangeCustomFilePropertiesPart()
        {
            _logger.LogCritical($"Yug 79 -- Sandy ChangeCustomFilePropertiesPart start");
            if (_doc.CustomFilePropertiesPart != null && _doc.CustomFilePropertiesPart.Properties != null)
            {
                var props = from n in _doc.CustomFilePropertiesPart.Properties.Elements<CustomDocumentProperty>()
                                // where n.Name == "Solution ID"
                            select n;
                _logger.LogCritical($"Yug 80 -- Sandy {props}");
                foreach (var prop in props)
                {
                    DocumentFormat.OpenXml.VariantTypes.VTLPWSTR value = prop.GetFirstChild<DocumentFormat.OpenXml.VariantTypes.VTLPWSTR>();
                    value.Text = null;
                }
            }
            _logger.LogCritical($"Yug 191 -- Sandy inside ChangeCustomFilePropertiesPart end");

        }

        private void ProcessConditions(bool enableLog)
        {
            List<Tuple<string, bool>> conditionResults = new List<Tuple<string, bool>>();

            _logger.LogCritical($"Yug 57 -- Sandy {conditionResults}");

            /*<Get Conditions>*/
            List<Condition> conditions = GetConditionsFromTemplate();
            //_logger.LogCritical($"Yug 58 -- Sandy {JsonConvert.SerializeObject(conditions)}");
            foreach (Condition condition in conditions)
            {
                try
                {
                    if (MainDataSet.DataTableExists(condition.Table))
                    {
                        DataTable dataTable = MainDataSet.Tables[condition.Table].Copy();

                        if (dataTable.Rows.Count < 1)
                        {
                            conditionResults.Add(new Tuple<string, bool>(condition.Name, false));
                        }
                        else
                        {
                            TableFormula tableFormula = CreateDataTableFormula(enableLog, condition, dataTable);

                            if (dataTable.Columns.Contains(tableFormula.FormulaName))
                            {
                                if (condition.EvaluateAllRows)
                                {
                                    bool conditionTrueFalse = false;

                                    foreach (DataRow row in dataTable.Rows)
                                    {
                                        bool.TryParse(row[tableFormula.FormulaName].ToString(), out conditionTrueFalse);

                                        if (conditionTrueFalse)
                                        {
                                            break;
                                        }
                                    }

                                    conditionResults.Add(new Tuple<string, bool>(condition.Name, conditionTrueFalse));
                                }
                                else
                                {
                                    bool conditionTrueFalse;
                                    bool.TryParse(dataTable.Rows[0][tableFormula.FormulaName].ToString(), out conditionTrueFalse);

                                    conditionResults.Add(new Tuple<string, bool>(condition.Name, conditionTrueFalse));
                                }
                            }
                            else
                            {
                                conditionResults.Add(new Tuple<string, bool>(condition.Name, false));
                            }

                        }
                    }
                    else
                    {
                        conditionResults.Add(new Tuple<string, bool>(condition.Name, false));
                    }

                }
                catch (Exception exception)
                {
                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                    _logger.LogCritical($"Yug 59 -- Sandy {exception.Message}");
                    _logger.LogCritical($"Yug 60 -- Sandy {exception.InnerException}");
                    AddinBase.ErrorMessages.Add(exception.Message);
                }
            }
            /*</Get Conditions>*/

            List<SimpleField> simpleFields = _doc.MainDocumentPart.Document.Body.Descendants<SimpleField>().Where(x => x.InnerText.Contains("CONDITION")).ToList();
            List<Run> runs = _doc.MainDocumentPart.Document.Body.Descendants<Run>().Where(x => x.InnerText.Contains("«BEGIN_CONDITION:") || x.InnerText.Contains("«END_CONDITION:")).ToList();

            /*<Replaces all Condition == false tags with a <ReportDocDeleteMe> to be replaced by RegEx later> */
            foreach (var simpleField in simpleFields)
            {
                string fieldText = simpleField.InnerText;

                if (string.IsNullOrEmpty(fieldText))
                {
                    fieldText = simpleField.Instruction;
                }

                var conditionResult = conditionResults.Find(x => fieldText.Contains("CONDITION:" + x.Item1 + "»") ||
                                                                 simpleField.Instruction == String.Format(" MERGEFIELD BEGIN_CONDITION:{0} \\* MERGEFORMAT ", x.Item1) ||
                                                                 simpleField.Instruction == String.Format(" MERGEFIELD END_CONDITION:{0} \\* MERGEFORMAT ", x.Item1));

                RemoveConditionField(simpleField, fieldText, conditionResult);
            }
            /*</Replaces all Condition == false tags with a <ReportDocDeleteMe> to be replaced by RegEx later> */

            //simpleFields = _doc.MainDocumentPart.Document.Body.Descendants<SimpleField>().Where(x => x.InnerText.Contains("CONDITION")).ToList();
            //for (int i = 0; i < simpleFields.Count; i++)
            //{
            //    simpleFields[i].Remove();
            //}

            /*<Deletes all Conditional tags that are left> */
            for (int runIndex = runs.Count - 1; runIndex >= 0; --runIndex)
            {
                string runText = runs[runIndex].InnerText;

                if (runText.StartsWith(" MERGEFIELD "))
                {
                    runs[runIndex].Remove();
                    continue;
                }

                runs[runIndex].RemoveAllChildren<Text>();
            }
            /*</Deletes all Conditional tags that are left> */

            /*<Find and delete all Runs and Paragraphs between the 'ReportDocDeletMe' tags>*/

            if (ConditionsRemoveList.Count > 0)
            {
                SetDocText();

                var xml = new XmlDocument();
                xml.LoadXml(_docText);
                try
                {
                    var mainChildNodes = xml.ChildNodes;

                    /*<Get Document Node>*/
                    XmlNode documentNode = null;
                    for (var index = 0; index < mainChildNodes.Count; ++index)
                    {
                        if (mainChildNodes[index].LocalName == "document")
                        {
                            documentNode = mainChildNodes[index];
                            break;
                        }
                    }

                    if (documentNode == null)
                    {
                        return;
                    }
                    /*</Get Document Node>*/

                    /*<Get Body Node>*/
                    XmlNode bodyNode = null;
                    for (var index = 0; index < documentNode.ChildNodes.Count; ++index)
                    {
                        if (documentNode.ChildNodes[index].LocalName == "body")
                        {
                            bodyNode = documentNode.ChildNodes[index];
                            break;
                        }
                    }

                    if (bodyNode == null)
                    {
                        return;
                    }
                    /*</Get Body Node>*/

                    // var bodyNode = documentNode.ChildNodes[0];
                    var bodyChildNodes = bodyNode.ChildNodes;

                    foreach (string conditionName in ConditionsRemoveList)
                    {
                        bool isDeleting = false;
                        for (int index = bodyChildNodes.Count - 1; index >= 0; --index)
                        {
                            if (bodyChildNodes[index].InnerText == string.Format("<ReportDocDeleteMe{0}>", conditionName))
                            {
                                bodyNode.RemoveChild(bodyChildNodes[index]);
                                break;
                            }
                            else if (bodyChildNodes[index].InnerText.Contains(string.Format("<ReportDocDeleteMe{0}>", conditionName)))
                            {
                                var childNodes = bodyChildNodes[index].ChildNodes;

                                bool isDeletingChild = false;
                                for (int childIndex = 0; childIndex < childNodes.Count; ++childIndex)
                                {
                                    if (childNodes[childIndex].InnerText == string.Format("<ReportDocDeleteMe{0}>", conditionName))
                                    {
                                        isDeletingChild = true;
                                    }

                                    if (isDeletingChild)
                                    {
                                        bodyChildNodes[index].RemoveChild(childNodes[childIndex]);
                                        --childIndex;
                                    }
                                }

                                break;
                            }

                            if (isDeleting)
                            {
                                bodyNode.RemoveChild(bodyChildNodes[index]);
                                continue;
                            }

                            if (bodyChildNodes[index].InnerText == string.Format("</ReportDocDeleteMe{0}>", conditionName))
                            {
                                bodyNode.RemoveChild(bodyChildNodes[index]);
                                isDeleting = true;
                            }
                            else if (bodyChildNodes[index].InnerText.Contains(string.Format("</ReportDocDeleteMe{0}>", conditionName)))
                            {
                                var childNodes = bodyChildNodes[index].ChildNodes;

                                bool isDeletingChild = false;
                                for (int childIndex = childNodes.Count - 1; childIndex >= 0; --childIndex)
                                {
                                    if (childNodes[childIndex].InnerText == string.Format("</ReportDocDeleteMe{0}>", conditionName))
                                    {
                                        isDeletingChild = true;
                                    }

                                    if (isDeletingChild && childNodes[childIndex].Name != "w:pPr")
                                    {
                                        bodyChildNodes[index].RemoveChild(childNodes[childIndex]);
                                    }
                                }

                                isDeleting = true;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                }

                _docText = xml.InnerXml;

                WriteDocTextToFile();

                List<Paragraph> reportDeleteMeParagraphs = _doc.MainDocumentPart.Document.Body.Descendants<Paragraph>().Where(x => x.InnerText.Contains("ReportDocDeleteMe")).ToList();
                foreach (var paragraph in reportDeleteMeParagraphs)
                {
                    paragraph.Remove();
                }
            }

            /*<Find and delete all Runs and Paragraphs between the 'ReportDocDeletMe' tags>*/
        }

        private void RemoveConditionField(SimpleField simpleField, string fieldText, Tuple<string, bool> conditionResult)
        {
            if (conditionResult == null || conditionResult.Item2)
            {
                Paragraph x = simpleField.Ancestors<Paragraph>().FirstOrDefault();
                if (x.InnerText == simpleField.InnerText)
                {
                    x.Remove();
                }
                else
                {
                    simpleField.Remove();
                }
                //ReplaceMergeFieldInRow(simpleField, string.Empty);
                //continue;
            }
            if (conditionResult != null && !conditionResult.Item2)
            {
                if (fieldText.Contains("BEGIN_CONDITION:"))
                {
                    ConditionsRemoveList.Add(conditionResult.Item1);
                    ReplaceMergeFieldInRow(simpleField, string.Format("<ReportDocDeleteMe{0}>", conditionResult.Item1));
                }
                else if (fieldText.Contains("END_CONDITION:"))
                {
                    ReplaceMergeFieldInRow(simpleField, string.Format("</ReportDocDeleteMe{0}>", conditionResult.Item1));
                }

            }
        }

        private static TableFormula CreateDataTableFormula(bool enableLog, Condition condition, DataTable dataTable)
        {
            var tableFormula = new TableFormula
            {
                FormulaName = "CONDITION_" + condition.Name,
                Formula = string.Format("iif({0},true,false)", condition.ConditionDescription),
                FormulaType = "Bool",
                Table = condition.Table
            };

            OpenXmlAddinBase.CreateDataTableFormula(dataTable, tableFormula, enableLog);
            return tableFormula;
        }

        private static List<Condition> GetConditionsFromTemplate()
        {
            List<Condition> conditions = new List<Condition>();
            foreach (var reportDocProperty in _reportDocPropertyList)
            {
                foreach (XmlNode xNodeReportDoc in reportDocProperty)
                {
                    if (xNodeReportDoc.Name.ToUpper().Contains("CONDITION"))
                    {
                        conditions.AddRange(OpenXmlAddinBase.DeserializeClass<List<Condition>>(xNodeReportDoc.InnerText));
                    }
                }
            }

            return conditions;
        }

        private void SetDocText()
        {
            _doc.MainDocumentPart.Document.Save();
            using (StreamReader sr = new StreamReader(_doc.MainDocumentPart.GetStream()))
            {
                _docText = sr.ReadToEnd();
            }
        }

        private void WriteDocTextToFile()
        {
            using (StreamWriter sw = new StreamWriter(_doc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                _logger.LogCritical($"Yug 64 -- Sandy Creadted Object of StreamWriter");
                sw.Write(_docText);
            }

            _doc.Package.Close();

            _logger.LogCritical($"Yug 65 -- sandy _doc is Closed");
            GetDocument(NewFileLocation);
        }

        private void ProcessTables(bool enableLog)
        {
            _logger.LogCritical($"Yug 30 -- Sandy ProcessTables method start");
            // Do we have a valid DataSet
            if (MainDataSet != null)
            {
                //_logger.LogCritical($"Yug 31 -- Sandy inside the if copndition");
                Dictionary<string, List<ColumnSort>> columnSorts = new Dictionary<string, List<ColumnSort>>();
                Dictionary<string, List<ColumnFilter>> columnFilters = new Dictionary<string, List<ColumnFilter>>();
                try
                {


                    using (OpenXmlAddinBase util = new OpenXmlAddinBase(_doc))
                    {
                        _logger.LogCritical($"Yug 32 -- Sandy created object OpenXmlAddinBase");
                        //<Find or create the meta_data Table>
                        MetaData = MainDataSet.GetDataTable("meta_data");
                        //_logger.LogCritical($"Yug 33 -- Sandy {JsonConvert.SerializeObject(MetaData)}");
                        var metadataColumns = new List<string>
                    {
                        "table",
                        "table_label",
                        "field",
                        "label",
                        "unit_label",
                        "precision",
                        "decimal_places",
                        "number_format",
                        "is_image",
                        "data_type",
                        "format",
                        "dynamic_hide_column",
                        "dynamic_column_width"
                    };
                        //_logger.LogCritical($"Yug 34 -- Sandy {JsonConvert.SerializeObject(metadataColumns)}");
                        if (MetaData == null)
                        {
                           // _logger.LogCritical($"Yug 35 -- Sandy MetaData Is Null inside if condition");
                            MetaData = new DataTable("meta_data");
                            MetaData.Namespace = "meta_data";

                            foreach (var metadataColumn in metadataColumns)
                            {
                                MetaData.Columns.Add(metadataColumn);
                            }

                            MainDataSet.Tables.Add(MetaData);
                            //_logger.LogCritical($"Yug 36 -- Sandy {JsonConvert.SerializeObject(MainDataSet)}");
                        }
                        else
                        {
                            //_logger.LogCritical($"Yug 37 -- Sandy Inside else condition");
                            foreach (var metadataColumn in metadataColumns)
                            {
                                if (MetaData.Columns[metadataColumn] == null)
                                {
                                    MetaData.Columns.Add(metadataColumn);
                                    _logger.LogCritical($"Yug 38 -- Sandy {MetaData}");
                                }
                            }
                        }
                        //</Find or create the meta_data Table>

                        MainDataSet = util.AddCustomDataTables(MainDataSet, enableLog);

                        //_logger.LogCritical($"Yug 39 -- sandy {JsonConvert.SerializeObject(MainDataSet)}");

                        MainDataSet = ProcessDataSet();
                        //_logger.LogCritical($"Yug 45 -- Sandy {JsonConvert.SerializeObject(MainDataSet)}");
                        List<Condition> Conditions = GetConditionsFromTemplate();
                        //_logger.LogCritical($"Yug 46 -- Sndy {JsonConvert.SerializeObject(Conditions)}");

                        // Tell the Utility Class that the User wants to Exclude certain Tables from the Report
                        //util.CustomExcludeTables = tablesToExclude;

                        List<Tuple<string, string, string>> allProperties = util.ReadAllCustomProperties(enableLog);
                        //_logger.LogCritical($"Yug 47 -- Sandy {JsonConvert.SerializeObject(allProperties)}");

                        if (allProperties != new List<Tuple<string, string, string>>())
                        {
                            Tuple<string, string, string> reportDocProperty = allProperties.Find(x => x.Item1.ToUpper() == "REPORTDOC");
                            //_logger.LogCritical($"Yug 48 -- Sandy {JsonConvert.SerializeObject(reportDocProperty)}");

                            if (reportDocProperty != null)
                            {
                                ReportDocProperty reportDocCustomProperties = OpenXmlAddinBase.DeserializeClass<ReportDocProperty>(reportDocProperty.Item3);

                               // _logger.LogCritical($"Yug 49 -- Sandy {JsonConvert.SerializeObject(reportDocCustomProperties)}");

                                XmlDocument xml = new XmlDocument();
                                XMLHelper.LoadXmlSafely(xml, reportDocCustomProperties.Value, enableLog);

                                //_logger.LogCritical($"Yug 50 -- Sandy {JsonConvert.SerializeObject(xml)}");

                                foreach (XmlNode xNode in xml.ChildNodes)
                                {
                                    if (xNode.Name != "ReportDoc")
                                    {
                                        continue;
                                    }

                                    XmlNodeList xmlReportDoc = xNode.ChildNodes;

                                    foreach (XmlNode xNodeReportDoc in xmlReportDoc)
                                    {
                                        if (xNodeReportDoc.Name.ToUpper().Contains("-SORT"))
                                        {
                                            List<ColumnSort> columnSort = OpenXmlAddinBase.DeserializeClass<List<ColumnSort>>(xNodeReportDoc.InnerText);

                                            if (string.IsNullOrEmpty(columnSort.ToString()) && columnSorts.ContainsKey(xNodeReportDoc.Name))
                                            {
                                                columnSorts.Remove(xNodeReportDoc.Name);
                                            }
                                            else
                                            {
                                                if (columnSorts.ContainsKey(xNodeReportDoc.Name))
                                                {
                                                    columnSorts[xNodeReportDoc.Name] = columnSort;
                                                }
                                                else
                                                {
                                                    columnSorts.Add(xNodeReportDoc.Name, columnSort);
                                                }
                                            }
                                        }

                                        if (xNodeReportDoc.Name.ToUpper().Contains("-FILTER"))
                                        {
                                            List<ColumnFilter> columnFilter = OpenXmlAddinBase.DeserializeClass<List<ColumnFilter>>(xNodeReportDoc.InnerText);

                                            if (string.IsNullOrEmpty(columnFilter.ToString()) && columnSorts.ContainsKey(xNodeReportDoc.Name))
                                            {
                                                columnFilters.Remove(xNodeReportDoc.Name);
                                            }
                                            else
                                            {
                                                if (columnFilters.ContainsKey(xNodeReportDoc.Name))
                                                {
                                                    columnFilters[xNodeReportDoc.Name] = columnFilter;
                                                }
                                                else
                                                {
                                                    columnFilters.Add(xNodeReportDoc.Name, columnFilter);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        List<OXmlTable> tbls = _doc.MainDocumentPart.Document.Body.Descendants<OXmlTable>().ToList();
                        //_logger.LogCritical($"Yug 51 -- Sandy {JsonConvert.SerializeObject(tbls)}");
                        foreach (OXmlTable tbl in tbls)
                        {
                            var tableProperties = tbl.Descendants<TableProperties>().FirstOrDefault();

                            string tableTitle = string.Empty;

                            if (tableProperties.TableCaption != null)
                            {
                                tableTitle = tableProperties.TableCaption.Val.ToString();
                            }

                            if (tableTitle.Contains("DynamicTable"))
                            {
                                SetupDynamicTable(tbl, tableTitle);
                            }

                            Dictionary<string, List<ColumnFilter>> specificColumnFilters = columnFilters.Where(x => x.Key.Contains(string.Format("{0}-", tableTitle.Replace("|", ".")))).ToDictionary(y => y.Key, y => y.Value);
                            Dictionary<string, List<ColumnSort>> specificColumnSorts = columnSorts.Where(x => x.Key.Contains(string.Format("{0}-", tableTitle.Replace("|", ".")))).ToDictionary(y => y.Key, y => y.Value);

                            UpdateTableInformation(tbl, specificColumnFilters, specificColumnSorts, Conditions, enableLog);
                        }

                        /*Update Table Aggregate Fields*/
                        UpdateTableAggregateFields(enableLog);
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogCritical($"Yug 55 -- Sandy {exception.Message}");
                    _logger.LogCritical($"Yug 56 -- Sandy {exception.InnerException}");
                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                }
            }

        }

        private void SetupDynamicTable(OXmlTable tbl, string tableTitle)
        {
            tableTitle = tableTitle.Replace("DynamicTable:", "").Left("|");
            DataTable dataTable = MainDataSet.Tables[tableTitle];

            var hiddenColumns = new List<string>();

            if (MainDataSet.Tables["meta_data"] != null)
            {
                var foundRows = MainDataSet.Tables["meta_data"].Select(string.Format("table='{0}'", tableTitle));

                foreach (DataRow row in foundRows)
                {
                    if (row["dynamic_hide_column"] != null && row["dynamic_hide_column"].ToString().ToUpper() == "TRUE")
                    {
                        hiddenColumns.Add(row["field"].ToString());
                    }
                }
            }

            if (dataTable == null)
            {
                return;
            }

            var rows = tbl.Descendants<TableRow>();
            RunProperties originalMergeTextProperties = null;
            TableCell tableCell = new TableCell();
            bool hasRepeatHeader = false;

            if (rows.First().Descendants<Run>().Any())
            {
                tableCell = rows.First().Descendants<TableCell>().FirstOrDefault().CloneNode(true) as TableCell;

                //<Assign hasRepeatHeader>
                var tableRowProperty = rows.FirstOrDefault().Descendants<TableRowProperties>();

                foreach (var headerSubProperty in tableRowProperty.FirstOrDefault())
                {
                    if (headerSubProperty as TableHeader != null)
                    {
                        hasRepeatHeader = true;
                    }
                }
                //</Assign hasRepeatHeader>
            }

            Run originalMergeRun;
            if (rows.Last().Descendants<Run>().Any())
            {
                originalMergeRun = rows.Last().Descendants<Run>().First();
                originalMergeTextProperties = originalMergeRun.RunProperties;
            }

            for (int index = rows.Count() - 1; index >= 0; --index)
            {
                tbl.RemoveChild(rows.Last());
            }

            var tableProperties = new TableProperties();
            foreach (var props in tbl.Descendants<TableProperties>())
            {
                foreach (var prop in props)
                {
                    tbl.AppendChild(prop.CloneNode(true));
                }
            }

            tbl.AppendChild(tableProperties);

            var headerCells = new List<TableCell>();
            var mergeCells = new List<TableCell>();
            int colCount = 0;

            /*<Make merge fields>*/
            foreach (DataColumn column in dataTable.Columns)
            {
                if (hiddenColumns.Contains(column.ColumnName))
                {
                    continue;
                }

                TableCell tblCell = tableCell.CloneNode(true) as TableCell;

                bool showT1Column = true;
                bool showT2Column = true;
                if (column.ColumnName.Contains("TableRowNumber"))
                {
                    if (column.ColumnName.Contains("t1."))
                    {
                        showT1Column = false;
                    }
                    else if (column.ColumnName.Contains("t2."))
                    {
                        showT2Column = false;
                    }

                    if (showT1Column && showT2Column)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                if ((column.ColumnName.Contains("t1.") && !showT1Column) || (column.ColumnName.Contains("t2.") && !showT2Column))
                {
                    continue;
                }

                if (tableCell == null)
                    break;

                List<SimpleField> orignalHeaderFields = tblCell.Descendants<SimpleField>().ToList();
                foreach (SimpleField field in orignalHeaderFields)
                {
                    string columnName = field.Instruction.Value.Replace(" MERGEFIELD ", string.Empty).Trim().Replace(@"\* MERGEFORMAT", string.Empty).Trim();

                    if (columnName.Contains("[Label]"))
                    {
                        var headerRun = new Run(new Text(columnName.Replace("[Label]", column.Caption.Replace("t1.", "").Replace("t2.", ""))));
                        field.Parent.ReplaceChild(headerRun, field);
                    }
                    if (columnName.Contains("[Unit]"))
                    {
                        DataRow metaData = MetaData.Select(string.Format("table='{0}' and field = '{1}'", dataTable.TableName, column)).FirstOrDefault();

                        if (metaData != null)
                        {
                            string unitLabel = metaData["unit_label"].ToString();
                            if (!string.IsNullOrEmpty(unitLabel))
                            {
                                var unitRun = new Run(new Text(columnName.Replace("[Unit]", unitLabel)));
                                field.Parent.ReplaceChild(unitRun, field);
                            }
                        }
                    }
                }

                headerCells.Add(tblCell);

                string instructionText = String.Format(" MERGEFIELD  {0}  \\* MERGEFORMAT ", column.ColumnName);
                var simpleField = new SimpleField() { Instruction = instructionText };

                var run = new Run();

                var text = new Text { Text = String.Format("«{0}»", column.ColumnName) };
                run.AppendChild(text);

                simpleField.AppendChild(run);

                var mergeRun = new Run(simpleField);

                var mergeParagraph = new Paragraph();
                var mergeParagraphProperties = new ParagraphProperties();

                if (originalMergeTextProperties != null)
                {
                    var fieldRuns = simpleField.Descendants<Run>();

                    if (fieldRuns.Any())
                    {
                        fieldRuns.First().RunProperties = (RunProperties)originalMergeTextProperties.CloneNode(true);
                    }

                    mergeParagraphProperties.AppendChild(originalMergeTextProperties.CloneNode(true));

                    if (column.DataType != typeof(string))
                    {
                        mergeParagraphProperties.AppendChild(new Justification { Val = JustificationValues.Right });
                        mergeParagraph.AppendChild(mergeParagraphProperties);
                    }
                }


                mergeParagraph.AppendChild(mergeRun);

                var mergeCell = new TableCell(mergeParagraph);
                mergeCell.AppendChild(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                mergeCells.Add(mergeCell);

                if (++colCount > 62) //63 is the maximum number of columns
                {
                    break;
                }
            }
            /*</Make merge fields>*/
            if (headerCells.Count > 0)
            {
                var headerRow = new TableRow();
                var mergeRow = new TableRow();

                headerRow.Append(headerCells);
                mergeRow.Append(mergeCells);

                if (hasRepeatHeader)
                {
                    headerRow.Append(new TableHeader());
                }

                tbl.AppendChild(headerRow);
                tbl.AppendChild(mergeRow);
            }
        }

        private void UpdateTableInformation(OXmlTable table, Dictionary<string, List<ColumnFilter>> columnFilters, Dictionary<string, List<ColumnSort>> columnSorts, List<Condition> cachedConditions, bool enableLog)
        {
            try
            {
                string tableTitle = string.Empty;
                string originalTableTitle = string.Empty;

                List<TableRow> rows = table.Descendants<TableRow>().ToList();
                List<Condition> conditions = cachedConditions.Clone();
                Dictionary<string, string> conditionFormulaMapping = new Dictionary<string, string>();

                TableRowProperties dataRowProperties = null;

                int rowToClone = 1;

                TableDescription tableDescription = table.Descendants<TableDescription>().FirstOrDefault();
                if (tableDescription != null)
                {
                    string descriptionText = tableDescription.Val;
                    var descriptions = descriptionText.Split('|');
                    string templateRow = descriptions.FirstOrDefault(x => x.Contains("TemplateRow:"));
                    if (templateRow != string.Empty)
                    {
                        int.TryParse(templateRow.Replace("TemplateRow:", "").Trim(), out rowToClone);
                    }
                }

                if (rows.Count >= 2 && rows[rowToClone].TableRowProperties != null)
                {
                    dataRowProperties = rows[rowToClone].TableRowProperties.CloneNode(true) as TableRowProperties;
                }

                if (table != null && table.Descendants<TableProperties>().FirstOrDefault().TableCaption != null)
                {
                    originalTableTitle = table.Descendants<TableProperties>().FirstOrDefault().TableCaption.Val.ToString();
                    tableTitle = originalTableTitle.Left("|");

                    if (tableTitle.Contains("DynamicTable:"))
                    {
                        tableTitle = tableTitle.Replace("DynamicTable:", "");
                    }
                }

                if (!string.IsNullOrEmpty(tableTitle))
                {
                    int rowNumber = 1;

                    DataTable mydt = MainDataSet.Tables[tableTitle];

                    DataTable metadataStringFormat = null;

                    if (MetaData != null)
                    {
                        if (MetaData.Columns["table"] != null && MetaData.Columns["field"] != null && MetaData.Columns["number_format"] != null)
                        {
                            DataRow[] rowsStringFormat = MetaData.Select(string.Format("table='{0}' and (number_format is not null or (data_type is not null and format is not null))", tableTitle));

                            if (rowsStringFormat.Length > 0)
                            {
                                metadataStringFormat = rowsStringFormat.CopyToDataTable();
                            }
                        }
                    }

                    if (mydt != null)
                    {
                        mydt = FilterSortDataTable(mydt, columnSorts, columnFilters);
                    }

                    if (mydt != null && mydt.Rows.Count > 0)
                    {
                        Boolean hasTotalRow = false;
                        Regex rgx = new Regex(@"\«.*?\»");

                        if (rows.Count > 2)
                        {
                            hasTotalRow = true;
                        }

                        TableRow templateRow = null;

                        foreach (var condition in conditions)
                        {
                            if (condition.Table == tableTitle)
                            {
                                if (!conditionFormulaMapping.ContainsKey(condition.Table + "_" + condition.Name))
                                {
                                    var tableFormula = CreateDataTableFormula(enableLog, condition, mydt);
                                    conditionFormulaMapping.Add(condition.Table + "_" + condition.Name, tableFormula.FormulaName);
                                }
                            }
                        }

                        foreach (DataRow row in mydt.Rows)
                        {
                            if (rows.Count <= 1)
                            {
                                continue;
                            }

                            templateRow = rows[rowToClone];
                            var clonedRow = (TableRow)templateRow.Clone();
                            var cells = clonedRow.Descendants<TableCell>().ToList();
                            //var runs = cells[0].Descendants<Run>().ToList();

                            //List<TableCell> myCells = new List<TableCell>();
                            var tableRow = new TableRow();
                            foreach (var cell in cells)
                            {
                                var newCell = (TableCell)cell.Clone();


                                var inlineParts = newCell.Descendants<OXmlWordProc.Inline>().ToList();
                                foreach (var inlinePart in inlineParts)
                                {
                                    if (string.IsNullOrEmpty(inlinePart.DocProperties.Title) || !row.Table.Columns.Contains(inlinePart.DocProperties.Title))
                                    {
                                        //inlinePart.Parent.RemoveChild(inlinePart);
                                        continue;
                                    }



                                    string fileName = row[inlinePart.DocProperties.Title].ToString();
                                    Boolean deleteImageObject = true;

                                    if (File.Exists(fileName))
                                    {
                                        var imagePart = inlinePart.Descendants<OXmlDrawing.Blip>().FirstOrDefault();

                                        ScaleImage(new FileInfo(fileName), inlinePart);

                                        ReplaceImage(imagePart, new FileInfo(fileName), enableLog);
                                        deleteImageObject = false;
                                        inlinePart.DocProperties.Title = "";
                                    }

                                    if (deleteImageObject)
                                    {
                                        //newCell.RemoveChild(inlinePart);
                                    }

                                }
                                /*<Replace Simple Fields>*/
                                List<SimpleField> simpleFields = newCell.Descendants<SimpleField>().ToList();
                                foreach (var simpleField in simpleFields)
                                {
                                    string columnName = simpleField.Instruction.Value.Replace(" MERGEFIELD ", string.Empty).Trim().Replace(@"\* MERGEFORMAT", string.Empty).Trim();

                                    if (columnName.Contains("StandAlone:") || columnName.Contains("SA:") || columnName.Contains("Units:"))
                                        continue;

                                    if (columnName == string.Empty)
                                    {
                                        columnName = simpleField.InnerText.Replace("«", string.Empty).Replace("»", string.Empty);
                                    }

                                    string replaceText = string.Empty;

                                    if (mydt.Columns.Contains(columnName))
                                    {
                                        replaceText = row[columnName].ToString();
                                    }

                                    if (columnName == "RowNumber")
                                    {
                                        replaceText = rowNumber++.ToString(CultureInfo.InvariantCulture);
                                    }

                                    if (columnName.Contains("CONDITION:"))
                                    {
                                        //Process conditions inside the table as the "ProcessCondition" method doesn't support data tables and losses references for table and data row
                                        string fieldText = simpleField.InnerText;

                                        if (string.IsNullOrEmpty(fieldText))
                                        {
                                            fieldText = simpleField.Instruction;
                                        }
                                        var condition = conditions.Find(x => fieldText.Contains("CONDITION:" + x.Name + "»") ||
                                                 simpleField.Instruction == String.Format(" MERGEFIELD BEGIN_CONDITION:{0} \\* MERGEFORMAT ", x.Name) ||
                                                 simpleField.Instruction == String.Format(" MERGEFIELD END_CONDITION:{0} \\* MERGEFORMAT ", x.Name));

                                        if (condition != null && conditionFormulaMapping.ContainsKey(condition.Table + "_" + condition.Name))
                                        {
                                            var formulaColumnName = conditionFormulaMapping[condition.Table + "_" + condition.Name];
                                            bool conditionTrueFalse;
                                            bool.TryParse(row[formulaColumnName].ToString(), out conditionTrueFalse);
                                            var conditionResult = new Tuple<string, bool>(condition.Name, conditionTrueFalse);


                                            RemoveConditionField(simpleField, fieldText, conditionResult);
                                        }

                                        continue;

                                    }

                                    if (metadataStringFormat != null)
                                    {
                                        DataRow[] formatRows = metadataStringFormat.Select(string.Format("field='{0}'", columnName));
                                        if (formatRows.Length > 0)
                                        {
                                            try
                                            {
                                                string stringFormat = formatRows[0]["number_format"].ToString();
                                                if (!string.IsNullOrEmpty(stringFormat))
                                                {
                                                    if (replaceText.IsNumeric())
                                                    {
                                                        decimal replaceNumber;
                                                        decimal.TryParse(replaceText, out replaceNumber);
                                                        replaceText = replaceNumber.ToString(stringFormat);
                                                    }
                                                    else
                                                    {
                                                        //replaceText = replaceText.ToString("MM/dd/yyyy");

                                                    }
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                                                //suppress error, continue
                                            }

                                            try
                                            {
                                                string dataType = formatRows[0]["data_type"].ToString();
                                                string format = formatRows[0]["format"].ToString();

                                                if (!string.IsNullOrEmpty(dataType) && !string.IsNullOrEmpty(format))
                                                {
                                                    switch (dataType.ToUpper())
                                                    {
                                                        case "DATE":
                                                            DateTime replaceDate;
                                                            DateTime.TryParse(replaceText, out replaceDate);

                                                            replaceText = replaceDate.ToString(format);
                                                            break;

                                                        case "NUMBER":
                                                            decimal replaceNumber;
                                                            decimal.TryParse(replaceText, out replaceNumber);

                                                            replaceText = replaceNumber.ToString(format);
                                                            break;
                                                    }

                                                }

                                            }
                                            catch (Exception exception)
                                            {
                                                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                                                //suppress error, continue
                                            }
                                        }
                                    }

                                    replaceText = NumberFormatFromColumnName(columnName, replaceText);

                                    ReplaceMergeFieldInRow(simpleField, replaceText);
                                }
                                /*</Replace Simple Fields>*/

                                /*<Replace Tags in Runs>*/
                                List<Run> cellRuns = newCell.Descendants<Run>().Where(x => x.InnerText.Contains("«")).ToList();

                                foreach (var run in cellRuns)
                                {
                                    string replaceText = run.InnerText;

                                    MatchCollection matches = rgx.Matches(replaceText);

                                    foreach (Match match in matches)
                                    {
                                        string columnName = match.ToString().Replace("«", string.Empty).Replace("»", string.Empty);

                                        if (columnName.Contains("StandAlone:") || columnName.Contains("SA:") || columnName.Contains("Units:") || columnName.Contains("CONDITION:"))
                                            continue;

                                        if (columnName == "RowNumber")
                                        {
                                            replaceText = rowNumber++.ToString(CultureInfo.InvariantCulture);
                                        }
                                        else
                                        {
                                            if (mydt.Columns.Contains(columnName))
                                            {
                                                replaceText = ReplaceMailMergeValue(match, replaceText, row[match.ToString().Replace("«", string.Empty).Replace("»", string.Empty)].ToString());
                                            }
                                            else
                                            {
                                                replaceText = replaceText.Replace(match.ToString(), string.Empty);
                                            }
                                        }
                                    }

                                    if (run.InnerText != replaceText)
                                    {
                                        run.RemoveAllChildren<Text>();
                                        run.AppendChild(new Text(replaceText));
                                    }
                                }

                                /*</Replace Tags in Runs>*/

                                tableRow.Append(newCell);
                            }

                            if (hasTotalRow)
                            {
                                //Insert Row before last row
                                table.InsertBefore(tableRow, rows[rows.Count - 1]);
                            }
                            else
                            {
                                //Insert row at end
                                if (dataRowProperties != null)
                                {
                                    tableRow.Append(dataRowProperties.CloneNode(true));
                                }

                                table.AppendChild(tableRow);
                            }
                        }

                        if (templateRow != null)
                        {
                            templateRow.Remove();
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

            }


        }

        private string NumberFormatFromColumnName(string columnName, string replaceText)
        {
            try
            {
                decimal replaceNumber;
                if (!decimal.TryParse(replaceText, out replaceNumber))
                {
                    return replaceText;
                }

                string numberFormat = string.Empty;

                var numberFormatSplit = columnName.Split(new string[] { "__" }, StringSplitOptions.None);
                if (numberFormatSplit.Count() > 1)
                {
                    numberFormat = numberFormatSplit.LastOrDefault();

                    if (!string.IsNullOrEmpty(numberFormat))
                    {
                        const string pattern = "[CcDcEeFfGgNnPp][0-9]?[0-9]?";

                        var match = Regex.Matches(numberFormat, pattern);

                        if (match.Count == 0)
                        {
                            return replaceText;
                        }
                    }
                }

                if (string.IsNullOrEmpty(numberFormat))
                {
                    return replaceText;
                }

                return replaceNumber.ToString(numberFormat);
            }
            catch (Exception)
            {
                return replaceText;
            }
        }

        private void CreateFieldsFromData()
        {
            _logger.LogCritical($"Yug 61 -- Sandy inside the CreateFieldsFromData start");
            SetDocText();

            Regex rgx = new Regex(@"\<w:t\>&lt;IMAGE-(.*?)&gt;\<\/w:t\>");

            //_logger.LogCritical($"Yug 62 -- Sandy {JsonConvert.SerializeObject(rgx)}");

            MatchCollection matches = rgx.Matches(_docText);

            //_logger.LogCritical($"Yug 63 -- Sandy {JsonConvert.SerializeObject(matches)}");

            // make matches unique ** speed **
            foreach (var match in matches)
            {
                string fieldText = match.ToString().Replace(@"<w:t>&lt;", "").Replace(@"&gt;</w:t>", "");
                string instructionText = String.Format(" MERGEFIELD  {0}  \\* MERGEFORMAT", fieldText);
                SimpleField simpleField1 = new SimpleField() { Instruction = instructionText };

                Run run = new Run();

                RunProperties runProperties = new RunProperties();
                NoProof noProof = new NoProof();

                runProperties.Append(noProof);
                Text text1 = new Text();
                text1.Text = String.Format("«{0}»", fieldText);

                run.Append(runProperties);
                run.Append(text1);

                simpleField1.Append(run);

                Paragraph paragraph = new Paragraph();
                paragraph.Append(new OpenXmlElement[] { simpleField1 });

                _docText = _docText.Replace(match.ToString(), paragraph.InnerXml);
            }

            WriteDocTextToFile();
            _logger.LogCritical($"Yug 63 -- Sandy WriteDocTextToFile()");
        }

        private DataTable FilterSortDataTable(DataTable tbl, Dictionary<string, List<ColumnSort>> columnSorts, Dictionary<string, List<ColumnFilter>> columnFilters)
        {
            //<Filter tbl>
            StringBuilder filterString = new StringBuilder();
            int tabCount = 0;

            foreach (var grpFilter in columnFilters)
            {
                foreach (ColumnFilter columnFilter in grpFilter.Value)
                {

                    if (columnFilter != null && columnFilter.Column != null && !string.IsNullOrEmpty(columnFilter.Column.DisplayName))
                    {
                        string tabs = String.Concat(Enumerable.Repeat("\t", tabCount));

                        if (columnFilter.OpenParen == "(")
                        {
                            filterString.AppendLine(string.Format("{0}{1}", tabs, "("));
                            tabs = String.Concat(Enumerable.Repeat("\t", ++tabCount));
                        }

                        if (!tbl.Columns.Contains(columnFilter.Column.DisplayName))
                            continue;

                        Type dataColumnType = tbl.Columns[columnFilter.Column.DisplayName].DataType;
                        Decimal result;

                        if (dataColumnType == typeof(decimal) && decimal.TryParse(columnFilter.Value, out result))
                        {
                            filterString.AppendLine(string.Format("{0}{1} {2} {3}", tabs, columnFilter.Column.DisplayName, columnFilter.Operator, result));
                        }
                        else
                        {
                            filterString.AppendLine(string.Format("{0}{1} {2} {3}", tabs, columnFilter.Column.DisplayName, columnFilter.Operator, WrapStringInQuotes(columnFilter.Value)));
                        }

                        if (!string.IsNullOrEmpty(columnFilter.AndOr))
                        {
                            filterString.AppendLine(string.Format("{0}{1}", tabs, columnFilter.AndOr));
                        }

                        if (columnFilter.CloseParen == ")")
                        {
                            tabs = String.Concat(Enumerable.Repeat("\t", --tabCount));
                            filterString.AppendLine(string.Format("{0}{1}", tabs, ")"));
                        }
                    }
                }
            }

            if (filterString != new StringBuilder() && !string.IsNullOrEmpty(filterString.ToString()))
            {
                string tblName = tbl.TableName;
                DataRow[] drTablView = tbl.Select(filterString.ToString());

                if (drTablView.Any())
                {
                    tbl = drTablView.CopyToDataTable();
                    tbl.TableName = tblName;
                }
                else
                {
                    tbl = tbl.Clone();
                    //tbl.Clear();
                    tbl.TableName = tblName;
                }
            }
            // </Filter tbl>

            // <Sort tbl>
            string sortString = null;
            foreach (var grpSort in columnSorts)
            {
                string grpToSort = grpSort.Key.Contains("-SORT.") ? grpSort.Key.Replace("-SORT.", "|") : grpSort.Key.Replace("-SORT", "");

                if (grpToSort.Contains("."))
                {
                    var splitGrpToSort = grpToSort.Split('.');
                    int tableNumber = 0;

                    if (splitGrpToSort.Length > 1 && int.TryParse(splitGrpToSort.Last(), out tableNumber))
                    {
                        grpToSort = string.Join(".", splitGrpToSort.Take(splitGrpToSort.Length - 1));
                    }
                }

                if (tbl.TableName == XMLHelper.XMLEscapeCharsToSpecialChars(grpToSort))
                {
                    foreach (ColumnSort columnSort in grpSort.Value)
                    {
                        if (columnSort.Column != null && !string.IsNullOrEmpty(columnSort.Column.DisplayName) && tbl.Columns.Contains(columnSort.Column.DisplayName))
                        {
                            sortString += string.Format("{0} {1}, ", columnSort.Column.DisplayName, columnSort.Sort);
                        }
                    }

                    sortString = sortString != null && sortString.Length > 2 ? sortString.Substring(0, sortString.Length - 2) : string.Empty;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(sortString))
            {
                DataView dataView = tbl.DefaultView;

                if (sortString != null)
                {
                    dataView.Sort = sortString.Replace("Ascending", "ASC").Replace("Descending", "DESC");
                }

                DataTable tempTbl = dataView.ToTable();

                foreach (DataColumn column in tbl.Columns)
                {
                    if (!string.IsNullOrEmpty(column.Expression))
                    {
                        tempTbl.Columns[column.ColumnName].Expression = column.Expression;
                    }
                }

                tbl = tempTbl;
            }
            // </Sort tbl>


            //<RunningTotals>
            if (tbl.Rows.Count > 0)
            {

                foreach (var reportDocProperty in _reportDocPropertyList)
                {
                    foreach (XmlNode xNodeReportDoc in reportDocProperty)
                    {
                        if (xNodeReportDoc.Name.ToUpper().Contains("TABLEFORMULA"))
                        {
                            List<TableFormula> runningTotals = OpenXmlAddinBase.DeserializeClass<List<TableFormula>>(xNodeReportDoc.InnerText);

                            foreach (var runningTotal in runningTotals)
                            {
                                if (runningTotal.Table == tbl.TableName && runningTotal.Formula.ToUpper().Contains("RUNNINGTOTAL"))
                                {

                                    string runningColumn = runningTotal.Formula;
                                    runningColumn = runningColumn.ToUpper().Replace("RUNNINGTOTAL", string.Empty).Trim();
                                    runningColumn = runningColumn.Replace(")", "").Replace("(", "").Replace("-", "");
                                    runningColumn = runningColumn.Replace("[", "").Replace("]", "");
                                    runningColumn = runningColumn.Replace("'", "").Trim();

                                    string columnName = runningTotal.FormulaName;

                                    double runningValue = 0;
                                    for (int rowIndex = 0; rowIndex < tbl.Rows.Count; rowIndex++)
                                    {
                                        double cellValue;
                                        double.TryParse(tbl.Rows[rowIndex][runningColumn].ToString(), out cellValue);

                                        if (rowIndex == 0)
                                        {
                                            runningValue = cellValue;
                                            tbl.Rows[rowIndex][columnName] = runningValue;
                                        }
                                        else
                                        {
                                            runningValue += cellValue;
                                            tbl.Rows[rowIndex][columnName] = runningValue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //</RunningTotals>

            }
            return tbl;
        }

        private string ReplaceMailMergeValue(Match match, string cellValue, string replaceValue)
        {
            string emptyCell = cellValue.Replace(match.ToString(), string.Empty);

            if (emptyCell == "\r\a")
            {
                cellValue = cellValue.Replace(emptyCell, string.Empty);
            }

            if (!string.IsNullOrEmpty(cellValue))
                cellValue = cellValue.Replace(match.ToString(), replaceValue);//.Replace(emptyCell, string.Empty);

            return cellValue;
        }

        private string WrapStringInQuotes(string columnValue)
        {
            return "'" + columnValue.Replace("\"", "").Replace("'", "") + "'";
        }

        private void ReplaceRemainingFields(bool enableLog)
        {
            _logger.LogCritical($"Yug 76 -- Sandy ReplaceRemainingFields start");

            ConvertPartToSimpleField();

            /*<Replace Simple Fields>*/
            List<SimpleField> docSimpleFields = new List<SimpleField>();
            List<OpenXmlUnknownElement> openXmlUnknownElements = new List<OpenXmlUnknownElement>();

            try
            {
                _logger.LogCritical($"Yug 76.13 -- Sandy inside the try block");

                //<Header>
                foreach (var headerPart in _doc.MainDocumentPart.HeaderParts)
                {
                    _logger.LogCritical($"Yug 76.14 -- Sandy inside the foreach block {headerPart}");

                    openXmlUnknownElements.AddRange(headerPart.Header.Descendants<OpenXmlUnknownElement>().Where(x => x.LocalName == "fldSimple"));
                    docSimpleFields.AddRange(headerPart.Header.Descendants<SimpleField>());
                    _logger.LogCritical($"Yug 76.15 -- Sandy inside the foreach block end");

                }
                //</Header>

                //<Footer>
                foreach (var footerPart in _doc.MainDocumentPart.FooterParts)
                {
                    _logger.LogCritical($"Yug 76.16 -- Sandy inside the foreach block {footerPart}");

                    openXmlUnknownElements.AddRange(footerPart.Footer.Descendants<OpenXmlUnknownElement>().Where(x => x.LocalName == "fldSimple"));
                    docSimpleFields.AddRange(footerPart.Footer.Descendants<SimpleField>());

                    _logger.LogCritical($"Yug 76.17 -- Sandy inside the foreach block end");

                }
                //</Footer>

                //<Body>
                openXmlUnknownElements.AddRange(_doc.MainDocumentPart.Document.Descendants<OpenXmlUnknownElement>().Where(x => x.LocalName == "fldSimple"));
                docSimpleFields.AddRange(_doc.MainDocumentPart.Document.Descendants<SimpleField>());
                //</Body>

                _logger.LogCritical($"Yug 76.18 -- Sandy try block end");
            }
            catch(Exception ex) 
            {

            }
            foreach (var simpleField in docSimpleFields)
            {
                try
                {
                    if (simpleField.InnerXml.Contains("Sum(ABOVE)") || simpleField.InnerXml.Contains("Max(ABOVE)")
                         || simpleField.InnerXml.Contains("Min(ABOVE)") || simpleField.InnerXml.Contains("Avg(ABOVE)")
                         || simpleField.InnerXml.Contains("IMAGE-")
                        || (simpleField.Instruction.InnerText.Contains("PAGE") && string.IsNullOrEmpty(GetFieldValue(simpleField, enableLog))))
                    {
                        continue;
                    }

                    if (simpleField.InnerXml.Contains("SUBREPORT:"))
                    {
                        ProcessSubreportSimpleField(simpleField, enableLog);
                    }
                    else
                    {
                        ReplaceMergeFieldInRow(simpleField, GetFieldValue(simpleField, enableLog));
                    }
                }
                catch (Exception exc)
                {
                    //LoggingSingleton.Instance.LogMessage(exc, enableLog);

                    AddinBase.ErrorMessages.Add(exc.Message);
                }
            }

            foreach (var openXmlUnknownElement in openXmlUnknownElements)
            {
                try
                {
                    if (openXmlUnknownElement.InnerXml == "" || openXmlUnknownElement.InnerXml.Contains("Sum(ABOVE)") || openXmlUnknownElement.InnerXml.Contains("Max(ABOVE)")
                         || openXmlUnknownElement.InnerXml.Contains("Min(ABOVE)") || openXmlUnknownElement.InnerXml.Contains("Avg(ABOVE)")
                        || openXmlUnknownElement.InnerXml.Contains("PAGE") || openXmlUnknownElement.InnerXml.Contains("IMAGE-"))
                    {
                        continue;
                    }

                    if (openXmlUnknownElement.OuterXml.Contains("SUBREPORT:"))
                    {
                        ProcessSubreportUnknownElement(openXmlUnknownElement, enableLog);
                    }
                    else
                    {
                        ReplaceUnknownFields(openXmlUnknownElement, GetFieldValue(openXmlUnknownElement, enableLog));
                    }
                }
                catch (Exception exc)
                {
                    //LoggingSingleton.Instance.LogMessage(exc, enableLog);

                    AddinBase.ErrorMessages.Add(exc.Message);

                    _logger.LogCritical($"Yug 77 -- Sandy {exc.Message}");
                    _logger.LogCritical($"Yug 78 -- Sandy {exc.InnerException}");
                }
            }

            //<Setting Document to Update when Open if there are Word Formulas>
            var containsDirtyFormulas = _doc.MainDocumentPart.Document.Descendants<OpenXmlElement>().Where(x => !string.IsNullOrEmpty(x.InnerText) && x.InnerText.Contains("ABOVE"));

            if (containsDirtyFormulas.Any())
            {
                var updateFields = new UpdateFieldsOnOpen { Val = new OnOffValue(true) };
                DocumentSettingsPart settingsPart = _doc.MainDocumentPart.GetPartsOfType<DocumentSettingsPart>().FirstOrDefault();
                if (settingsPart != null)
                {
                    settingsPart.Settings.PrependChild(updateFields);
                    settingsPart.Settings.Save();
                }
            }
            //</Setting Document to Update when Open if there are Word Formulas>
        }

        private void ProcessSubreportSimpleField(SimpleField simpleField, bool enableLog)
        {
            try
            {
                string subreportName = simpleField.Instruction;
                subreportName = subreportName.Replace("SUBREPORT:", "").Replace("MERGEFIELD", "").Replace(@"\* MERGEFORMAT", "").Trim();

                AltChunk subreportAltChunk = GetSubreport(subreportName);

                if (subreportAltChunk != null)
                {
                    //simpleField.Parent.InsertAfter(subreportAltChunk, simpleField);
                    simpleField.Parent.Parent.InsertBefore(subreportAltChunk, simpleField.Parent);
                }
            }
            catch (Exception exc)
            {
                //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Error, "OpenXMLReportGenerator", "ProcessSubreportSimpleField - " + exc.Message + " | " + exc.StackTrace, enableLog);

                AddinBase.ErrorMessages.Add(exc.Message);
            }

            ReplaceMergeFieldInRow(simpleField, string.Empty);
        }

        private void ProcessSubreportUnknownElement(OpenXmlUnknownElement unknownElement, bool enableLog)
        {
            try
            {
                string subreportName = unknownElement.OuterXml;

                string regexPattern = @"MERGEFIELD(.*?)MERGEFORMAT";
                Regex regexText = new Regex(regexPattern);
                MatchCollection matches = regexText.Matches(subreportName);

                if (matches.Count > 0)
                {
                    subreportName = matches[0].ToString().Replace("SUBREPORT:", "").Replace("MERGEFIELD", "").Replace(@"\* MERGEFORMAT", "").Trim();
                }

                AltChunk subreportAltChunk = GetSubreport(subreportName);

                if (subreportAltChunk != null)
                {
                    //This may be a little weak, I'm just not sure what the unknownElement could be.  Works in my test examples
                    unknownElement.Parent.Parent.Parent.InsertAfter(subreportAltChunk, unknownElement.Parent.Parent);
                }
            }
            catch (Exception exc)
            {
                //LoggingSingleton.Instance.LogMessage(exc, enableLog);

                AddinBase.ErrorMessages.Add(exc.Message);
            }

            var someRun = new Run();
            someRun.Append(new Text(""));
            OpenXmlElement parent = unknownElement.Parent;
            parent.ReplaceChild(someRun, unknownElement);

        }

        private AltChunk GetSubreport(string subreportName)
        {
            foreach (var reportDocProperty in _reportDocPropertyList)
            {
                foreach (XmlNode xNodeReportDoc in reportDocProperty)
                {
                    if (xNodeReportDoc.Name != "Subreport")
                    {
                        continue;
                    }

                    var subreports = OpenXmlAddinBase.DeserializeClass<List<Subreport>>(xNodeReportDoc.InnerText);

                    Subreport subreport = subreports.FirstOrDefault(x => x.SubreportName == subreportName);

                    if (subreport != null)
                    {
                        string fileName = string.Format("{0}.{1}", subreport.SubreportNamespace, subreport.SubreportName);

                        if (!fileName.Contains(".doc"))
                        {
                            fileName = fileName + ".docx";
                        }

                        string filePath = SavePath + @"\" + fileName;

                        if (!string.IsNullOrEmpty(subreport.SubreportName))
                        {
                            if (!WinFileSystem.Exists(filePath))
                                return null;

                            string altChunkId = "AltChunkId" + new Random((int)DateTime.Now.Ticks).Next(0, 1000000000);

                            AlternativeFormatImportPart chunk = _doc.MainDocumentPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.WordprocessingML, altChunkId);

                            chunk.FeedData(File.Open(filePath, FileMode.Open));

                            AltChunk altChunk = new AltChunk();

                            altChunk.Id = altChunkId;

                            return altChunk;

                        }
                    }
                }
            }

            return null;
        }

        private void UpdateTableAggregateFields(bool enableLog)
        {
            List<SimpleField> simpleFields = _doc.MainDocumentPart.Document.Body.Descendants<SimpleField>().ToList();
            try
            {

                _logger.LogCritical($"Yug 52 -- Sandy {simpleFields}");

                foreach (var simpleField in simpleFields)
                {
                    string fieldText = simpleField.InnerText;

                    if (string.IsNullOrEmpty(fieldText))
                    {
                        fieldText = simpleField.Instruction;
                    }

                    if (fieldText.ToUpper().Contains(@"=")
                        &&
                        (
                            fieldText.ToUpper().Contains(@"SUM(ABOVE)") ||
                            fieldText.ToUpper().Contains(@"AVERAGE(ABOVE)") ||
                            fieldText.ToUpper().Contains(@"MAX(ABOVE)") ||
                            fieldText.ToUpper().Contains(@"MIN(ABOVE)")
                        )
                        &&
                        fieldText.ToUpper().Contains(@"\* MERGEFORMAT")
                        )
                    {
                        simpleField.Dirty = true;
                    }

                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                _logger.LogCritical($"Yug 53 -- Sandy Catch Block Exception {exception.Message}");
                _logger.LogCritical($"Yug 54 -- Sandy Catch Block Exception {exception.InnerException}");
            }
        }

        private string GetFieldValue(OpenXmlElement openXmlElement, bool enableLog)
        {
            if (openXmlElement == null || string.IsNullOrEmpty(openXmlElement.InnerText))
            {
                return string.Empty;
            }

            string columnName = openXmlElement.InnerText.Replace("«", string.Empty).Replace("»", string.Empty);
            if (columnName == string.Empty)
            {
                columnName = openXmlElement.OuterXml;
            }

            if (columnName.Contains("StandAlone:") || columnName.Contains("SA:") || columnName.Contains("Units:"))
            {
                if ((openXmlElement as SimpleField) != null)
                {
                    columnName = (openXmlElement as SimpleField).Instruction.Value;
                }
                if ((openXmlElement.Parent as SimpleField) != null)
                {
                    columnName = (openXmlElement.Parent as SimpleField).Instruction.Value;
                }
                else if ((openXmlElement.Parent.Parent as SimpleField) != null)
                {
                    columnName = (openXmlElement.Parent.Parent as SimpleField).Instruction.Value;
                }
                else if (openXmlElement.OuterXml.Contains("MERGEFIELD"))
                {
                    string regexPattern = @"MERGEFIELD(.*?)MERGEFORMAT";
                    Regex regexText = new Regex(regexPattern);
                    MatchCollection matches = regexText.Matches(openXmlElement.OuterXml);

                    if (matches.Count > 0)
                    {
                        columnName = matches[0].ToString();
                    }
                }
                else
                {
                    try
                    {
                        string regexPattern = @"MERGEFIELD(.*?)~" + columnName.Right(":");
                        Regex regexText = new Regex(regexPattern);
                        MatchCollection matches = regexText.Matches(openXmlElement.Parent.Parent.InnerXml);

                        if (matches.Count > 0)
                        {
                            columnName = matches[0].ToString();
                        }

                    }
                    catch (Exception exception)
                    {
                        //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                        //Continue
                    }
                }
            }

            if (columnName.Contains("Units:") || columnName.Contains("StandAlone:") ||
                columnName.Contains("SA:") || columnName.Contains("ReportDoc-"))
            {
                string sTempName = columnName.Replace("MERGEFIELD", string.Empty);
                sTempName = sTempName.Replace("MERGEFORMAT", string.Empty);
                sTempName = sTempName.Replace(@"\*", string.Empty).Trim();

                //<Units>
                if (MetaData != null && sTempName.StartsWith("Units:"))
                {
                    string field = sTempName.Right(":");
                    string tableName = field.Left("~");
                    string labelName = field.Right("~");
                    DataTable tbl = MainDataSet.GetDataTable(tableName);

                    if (tbl != null)
                    {
                        if (field.IndexOf('.') > 0)
                        {
                            labelName = field.Substring(field.Length - field.Reverse().IndexOf('.'));
                        }
                    }
                    List<DataRow> metadataRows = (from rows in MetaData.AsEnumerable()
                                                  where rows.Field<string>("table") == tableName &&
                                                        rows.Field<string>("field") == labelName
                                                  select rows).ToList();

                    if (metadataRows.Any())
                    {
                        string unitLabel = metadataRows[0].Get<string>("unit_label", enableLog);
                        return unitLabel;
                    }
                }
                //</Units>

                //<StandAlone>
                if (sTempName.StartsWith("StandAlone:") || columnName.Contains("SA:"))
                {
                    foreach (var reportDocProperty in _reportDocPropertyList)
                    {
                        foreach (XmlNode xNodeReportDoc in reportDocProperty)
                        {
                            string xNodeName = xNodeReportDoc.Name.Replace("-", ":");
                            if (xNodeReportDoc.Attributes != null && xNodeReportDoc.Attributes.Count > 0)
                            {
                                if (xNodeReportDoc.Attributes.GetNamedItem("name").ToString().Length > 0)
                                    xNodeName = xNodeReportDoc.Attributes.GetNamedItem("name").InnerXml;
                            }
                            if (sTempName == xNodeName)
                            {
                                sTempName = xNodeReportDoc.InnerText;
                                break;
                            }
                        }
                    }

                    string field = sTempName.Right("-").Right(":");
                    string tableName = field.Left("~");
                    string labelName = field.Right("~");

                    DataTable tbl = MainDataSet.GetDataTable(tableName);

                    string standAlone = string.Empty;

                    if (tbl != null && (tbl.Rows.Count > 0) && tbl.Columns.Contains(labelName))
                    {
                        standAlone = tbl.Rows[0].Get<string>(labelName, enableLog);

                        string dataType = null;
                        string format = null;

                        List<DataRow> metadataRows = (from rows in MetaData.AsEnumerable()
                                                      where rows.Field<string>("table") == tableName &&
                                                            rows.Field<string>("field") == labelName
                                                      select rows).ToList();
                        if (metadataRows.Any())
                        {
                            dataType = metadataRows[0].Get<string>("data_type", enableLog);
                            format = metadataRows[0].Get<string>("format", enableLog);
                        }

                        try
                        {

                            if (!string.IsNullOrEmpty(dataType) && !string.IsNullOrEmpty(format))
                            {
                                switch (dataType.ToUpper())
                                {
                                    case "DATE":
                                        DateTime replaceDate;
                                        DateTime.TryParse(standAlone, out replaceDate);

                                        standAlone = replaceDate.ToString(format);
                                        break;

                                    case "NUMBER":
                                        decimal replaceNumber;
                                        decimal.TryParse(standAlone, out replaceNumber);

                                        standAlone = replaceNumber.ToString(format);
                                        break;
                                }

                            }

                            standAlone = NumberFormatFromColumnName(labelName, standAlone);

                        }
                        catch (Exception exception)
                        {
                            //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                            //suppress error, continue
                        }
                    }


                    return standAlone;
                }

                //</StandAlone>

                //<ReportDoc Tags>
                if (sTempName.StartsWith("ReportDoc-"))
                {
                    if (MainDataSet.Tables["from_app"] != null && MainDataSet.Tables["from_app"].Rows.Count > 0)
                    {
                        if (MainDataSet.Tables["from_app"].Columns[sTempName] != null)
                        {
                            string reportDocValue = MainDataSet.Tables["from_app"].Rows[0][sTempName].ToString();
                            return reportDocValue;
                        }
                    }
                }
                //</ReportDoc Tags>

            }
            return string.Empty;
        }

        private string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        private string ConvertHexToString(string hexValue)
        {
            string StrValue = "";
            while (hexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(hexValue.Substring(0, 2), 16)).ToString();
                hexValue = hexValue.Substring(2, hexValue.Length - 2);
            }
            return StrValue;
        }

        private void ReplaceToc()
        {
            SimpleField tableOfContents = _doc.MainDocumentPart.Document.Body.Descendants<SimpleField>().FirstOrDefault(b => b.InnerText.Contains("ReportDoc-TOC"));

            _logger.LogCritical($"Yug 26 -- Sandy {tableOfContents}");
            if (tableOfContents != null)
            {
                OpenXmlElement parent = tableOfContents.Parent;

                _logger.LogCritical($"Yug 27 -- Sandy {parent}");
                if (parent != null)
                {
                    SdtBlock sdtBlock = GenerateTableOfContents();

                    _logger.LogCritical($"Yug 28 -- Sandy {sdtBlock}");
                    parent.ReplaceChild(sdtBlock, tableOfContents);

                    _logger.LogCritical($"Yug 29 -- Sandy parent.ReplaceChild(sdtBlock, tableOfContents) {parent}");
                }
            }
        }

        // Creates an SdtBlock instance and adds its children.
        private SdtBlock GenerateTableOfContents()
        {
            SdtBlock sdtBlock1 = new SdtBlock();

            SdtProperties sdtProperties1 = new SdtProperties();
            SdtId sdtId1 = new SdtId() { Val = -1533185764 };

            SdtContentDocPartObject sdtContentDocPartObject1 = new SdtContentDocPartObject();
            DocPartGallery docPartGallery1 = new DocPartGallery() { Val = "Table of Contents" };
            DocPartUnique docPartUnique1 = new DocPartUnique();

            sdtContentDocPartObject1.Append(docPartGallery1);
            sdtContentDocPartObject1.Append(docPartUnique1);

            sdtProperties1.Append(sdtId1);
            sdtProperties1.Append(sdtContentDocPartObject1);

            SdtEndCharProperties sdtEndCharProperties1 = new SdtEndCharProperties();

            RunProperties runProperties1 = new RunProperties();
            RunFonts runFonts1 = new RunFonts() { Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "SimSun", ComplexScript = "Times New Roman" };
            SmallCaps smallCaps1 = new SmallCaps() { Val = false };
            NoProof noProof1 = new NoProof();
            Color color1 = new Color() { Val = "auto" };
            FontSize fontSize1 = new FontSize() { Val = "22" };
            FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "22" };
            Languages languages1 = new Languages() { Val = "en-US", EastAsia = "en-US" };

            runProperties1.Append(runFonts1);
            runProperties1.Append(smallCaps1);
            runProperties1.Append(noProof1);
            runProperties1.Append(color1);
            runProperties1.Append(fontSize1);
            runProperties1.Append(fontSizeComplexScript1);
            runProperties1.Append(languages1);

            sdtEndCharProperties1.Append(runProperties1);

            SdtContentBlock sdtContentBlock1 = new SdtContentBlock();

            Paragraph paragraph1 = new Paragraph() { RsidParagraphAddition = "007843E7", RsidRunAdditionDefault = "007843E7" };

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId1 = new ParagraphStyleId() { Val = "TOCHeading" };

            paragraphProperties1.Append(paragraphStyleId1);

            Run run1 = new Run();
            Text text1 = new Text();
            text1.Text = "Contents";

            run1.Append(text1);
            BookmarkEnd bookmarkEnd1 = new BookmarkEnd() { Id = "0" };

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);
            paragraph1.Append(bookmarkEnd1);

            Paragraph paragraph2 = new Paragraph() { RsidParagraphAddition = "007843E7", RsidRunAdditionDefault = "007843E7" };

            ParagraphProperties paragraphProperties2 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId2 = new ParagraphStyleId() { Val = "TOC1" };

            ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
            RunFonts runFonts2 = new RunFonts() { AsciiTheme = ThemeFontValues.MinorHighAnsi, HighAnsiTheme = ThemeFontValues.MinorHighAnsi, EastAsiaTheme = ThemeFontValues.MinorEastAsia, ComplexScriptTheme = ThemeFontValues.MinorBidi };
            Color color2 = new Color() { Val = "auto" };
            Kern kern1 = new Kern() { Val = (UInt32Value)0U };
            FontSize fontSize2 = new FontSize() { Val = "22" };
            FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "22" };

            paragraphMarkRunProperties1.Append(runFonts2);
            paragraphMarkRunProperties1.Append(color2);
            paragraphMarkRunProperties1.Append(kern1);
            paragraphMarkRunProperties1.Append(fontSize2);
            paragraphMarkRunProperties1.Append(fontSizeComplexScript2);

            paragraphProperties2.Append(paragraphStyleId2);
            paragraphProperties2.Append(paragraphMarkRunProperties1);

            Run run2 = new Run();
            FieldChar fieldChar1 = new FieldChar() { FieldCharType = FieldCharValues.Begin };
            fieldChar1.Dirty = true;

            run2.Append(fieldChar1);

            Run run3 = new Run();
            FieldCode fieldCode1 = new FieldCode() { Space = SpaceProcessingModeValues.Preserve };
            fieldCode1.Text = " TOC \\o \"1-5\" \\h \\z \\u ";

            run3.Append(fieldCode1);

            Run run4 = new Run();
            FieldChar fieldChar2 = new FieldChar() { FieldCharType = FieldCharValues.Separate };

            run4.Append(fieldChar2);

            Hyperlink hyperlink1 = new Hyperlink() { History = true, Anchor = "_Toc368323595" };

            Run run5 = new Run() { RsidRunProperties = "0097188B" };

            RunProperties runProperties2 = new RunProperties();
            RunStyle runStyle1 = new RunStyle() { Val = "Hyperlink" };

            runProperties2.Append(runStyle1);
            Text text2 = new Text();
            text2.Text = "Contents";

            run5.Append(runProperties2);
            run5.Append(text2);

            Run run6 = new Run();

            RunProperties runProperties3 = new RunProperties();
            WebHidden webHidden1 = new WebHidden();

            runProperties3.Append(webHidden1);
            TabChar tabChar1 = new TabChar();

            run6.Append(runProperties3);
            run6.Append(tabChar1);

            Run run7 = new Run();

            RunProperties runProperties4 = new RunProperties();
            WebHidden webHidden2 = new WebHidden();

            runProperties4.Append(webHidden2);
            FieldChar fieldChar3 = new FieldChar() { FieldCharType = FieldCharValues.Begin };

            run7.Append(runProperties4);
            run7.Append(fieldChar3);

            Run run8 = new Run();

            RunProperties runProperties5 = new RunProperties();
            WebHidden webHidden3 = new WebHidden();

            runProperties5.Append(webHidden3);
            FieldCode fieldCode2 = new FieldCode() { Space = SpaceProcessingModeValues.Preserve };
            fieldCode2.Text = " PAGEREF _Toc368323595 \\h ";

            run8.Append(runProperties5);
            run8.Append(fieldCode2);

            Run run9 = new Run();

            RunProperties runProperties6 = new RunProperties();
            WebHidden webHidden4 = new WebHidden();

            runProperties6.Append(webHidden4);

            run9.Append(runProperties6);

            Run run10 = new Run();

            RunProperties runProperties7 = new RunProperties();
            WebHidden webHidden5 = new WebHidden();

            runProperties7.Append(webHidden5);
            FieldChar fieldChar4 = new FieldChar() { FieldCharType = FieldCharValues.Separate };

            run10.Append(runProperties7);
            run10.Append(fieldChar4);

            Run run11 = new Run();

            RunProperties runProperties8 = new RunProperties();
            WebHidden webHidden6 = new WebHidden();

            runProperties8.Append(webHidden6);
            Text text3 = new Text();
            text3.Text = "1";

            run11.Append(runProperties8);
            run11.Append(text3);

            Run run12 = new Run();

            RunProperties runProperties9 = new RunProperties();
            WebHidden webHidden7 = new WebHidden();

            runProperties9.Append(webHidden7);
            FieldChar fieldChar5 = new FieldChar() { FieldCharType = FieldCharValues.End };

            run12.Append(runProperties9);
            run12.Append(fieldChar5);

            hyperlink1.Append(run5);
            hyperlink1.Append(run6);
            hyperlink1.Append(run7);
            hyperlink1.Append(run8);
            hyperlink1.Append(run9);
            hyperlink1.Append(run10);
            hyperlink1.Append(run11);
            hyperlink1.Append(run12);

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(run2);
            paragraph2.Append(run3);
            paragraph2.Append(run4);
            paragraph2.Append(hyperlink1);

            Paragraph paragraph3 = new Paragraph() { RsidParagraphAddition = "007843E7", RsidRunAdditionDefault = "007843E7" };

            Run run13 = new Run();

            RunProperties runProperties10 = new RunProperties();
            Bold bold1 = new Bold();
            BoldComplexScript boldComplexScript1 = new BoldComplexScript();
            NoProof noProof2 = new NoProof();

            runProperties10.Append(bold1);
            runProperties10.Append(boldComplexScript1);
            runProperties10.Append(noProof2);
            FieldChar fieldChar6 = new FieldChar() { FieldCharType = FieldCharValues.End };

            run13.Append(runProperties10);
            run13.Append(fieldChar6);

            paragraph3.Append(run13);

            sdtContentBlock1.Append(paragraph1);
            sdtContentBlock1.Append(paragraph2);
            sdtContentBlock1.Append(paragraph3);

            sdtBlock1.Append(sdtProperties1);
            sdtBlock1.Append(sdtEndCharProperties1);
            sdtBlock1.Append(sdtContentBlock1);
            return sdtBlock1;
        }

        private void SetDirty(MainDocumentPart mainPart)
        {
            _logger.LogCritical($"Yug 76.8 -- Sandy inside the SetDirty {mainPart}");

            DocumentSettingsPart documentSettingsPart = mainPart.GetPartsOfType<DocumentSettingsPart>().FirstOrDefault();
            _logger.LogCritical($"Yug 76.9 -- Sandy inside the SetDirty {documentSettingsPart}");
            if (documentSettingsPart != null)
            {
                _logger.LogCritical($"Yug 76.10 -- Sandy inside the if condition documentSettingsPart is not null");
                documentSettingsPart.Settings.AppendChild(new UpdateFieldsOnOpen() { Val = true });
                _logger.LogCritical($"Yug 76.11 -- Sandy inside the if condition documentSettingsPart is AppendChild");
            }
        }

        private void InsertPictures()
        {
            _logger.LogCritical($"Yug 68 -- Sandy InsertPictures start");
            if (MainDataSet.Tables["Plot"] == null)
            {
                //_logger.LogCritical($"Yug 69 -- Sandy {JsonConvert.SerializeObject(MainDataSet)}");
                return;
            }

            List<Run> tagRuns = _doc.MainDocumentPart.Document.Body.Descendants<Run>().Where(x => x.InnerText != null && x.InnerText.Contains("«")).ToList();

            _logger.LogCritical($"Yug 70 -- Sandy {tagRuns}");

            //Dictionary<string, ImagePart> imageDictionary = new Dictionary<string, ImagePart>(); 
            List<Tuple<string, string, ImagePart>> imageSpecs = new List<Tuple<string, string, ImagePart>>();

            _logger.LogCritical($"Yug 71 -- Sandy {imageSpecs}");

            MainDocumentPart mainPart = _doc.MainDocumentPart;

            _logger.LogCritical($"Yug 72 -- Sandy {mainPart.Uri}");

            foreach (DataRow row in MainDataSet.Tables["Plot"].Rows)
            {
                string imageName = row["Name"].ToString();
                string imagePath = row["Path"].ToString();

                ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

                if (File.Exists(imagePath))
                {
                    try
                    {
                        using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            imagePart.FeedData(stream);
                        }

                        //imageDictionary.Add(imageName, imagePart);
                        imageSpecs.Add(new Tuple<string, string, ImagePart>(imageName, imagePath, imagePart));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical($"Yug 73 -- Sandy {ex.Message}");
                        _logger.LogCritical($"Yug 74 -- Sandy {ex.InnerException}");
                    }

                }
            }

            foreach (Run tagRun in tagRuns)
            {
                if (tagRun.InnerText.Contains("IMAGE-"))
                {
                    //string innerText = tagRun.InnerText;
                    //SearchAndReplace(@"\<(.*?)\>", innerText);

                    string fieldText = tagRun.InnerText.Replace("IMAGE-", "").Replace("«", "").Replace("»", "");

                    if (imageSpecs.Any(x => x.Item1 == fieldText))
                    // if (imageDictionary.ContainsKey(fieldText))
                    {
                        long widthEMU;
                        long heightEMU;
                        GetImageEMU(imageSpecs.First(x => x.Item1 == fieldText).Item2, out widthEMU, out heightEMU);

                        string rela = mainPart.GetIdOfPart(imageSpecs.First(x => x.Item1 == fieldText).Item3);
                        Drawing pic = GetDrawing(_doc, rela, widthEMU, heightEMU);
                        //Drawing pic = GetDrawing(wordprocessingDocument, mainPart.GetIdOfPart(imageDictionary[fieldText]), null, null);// widthEMU, heightEMU);
                        tagRun.Parent.ReplaceChild(new Run(pic), tagRun);
                    }
                    else
                    {
                        tagRun.Parent.ReplaceChild(new Run(new Text("")), tagRun);
                    }
                }
                //Drawing pic = GetDrawing(wordprocessingDocument, mainPart.GetIdOfPart(imagePart));
                //tagRun.Parent.ReplaceChild(new Run(pic), tagRun);
            }
        }

        private void GetImageEMU(string imagePath, out long widthEMU, out long heightEMU)
        {
            var img = new Bitmap(imagePath);

            var widthPx = img.Width;
            var heightPx = img.Height;
            var horzRezDpi = img.HorizontalResolution;
            var vertRezDpi = img.VerticalResolution;
            const int emusPerInch = 914400;
            var widthEmus = (long)(widthPx / horzRezDpi * emusPerInch);
            var heightEmus = (long)(heightPx / vertRezDpi * emusPerInch);
            var maxWidthEmus = (long)(emusPerInch * 7);
            if (widthEmus > maxWidthEmus)
            {
                var ratio = (heightEmus * 1.0m) / widthEmus;
                widthEmus = maxWidthEmus;
                heightEmus = (long)(widthEmus * ratio);
            }

            widthEMU = widthEmus;
            heightEMU = heightEmus;
        }

        private Drawing GetDrawing(WordprocessingDocument wordDoc, string relationshipId, Int64Value imgWidthEMU = null, Int64Value imgHeightEMU = null)
        {
            imgWidthEMU = imgWidthEMU ?? 990000L;
            imgHeightEMU = imgHeightEMU ?? 792000L;
            // Define the reference of the image.
            var element =
                new Drawing(
                    new OXmlWordProc.Inline(
                        //new DW.Extent() { Cx = 990000L, Cy = 792000L },
                        new OXmlWordProc.Extent() { Cx = imgWidthEMU, Cy = imgHeightEMU },
                        new OXmlWordProc.EffectExtent()
                        {
                            LeftEdge = 0L,
                            TopEdge = 0L,
                            RightEdge = 0L,
                            BottomEdge = 0L
                        },
                        new OXmlWordProc.DocProperties()
                        {
                            Id = (UInt32Value)1U,
                            Name = "Picture 1"
                        },
                        new OXmlWordProc.NonVisualGraphicFrameDrawingProperties(
                            new OXmlDrawing.GraphicFrameLocks() { NoChangeAspect = true }),
                        new OXmlDrawing.Graphic(
                            new OXmlDrawing.GraphicData(
                                new OXmlPIC.Picture(
                                    new OXmlPIC.NonVisualPictureProperties(
                                        new OXmlPIC.NonVisualDrawingProperties()
                                        {
                                            Id = (UInt32Value)0U,
                                            Name = "Something.jpg"
                                        },
                                        new OXmlPIC.NonVisualPictureDrawingProperties()),
                                    new OXmlPIC.BlipFill(
                                        new OXmlDrawing.Blip(
                                            new OXmlDrawing.BlipExtensionList(
                                                new OXmlDrawing.BlipExtension()
                                                {
                                                    Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                })
                                            )
                                        {
                                            Embed = relationshipId,
                                            CompressionState =
                                                OXmlDrawing.BlipCompressionValues.Print
                                        },
                                        new OXmlDrawing.Stretch(
                                            new OXmlDrawing.FillRectangle())),
                                    new OXmlPIC.ShapeProperties(
                                        new OXmlDrawing.Transform2D(
                                            new OXmlDrawing.Offset() { X = 0L, Y = 0L },
                                            //new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                            new OXmlDrawing.Extents() { Cx = imgWidthEMU, Cy = imgHeightEMU }),
                                        new OXmlDrawing.PresetGeometry(
                                            new OXmlDrawing.AdjustValueList()
                                            )
                                        { Preset = OXmlDrawing.ShapeTypeValues.Rectangle }))
                                )
                            { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                        )
                    {
                        DistanceFromTop = (UInt32Value)0U,
                        DistanceFromBottom = (UInt32Value)0U,
                        DistanceFromLeft = (UInt32Value)0U,
                        DistanceFromRight = (UInt32Value)0U,
                        EditId = "50D07946"
                    });

            // Append the reference to body, the element should be in a Run.
            //wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
            return element;
        }

        private void ShowChartConfig(string xmlDefinition, string chartFileName)
        {
            _logger.LogCritical($"Yug 168 -- Sandy inside the ShowChartConfig start");
            try
            {
                AddInUtilChartConfig form = new AddInUtilChartConfig(MainDataSet, xmlDefinition, loggerFactory: _loggerFactory);
                _logger.LogCritical($"Yug 169 -- Sandy {form}");


                form.Dispatcher.Invoke(() =>
                {
                    form.ContentRendered += (sender, args) =>
                    {
                        form.Close();
                    };

                    form.SourceInitialized += (sender, args) =>
                    {
                        //form.WindowState = System.Windows.WindowState.Minimized;
                        //form.WindowState = System.Windows.WindowState.Normal;
                        form.ShowInTaskbar = false;

                        form.WindowStartupLocation = WindowStartupLocation.Manual;
                        form.Top = form.Top - 90000;

                        form.Width = 1000;
                        form.Height = 450;

                        form.ChartFormGrid.RowDefinitions[0].MinHeight = 0;
                        form.ChartFormGrid.RowDefinitions[2].MinHeight = 0;
                        form.ChartFormGrid.RowDefinitions[3].MinHeight = 0;
                        form.ChartFormGrid.RowDefinitions[0].Height = new GridLength(0);
                        form.ChartFormGrid.RowDefinitions[2].Height = new GridLength(0);
                        form.ChartFormGrid.RowDefinitions[3].Height = new GridLength(0);

                        form.ChartFormGrid.ColumnDefinitions[0].MinWidth = 0;
                        form.ChartFormGrid.ColumnDefinitions[1].MinWidth = 0;
                        form.ChartFormGrid.ColumnDefinitions[0].Width = new GridLength(0);
                        form.ChartFormGrid.ColumnDefinitions[1].Width = new GridLength(0);
                    };

                    form.Closing += (sender, args) =>
                    {
                        int width = (int)form.ChartFormGrid.ActualWidth;
                        int height = (int)form.ChartFormGrid.ActualHeight;

                        RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
                        bmp.Render(form.ChartFormGrid);

                        PngBitmapEncoder png = new PngBitmapEncoder();
                        png.Frames.Add(BitmapFrame.Create(bmp));

                        using (Stream stm = File.Create(chartFileName))
                        {
                            png.Save(stm);
                        }
                    };

                    form.ShowDialog();
                });
                _logger.LogCritical($"Yug 169.1 -- Sandy from data");
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"Yug 168.1 -- Sandy {exception.Message}");
                _logger.LogCritical($"Yug 168.2 -- Sandy {exception.InnerException}");
            }
            _logger.LogCritical($"Yug 170 -- Sandy ShowChartConfig end");
        }

        private void ReplaceImageObjects(bool enableLog, IEnumerable<ChartInfo> chartInfo)
        {
            List<OXmlWordProc.Inline> tagShapes = _doc.MainDocumentPart.Document.Body.Descendants<OXmlWordProc.Inline>().ToList();

            _logger.LogCritical($"Yug 75 -- Sandy {tagShapes}");

            foreach (var header in _doc.MainDocumentPart.HeaderParts)
            {
                _logger.LogCritical($"Yug 108 -- Sandy Inside the foreach loop of header start {header}");
                tagShapes.AddRange(header.Header.Descendants<OXmlWordProc.Inline>());

                _logger.LogCritical($"Yug 109 -- Sandy Inside the foreach loop of header end {header}");
            }
            _logger.LogCritical($"Yug 110 -- Sandy Outside the foreach loop of header");
            foreach (var footer in _doc.MainDocumentPart.FooterParts)
            {
                _logger.LogCritical($"Yug 111 -- Sandy Inside the foreach loop start of footer {footer}");
                tagShapes.AddRange(footer.Footer.Descendants<OXmlWordProc.Inline>());
                _logger.LogCritical($"Yug 112 -- Sandy Inside the foreach loop end of footer {tagShapes}");
            }
            _logger.LogCritical($"Yug 113 -- Sandy Outside the foreach loop of footer");

            bool deleteImageObject = true;

            foreach (OXmlWordProc.Inline tagShape in tagShapes)
            {
                _logger.LogCritical($"Yug 114 -- Sandyinside the foreach loop of OXmlWordProc.Inline");
                deleteImageObject = true;

                string title = tagShape.DocProperties.Title;
                _logger.LogCritical($"Yug 115 -- Sandy {title}");
                if (!string.IsNullOrEmpty(title))
                {
                    _logger.LogCritical($"Yug 116 -- Sandy iside the if condition !string.IsNullOrEmpty(title)");
                    if (title.StartsWith("SA:"))
                    {
                        _logger.LogCritical($"Yug 117 -- Sandy iside the if condition !string.IsNullOrEmpty(title)");
                        string field = title.Right("-").Right(":");
                        string tableName = field.Left("~");
                        string labelName = field.Right("~");
                        DataTable tbl = MainDataSet.GetDataTable(tableName);
                        _logger.LogCritical($"Yug 118 -- Sandy {tbl}");
                        if (tbl != null)
                        {
                            _logger.LogCritical($"Yug 119 -- Sandy inside if condition tbl is not null");
                            if (tbl.Columns.Contains(labelName) && !string.IsNullOrEmpty(tbl.Columns[labelName].Namespace)
                                && string.IsNullOrEmpty(tbl.Columns[labelName].Expression))
                            {
                                _logger.LogCritical($"Yug 120 -- Sandy iside the if condition start");
                                tableName = tbl.Columns[labelName].Namespace;
                                _logger.LogCritical($"Yug 121 -- Sandy iside the if condition end{tableName}");
                            }

                            if (field.IndexOf('.') > 0)
                            {
                                _logger.LogCritical($"Yug 122 -- Sandy iside the if condition field.IndexOf('.') > 0 start");
                                labelName = field.Substring(field.Length - field.Reverse().IndexOf('.'));
                                _logger.LogCritical($"Yug 123 -- Sandy iside the if condition{labelName}");
                            }
                        }
                        _logger.LogCritical($"Yug 124 -- Sandy close");

                        tbl = MainDataSet.GetDataTable(tableName);
                        _logger.LogCritical($"Yug 125 -- Sandy {tbl}");
                        if (tbl != null && (tbl.Rows.Count > 0) && tbl.Columns.Contains(labelName))
                        {
                            _logger.LogCritical($"Yug 126 -- Sandy iside the if condition (tbl != null && (tbl.Rows.Count > 0) && tbl.Columns.Contains(labelName)");
                            string standAlone = tbl.Rows[0].Get<string>(labelName, enableLog);
                            _logger.LogCritical($"Yug 127 -- Sandy {standAlone}");
                            if (!string.IsNullOrEmpty(standAlone))
                            {
                                _logger.LogCritical($"Yug 128 -- Sandy iside the if condition (!string.IsNullOrEmpty(standAlone)");
                                if (File.Exists(standAlone))
                                {
                                    _logger.LogCritical($"Yug 129 -- Sandy iside the if condition (File.Exists(standAlone))");
                                    OXmlDrawing.Blip blip = tagShape.Descendants<OXmlDrawing.Blip>().FirstOrDefault();
                                    _logger.LogCritical($"Yug 132 -- Sandy {blip}");
                                    ScaleImage(new FileInfo(standAlone), tagShape);
                                    _logger.LogCritical($"Yug 133 -- Sandy close");
                                    ReplaceImage(blip, new FileInfo(standAlone), enableLog);
                                    _logger.LogCritical($"Yug 137 -- Sandy ReplaceImage method end");
                                    deleteImageObject = false;

                                }
                            }
                        }
                    }
                    else
                    {
                        _logger.LogCritical($"Yug 138 -- Sandy start else block");

                        if (title.StartsWith("CHART:"))
                        {
                            _logger.LogCritical($"Yug 139 -- Sandy inside the (title.StartsWith(\"CHART:\")");

                            bool isChartReplaced = false;

                            foreach (var reportDocProperty in _reportDocPropertyList)
                            {
                                _logger.LogCritical($"Yug 140 -- Sandy inside the foreach loop of reportDocProperty");

                                foreach (XmlNode xNodeReportDoc in reportDocProperty)
                                {
                                    _logger.LogCritical($"Yug 141 -- Sandy inside the foreach loop of xNodeReportDoc");
                                    if (xNodeReportDoc.Name.ToUpper().Contains("CHARTS"))
                                    {
                                        _logger.LogCritical($"Yug 142 -- Sandy inside the if condition of xNodeReportDoc.Name.ToUpper().Contains(\"CHARTS\")");
                                        ObservableCollection<MyKeyValuePair> charts = XMLHelper.DeserializeClass<ObservableCollection<MyKeyValuePair>>(xNodeReportDoc.InnerText);
                                        _logger.LogCritical($"Yug 143 -- Sandy {charts}");
                                        OverrideDefaultSettinga(charts, chartInfo);
                                        _logger.LogCritical($"Yug 159 -- Sandy come back the OverrideDefaultSettinga");
                                        foreach (var chart in charts)
                                        {
                                            _logger.LogCritical($"Yug 160 -- Sandy inside the foreach loop of chart");
                                            try
                                            {
                                                _logger.LogCritical($"Yug 161 -- Sandy inside the try block");
                                                string chartName = title.Replace("CHART:", "");
                                                _logger.LogCritical($"Yug 162 -- Sandy {chartName}");
                                                if (chart.Key == chartName)
                                                {
                                                    _logger.LogCritical($"Yug 163 -- Sandy inside the foreach loop of chart");
                                                    string chartFileName = string.Format("{0}{1}{2}.png", System.IO.Path.GetTempPath(), chartName, new Random().Next(0, 10000).ToString(CultureInfo.InvariantCulture));
                                                    _logger.LogCritical($"Yug 164 -- Sandy {chartFileName}");
                                                    File.Delete(chartFileName);

                                                    if (Application.Current != null)
                                                    {
                                                        _logger.LogCritical($"Yug 165 -- Sandy inside the if condition Application.Current != null");
                                                        Application.Current.Dispatcher.Invoke(() => ShowChartConfig(chart.Value, chartFileName));
                                                        _logger.LogCritical($"Yug 166 -- Sandy inside the if condition Application.Current != null end");

                                                    }
                                                    else
                                                    {
                                                        _logger.LogCritical($"Yug 167 -- Sandy inside the Else block");
                                                        ShowChartConfig(chart.Value, chartFileName);
                                                        //Thread newWindowThread = new Thread(() => ShowChartConfig(chart.Value, chartFileName));
                                                        //newWindowThread.SetApartmentState(ApartmentState.STA);
                                                        //newWindowThread.IsBackground = true;
                                                        //newWindowThread.Start();
                                                        //newWindowThread.Join();
                                                        _logger.LogCritical($"Yug 171 -- Sandy else block end");

                                                    }

                                                    //TODO: put some type of fail safe here!
                                                    while (!File.Exists(chartFileName))
                                                    {
                                                        _logger.LogCritical($"Yug 172 -- Sandy while conduction (!File.Exists(chartFileName))");

                                                        Thread.Sleep(200);
                                                    }
                                                    _logger.LogCritical($"Yug 173 -- Sandy while conduction (!File.Exists(chartFileName)) end");
                                                    if (File.Exists(chartFileName))
                                                    {
                                                        _logger.LogCritical($"Yug 174 -- Sandy inside the if condition (File.Exists(chartFileName)");
                                                        OXmlDrawing.Blip blip = tagShape.Descendants<OXmlDrawing.Blip>().FirstOrDefault();

                                                        FileInfo chartFileInfo = new FileInfo(chartFileName);
                                                        _logger.LogCritical($"Yug 175 -- Sandy {chartFileInfo}");
                                                        ScaleImage(chartFileInfo, tagShape);

                                                        ReplaceImage(blip, chartFileInfo, enableLog);
                                                        deleteImageObject = false;

                                                        File.Delete(chartFileName);
                                                    }

                                                    isChartReplaced = true;
                                                    _logger.LogCritical($"Yug 176 -- Sandy break the loop");

                                                    break;
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                _logger.LogCritical($"Yug 177 -- Sandy {exception.Message}");
                                                _logger.LogCritical($"Yug 178 -- Sandy {exception.InnerException}");
                                                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                                            }

                                        }
                                    }
                                }

                                if (isChartReplaced)
                                {
                                    _logger.LogCritical($"Yug 179 -- Sandy in if condition isChartReplaced is true break the loop");
                                    break;
                                }
                            }

                        }
                        else
                        {
                            _logger.LogCritical($"Yug 180 -- Sandy inside else block");
                            DataTable plots = MainDataSet.GetDataTable("Plot");
                            _logger.LogCritical($"Yug 181 -- Sandy {plots}");

                            if (plots != null)
                            {
                                _logger.LogCritical($"Yug 182 -- Sandy inside if condition plots is not null");

                                DataRow[] plotRow = plots.Select("Name = '" + title.Trim() + "'");
                                _logger.LogCritical($"Yug 183 -- Sandy {plotRow}");

                                if ((plotRow.GetUpperBound(0) > -1))
                                {
                                    _logger.LogCritical($"Yug 184 -- Sandy inside else block");

                                    FileInfo fileInfo = new FileInfo(plotRow[0]["path"].ToString());
                                    _logger.LogCritical($"Yug 185 -- Sandy {fileInfo}");

                                    if (fileInfo.Exists)
                                    {
                                        _logger.LogCritical($"Yug 186 -- Sandy inside the if condition (fileInfo.Exists)");

                                        OXmlDrawing.Blip blip = tagShape.Descendants<OXmlDrawing.Blip>().FirstOrDefault();
                                        ScaleImage(fileInfo, tagShape);
                                        ReplaceImage(blip, fileInfo, enableLog);
                                        deleteImageObject = false;
                                        _logger.LogCritical($"Yug 187 -- Sandy inside the if condition (fileInfo.Exists) end");

                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    deleteImageObject = false;
                    _logger.LogCritical($"Yug 188 -- Sandy inside else condition deleteImageObject = false");

                }

                if (deleteImageObject)
                {
                    _logger.LogCritical($"Yug 189 -- Sandy inside the if condition deleteImageObject is true start");

                    tagShape.Parent.Parent.Parent.RemoveChild(tagShape.Parent.Parent);
                    _logger.LogCritical($"Yug 190 -- Sandy inside the if condition deleteImageObject is true end");

                }
            }
        }

        void OverrideDefaultSettinga(ObservableCollection<MyKeyValuePair> chartConfigSettings, IEnumerable<ChartInfo> chartInfo)
        {
            _logger.LogCritical($"Yug 144 -- Sandy {chartConfigSettings}");
            _logger.LogCritical($"Yug 145 -- Sandy {chartInfo}");
            _logger.LogCritical($"Yug 146 -- Sandy inside OverrideDefaultSettinga method start");
            if (chartInfo != null && chartInfo.Any())
            {
                _logger.LogCritical($"Yug 147 -- Sandy inside the if condition (chartInfo != null && chartInfo.Any()");
                foreach (var chartConfig in chartConfigSettings)
                {
                    _logger.LogCritical($"Yug 148 -- Sandy inside the foreach loop of chartConfig");
                    var chartValueToOverride = chartInfo.FirstOrDefault(i =>
                        i.ChartName.Equals(chartConfig.Key));
                    _logger.LogCritical($"Yug 149 -- Sandy {chartValueToOverride}");
                    if (chartValueToOverride != null)
                    {
                        _logger.LogCritical($"Yug 150 -- Sandy inside the if condition chartValueToOverride is not null");
                        var configXml = chartConfig.Value;
                        _logger.LogCritical($"Yug 151 -- Sandy {configXml}");

                        var configDocument = XDocument.Parse(configXml);
                        _logger.LogCritical($"Yug 152 -- Sandy {configDocument}");


                        UpdateNodeValue(configDocument, "yaxismin", chartValueToOverride.YAxisMin);
                        _logger.LogCritical($"Yug 153 -- Sandy yaxismin");

                        UpdateNodeValue(configDocument, "yaxismax", chartValueToOverride.YAxisMax);
                        _logger.LogCritical($"Yug 154 -- Sandy yaxismax");

                        UpdateNodeValue(configDocument, "minimumvalue", chartValueToOverride.XAxisMin);
                        _logger.LogCritical($"Yug 155 -- Sandy minimumvalue");

                        UpdateNodeValue(configDocument, "maximumvalue", chartValueToOverride.XAxisMax);
                        _logger.LogCritical($"Yug 156 -- Sandy maximumvalue");

                        UpdateNodeValue(configDocument, "xaxisintervalnumber", chartValueToOverride.XAxisIntervalNumber);
                        _logger.LogCritical($"Yug 157 -- Sandy xaxisintervalnumber");

                        chartConfig.Value = configDocument.ToString();
                        _logger.LogCritical($"Yug 158 -- Sandy {chartConfig.Value}");

                    }
                }
            }
        }

        private static void UpdateNodeValue(XDocument configDocument, string name, double? value)
        {
            var element = configDocument.Descendants().FirstOrDefault(
                e => e.Name.ToString().ToLower().Contains(name));

            if (element != null && !string.IsNullOrEmpty(element.Name.ToString()))
            {
                if (value.HasValue)
                {
                    if (element.FirstAttribute != null && element.FirstAttribute.Name != null && element.FirstAttribute.Name.LocalName.Equals("nil"))
                    {
                        element.FirstAttribute.Value = "false";
                    }
                    element.Value = Convert.ToString(value);
                }
                else
                {
                    element.Remove();
                }
            }
        }

        private void ScaleImage(FileInfo fileInfo, OXmlWordProc.Inline tagShape)
        {
            _logger.LogCritical($"Yug 130 -- Sandy iside the ScaleImage method start");
            System.Drawing.Size imageSize = GetImageSize(fileInfo);

            decimal lengthRatio = (tagShape.Extent.Cx / PIXEL_TO_EMU) / imageSize.Width;

            tagShape.Extent.Cy = (int)(imageSize.Height * PIXEL_TO_EMU * lengthRatio);

            var transform2D = tagShape.Descendants<DocumentFormat.OpenXml.Drawing.Transform2D>().FirstOrDefault();
            transform2D.Extents.Cy = tagShape.Extent.Cy;
            transform2D.Extents.Cx = tagShape.Extent.Cx;
            _logger.LogCritical($"Yug 131 -- Sandy iside the ScaleImage method end");
        }

        private System.Drawing.Size GetImageSize(FileInfo imageFile)
        {
            var uri = new Uri(imageFile.FullName);

            BitmapDecoder bitmapDecoder = null;
            switch (imageFile.Extension.ToLower())
            {
                case ".png":
                    bitmapDecoder = new PngBitmapDecoder(uri, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    break;
                case ".jpg":
                case ".jpeg":
                    bitmapDecoder = new JpegBitmapDecoder(uri, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    break;
                case ".tif":
                    bitmapDecoder = new TiffBitmapDecoder(uri, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    break;
                case ".bmp":
                    bitmapDecoder = new BmpBitmapDecoder(uri, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    break;
            }

            if (bitmapDecoder.Frames.Count == 0)
            {
                return new System.Drawing.Size();
            }

            System.Drawing.Size returnSize = new System.Drawing.Size(bitmapDecoder.Frames[0].PixelWidth, bitmapDecoder.Frames[0].PixelHeight);

            bitmapDecoder = null;

            return returnSize;
        }

        /// <summary>
        /// To replace an image in a document
        /// 1. Add an ImagePart to the document.
        /// 2. Edit the Blip element to refer to the new ImagePart.
        /// </summary>
        /// <param name="blipElement">Open XML Drawing Blip</param>
        /// <param name="newImg">File Info for the new Image to replace</param>
        /// <param name="enableLog"></param>
        private void ReplaceImage(OXmlDrawing.Blip blipElement, FileInfo newImg, bool enableLog)
        {
            _logger.LogCritical($"Yug 134 -- Sandy iside the ScaleImage method");
            string rid = AddImagePart(blipElement, newImg, enableLog);
            _logger.LogCritical($"Yug 135 -- Sandy iside the ScaleImage {rid}");
            blipElement.Embed.Value = rid;
            _logger.LogCritical($"Yug 136 -- Sandy ");
        }

        /// <summary>
        /// Add ImagePart to the document.
        /// </summary>
        private string AddImagePart(OXmlDrawing.Blip blipElement, FileInfo newImg, bool enableLog)
        {
            ImagePartType type;
            switch (newImg.Extension.ToLower())
            {
                case ".jpeg":
                case ".jpg":
                    type = ImagePartType.Jpeg;
                    break;
                case ".png":
                    type = ImagePartType.Png;
                    break;
                default:
                    type = ImagePartType.Bmp;
                    break;
            }

            ImagePart newImgPart = null;
            string rId = null;

            try
            {
                var element = blipElement.Ancestors<OpenXmlElement>().ToList().FindLast(x => x.InnerText != null);

                if (element.IsOfType<Header>())
                {
                    newImgPart = ((Header)element).HeaderPart.AddImagePart(type);
                    using (FileStream stream = newImg.OpenRead())
                    {
                        newImgPart.FeedData(stream);
                    }
                    rId = ((Header)element).HeaderPart.GetIdOfPart(newImgPart);
                }
                else if (element.IsOfType<Footer>())
                {
                    newImgPart = ((Footer)element).FooterPart.AddImagePart(type);
                    using (FileStream stream = newImg.OpenRead())
                    {
                        newImgPart.FeedData(stream);
                    }
                    rId = ((Footer)element).FooterPart.GetIdOfPart(newImgPart);
                }
                else
                {
                    newImgPart = _doc.MainDocumentPart.AddImagePart(type);
                    using (FileStream stream = newImg.OpenRead())
                    {
                        newImgPart.FeedData(stream);
                    }
                    rId = _doc.MainDocumentPart.GetIdOfPart(newImgPart);
                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                AddinBase.ErrorMessages.Add(exception.Message);
            }

            return rId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        public static Dictionary<string, ChartProperties> GetChartSettingsFromTemplate(string templatePath)
        {
            var result = new Dictionary<string, ChartProperties>();
            using (var doc = WordprocessingDocument.Open(templatePath, true))
            {
                var reportPropertyList = GetXmparts(doc, false);
                var tagShapes = doc.MainDocumentPart.Document.Body.Descendants<OXmlWordProc.Inline>().ToList();
                foreach (OXmlWordProc.Inline tagShape in tagShapes)
                {
                    string title = tagShape.DocProperties.Title;
                    if (!string.IsNullOrWhiteSpace(title) && title.StartsWith("CHART:"))
                    {
                        foreach (var reportDocProperty in reportPropertyList)
                        {
                            foreach (XmlNode xNodeReportDoc in reportDocProperty)
                            {
                                if (xNodeReportDoc.Name.ToUpper().Contains("CHARTS"))
                                {
                                    var charts = XMLHelper.DeserializeClass<List<MyKeyValuePair>>(xNodeReportDoc
                                        .InnerText);

                                    foreach (var chart in charts)
                                    {
                                        var chartSettings = XMLHelper.DeserializeClass<ChartProperties>(chart.Value);
                                        result[chart.Key] = chartSettings;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        static List<XmlNodeList> GetXmparts(WordprocessingDocument doc, bool enableLog)
        {
            var reportPropertyList = new List<XmlNodeList>();
            using (var util = new OpenXmlAddinBase(doc))
            {
                List<Tuple<string, string, string>> allProperties = util.ReadAllCustomProperties(enableLog);

                if (allProperties != new List<Tuple<string, string, string>>())
                {
                    Tuple<string, string, string> reportDocProperty = allProperties.Find(x => x.Item1.ToUpper() == "REPORTDOC");

                    if (reportDocProperty != null)
                    {
                        var reportDocCustomProperties = AddinBase.DeserializeClass<ReportDocProperty>(reportDocProperty.Item3);

                        var xml = new XmlDocument();
                        XMLHelper.LoadXmlSafely(xml, reportDocCustomProperties.Value, enableLog);
                        reportPropertyList.Clear();

                        foreach (XmlNode xNode in xml.ChildNodes)
                        {
                            if (xNode.Name != "ReportDoc")
                            {
                                continue;
                            }

                            reportPropertyList.Add(xNode.ChildNodes);
                        }
                    }
                }
            }
            return reportPropertyList;
        }
        private static List<QueryConditionalExpressions> ProcessXMLParts(WordprocessingDocument doc, bool enableLog)
        {
            var reportDoList = new List<XmlNodeList>();
            var conditionsExpressions = new List<QueryConditionalExpressions>();

            //<Populate reportDoList>
            using (var util = new OpenXmlAddinBase(doc))
            {
                List<Tuple<string, string, string>> allProperties = util.ReadAllCustomProperties(enableLog);

                if (allProperties != new List<Tuple<string, string, string>>())
                {
                    Tuple<string, string, string> reportDocProperty = allProperties.Find(x => x.Item1.ToUpper() == "REPORTDOC");

                    if (reportDocProperty != null)
                    {
                        var reportDocCustomProperties = OpenXmlAddinBase.DeserializeClass<ReportDocProperty>(reportDocProperty.Item3);

                        var xml = new XmlDocument();
                        XMLHelper.LoadXmlSafely(xml, reportDocCustomProperties.Value, enableLog);

                        reportDoList.AddRange(from XmlNode xNode in xml.ChildNodes where xNode.Name == "ReportDoc" select xNode.ChildNodes);
                    }
                }
            }
            //</Populate reportDoList>

            foreach (var xNodeReportDoc in reportDoList.SelectMany(reportDocProperty => reportDocProperty.Cast<XmlNode>()))
            {
                if (xNodeReportDoc.Name.ToUpper().Contains("TABLEJOIN"))
                {
                    tableJoins = AddinBase.DeserializeClass<List<TableJoin>>(xNodeReportDoc.InnerText);
                }
                if (xNodeReportDoc.Name.ToUpper().Contains("TABLEGROUP"))
                {
                    tableGroups = AddinBase.DeserializeClass<List<TableGrp>>(xNodeReportDoc.InnerText);
                }

                if (xNodeReportDoc.Name.ToUpper().Contains("CONDITION"))
                {
                    var conditions = AddinBase.DeserializeClass<List<Condition>>(xNodeReportDoc.InnerText);
                    foreach (var condition in conditions.Where(condition => conditionsExpressions.FirstOrDefault(x => x.ConditionalExpression == condition.ConditionDescription && x.ExpressionType == "Condition") == null))
                    {
                        conditionsExpressions.Add(new QueryConditionalExpressions()
                        {
                            TableName = condition.Table,
                            ExpressionName = condition.Name,
                            ConditionalExpression = condition.ConditionDescription,
                            ExpressionType = "Condition"
                        });
                    }
                }
                if (xNodeReportDoc.Name.ToUpper().Contains("TABLEFORMULA"))
                {
                    var tableFormulas = AddinBase.DeserializeClass<List<TableFormula>>(xNodeReportDoc.InnerText);
                    foreach (var formula in tableFormulas.Where(formula => conditionsExpressions.FirstOrDefault(x => x.ConditionalExpression == formula.Formula && x.ExpressionType == "Formula") == null))
                    {
                        conditionsExpressions.Add(new QueryConditionalExpressions()
                        {
                            TableName = formula.Table,
                            ExpressionName = formula.FormulaName,
                            ConditionalExpression = formula.Formula,
                            ExpressionType = "Formula"
                        });
                    }
                }
            }
            return conditionsExpressions;
        }
        private static string FindParent(string tableTitle, ref string columnName)
        {
            tableTitle = FindParentJoin(tableTitle, ref columnName);
            tableTitle = FindParentGroup(tableTitle, ref columnName);
            return tableTitle;
        }
        private static string FindParentJoin(string tableTitle, ref string columnName)
        {

            var availableTables = tableJoins.FirstOrDefault(x => x.TableName == tableTitle);

            if (availableTables != null && (columnName.StartsWith("t1.") || columnName.StartsWith("t2.")))
            {
                if (columnName.StartsWith("t1."))
                {
                    columnName = columnName.Remove(0, 3);
                    tableTitle = availableTables.ATableName;
                }
                if (columnName.StartsWith("t2."))
                {
                    columnName = columnName.Remove(0, 3);
                    tableTitle = availableTables.BTableName;
                }
                tableTitle = FindParentJoin(tableTitle, ref columnName);
            }
            return tableTitle;
        }
        private static string FindParentGroup(string tableTitle, ref string columnName)
        {

            var availableTables = tableGroups.FirstOrDefault(x => x.TableName == tableTitle);

            if (availableTables != null)
            {
                columnName = columnName.Remove(0, 3);
                tableTitle = availableTables.ATableName;

                tableTitle = FindParentGroup(tableTitle, ref columnName);
            }
            return tableTitle;
        }

        /// <summary>
        /// Since MS Word 2010 the SimpleField element is not longer used. It has been replaced by a combination of
        /// Run elements and a FieldCode element. This method will convert the new format to the old SimpleField-compliant 
        /// format.
        /// </summary>
        /// <param name="mainElement">OpenXML Element</param>
        /// <remarks>It has been replaced by a combination of
        /// Run elements and a FieldCode element. This method will convert the new format to the old SimpleField-compliant 
        /// format.</remarks>
        private static bool ConvertFieldCodes(OpenXmlElement mainElement)
        {
            bool setToDirty = false;
            SdtBlock[] sdtBlocks = mainElement.Descendants<SdtBlock>().ToArray();
            var sdtBlockRuns = sdtBlocks.Length == 0 ? null : sdtBlocks.FirstOrDefault().Descendants<Run>().ToList();

            //  search for all the Run elements 
            List<Run> runs = mainElement.Descendants<Run>().ToList();
            if (runs.Count == 0) return false;

            if (sdtBlockRuns != null && sdtBlockRuns.Count != 0)
            {
                /*<Remove SdtBlockTOC runs>*/
                int startRunIndex = 0;
                int endRunIndex = 0;
                int tocRunIndex = 0;
                bool inToc = false;
                for (int index = 0; index < runs.Count; index++)
                {
                    if (runs[index] == sdtBlockRuns[tocRunIndex])
                    {
                        if (!inToc)
                        {
                            startRunIndex = index;
                        }
                        else if (index - startRunIndex == sdtBlockRuns.Count - 1)
                        {
                            endRunIndex = index;
                            break;
                        }
                        inToc = true;
                        ++tocRunIndex;
                    }
                    else
                    {
                        startRunIndex = 0;
                        inToc = false;
                        tocRunIndex = 0;
                    }

                }

                if (endRunIndex - startRunIndex == sdtBlockRuns.Count - 1)
                {
                    runs.RemoveRange(startRunIndex, sdtBlockRuns.Count);
                    setToDirty = true;
                }
                /*</Remove SdtBlockTOC runs>*/
            }

            Dictionary<Run, Run[]> newfields = new Dictionary<Run, Run[]>();

            int cursor = 0;
            do
            {
                Run run = runs[cursor];

                if (run.HasChildren && run.Descendants<FieldChar>().Any()
                    && (run.Descendants<FieldChar>().First().FieldCharType & FieldCharValues.Begin) == FieldCharValues.Begin)
                {
                    List<Run> innerRuns = new List<Run>();
                    innerRuns.Add(run);

                    //  loop until we find the 'end' FieldChar
                    bool found = false;
                    string instruction = null;
                    RunProperties runprop = null;
                    do
                    {
                        cursor++;
                        if (cursor >= runs.Count)
                            break;

                        run = runs[cursor];

                        innerRuns.Add(run);
                        if (run.HasChildren && run.Descendants<FieldCode>().Any())
                            instruction += run.GetFirstChild<FieldCode>().Text;
                        if (run.HasChildren && run.Descendants<FieldChar>().Any()
                            && (run.Descendants<FieldChar>().First().FieldCharType & FieldCharValues.End) == FieldCharValues.End)
                        {
                            found = true;
                        }
                        if (run.HasChildren && run.Descendants<RunProperties>().Any())
                            runprop = run.GetFirstChild<RunProperties>();

                    } while (found == false && cursor < runs.Count);

                    /* Removed because of an issue found when Table of Contents is in the file.  Throughs an error above when cursor goes past runs length */
                    //  something went wrong : found Begin but no End. Throw exception
                    //if (!found)
                    //    throw new Exception("Found a Begin FieldChar but no End !");

                    if (!string.IsNullOrEmpty(instruction) && instruction.Contains("MERGEFIELD"))
                    {
                        //  build new Run containing a SimpleField
                        Run newrun = new Run();
                        if (runprop != null)
                            newrun.AppendChild(runprop.CloneNode(true));
                        SimpleField simplefield = new SimpleField();
                        simplefield.Instruction = instruction;


                        if (innerRuns.Any(x => x.InnerText.Contains("«")))
                        {
                            simplefield.AppendChild((Run)innerRuns.First(x => x.InnerText.Contains("«")).Clone());
                        }

                        newrun.AppendChild(simplefield);

                        newfields.Add(newrun, innerRuns.ToArray());
                    }
                }

                cursor++;
            } while (cursor < runs.Count);

            //  replace all FieldCodes by old-style SimpleFields
            foreach (KeyValuePair<Run, Run[]> kvp in newfields)
            {
                int length = kvp.Value.Length;
                kvp.Value[0].Parent.ReplaceChild(kvp.Key, kvp.Value[0]);
                for (int i = 1; i < length; i++)
                    if (kvp.Value[i].Parent != null)
                        kvp.Value[i].Remove();
            }

            return setToDirty;
        }


        private static QuerySubReport GetSubReportFromName(string columnName)
        {
            var querySubreport = new QuerySubReport();
            string name = columnName.Replace("SUBREPORT:", string.Empty).Trim();

            foreach (var reportDocProperty in _reportDocPropertyList)
            {
                foreach (XmlNode xNodeReportDoc in reportDocProperty)
                {
                    if (xNodeReportDoc.Name != "Subreport")
                    {
                        continue;
                    }

                    var subreports = OpenXmlAddinBase.DeserializeClass<List<Subreport>>(xNodeReportDoc.InnerText);

                    Subreport subreport = subreports.FirstOrDefault(x => x.SubreportName == name);

                    if (subreport != null)
                    {
                        querySubreport.Namespace = subreport.SubreportNamespace;
                        querySubreport.ReportName = subreport.SubreportName;

                        return querySubreport;
                    }
                }
            }
            return null;
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

