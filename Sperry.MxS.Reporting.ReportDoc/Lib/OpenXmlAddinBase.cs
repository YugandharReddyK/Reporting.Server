using DocumentFormat.OpenXml.Packaging;
using Microsoft.VisualStudio.Tools.Applications;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class OpenXmlAddinBase : AddinBase
    {
        #region "Private Variables"

        private readonly object padLock = new object();
        #endregion

        #region "Properties"

        /// <summary>
        /// doc - OpenXML Word Processing Documenbt
        /// </summary>
        public WordprocessingDocument doc;

        #endregion

        // ************************************ CONSTRUCTORS *************************************
        #region "Constructors"

        /// <summary>
        /// AddInUtil Constructor - Initializes a new instance of the <see><cref>AddInUtil</cref></see>
        /// </summary>
        public OpenXmlAddinBase(WordprocessingDocument doc)
        {
            this.doc = doc;
        }

        #endregion

        #region "Public Methods"

      
        /// <returns>Dataset</returns>
        public DataSet AddCustomDataTables(DataSet dataset, bool enableLog)
        {
            List<Tuple<string, string, string>> allProperties = ReadAllCustomProperties(enableLog);

            return base.AddCustomDataTables(dataset, allProperties, enableLog);
        }

      
        public static void RemoveDependency(string saveLocation, bool enableLog)
        {
            try
            {
                int runtimeVersion = ServerDocument.GetCustomizationVersion(saveLocation);
                if (runtimeVersion == 3)
                {
                    ServerDocument.RemoveCustomization(saveLocation);
                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                ErrorMessages.Add(exception.Message);
            }
        }

      
        public List<Tuple<string, string, string>> ReadAllCustomProperties(bool enableLog)
        {
            try
            {
                // Make room to hold the Properties
                //Dictionary<string, string> props = new Dictionary<string, string>();
                List<Tuple<string, string, string>> props = new List<Tuple<string, string, string>>();

                lock (padLock)
                {
                    // Go through each XMLPart
                    var parts = doc.MainDocumentPart.CustomXmlParts;

                    foreach (var part in parts)
                    {
                        string partText = new StreamReader(part.GetStream()).ReadToEnd();
                        if (partText.ToUpper().Contains("<KEY>REPORTDOCPROPERTY</KEY>"))
                        {
                            props.Add(new Tuple<string, string, string>("ReportDoc", null, partText));
                        }
                        //TODO: FIX THIS item2?
                    }

                    //foreach (CustomXMLPart xmlPart in wordApplication.ActiveDocument.CustomXMLParts)
                    //{
                    //    Tuple<string, string> part = NewXmlDocument(xmlPart.XML).GetKeyValuePair();
                    //    //Tuple<string, string> part = XMLHelper.GetKeyValuePair(xmlPart.XML);

                    //    //props.Add(part.Item1, part.Item2);
                    //    if (!xmlPart.BuiltIn && xmlPart.XML.ToUpper().Contains("<KEY>REPORTDOCPROPERTY</KEY>"))
                    //    {
                    //        props.Add(new Tuple<string, string, string>("ReportDoc", part.Item2, xmlPart.XML));
                    //    }
                    //    else
                    //    {
                    //        props.Add(new Tuple<string, string, string>(part.Item1, part.Item2, xmlPart.XML));
                    //    }
                    //}
                }

                return props;
            }
            catch (Exception ex)
            {
                //LoggingSingleton.Instance.LogMessage(ex, enableLog);

                if (ex.Message.ToUpper().Contains("ONE OR MORE CHANGES MADE DURING"))
                {
                    return null;
                }

                return null;
            }
        }

        #endregion
    }
}
