using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Interface
{
    public interface IMxSReportGenerator : IDisposable
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
        /// HalReport.FileFormatType - Exported File Format Type
        /// </summary>
        /// <remarks>Exported File Format Type</remarks>
        /// <returns>HalReport.FileFormatType (Enum)</returns>
       MxSFileFormatType FileFormatType { get; set; }

        /// <summary>
        /// SaveLocation - Location/Path of the Saved Document
        /// </summary>
        /// <remarks>Location/Path of the Saved Document</remarks>
        /// <returns>string</returns>
        string SaveLocation { get; set; }

        /// <summary>
        /// ReturnReport - Returns a HalReport with ResultMessage, ErrorMessage, ReturnUri
        /// </summary>
        /// <remarks>Returns a HalReport with ResultMessage, ErrorMessage, ReturnUri</remarks>
        /// <returns>HalReport</returns>
        HalReport ReturnReport { get; set; }

        /// <summary>
        /// IsDocumentProtected - Boolean Operator indicating if the current document is to be Locked from edits
        /// </summary>
        /// <remarks>Boolean Operator indicating if the current document is to be Locked from edits</remarks>
        /// <returns>Boolean</returns>
        Boolean IsDocumentProtected { get; set; }

        /// <summary>
        /// DocumentProtectedPassword - Password used to protect the current document from edits
        /// </summary>
        /// <remarks>Password used to protect the current document from edits</remarks>
        /// <returns>string</returns>
        string DocumentProtectedPassword { get; set; }

        #endregion

        // ************************************ CONSTRUCTORS *************************************
        #region "Constructors"
        #endregion

        // ************************************ DESTRUCTORS **************************************
        #region "Destructors"
        #endregion

        // *********************************** PUBLIC METHODS ************************************
        #region "Public Methods"

        /// <summary>
        /// CreateReport Method - Creates the report using the provided XML Data file
        /// </summary>
        /// <param name="xmlDataFile">String - Full Path and File name of XML File containing report data</param>
        /// <param name="chartParameters"></param>
        /// <remarks>Creates the report using the provided XML Data file</remarks>
        /// <returns>HalReport</returns>
        HalReport CreateReport(string xmlDataFile, IEnumerable<ChartInfo> chartParameters);

        /// <summary>
        /// CreateReport Method - Creates the report using the provided XML Data file
        /// </summary>
        /// <param name="xmlDataFile">String - Full Path and File name of XML File containing report data</param>
        /// <param name="enableLog"></param>
        /// <param name="chartInfo"></param>
        /// <remarks>Creates the report using the provided XML Data file</remarks>
        /// <returns>HalReport</returns>
        HalReport CreateReport(string xmlDataFile, bool enableLog, IEnumerable<ChartInfo> chartInfo);

        /// <summary>
        /// CreateReport Method - Create the report using the provided DataSet
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="chartInfo"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>Create the report using the provided DataSet</remarks>
        /// <returns>HalReport</returns>
        HalReport CreateReport(DataSet dataSet, IEnumerable<ChartInfo> chartInfo);


        /// <summary>
        /// CreateReport Method - Create the report using the provided DataSet
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="enableLog"></param>
        /// <param name="chartInfo"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>Create the report using the provided DataSet</remarks>
        /// <returns>HalReport</returns>
        HalReport CreateReport(DataSet dataSet, bool enableLog, IEnumerable<ChartInfo> chartInfo);

        // /// <summary>
        // /// CreateReport Method - Create the report using the provided DataTable
        // /// <para>The Columns names in the DataTable must match the Field Names</para>
        // /// </summary>
        // /// <param name="dataTable">DataTable - DataTable containing the Field values to use in the report</param>
        // /// <remarks>Create the report using the provided DataTable</remarks>
        // /// <returns>HalReport</returns>
        // HalReport CreateReport(DataTable dataTable);

        // /// <summary>
        // /// CreateReport Method - Create the report using the provided DataView
        // /// <para>The Columns names in the DataView must match the Field Names</para>
        // /// </summary>
        // /// <param name="dataView">DataView - DataView containing the Field values to use in the report</param>
        // /// <remarks>Create the report using the provided DataView</remarks>
        // /// <returns>HalReport</returns>
        // HalReport CreateReport(DataView dataView);

        // /// <summary>
        // /// CreateReport Method - Create the report using the provided IDataReader
        // /// <para>The Columns names in the IDataReader must match the Field Names</para>
        // /// </summary>
        // /// <param name="dataReader">IDataReader - IDataReader containing the Field values to use in the report</param>
        // /// <remarks>Create the report using the provided IDataReader</remarks>
        // /// <returns>HalReport</returns>
        // HalReport CreateReport(IDataReader dataReader);


        /// <summary>
        /// RenderControlAsImage - Captures an snapshot of the specified Control
        /// </summary>
        /// <param name="ctrl">ctrl - Control to be Rendered</param>
        /// <returns>Image</returns>
        /// <remarks>Captures an snapshot of the specified Control</remarks>
        Image RenderControlAsImage(System.Windows.Forms.Control ctrl);

        /// <summary>
        /// RenderFormAsImage Method - Captures an snapshot of the specified Form
        /// </summary>
        /// <param name="frm">Form - Form to be Rendered</param>
        /// <param name="clientAreaOnly">Bool - Flag indicating whether or not to render just the Client Area or the Entire Form</param>
        /// <returns>Image - Returns an Image object for the specified Form</returns>
        /// <remarks>Captures an snapshot of the specified Form</remarks>
        Image RenderFormAsImage(System.Windows.Forms.Form frm, bool clientAreaOnly = false);

        #endregion

        // *********************************** PRIVATE METHODS ***********************************
        #region "Private Methods"

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
