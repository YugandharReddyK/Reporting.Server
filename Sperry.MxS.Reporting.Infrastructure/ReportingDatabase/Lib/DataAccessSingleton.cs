using Sperry.MxS.Core.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Infrastructure.ReportingDatabase.Lib
{
    public class DataAccessSingleton
    {
        private static DataAccessSingleton _instance;

        private SqLiteDatabase _db;

        private readonly string _dbName = string.Format("ReportDb{0}.s3db", new Random().Next(0, 10000));


        public static DataAccessSingleton Instance
        {
            get
            {

                if (_instance == null)
                {
                    _instance = new DataAccessSingleton();
                }

                return _instance;
            }
        }

        private DataAccessSingleton()
        {
        }

        private void OpenConnection(bool enableLog)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            _db = new SqLiteDatabase(":memory:", enableLog);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (new AssemblyName(args.Name).Name == "System.Data.SQLite")
            {
                Assembly assembly = null;

                string path = Path.Combine(Path.GetTempPath(), "dr", Environment.Is64BitProcess ? "64" : "32");
                string fullPath = Path.Combine(path, "System.Data.SQLite.dll");

                if (!File.Exists(fullPath))
                {
                    Directory.CreateDirectory(Path.GetFullPath(path));

                    File.WriteAllBytes(fullPath,
                        Environment.Is64BitProcess
                            ? System_Data_SQLite64
                            : System_Data_SQLite32);
                }
                assembly = Assembly.LoadFrom(fullPath);

                return assembly;
            }
            return null;
        }

        /// <summary>
        ///   Looks up a localized resource in Resources folder in common library.
        /// </summary>
        private byte[] System_Data_SQLite32 => EmbeddedResourceHelper.ReadDllResource("System.Data.SQLite32.dll");
        
        private byte[] System_Data_SQLite64 => EmbeddedResourceHelper.ReadDllResource("System.Data.SQLite64.dll");

        public void CreateDatabaseTables(DataSet dataset, bool enableLog)
        {
            try
            {
                IEnumerable<string> dbScripts = CreateDbScript(dataset);

                OpenConnection(enableLog);

                DropExistingTables(enableLog);

                _db.ExecuteNonQuery("BEGIN TRANSACTION");

                foreach (var script in dbScripts)
                {
                    try
                    {
                        _db.ExecuteNonQuery(script);
                    }
                    catch (Exception exception)
                    {
                        //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                    }
                }

                _db.ExecuteNonQuery("END TRANSACTION");
            }
            catch (Exception exception)
            {
               //LoggingSingleton.Instance.LogMessage(exception, enableLog);
            }
        }

        public DataTable GetData(string sqlStatement, bool enableLog)
        {
            try
            {
                string error;
                return GetData(sqlStatement, out error, enableLog);
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                return null;
            }
        }


        public DataTable GetData(string sqlStatement, out string error, bool enableLog)
        {
            var returnData = new DataTable();
            error = string.Empty;

            try
            {
                returnData = _db.GetDataTable(sqlStatement, enableLog);

                //_sqlCeCommand.CommandText = sqlStatement;
                //_sqlCeCommand.ExecuteNonQuery();

                //var sqlDataAdapter = new SqlCeDataAdapter(_sqlCeCommand);

                //sqlDataAdapter.Fill(returnData);
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                error = exception.Message;
            }

            return returnData;
        }

        private bool ExecuteStatement(string sqlStatement, bool enableLog)
        {
            try
            {
                _db.ExecuteNonQuery(sqlStatement);

                //_sqlCeCommand.CommandText = sqlStatement;
                //_sqlCeCommand.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                return false;
            }

            return true;
        }

        private void DropExistingTables(bool enableLog)
        {
            _db.DropTables(enableLog);
            //DataTable dbTables = GetData("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES");

            //foreach (DataRow row in dbTables.Rows)
            //{
            //    ExecuteStatement("Drop Table " + row[0]);
            //}
        }

        private IEnumerable<string> CreateDbScript(DataSet dataSet)
        {
            var createTableScripts = new List<string>();

            var dateColumnIndies = new List<int>();


            foreach (DataTable dataTable in dataSet.Tables)
            {
                var decimalColumnIndex = new List<int>();
                /*<Create Table>*/
                string tableName = dataTable.TableName;
                string columnScript = string.Empty;
                dateColumnIndies.Clear();
                int cnt = 0;
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    //string dataType = "nvarchar(4000)";
                    string dataType = "text";

                    if ((new List<Type> { typeof(int), typeof(decimal), typeof(double) }).Contains(dataColumn.DataType))
                    {
                        //dataType = "decimal";
                        dataType = "real";
                        decimalColumnIndex.Add(dataTable.Columns.IndexOf(dataColumn));
                    }
                    else if (typeof(DateTime) == dataColumn.DataType)
                    {
                        //dataType = "datetime";
                        dataType = "text";
                        dateColumnIndies.Add(dataTable.Columns.IndexOf(dataColumn));
                    }
                    string columnName = string.Format("[{0}] {1},", dataColumn.ColumnName, dataType);

                    if (columnScript.ToUpper().Contains(columnName.ToUpper()))
                        columnName = string.Format("[{0}_{2}] {1},", dataColumn.ColumnName, dataType, (cnt++));

                    columnScript += columnName;
                }

                if (columnScript != string.Empty)
                {
                    columnScript = columnScript.Substring(0, columnScript.Length - 1);
                }

                var createTableScript = string.Format("Create Table {0} ({1})", tableName, columnScript);

                createTableScripts.Add(createTableScript);
                /*</Create Table>*/

                /*<Insert Rows>*/
                string rowDataFormat = string.Empty;
                int columnIndex = 0;
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if ((new List<Type> { typeof(int), typeof(decimal), typeof(double) }).Contains(dataColumn.DataType))
                    {
                        rowDataFormat += "{" + columnIndex + "},";
                    }
                    else if (typeof(DateTime) == dataColumn.DataType)
                    {
                        rowDataFormat += "'{" + columnIndex + "}',";
                    }
                    else
                    {
                        rowDataFormat += "'{" + columnIndex + "}',";
                    }

                    columnIndex++;
                }

                rowDataFormat = rowDataFormat.Substring(0, rowDataFormat.Length - 1);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    //string rowData = "'" + string.Join("','", dataRow.ItemArray) + "'";
                    var dataRowArray = dataRow.ItemArray.Select(x => x.ToString().Replace("'", "''")).ToArray();

                    foreach (var dateColumnIndex in dateColumnIndies)
                    {
                        DateTime dummyDateTime = default(DateTime);
                        DateTime.TryParse(dataRowArray[dateColumnIndex].Replace("'", ""), out dummyDateTime);
                        if (dummyDateTime != default(DateTime))
                        {
                            dataRowArray[dateColumnIndex] = dummyDateTime.ToString("yyyy-MM-dd HH:mm:ss");//string.Format("'{0}'", );
                        }
                    }
                    foreach (var decColumnIndex in decimalColumnIndex)
                    {

                        dataRowArray[decColumnIndex] = dataRowArray[decColumnIndex].Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");

                    }

                    string rowData = string.Format(rowDataFormat, dataRowArray);

                    while (rowData.Contains(",,"))
                    {
                        rowData = rowData.Replace(",,", ",NULL,");
                    }

                    if (rowData.StartsWith(","))
                    {
                        rowData = "NULL" + rowData;
                    }

                    if (rowData.EndsWith(","))
                    {
                        rowData = rowData + "NULL";
                    }

                    string rowScript = string.Format("Insert Into {0} values({1})", tableName, rowData);

                    createTableScripts.Add(rowScript);
                }
                /*</Insert Rows>*/
            }

            return createTableScripts;
        }
    }
}
