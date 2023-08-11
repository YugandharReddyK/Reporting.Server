using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.ReportDoc;

namespace Hal.Core.ReportDoc.ReportGenerator
{
    /// <summary>
    /// BaseReportGenerator - Class for a Base Report Generator 
    /// </summary>
    /// <remarks>Public Class for Generating a Report </remarks>
    public class BaseReportGenerator
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


        private MxSFileFormatType fileFormatType;
        private HalReport returnReport;
        private string saveLocation;

        #endregion

        // ********************************* PROTECTED VARIABLES *********************************
        #region "Protected Variables"
        /// <summary>
        /// Nullobject - Missing Value 
        /// </summary>
        /// <remarks>Missing.Value</remarks>
        internal object Nullobject = Missing.Value;
        /// <summary>
        /// MainDataSet - Dataset containing all avaliable tables
        /// </summary>
        /// <remarks>Dataset containing all avaliable tables</remarks>
        internal DataSet MainDataSet;
        /// <summary>
        /// MetaData - DataTable housing current metadata information
        /// </summary>
        /// <remarks>DataTable holding meta data </remarks>
        internal DataTable MetaData;
        /// <summary>
        /// TemplatePath - String path of the Template location
        /// </summary>
        /// <remarks>String path of the Template location</remarks>
        internal string TemplatePath;
        /// <summary>
        /// SavePath - String path of the Save Location
        /// </summary>
        /// <remarks>String path of the Save Location</remarks>
        internal string SavePath = WinFileSystem.GetDirectoryName(WinFileSystem.GetTempFileName());
        /// <summary>
        /// ReturnValue - Hal Report Class of the return values
        /// </summary>
        /// <remarks>Hal Report Class of the return values </remarks>
        internal HalReport ReturnValue;
        /// <summary>
        /// Extension - String path of the file extentsion
        /// </summary>
        /// <remarks>String path of the file extension </remarks>
        internal string Extension;
        /// <summary>
        /// NewFileLocation - String path of the 
        /// </summary>
        /// <remarks>String path of the </remarks>
        internal string NewFileLocation;
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
        #endregion

        // ************************************ CONSTRUCTORS *************************************
        #region "Constructors"

        /// <summary>
        /// BaseReportGenerator - Base Report Generator Constructor
        /// </summary>
        /// <remarks>Base Report Generator Constructor</remarks>
        public BaseReportGenerator()
        {
            FileFormatType = new MxSFileFormatType();
        }
        #endregion

        // ************************************ DESTRUCTORS **************************************
        #region "Destructors"
        #endregion

        // *********************************** PUBLIC METHODS ************************************
        #region "Public Methods"
        /// <summary>
        /// RenderControlAsImage - Captures an snapshot of the specified Control
        /// </summary>
        /// <param name="ctrl">ctrl - Control to be Rendered</param>
        /// <remarks>Captures an snapshot of the specified Control</remarks>
        /// <returns>Bitmap Image</returns>
        public Image RenderControlAsImage(System.Windows.Forms.Control ctrl)
        {
            System.Drawing.Rectangle bounds = ctrl.RectangleToScreen(ctrl.ClientRectangle);

            Bitmap returnBitmap;

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
                }

                returnBitmap = new Bitmap(bitmap);
            }

            return returnBitmap;
        }


        /// <summary>
        /// RenderFormAsImage Method - Captures an snapshot of the specified Form
        /// </summary>
        /// <param name="frm">Form - Form to be Rendered</param>
        /// <param name="clientAreaOnly">Bool - Flag indicating whether or not to render just the Client Area or the Entire Form</param>
        /// <returns>Image - Returns an Image object for the specified Form</returns>
        /// <remarks>Captures an snapshot of the specified Form</remarks>
        public Image RenderFormAsImage(System.Windows.Forms.Form frm, bool clientAreaOnly = false)
        {
            System.Drawing.Rectangle bounds = (clientAreaOnly) ? frm.ClientRectangle : frm.Bounds;

            Bitmap returnBitmap;

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
                }

                returnBitmap = new Bitmap(bitmap);
            }

            return returnBitmap;
        }

        /// <summary>
        /// GetJpgImage - Converts UI Element to scaled image
        /// </summary>
        /// <param name="source">UIElemet to convert</param>
        /// <param name="scale">Scale Value</param>
        /// <param name="quality">Image Quality</param>
        /// <remarks>Converts UI Element to scaled image returning a memory stream byte[]</remarks>
        /// <returns>A memory stream byte array </returns>
        public byte[] GetJpgImage(UIElement source, double scale, int quality)
        {
            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;

            double renderHeight = actualHeight * scale;
            double renderWidth = actualWidth * scale;

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            VisualBrush sourceBrush = new VisualBrush(source);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new System.Windows.Point(0, 0), new System.Windows.Point(actualWidth, actualHeight)));
            }
            renderTarget.Render(drawingVisual);

            JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
            jpgEncoder.QualityLevel = quality;
            jpgEncoder.Frames.Add(BitmapFrame.Create(renderTarget));

            Byte[] _imageArray;

            using (MemoryStream outputStream = new MemoryStream())
            {
                jpgEncoder.Save(outputStream);
                _imageArray = outputStream.ToArray();
            }

            return _imageArray;
        }
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
