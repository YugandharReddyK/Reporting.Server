using Hal.Core.StringExtensions;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.Infrastructure.Extensions;
using Sperry.MxS.Reporting.Infrastructure.ReportingDatabase.Lib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Hal.Core.XML
{
    /// <summary>
    /// XMLHelper - XML Helper
    /// </summary>
    public class XMLHelper : IDisposable
    {
        // ********************************** PRIVATE VARIABLES **********************************
        #region "Private Variables"
        private string xmlFile;
        private string xmlFileName;
        private DataSet dataset;

        private bool disposed;
        #endregion

        #region "Events"
        #endregion

        // ************************************* DELEGATES ***************************************
        #region "Delegates"
        #endregion

        // ************************************* PROPERTIES **************************************
        #region "Properties"
        /// <summary>
        /// XmlFile Property - Gets or Sets the XML string to use for Transforming to a DataSet
        /// </summary>
        public string XmlFile
        {
            get { return xmlFile; }
            set
            {
                xmlFile = value;
                xmlFileName = null;
            }
        }

        /// <summary>
        /// XmlFileName Property - Gets or Sets the name of an XML file containing the XML to use for Transforming to a DataSet
        /// </summary>
        public string XmlFileName
        {
            get { return xmlFileName; }
            set
            {
                xmlFileName = value;
                xmlFile = null;
            }
        }

        /// <summary>
        /// Dataset Property - Gets the DataSet which was created from the provided XML
        /// </summary>
        public DataSet Dataset
        {
            get { return dataset; }
            private set { dataset = value; }
        }
        #endregion

        // ************************************ CONSTRUCTORS *************************************
        #region "Constructors"
        /// <summary>
        /// XMLHelper Constructor
        /// </summary>
        public XMLHelper()
        {
        }
        #endregion

        // ************************************ DESTRUCTORS **************************************
        #region "Destructors"

        /// <summary>
        /// Allows an <see cref="T:System.Object"/> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object"/> is reclaimed by garbage collection.
        /// </summary>
        ~XMLHelper()
        {
            Dispose(false);
        }
        #endregion

        // *********************************** PUBLIC METHODS ************************************
        #region "Public Methods"
        /// <summary>
        /// ElementExists Method - Elements Exists.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="element">The element.</param>
        /// <returns>bool</returns>
        public static bool ElementExists(string xml, string element)
        {
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Load the XML into the XmlDocument
            xmlDoc.LoadXml(xml);

            return (xmlDoc.SelectSingleNode(element) != null);
        }

        /// <summary>
        /// GetKeyValuePair = Get Key/Value Pair from xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Tuple of string, string </returns>
        public static Tuple<string, string> GetKeyValuePair(string xml)
        {
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Load the XML into the XmlDocument
            xmlDoc.LoadXml(xml);

            XmlNode node = xmlDoc.FirstChild;

            if (node != null)
            {
                return new Tuple<string, string>(node.Name, node.InnerText);
            }

            return (Tuple<string, string>)null;
        }

        /// <summary>
        /// GetElementText = Get Element Text from xml
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="element"></param>
        /// <returns>string</returns>
        public static string GetElementText(string xml, string element)
        {
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Load the XML into the XmlDocument
            xmlDoc.LoadXml(xml);

            XmlNode node = xmlDoc.SelectSingleNode(element);

            if (node != null)
            {
                return node.InnerText;
            }

            return null;
        }

        /// <summary>
        /// XMLSpecialCharsEscapeChars - Removes Special Characters
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>string</returns>
        public static string XMLSpecialCharsEscapeChars(string xml)
        {
            return xml.Replace(":", "-").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace("|", ".");
        }

        /// <summary>
        /// XMLSpecialCharsEscapeChars - Removes Special Characters
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>string</returns>
        public static string XMLSpecialCharsEscapeCharsWithoutPipe(string xml)
        {
            return xml./*Replace(":", "-").*/Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"); //.Replace("|", ".");
        }

        /// <summary>
        /// XMLSpecialCharsEscapeChars - Replaces Special Characters
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>string</returns>
        public static string XMLEscapeCharsToSpecialChars(string xml)
        {
            return xml.Replace("-", ":").Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'");//.Replace(".", "|");
        }

        /// <summary>
        /// TransformXMLToDataSet Method - Transforms the specified XML into a DataSet containing one or more Data Tables
        /// </summary>
        /// <returns>DataSet - Returns a DataSet with one or more Data Tables containing data from the XML Data</returns>
        public DataSet TransformXMLToDataSet()
        {
            return TransformXMLToDataSet(true);
        }

        /// <summary>
        /// TransformXMLToDataSet Method - Transforms the specified XML into a DataSet containing one or more Data Tables
        /// </summary>
        /// <returns>DataSet - Returns a DataSet with one or more Data Tables containing data from the XML Data</returns>
        public DataSet TransformXMLToDataSet(bool enableLog)
        {
            return TransformXMLToDataSet(true, enableLog);
        }

        /// <summary>
        /// TransformXMLToDataSet Method - Transforms the specified XML into a DataSet containing one or more Data Tables
        /// </summary>
        /// <param name="createSqlTables">Creates internal SQL Tables for queries</param>
        /// <param name="enableLog"></param>
        /// <returns>DataSet - Returns a DataSet with one or more Data Tables containing data from the XML Data</returns>
        public DataSet TransformXMLToDataSet(bool createSqlTables, bool enableLog)
        {
            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - BEGIN", enableLog);

            dataset = new DataSet();

            StringBuilder stringbuilder = new StringBuilder();
            if (!String.IsNullOrEmpty(xmlFileName))
            {

                StreamReader fsReadXml = null;

                try
                {

                    if (System.IO.File.Exists(xmlFileName))
                        fsReadXml = new StreamReader(xmlFileName, Encoding.UTF8);
                    else
                    {
                        fsReadXml = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(xmlFileName)));
                        //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - ReadXML - BEGIN", enableLog);
                        dataset.ReadXml(fsReadXml);
                        //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - ReadXML - END", enableLog);
                    }

                    // Is this an XML file that we want to import
                    if (
                        System.String.Compare(xmlFileName.Substring(xmlFileName.Length - 4, 4), ".XML",
                            System.StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        // Import the XML
                        //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - ReadXML - BEGIN", enableLog);
                        dataset.ReadXml(fsReadXml);
                        //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - ReadXML - END", enableLog);
                    }
                    else // Is this an XML file that we want to import
                        if (
                            System.String.Compare(xmlFileName.Substring(xmlFileName.Length - 4, 4), ".TMP",
                                System.StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            // Import the XML
                            dataset.ReadXml(fsReadXml);
                        }
                        else if (
                            System.String.Compare(xmlFileName.Substring(xmlFileName.Length - 4, 4), ".XSD",
                                System.StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            // Import the XSD
                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - ReadXMLSchema - BEGIN", enableLog);
                            dataset.ReadXmlSchema(fsReadXml);
                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - ReadXMLSchema - END", enableLog);
                        }

                    CleanDataColumnNames();
                }
                finally
                {
                    if (fsReadXml != null)
                    {
                        fsReadXml.Close();
                    }
                }
            }
            else
            {
                // Create a StringReader to read our XML string
                using (StringReader stringReader = new StringReader(xmlFile))
                {
                    // Transform the XML into a DataSet
                    dataset.ReadXml(stringReader);
                }
            }

            dataset = FormatDataTables(dataset, enableLog);

            if (createSqlTables)
            {
                DataAccessSingleton.Instance.CreateDatabaseTables(dataset, enableLog);
            }

            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "TransformXMLToDataset - END", enableLog);

            return dataset;

        }



        /// <summary>
        /// Generic Class Deserializer
        /// </summary>
        /// <typeparam name="T">T - Type of class to serialize to</typeparam>
        /// <param name="serializedstring">String - xml in string form</param>
        /// <returns>T - Returns the deserialized class</returns>
        public static T DeserializeClass<T>(string serializedstring)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringReader = new StringReader(serializedstring);

            var obj = (T)serializer.Deserialize(stringReader);
            stringReader.Dispose();

            return obj;
        }

        /// <summary>
        /// Load xml string to Xml object
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xmlstring"></param>
        /// <param name="enableLog"></param>
        public static void LoadXmlSafely(XmlDocument xml, string xmlstring, bool enableLog)
        {
            bool xmlRetry = true;
            int maxLoopCount = 50;

            while (xmlRetry && --maxLoopCount > 0)
            {
                try
                {
                    xml.LoadXml(xmlstring);
                    xmlRetry = false;
                }
                catch (XmlException exc)
                {
                    //LoggingSingleton.Instance.LogMessage(exc, enableLog);

                    string[] lines = xmlstring.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    string lineString = lines[exc.LineNumber - 1];
                    string badChar = lineString.Substring(exc.LinePosition - 1, 1);
                    lineString = lineString.Replace(badChar, "");

                    lines[exc.LineNumber - 1] = lineString;

                    xmlstring = string.Join(Environment.NewLine, lines);
                }

            }
        }

        /// <summary>
        /// Dispose Method - Used to Dispose of this object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method. Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }
        #endregion

        // *********************************** PRIVATE METHODS ***********************************
        #region "Private Methods"
        /// <summary>
        /// Dispose Method - Called when the object is being disposed.
        /// <para>Used to Clean up any left over Transactions and Open connections.</para>
        /// </summary>
        /// <param name="disposing">Bool - Flag to indicate that the object is in the process of being disposed</param>
        private void Dispose(bool disposing)
        {
            // Code, somewhere, has destroyed this object
            if (!disposed)
            {
                // Clean up managed objects. Only Managed objects
            }

            // Clean up unmanaged objects. Only UnManaged objects
            disposed = true;
        }

        private bool IsValidXML(string value, bool enableLog)
        {
            try
            {
                // Check we actually have a value
                if (string.IsNullOrEmpty(value) == false)
                {
                    // Try to load the value into a document
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.LoadXml(value);

                    // If we managed with no exception then this is valid XML!
                    return true;
                }
                else
                {
                    // A blank value is not valid xml
                    return false;
                }
            }
            catch (System.Xml.XmlException xmlException)
            {
                //LoggingSingleton.Instance.LogMessage(xmlException, enableLog);

                return false;
            }
        }
        private void CleanDataColumnNames()
        {

            foreach (DataTable dtCurrent in dataset.Tables)
            {
                foreach (DataColumn dataColumn in dtCurrent.Columns)
                {
                    dataColumn.ColumnName = dataColumn.ColumnName.Replace(" ", "_");
                }
            }
        }

        private DataSet FormatDataTables(DataSet dataSet, bool enableLog)
        {
            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "FormatDataTables - BEGIN", enableLog);

            //<Remove autogenerated column data_Id>
            int loopcounter = 0;
            bool constraintLoop = true;
            while (constraintLoop && loopcounter++ <= 5)
            {
                constraintLoop = false;

                foreach (DataTable table in dataSet.Tables)
                {
                    for (int constraintIndex = table.Constraints.Count - 1; constraintIndex >= 0; --constraintIndex)
                    {
                        var constrint = table.Constraints[constraintIndex];
                        if (table.Constraints.CanRemove(constrint))
                        {
                            table.Constraints.Remove(constrint);
                        }
                        else
                        {
                            constraintLoop = true;
                        }
                    }
                }
            }

            loopcounter = 0;
            bool parentRelationLoop = true;
            while (parentRelationLoop && loopcounter++ <= 5)
            {
                parentRelationLoop = false;

                foreach (DataTable table in dataSet.Tables)
                {
                    for (int parentRelationIndex = table.ParentRelations.Count - 1; parentRelationIndex >= 0; --parentRelationIndex)
                    {
                        var parentRelation = table.ParentRelations[parentRelationIndex];
                        if (table.ParentRelations.CanRemove(parentRelation))
                        {
                            table.ParentRelations.Remove(parentRelation);
                        }
                        else
                        {
                            parentRelationLoop = true;
                        }
                    }
                }
            }

            foreach (DataTable table in dataSet.Tables)
            {
                try
                {
                    table.PrimaryKey = null;

                    if (table.Columns["data_Id"] != null)
                    {
                        table.Columns.Remove("data_Id");
                    }

                }
                catch (Exception exception)
                {
                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                }
            }
            //</Remove autogenerated column data_Id>


            DataSet dsNewDataSet = new DataSet();
            dsNewDataSet.Locale = CultureInfo.CurrentCulture;
            DataTable metaData = dataSet.Tables["meta_data"];
            metaData.Locale = CultureInfo.CurrentCulture;
            dataSet.Locale = CultureInfo.CurrentCulture;
            DBNull dbNull = DBNull.Value;

            DataTable uniqueTables = new DataTable();

            if (metaData != null)
            {
                uniqueTables = metaData.AsDataView().ToTable(true, "table");
            }

            foreach (DataRow dataRow in uniqueTables.Rows)
            {
                if (!dataset.DataTableExists(dataRow["table"].ToString()))
                {
                    dataset.Tables.Add(new DataTable(dataRow["table"].ToString()));
                }
            }

            foreach (DataTable dataTable in dataSet.Tables)
            {
                dataTable.Locale = CultureInfo.CurrentCulture;
                DataTable addTable = new DataTable();
                addTable = dataTable.Clone();
                addTable.Locale = CultureInfo.CurrentCulture;

                var tableRowNumber = new DataColumn("TableRowNumber", typeof(int));
                tableRowNumber.AutoIncrement = true;
                tableRowNumber.AutoIncrementSeed = 1;
                tableRowNumber.AutoIncrementStep = 1;

                addTable.Columns.Add(tableRowNumber);

                string tableFriendlyName = string.Empty;
                if (metaData != null && metaData.Columns["table_label"] != null)
                {
                    tableFriendlyName = metaData.Compute("MAX(table_label)", string.Format("table = '{0}'", addTable.TableName)).ToString();
                }

                addTable.Namespace = string.IsNullOrEmpty(tableFriendlyName) ? addTable.TableName : tableFriendlyName;

                foreach (DataColumn column in addTable.Columns)
                {
                    DataRow[] metadataRows = null;
                    if (metaData != null)
                    {
                        //TODO: CHange to Linq for performance... Something like this
                        //var metadataRows = (from rows in metadata.AsEnumerable()
                        //                    where rows.Field<string>("table") == parentDataTable.TableName
                        //                    select rows).ToList();
                        metadataRows = metaData.Select("table = '" + addTable.TableName + "' AND field = '" + column.ColumnName + "'");
                    }

                    bool isNumber = false;

                    string datatype = "";

                    if ((metadataRows != null) && (metadataRows.GetUpperBound(0) > -1))
                    {
                        int decimalPlaces = default(int);
                        decimal precision = default(decimal);

                        if (metadataRows[0].Table.Columns.Contains("decimal_places"))
                        {
                            decimalPlaces = metadataRows[0].Get<int>("decimal_places", enableLog);
                        }

                        if (metadataRows[0].Table.Columns.Contains("precision"))
                        {
                            precision = metadataRows[0].Get<decimal>("precision", enableLog);
                        }

                        if (metadataRows[0].Table.Columns.Contains("data_type"))
                        {
                            datatype = metadataRows[0].Get<string>("data_type", enableLog);
                        }

                        if (decimalPlaces != default(int) || precision != default(decimal) || (datatype ?? "").ToUpper() == "NUMBER")
                        {
                            isNumber = true;
                        }

                        if (metadataRows[0].Table.Columns.Contains("label") && !string.IsNullOrEmpty(metadataRows[0].Get<string>("label", enableLog)))
                        {
                            column.Caption = metadataRows[0].Get<string>("label", enableLog);
                        }

                        if (metadataRows[0].Table.Columns.Contains("unit_label"))
                        {
                            column.Namespace = metadataRows[0].Get<string>("unit_label", enableLog);
                        }

                    }

                    if (!string.IsNullOrEmpty(datatype) && new List<String> { "STRING", "NUMBER", "DATE" }.Contains(datatype.ToUpper()))
                    {
                        switch (datatype.ToUpper())
                        {
                            case "STRING":
                                column.DataType = typeof(string);

                                break;
                            case "NUMBER":
                                decimal dummy;
                                var textInNumberColumnQuery = from tbl in dataTable.AsEnumerable()
                                                              where !decimal.TryParse(tbl.Field<string>(column.ColumnName), NumberStyles.Any, CultureInfo.CurrentCulture, out dummy)
                                                                      && !string.IsNullOrEmpty(tbl.Field<string>(column.ColumnName))
                                                              select tbl;

                                if (!textInNumberColumnQuery.Any())
                                {
                                    column.DataType = typeof(decimal);
                                    column.AllowDBNull = true;
                                }

                                break;
                            case "DATE":
                                DateTime dummyDate = default(DateTime);
                                var textInDateColumnQuery = from tbl in dataTable.AsEnumerable()
                                                            where !DateTime.TryParse(tbl.Field<string>(column.ColumnName), CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dummyDate) && !string.IsNullOrEmpty(tbl.Field<string>(column.ColumnName))
                                                            select tbl;

                                if (!textInDateColumnQuery.Any())
                                {
                                    column.DataType = typeof(DateTime);
                                    column.AllowDBNull = true;
                                }

                                break;
                        }
                    }
                    else if (column.DataType == typeof(string))
                    {

                        decimal dummy;
                        var textInColumnQuery = from tbl in dataTable.AsEnumerable()
                                                where (!(decimal.TryParse(tbl.Field<string>(column.ColumnName), NumberStyles.Any, CultureInfo.CurrentCulture, out dummy) || (isNumber && decimal.TryParse(tbl.Field<string>(column.ColumnName), NumberStyles.Any, CultureInfo.CurrentCulture, out dummy))))
                                                       && !string.IsNullOrEmpty(tbl.Field<string>(column.ColumnName))
                                                select tbl;


                        if (!textInColumnQuery.Any())
                        {
                            column.DataType = typeof(decimal);
                            column.AllowDBNull = true;
                        }
                        else
                        {
                            DateTime dummyDate = default(DateTime);
                            var dateInColumnQuery = from tbl in dataTable.AsEnumerable()
                                                    where !DateTime.TryParse(tbl.Field<string>(column.ColumnName), CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dummyDate) && !string.IsNullOrEmpty(tbl.Field<string>(column.ColumnName))
                                                    select tbl;

                            if (!dateInColumnQuery.Any() && dummyDate > new DateTime(1901, 1, 1))
                            {
                                column.DataType = typeof(DateTime);
                                column.AllowDBNull = true;

                            }
                        }

                    }

                    if (new List<Type>() { typeof(int), typeof(Int16), typeof(Int32), typeof(Int64) }.Contains(column.DataType))
                    {
                        column.DataType = typeof(decimal);
                    }
                }


                if (dataTable.TableName != "meta_data")
                {
                    foreach (DataRow drRows in dataTable.Rows)
                    {

                        foreach (DataColumn column in addTable.Columns)
                        {
                            if (dataTable.Columns[column.ColumnName] == null)
                            {
                                continue;
                            }

                            if (string.IsNullOrEmpty(drRows[column.ColumnName].ToString()) && column.DataType == typeof(decimal))
                            {
                                drRows[column.ColumnName] = dbNull;
                            }
                            else
                            {

                                if (column.DataType == typeof(decimal))
                                {
                                    DataRow[] metadataRows = null;
                                    if (metaData != null)
                                    {
                                        metadataRows = metaData.Select("table = '" + dataTable.TableName + "' AND field = '" + column.ColumnName + "'");
                                    }
                                    if ((metadataRows != null) && (metadataRows.GetUpperBound(0) > -1))
                                    {
                                        int? decimalPlaces = null;
                                        decimal? precision = null;

                                        if (metaData.Columns["decimal_places"] != null)
                                        {
                                            decimalPlaces = metadataRows[0].Get<int?>("decimal_places", enableLog);
                                        }

                                        if (metaData.Columns["precision"] != null)
                                        {
                                            precision = metadataRows[0].Get<decimal?>("precision", enableLog);
                                        }

                                        if (string.IsNullOrEmpty(drRows[column.ColumnName].ToString()))
                                        {
                                            drRows[column.ColumnName] = DBNull.Value;
                                        }
                                        else
                                        {
                                            decimal returnValue = default(decimal);

                                            if (drRows[column.ColumnName].ToString().ToLower() == "true" || drRows[column.ColumnName].ToString().ToLower() == "false")
                                            {
                                                returnValue = 0;
                                                Boolean tempValue = Boolean.Parse(drRows[column.ColumnName].ToString());
                                                if (tempValue)
                                                    returnValue = 1;
                                            }
                                            else
                                            {
                                                returnValue = Decimal.Parse(drRows[column.ColumnName].ToString(), NumberStyles.Any, CultureInfo.CurrentCulture);
                                            }

                                            if (precision != null)
                                            {
                                                returnValue = Math.Round(returnValue, precision.ToString().Right(".").Length);
                                            }

                                            if (decimalPlaces != null)
                                            {

                                                returnValue = Convert.ToDecimal(returnValue.ToString("N" + decimalPlaces, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

                                            }

                                            if (!string.IsNullOrEmpty(returnValue.ToString(CultureInfo.CurrentCulture)))
                                            {
                                                drRows[column.ColumnName] = returnValue;

                                            }

                                        }
                                    }
                                }

                                if (column.DataType == typeof(DateTime))
                                {
                                    DateTime dummyDate = default(DateTime);
                                    if (string.IsNullOrEmpty(drRows[column.ColumnName].ToString()))
                                    {
                                        drRows[column.ColumnName] = dbNull;
                                    }
                                    else
                                        if (DateTime.TryParse(drRows[column.ColumnName].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dummyDate))
                                        {
                                            drRows[column.ColumnName] = DateTime.Parse(drRows[column.ColumnName].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal);

                                        }
                                }
                            }
                        }
                        addTable.ImportRow(drRows);
                    }

                }
                else
                {
                    addTable = metaData.Copy();
                    addTable.Namespace = addTable.TableName;
                }

                //<Add columns based on meta_data>
                EnumerableRowCollection<DataRow> dataRows = null;
                if (metaData != null)
                {
                    dataRows = metaData.AsEnumerable().Where(x => x.Field<string>("table") == addTable.TableName && addTable.Columns[x.Field<string>("field")] == null);
                }

                if (dataRows != null && dataRows.Any())
                {
                    foreach (var dataRow in dataRows)
                    {
                        string columnName = dataRow["field"].ToString();
                        string columnCaption = columnName;

                        if (dataRow.Table.Columns["label"] != null && !string.IsNullOrEmpty(dataRow["label"].ToString()))
                        {
                            columnCaption = dataRow["label"].ToString();
                        }

                        var column = new DataColumn { ColumnName = dataRow["field"].ToString(), Caption = columnCaption };

                        if (addTable.Columns[columnName] == null && !string.IsNullOrEmpty(columnName))
                        {
                            addTable.Columns.Add(column);
                        }
                    }
                }
                //</Add columns based on meta_data>

                dsNewDataSet.Tables.Add(addTable);
            }

            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "FormatDataTables - END", enableLog);

            return dsNewDataSet;
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
