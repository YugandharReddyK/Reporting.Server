using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using Sperry.MxS.Core.Common.Enums;
using Hal.Core.XML;
using Sperry.MxS.Reporting.Infrastructure.ReportingDatabase.Lib;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class AddinBase : IDisposable
    {
        #region "Private Variables"
        private bool disposed;
        private static List<string> _errorMessages = new List<string>();
        #endregion

        #region "Properties"

        public static List<string> ErrorMessages
        {
            get { return _errorMessages; }
            set { _errorMessages = value; }
        }

        #endregion

        #region "Destructors"
        ~AddinBase()
        {
            Dispose(false);
        }
        #endregion

        #region "Public Methods"

        public static DataTable SortDataTable(DataTable datatable, ObservableCollection<ColumnSort> columnsorts)
        {
            string sortString = string.Empty;

            foreach (ColumnSort columnSort in columnsorts)
            {
                if (columnSort.Column != null && datatable.Columns.Contains(columnSort.Column.DisplayName))
                {
                    sortString += string.Format("{0} {1}, ", columnSort.Column.DisplayName, columnSort.Sort);
                }
            }

            sortString = sortString.Length > 2 ? sortString.Substring(0, sortString.Length - 2) : string.Empty;

            if (sortString != string.Empty)
            {
                DataView dataView = datatable.DefaultView;
                dataView.Sort = sortString.Replace("Ascending", "ASC").Replace("Descending", "DESC");
                datatable = dataView.ToTable();
            }

            return datatable;
        }

     
        public static DataTable FilterDataTable(DataTable datatable, ObservableCollection<ColumnFilter> columnfilters)
        {
            StringBuilder filterString = new StringBuilder();
            int tabCount = 0;

            DataTable dtCurrentTable = datatable;

            foreach (ColumnFilter columnFilter in columnfilters)
            {
                if (columnFilter != null && columnFilter.Column != null && !string.IsNullOrEmpty(columnFilter.Column.MergeName))
                {
                    string tabs = String.Concat(Enumerable.Repeat("\t", tabCount));

                    if (columnFilter.OpenParen == "(")
                    {
                        filterString.AppendLine(string.Format("{0}{1}", tabs, "("));
                        tabs = String.Concat(Enumerable.Repeat("\t", ++tabCount));
                    }

                    System.Type dataColumnType = dtCurrentTable.Columns[columnFilter.Column.DisplayName].DataType;
                    System.Type columnValueType = columnFilter.Value.GetType();
                    Decimal result = 0;

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

            if (filterString != new StringBuilder() && !string.IsNullOrEmpty(filterString.ToString()))
            {
                string tblName = datatable.TableName;
                DataRow[] drTablView = datatable.Select(filterString.ToString());

                if (drTablView.Any())
                {
                    datatable = drTablView.CopyToDataTable();
                    datatable.TableName = tblName;
                }
                else
                {
                    datatable.Clear();
                    datatable.TableName = tblName;
                }
            }

            return datatable;
        }

        
        public static DataTable CreateDataTableFromJoin(DataSet dataset, TableJoin tablejoin)
        {
            System.Data.DataTable dtReturn = null;

            if (dataset.Tables[tablejoin.BTableName] != null && dataset.Tables[tablejoin.ATableName] != null)
            {
                List<DataColumn> aParameters = new List<DataColumn>();
                List<DataColumn> bParameters = new List<DataColumn>();

                foreach (TableJoin.TableJoinParam parameters in tablejoin.TableJoinParams)
                {
                    if (string.IsNullOrEmpty(parameters.ATableField.DisplayName) || string.IsNullOrEmpty(parameters.BTableField.DisplayName))
                        continue;
                    aParameters.Add(dataset.Tables[tablejoin.ATableName].Columns[parameters.ATableField.DisplayName]);
                    bParameters.Add(dataset.Tables[tablejoin.BTableName].Columns[parameters.BTableField.DisplayName]);
                }

                DataColumn[] aFields = aParameters.ToArray();
                DataColumn[] bFields = bParameters.ToArray();
                if (aFields.Any() && bFields.Any())
                {

                    DataRelation relation = new DataRelation(tablejoin.TableName, aFields, bFields, false);

                    if (dataset.Relations.Contains(tablejoin.TableName))
                    {
                        dataset.Relations.Remove(tablejoin.TableName);
                    }

                    dataset.Relations.Add(relation);

                    JoinDataSet joinDataSet = new JoinDataSet(ref dataset);
                    dtReturn = joinDataSet.JoinParentToChild(tablejoin.TableName, tablejoin.ATableName, tablejoin.BTableName, tablejoin.IsLeftJoin);

                }
            }

            return dtReturn;
        }

       
        public static DataTable GroupBy(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
        {

            DataView dv = new DataView(i_dSourceTable);

            //getting distinct values for group column
            DataTable dtGroup = dv.ToTable(true, new string[] { "well_id", i_sGroupByColumn });

            //adding column for the row count
            dtGroup.Columns.Add("Count", typeof(int));

            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["Count"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
            }

            //returning grouped/counted result
            return dtGroup;
        }

       
        public static DataTable GroupDataTable(DataSet dataset, DataTable datatable, TableGrp columngroup)
        {
            if (datatable == null)
                return null;

            List<string> groupColumns = new List<string>();

            foreach (var tableGrpParam in columngroup.TableGrpParams)
            {
                if (tableGrpParam.Aggregate == "Group")
                {
                    groupColumns.Add(tableGrpParam.ATableField.DisplayName);
                }
            }

            DataTable dataTableGrouped = new DataTable();

            if (groupColumns.Count() != 0)
            {
                dataTableGrouped = datatable.AsDataView().ToTable(true, groupColumns.ToArray());
            }
            else
            {
                dataTableGrouped.Rows.Add();
            }


            dataTableGrouped.TableName = columngroup.TableName;
            dataTableGrouped.Namespace = columngroup.TableName;


            foreach (var tableGrpParam in columngroup.TableGrpParams)
            {
                if (tableGrpParam.Aggregate != "Group")
                {
                    string aggregateColumnName = tableGrpParam.ATableField.MergeName + "__" + tableGrpParam.Aggregate;

                    if (datatable.Columns[tableGrpParam.ATableField.DisplayName].DataType == typeof(string))
                    {
                        dataTableGrouped.Columns.Add(aggregateColumnName, typeof(string));
                    }
                    else
                    {
                        dataTableGrouped.Columns.Add(aggregateColumnName, typeof(decimal));
                    }

                    foreach (DataRow row in dataTableGrouped.Rows)
                    {
                        string computeString = string.Empty;

                        foreach (var group in groupColumns)
                        {
                            if (row[group].ToString() == string.Empty)
                            {
                                computeString += string.Format(" and ({0} = '' or {0} is null)", group);
                            }
                            else
                            {
                                computeString += " and " + group + " = '" + row[group] + "'";
                            }
                        }

                        if (!string.IsNullOrEmpty(computeString))
                        {
                            computeString = computeString.Substring(5);
                        }

                        row[aggregateColumnName] = datatable.Compute(string.Format("{0}([{1}])", tableGrpParam.Aggregate, tableGrpParam.ATableField.DisplayName), computeString);
                    }
                }
            }

            DataTable metadata = dataset.Tables["meta_data"];
            DataRow[] metadataRows = metadata.Select("table = '" + datatable.TableName + "'");
            if ((metadataRows != null) && (metadataRows.GetUpperBound(0) > -1))
            {
                for (int rowIndex = metadataRows.Length - 1; rowIndex >= 0; --rowIndex)
                {
                    metadata.Rows.Remove(metadataRows[rowIndex]);
                }
            }

            foreach (DataColumn column in dataTableGrouped.Columns)
            {
                metadataRows = metadata.Select("table = '" + datatable.TableName + "' AND field = '" + column.ColumnName + "'");
                if ((metadataRows != null) && (metadataRows.GetUpperBound(0) > -1))
                {
                    object[] newMetadataRowArray = metadataRows[0].ItemArray;
                    if (metadata.Columns.Contains("table"))
                        newMetadataRowArray[metadata.Columns.IndexOf("table")] = dataTableGrouped.TableName;
                    if (metadata.Columns.Contains("table_label"))
                        newMetadataRowArray[metadata.Columns.IndexOf("table_label")] = dataTableGrouped.TableName;
                    if (metadata.Columns.Contains("field"))
                        newMetadataRowArray[metadata.Columns.IndexOf("field")] = column.ColumnName;
                    if (metadata.Columns.Contains("label"))
                        newMetadataRowArray[metadata.Columns.IndexOf("label")] = column.Caption;
                    metadata.Rows.Add(newMetadataRowArray);
                }
            }

            return dataTableGrouped;

        }

        public static DataTable GroupDataTableDEPRECATED(DataTable datatable, TableGrp columngroup)
        {
            string groupString = string.Empty;
            string columnString = string.Empty;
            List<string> columnsLoaded = new List<string>();

            foreach (TableGrp.TableGrpParam columnGroup in columngroup.TableGrpParams)
            {
                //string columnName = columnGroup.ATableField.MergeName.Replace(" ", "_");
                string columnName = columnGroup.ATableField.DisplayName.Replace(" ", "_").Replace(".", "_HalDotNotation_");

                int columnsCurrentCount = 0;

                if (columnGroup.Aggregate.ToUpper() != "GROUP")
                    columnName += "__" + columnGroup.Aggregate;

                foreach (string s in columnsLoaded)
                {
                    if (s == columnName)
                        columnsCurrentCount++;
                }


                if (columnGroup.Aggregate.ToUpper() == "GROUP")
                {
                    if (columnsCurrentCount > 0)
                        columnName = columnName + "__" + columnsCurrentCount;

                    groupString += string.Format(" it[\"{0}\"] as {1},", columnGroup.ATableField.DisplayName, columnName);
                    columnString += string.Format(" {0}(it[\"{1}\"]) as {2},", "Min", columnGroup.ATableField.DisplayName, columnName);
                }
                else
                {
                    if (columnsCurrentCount > 0)
                        columnName = columnName + "_" + columnsCurrentCount;

                    if (string.IsNullOrEmpty(groupString))
                    {
                        groupString += string.Format(" ' ' as {0},", columnName);
                    }

                    //TODO: REALLY REALLY TEST THIS CHANGE
                    //columnString += string.Format(" {0}(Convert.ToDecimal(it[\"{1}\"].ToString())) as {2},", columnGroup.Aggregate, columnGroup.ATableField.DisplayName, columnName);

                    columnString += string.Format(" {0}(it[\"{1}\"]) as {2},", columnGroup.Aggregate, columnGroup.ATableField.DisplayName, columnName);
                }
                columnsLoaded.Add(columnName);
            }


            if (groupString.Length > 1)
            {
                groupString = groupString.Substring(0, groupString.Length - 1);
                groupString = string.Format("new({0})", groupString);
            }

            if (columnString.Length > 1)
            {
                columnString = columnString.Substring(0, columnString.Length - 1);
                columnString = string.Format("new({0})", columnString);
            }

            DataTable tableToGroup = datatable;

            if (tableToGroup != null)
            {
                if (!string.IsNullOrEmpty(groupString) && !string.IsNullOrEmpty(columnString))
                {
                    var groupedData = tableToGroup.AsEnumerable().AsQueryable().GroupBy(groupString, "it").Select(columnString).Cast<DynamicClass>().ToList();

                    DataTable groupedTable = ConvertFromDynamicToDataTable<DynamicClass>(groupedData, tableToGroup);

                    groupedTable.TableName = columngroup.TableName.Replace(" ", "_");
                    groupedTable.Namespace = columngroup.TableName;

                    //<Change ColumnNames>
                    foreach (var tableGrpParam in columngroup.TableGrpParams)
                    {
                        if (groupedTable.Columns.Contains(tableGrpParam.ATableField.DisplayName))
                        {
                            groupedTable.Columns[tableGrpParam.ATableField.DisplayName].ColumnName = tableGrpParam.ATableField.MergeName;

                        }
                    }
                    //</Change ColumnNames>

                    tableToGroup = groupedTable;
                }
                else
                {
                    if (!string.IsNullOrEmpty(columnString))
                    {
                        IEnumerable<DynamicClass> groupedData = tableToGroup.AsEnumerable().AsQueryable().GroupBy(groupString, "it").Select(columnString).Cast<DynamicClass>().ToList();
                        DataTable groupedTable = ConvertFromDynamicToDataTable<DynamicClass>(groupedData, tableToGroup);
                        groupedTable.TableName = columngroup.TableName.Replace(" ", "_");
                        groupedTable.Namespace = columngroup.TableName;

                        tableToGroup = groupedTable;
                    }

                }
            }

            return tableToGroup;
        }


        public static DataTable ConvertFromDynamicToDataTable<T>(IEnumerable<T> varlist, DataTable dataTable)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null)
            {
                return dtReturn;
            }

            //foreach (var rec in varlist)
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();

                    foreach (PropertyInfo pi in oProps)
                    {
                        if (dataTable != null)
                        {
                            string[] colName = pi.Name.Split(new string[] { "__" }, StringSplitOptions.None);

                            string columnName = colName[0].Replace("_HalDotNotation_", ".");

                            DataColumn dataColumn = new DataColumn(dataTable.Columns[columnName].ColumnName, dataTable.Columns[columnName].DataType)
                            {

                                Caption = dataTable.Columns[columnName].Caption

                            };

                            if (colName.Count() > 1)
                            {
                                dataColumn.ColumnName += "__" + colName[1];
                                dataColumn.Caption += "__" + colName[1];
                            }

                            dtReturn.Columns.Add(dataColumn);
                        }
                        else
                        {
                            Type colType = pi.PropertyType;

                            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            {
                                colType = colType.GetGenericArguments()[0];
                            }
                            dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                        }
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    string columnName = pi.Name.Replace("_HalDotNotation_", ".");
                    dr[columnName] = pi.GetValue(rec, null) ?? DBNull.Value;
                }

                dtReturn.Rows.Add(dr);
            }

            return dtReturn;
        }


        public static DataTable CreateDataTableFormula(DataTable datatable, TableFormula tableFormula, bool enableLog)
        {
            if (datatable == null)
            {
                ErrorMessages.Add(string.Format("Formula: {0}, ErrorMessage: Table({1}) is null", tableFormula.FormulaName, tableFormula.Table));
                return datatable;
            }

            DataColumn dataColumn = null;
            bool dataTypeWasSet = false;

            dataColumn = new DataColumn();

            dataColumn.ColumnName = tableFormula.FormulaName;

            try
            {
                string expression = ConvertToDatabaseNames(tableFormula.Formula, datatable);

                if (string.IsNullOrEmpty(tableFormula.Formula))
                {
                    expression = " ";
                }
                else if (tableFormula.Formula.ToUpper().Contains("RUNNINGTOTAL"))
                {
                    expression = string.Empty;
                    dataColumn.DataType = typeof(decimal);
                    dataTypeWasSet = true;
                }
                else if (tableFormula.DataType == "Date")
                {
                    dataColumn.DataType = typeof(DateTime);
                    dataTypeWasSet = true;
                }
                else if (tableFormula.DataType == "String")
                {
                    dataColumn.DataType = typeof(string);
                    dataTypeWasSet = true;
                }

                if (!dataTypeWasSet)
                {
                    switch (tableFormula.FormulaType)
                    {
                        case "Text":
                            dataColumn.DataType = typeof(string);
                            break;
                        case "Number":
                            dataColumn.DataType = typeof(decimal);
                            break;
                        case "Bool":
                            dataColumn.DataType = typeof(bool);
                            break;
                        default:
                            {
                                dataColumn.DataType = typeof(decimal);
                                break;
                            }
                    }
                    dataTypeWasSet = true;
                }

                if (expression.Contains("[Var-"))
                {
                    expression = VariableSingleton.Instance.ReplaceText(expression);
                }

                dataColumn.Expression = expression;

                if (datatable.Columns.Contains(dataColumn.ColumnName))
                {
                    try
                    {
                        datatable.Columns.Remove(dataColumn.ColumnName);
                    }
                    catch (Exception exc)
                    {
                        //LoggingSingleton.Instance.LogMessage(exc, enableLog);

                        ErrorMessages.Add(string.Format("Column: {0}, Formula: {1}, ErrorMessage: {2}", dataColumn.ColumnName, dataColumn.Expression, exc.Message));
                    }
                }

                if (!datatable.Columns.Contains(dataColumn.ColumnName))
                {
                    try
                    {
                        datatable.Columns.Add(dataColumn);
                    }
                    catch (Exception exception)
                    {
                        //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                        if (datatable.Columns.Contains(dataColumn.ColumnName))
                        {
                            datatable.Columns.Remove(datatable.Columns[dataColumn.ColumnName]);
                        }

                        dataColumn.DataType = typeof(string);
                        try
                        {
                            datatable.Columns.Add(dataColumn);
                        }
                        catch
                        {
                            //continue
                        }
                    }

                    if (tableFormula.Formula.ToUpper().Contains("RUNNINGTOTAL"))
                    {
                        FillRunningTotalValues(datatable, tableFormula);

                        expression = string.Empty;
                        dataColumn.DataType = typeof(decimal);
                    }
                }
            }
            catch (Exception exc)
            {
                //Log error and continue.
                ErrorMessages.Add(string.Format("Column: {0}, Formula: {1}, ErrorMessage: {2}", dataColumn.ColumnName, dataColumn.Expression, exc.Message));

                if (datatable.Columns[dataColumn.ColumnName] != null && datatable.Columns.CanRemove(datatable.Columns[dataColumn.ColumnName]))
                {
                    datatable.Columns.Remove(dataColumn);
                }
            }

            return datatable;
        }

        private static void FillRunningTotalValues(DataTable tbl, TableFormula tableFormula)
        {
            if (tbl.Rows.Count > 0)
            {
                string runningColumn = tableFormula.Formula;
                runningColumn = runningColumn.ToUpper().Replace("RUNNINGTOTAL", string.Empty).Trim();
                runningColumn = runningColumn.Replace(")", "").Replace("(", "").Replace("-", "");
                runningColumn = runningColumn.Replace("[", "").Replace("]", "");
                runningColumn = runningColumn.Replace("'", "").Trim();

                string columnName = tableFormula.FormulaName;
                if (tbl.Columns[runningColumn] == null) return;

                double runningValue = 0;
                for (int rowIndex = 0; rowIndex < tbl.Rows.Count; rowIndex++)
                {
                    double cellValue;
                    if (!double.TryParse(tbl.Rows[rowIndex][runningColumn].ToString(), out cellValue))
                        cellValue = 0;

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

        /// <summary>
        /// NewXmlDocument
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public XmlDocument NewXmlDocument(string xml)
        {
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Load the XML into the XmlDocument
            xmlDoc.LoadXml(xml);

            return xmlDoc;
        }

        /// <summary>
        /// IsValidXML - Is Valid XML
        /// </summary>
        /// <param name="xmlDataFile">xml Data File</param>
        /// <param name="enableLog"></param>
        /// <returns></returns>
        public static bool IsValidXML(string xmlDataFile, bool enableLog)
        {
            bool hasValidXML = false;

            try
            {
                XmlDocument xDoc = new XmlDocument();
                if (WinFileSystem.Exists(xmlDataFile))
                    xDoc.Load(xmlDataFile);
                else
                {
                    TextReader textReader = new StringReader(xmlDataFile);
                    xDoc.Load(textReader);
                }
                hasValidXML = true;
            }
            catch (Exception ex)
            {
                //LoggingSingleton.Instance.LogMessage(ex, enableLog);

                ErrorMessages.Add("XML is not valid: " + xmlDataFile);
                ErrorMessages.Add(ex.Message);
            }

            return hasValidXML;
        }

      
        public static T DeserializeClass<T>(string serializedstring)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringReader = new StringReader(serializedstring);

            var obj = (T)serializer.Deserialize(stringReader);
            stringReader.Dispose();

            return obj;
        }


       
        public DataSet AddCustomDataTables(DataSet dataset, List<Tuple<string, string, string>> allProperties, bool enableLog)
        {
            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "AddCustomDataTables", enableLog);


            try
            {
                DataAccessSingleton.Instance.CreateDatabaseTables(dataset, enableLog);

                //List<Tuple<string, string, string>> allProperties = ReadAllCustomProperties();

                if (allProperties == null || allProperties.Count == 0)
                {
                    return dataset;
                }

                Tuple<string, string, string> reportDocProperty = allProperties.Find(x => x.Item1.ToUpper() == "REPORTDOC");

                var columnTableJoins = new Dictionary<string, List<TableJoin>>();
                var columnGroupDefinitions = new List<TableGrp>();
                var columnTableFormulas = new List<TableFormula>();
                var savedQueries = new List<SavedQuery>();
                var variables = new List<Variable>();

                if (reportDocProperty != null)
                {
                    ReportDocProperty reportDocCustomProperties = DeserializeClass<ReportDocProperty>(reportDocProperty.Item3);

                    try
                    {
                        //Dictionary<string,string> propertiesFromString
                        XmlDocument xml = new XmlDocument();
                        XMLHelper.LoadXmlSafely(xml, reportDocCustomProperties.Value, enableLog);
                        //xml.LoadXml(reportDocCustomProperties.Value);


                        foreach (XmlNode xNode in xml.ChildNodes)
                        {
                            if (xNode.Name != "ReportDoc")
                            {
                                continue;
                            }

                            XmlNodeList xmlReportDoc = xNode.ChildNodes;

                            foreach (XmlNode xNodeReportDoc in xmlReportDoc)
                            {
                                if (xNodeReportDoc.Name.ToUpper().Contains("TABLEJOIN"))
                                {
                                    List<TableJoin> columnTableJoin = DeserializeClass<List<TableJoin>>(xNodeReportDoc.InnerText);
                                    columnTableJoins.Add(xNodeReportDoc.Name, columnTableJoin);
                                }

                                if (xNodeReportDoc.Name.ToUpper().Contains("TABLEGROUP"))
                                {
                                    columnGroupDefinitions = DeserializeClass<List<TableGrp>>(xNodeReportDoc.InnerText);
                                }

                                if (xNodeReportDoc.Name.ToUpper().Contains("TABLEFORMULA"))
                                {
                                    columnTableFormulas = DeserializeClass<List<TableFormula>>(xNodeReportDoc.InnerText);
                                }

                                if (xNodeReportDoc.Name.ToUpper().Contains("QUERIES"))
                                {
                                    savedQueries = DeserializeClass<List<SavedQuery>>(xNodeReportDoc.InnerText);
                                }

                                if (xNodeReportDoc.Name.ToUpper().Contains("VARIABLES"))
                                {
                                    variables = DeserializeClass<List<Variable>>(xNodeReportDoc.InnerText);
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                        ErrorMessages.Add(exception.Message);

                    }

                    int cnt = 0;
                    bool bTableFormulas = false;
                    bool bGroupDefinitions = false;
                    bool bJoinList = false;
                    bool bQueries = false;
                    bool bVariables = false;

                    do
                    {

                        //<Variable>
                        if (!bVariables)
                        {
                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "AddCustomDataTables - Variables", enableLog);

                            try
                            {
                                VariableSingleton.Instance.Variables = variables;
                                VariableSingleton.Instance.RefreshAllValues(enableLog);

                                if (!VariableSingleton.Instance.Variables.Any(x => x.State == Variable.RunState.Error))
                                {
                                    bVariables = true;
                                }

                            }
                            catch (Exception exception)
                            {
                                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                            }
                        }
                        //</Variable>


                        //<Table Formula>
                        if (!bTableFormulas)
                        {
                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "AddCustomDataTables - Formulas", enableLog);

                            bool bTableExists = true;
                            bool bColumnExists = true;

                            // TODO - Fix logic to continue on adding even after first error.  should run through other available formulas before moving on
                            foreach (TableFormula tableFormula in columnTableFormulas)
                            {
                                try
                                {
                                    CreateDataTableFormula(dataset.Tables[tableFormula.Table], tableFormula, enableLog);

                                    if (bTableExists)
                                    {
                                        bTableExists = dataset.Tables[tableFormula.Table] != null;
                                    }

                                    if (bTableExists && bColumnExists)
                                    {
                                        bColumnExists = dataset.Tables[tableFormula.Table].Columns[tableFormula.FormulaName] != null;

                                        if ((!string.IsNullOrEmpty(tableFormula.FormatString) || (!string.IsNullOrEmpty(tableFormula.DataType) && !string.IsNullOrEmpty(tableFormula.DataTypeFormat)))
                                            && dataset.Tables["meta_data"] != null)
                                        {


                                            DataTable metadata = dataset.Tables["meta_data"];
                                            var newMetaDataRow = metadata.Rows.Add();

                                            if (metadata.Columns["table"] != null) newMetaDataRow["table"] = tableFormula.Table;
                                            if (metadata.Columns["table_label"] != null) newMetaDataRow["table_label"] = tableFormula.Table;
                                            if (metadata.Columns["field"] != null) newMetaDataRow["field"] = tableFormula.FormulaName;
                                            if (metadata.Columns["label"] != null) newMetaDataRow["label"] = tableFormula.FormulaName;

                                            if (metadata.Columns["number_format"] != null)
                                            {
                                                newMetaDataRow["number_format"] = tableFormula.FormatString;
                                            }
                                            else
                                            {
                                                metadata.Columns.Add("number_format");
                                                newMetaDataRow["number_format"] = tableFormula.FormatString;
                                            }

                                            if (metadata.Columns["data_type"] != null)
                                            {
                                                newMetaDataRow["data_type"] = tableFormula.DataType;
                                            }
                                            else
                                            {
                                                metadata.Columns.Add("data_type");
                                                newMetaDataRow["data_type"] = tableFormula.DataType;
                                            }

                                            if (metadata.Columns["format"] != null)
                                            {
                                                newMetaDataRow["format"] = tableFormula.DataTypeFormat;
                                            }
                                            else
                                            {
                                                metadata.Columns.Add("format");
                                                newMetaDataRow["format"] = tableFormula.DataTypeFormat;
                                            }

                                        }
                                    }
                                }
                                catch (Exception exception)
                                {
                                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                                }

                            }

                            bTableFormulas = bTableExists && bColumnExists;




                        }

                        //</Table Formula>

                        //<Table Group>
                        if (!bGroupDefinitions)
                        {
                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "WordAddinBase", "AddCustomDataTables - Groups", enableLog);

                            foreach (TableGrp columnGroupDefinition in columnGroupDefinitions)
                            {
                                try
                                {

                                    if (columnGroupDefinition.TableGrpParams.Count > 0 && dataset.Tables[columnGroupDefinition.TableName] == null)
                                    {
                                        dataset.Tables.Add(GroupDataTable(dataset, dataset.Tables[columnGroupDefinition.ATableName], columnGroupDefinition));

                                    }

                                }
                                catch (Exception exception)
                                {
                                    //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Error, "WordAddinBase","AddCustomDataTables - Groups | " + exception.Message, enableLog);
                                }
                            }

                            bGroupDefinitions = true;





                        }

                        //</Table Group>

                        //<Table Join>

                        if (!bJoinList)
                        {
                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "WordAddinBase", "AddCustomDataTables - TableJoins", enableLog);


                            bool tablesExist = true;

                            if (columnTableJoins.Count > 0)
                            {

                                // Create relationship and add to the list
                                foreach (List<TableJoin> tableJoinList in columnTableJoins.Values)
                                {
                                    foreach (TableJoin tableJoin in tableJoinList)
                                    {
                                        try
                                        {

                                            if (dataset.Tables[tableJoin.BTableName] != null && dataset.Tables[tableJoin.ATableName] != null)
                                            {
                                                if (dataset.Tables[tableJoin.TableName] == null)
                                                {
                                                    DataTable newTable = CreateDataTableFromJoin(dataset, tableJoin);

                                                    if (newTable != null)
                                                    {


                                                        dataset.Tables.Add(newTable);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                tablesExist = false;
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Error, "WordAddinBase","AddCustomDataTables - TableJoins | " + exception.Message, enableLog);
                                        }
                                    }




                                }
                            }

                            bJoinList = tablesExist;

                        }



                        //</Table Join>

                        //<Query>
                        if (!bQueries)
                        {
                            //LoggingSingleton.Instance.LogMessage(MxSLogLevel.Information, "WordAddinBase", "AddCustomDataTables - Queries", enableLog);

                            if (savedQueries.Count > 0)
                            {
                                foreach (var query in savedQueries)
                                {
                                    try
                                    {

                                        if (dataset.Tables[query.Name] == null)
                                        {
                                            DataTable newTable = DataAccessSingleton.Instance.GetData(query.SqlStatement, enableLog);

                                            newTable.TableName = query.Name;
                                            newTable.Namespace = query.Name;

                                            if (newTable != null)
                                            {
                                                dataset.Tables.Add(newTable);
                                            }
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                                    }

                                }
                            }
                            bQueries = true;
                        }
                        //</Query>

                        if (bTableFormulas && bGroupDefinitions && bJoinList && bQueries && bVariables)

                            break;


                        cnt++;
                    } while (cnt < 15);
                }
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                //Catch any error to return dataset as is

            }


            return dataset;
        }

        
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method. Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

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
        #endregion

        #region "Private Methods"
        private static string WrapStringInQuotes(string columnValue)
        {
            return "'" + columnValue.Replace("\"", "").Replace("'", "") + "'";
        }

        private static string ConvertToDatabaseNames(string text, DataTable datatable)
        {
            if (datatable != null)
            {
                foreach (DataColumn column in datatable.Columns)
                {
                    text = text.Replace(column.Caption, column.ColumnName);
                }
            }

            return text;
        }
        #endregion
    }
}
