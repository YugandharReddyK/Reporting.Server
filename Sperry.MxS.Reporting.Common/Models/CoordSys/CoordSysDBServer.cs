using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CoordSys
{
    /// <summary>
    /// MS3 : clsSqlServer
    /// </summary>
    // Need to ask Kiran Sir
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CoordSysDBServer
    {
        //private OleDbConnection _dbConnection = null;

        private string _connectionString;
        //private string _connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\CoordSys\\CoordSys.mdb;Persist Security Info=True;Jet OLEDB:Database Password=donkey";

        private static string _mdbFilePath;

        /// <summary>
        /// 
        /// </summary>
        public CoordSysDBServer()
        {
            GetDefaultConnectionString();
            //_dbConnection = new OleDbConnection(_connectionString);
            //_dbConnection.Open();
        }

        public CoordSysDBServer(string mdbFilePath)
        {
            _mdbFilePath = mdbFilePath;
            GetDefaultConnectionString();
        }

        /// <summary>
        /// 
        /// </summary>
        ~CoordSysDBServer()
        {
            //			base.Finalize();
        }

        private void GetDefaultConnectionString()
        {
            _connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbFilePath + "MaxSurveyCoordSys.mdb;Persist Security Info=True;Jet OLEDB:Database Password=donkey";
        }

        //private OleDbDataReader GetOleDbDataReader(OleDbConnection dbConnection, string queryName, string queryParameter)
        //{
        //    var sSQL = GetSql(queryName, queryParameter);
        //    var dbCmd = new OleDbCommand(sSQL, dbConnection);
        //    var dataReader = dbCmd.ExecuteReader();
        //    return dataReader;
        //}

        //public bool ExecuteNonQuery(string queryString)
        //{
        //    using (var conn = new OleDbConnection(_connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            var cmd = conn.CreateCommand();
        //            cmd.Connection = conn;
        //            cmd.CommandText = queryString;
        //            cmd.ExecuteNonQuery();
        //            conn.Close();
        //            cmd.Dispose();
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public SortedDictionary<string, string> GetKeyValuePairs(string queryName, string queryPara, string keyName, string valueName)
        //{
        //    var sName = "";
        //    var sKey = "";
        //    SortedDictionary<string, string> dataSet = null;
        //    using (var dbConnection = new OleDbConnection(_connectionString))
        //    {
        //        dbConnection.Open();
        //        var dataReader = GetOleDbDataReader(dbConnection, queryName, queryPara);
        //        if (dataReader != null)
        //        {
        //            dataSet = new SortedDictionary<string, string>();
        //            while (dataReader.Read())
        //            {
        //                sKey = Convert.ToString(dataReader[keyName]);
        //                sName = Convert.ToString(dataReader[valueName]);
        //                dataSet.Add(sKey, sName);
        //            }
        //            dataReader.Close();
        //        }
        //        dbConnection.Close();
        //        dataReader?.Dispose();
        //    }
        //    return dataSet;
        //}

        /// <summary>
        /// Type: 0 - Undefined, 1 - UTM27&UTM83, 2 - Standard UTM, 3 - User defined, 4 - Others
        /// </summary>
        /// <param name="coordinateSystem"></param>
        /// <returns></returns>
        //public Dictionary<string, string> ParseGridCoordSystem(string coordinateSystem)
        //{
        //    var coordSystem = string.IsNullOrWhiteSpace(coordinateSystem) ? "Undefined" : coordinateSystem;
        //    var system = coordSystem;
        //    var prjSys = new Dictionary<string, string>();
        //    prjSys.Add("Type", coordSystem);  //Type 0: Undefined, 1: UTM, 2: UserDefined, 3: Other
        //    prjSys.Add("CentreMeridian", "-999.999");
        //    prjSys.Add("System", "-999");      //("Coordinate System.Name", "0");
        //    prjSys.Add("Location", "-999");    //("Coordinate Location.Name", "0");
        //    prjSys.Add("Zone", "-999");        //("Coordinate Zone.Name", "0");
        //    prjSys.Add("UTMHemisphere", "-999");
        //    if (coordSystem.Length < 3)
        //    {
        //        coordSystem = coordSystem.PadRight(3);
        //    }
        //    //----- if its UTM grid then display Central Meridian and Hemisphere
        //    List<KeyValuePair<string, string>> paras = null;
        //    switch (coordSystem.Substring(0, 3))
        //    {
        //        //----- undefined coodinate system
        //        case "":
        //        case "Und":
        //            prjSys["Type"] = "0";
        //            prjSys["System"] = "0";
        //            return prjSys;

        //        //----- UTM coordinate systems
        //        case "UTM":
        //            prjSys["Type"] = "1";
        //            switch (coordSystem.Substring(3, 2))
        //            {
        //                //----- NAD83 UTM system
        //                case "83":
        //                    prjSys["System"] = "-2";
        //                    break;

        //                //----- NAD27 UTM system
        //                case "27":
        //                    prjSys["System"] = "-3";
        //                    break;

        //                //----- International UTM system
        //                default:
        //                    prjSys["System"] = "-1";
        //                    prjSys["Type"] = "2";
        //                    if (coordSystem.EndsWith("S"))
        //                    {
        //                        prjSys["UTMHemisphere"] = "-1";
        //                    }
        //                    else if (coordSystem.EndsWith("N"))
        //                    {
        //                        prjSys["UTMHemisphere"] = "1";
        //                    }
        //                    break;
        //            }
        //            break;

        //        //----- User defined coordinate systems
        //        case "UD:":
        //            prjSys["Type"] = "3";

        //            int cur = (short)coordSystem.LastIndexOf(":");
        //            system = coordSystem.Substring(0, cur);
        //            //double centreMeridian;
        //            //Double.TryParse(coordSystem.Substring(cur + 1), out centreMeridian);
        //            prjSys["CentreMeridian"] = coordSystem.Substring(cur + 1);
        //            paras = new List<KeyValuePair<string, string>>();
        //            paras.Add(new KeyValuePair<string, string>("System", "Coordinate System.PrimaryKey"));
        //            paras.Add(new KeyValuePair<string, string>("Location", "Coordinate Location.PrimaryKey"));
        //            QueryGridCoordSystem(system, paras, ref prjSys);
        //            break;

        //        //----- state plane and international coordinate systems (all other coordinate systems)
        //        default:
        //            prjSys["Type"] = "4";
        //            paras = new List<KeyValuePair<string, string>>();
        //            paras.Add(new KeyValuePair<string, string>("System", "Coordinate System.PrimaryKey"));
        //            paras.Add(new KeyValuePair<string, string>("Location", "Coordinate Location.PrimaryKey"));
        //            paras.Add(new KeyValuePair<string, string>("Zone", "Coordinate Zone.PrimaryKey"));
        //            QueryGridCoordSystem(system, paras, ref prjSys);
        //            break;

        //    }
        //    return prjSys;
        //}

        //public void QueryGridCoordSystem(string coordSystem, List<KeyValuePair<string, string>> paras, ref Dictionary<string, string> prjSys)
        //{
        //    var queryName = "Qry - Get Coordinate System from Zone ID";
        //    var queryPara = "[Zone ID] = \'" + coordSystem + "\'";
        //    var sSQL = GetSql(queryName, queryPara);

        //    using (var dbConnection = new OleDbConnection(_connectionString))
        //    {
        //        dbConnection.Open();
        //        var dbCmd = new OleDbCommand(sSQL, dbConnection);
        //        var reader = dbCmd.ExecuteReader();
        //        if (reader != null && reader.HasRows)
        //        {
        //            reader.Read();
        //            //int c = reader.VisibleFieldCount;
        //            //int lSystem = (int)(reader["Coordinate System.PrimaryKey"]);
        //            //string sSystem = Convert.ToString(reader["Coordinate System.Name"]);
        //            //int lLocation = (int)(reader["Coordinate Location.PrimaryKey"]);
        //            //string sLocation = Convert.ToString(reader["Coordinate Location.Name"]);
        //            //int lZone = (int)(reader["Coordinate Zone.PrimaryKey"]);
        //            //string sZone = Convert.ToString(reader["Coordinate Zone.Name"]);   
        //            prjSys["System"] = Convert.ToString(reader["Coordinate System.PrimaryKey"]);
        //            prjSys["Location"] = Convert.ToString(reader["Coordinate Location.PrimaryKey"]);
        //            prjSys["Zone"] = Convert.ToString(reader["Coordinate Zone.PrimaryKey"]);
        //            foreach (var kvp in paras)
        //            {
        //                prjSys[kvp.Key] = Convert.ToString(reader[kvp.Value]);
        //            }
        //        }
        //        reader?.Close();
        //        dbConnection.Close();
        //        dbCmd.Dispose();
        //        reader?.Dispose();
        //    }
        //}

        //public MagneticModel GetMageticModel(string id)
        //{
        //    var sSQL = "SELECT * FROM [Magnetic Model] WHERE ";
        //    sSQL += "[Magnetic Model].ID = " + "\'" + id + "\';";
        //    MagneticModel model = null;
        //    using (var dbConnection = new OleDbConnection(_connectionString))
        //    {
        //        dbConnection.Open();
        //        var dbCmd = new OleDbCommand(sSQL, dbConnection);
        //        var reader = dbCmd.ExecuteReader();
        //        if (reader != null && reader.HasRows)
        //        {
        //            reader.Read();
        //            int k;
        //            if (int.TryParse(reader["PrimaryKey"].ToString(), out k))
        //            {
        //                model = new MagneticModel(k, reader["ID"].ToString(), reader["Description"].ToString());
        //            }
        //        }
        //        reader?.Close();
        //        dbConnection.Close();
        //        dbCmd.Dispose();
        //        reader?.Dispose();
        //    }
        //    return model;
        //}

        //public DataTable GetDataTable(string tableName)
        //{
        //    var dt = new DataTable();
        //    using (var dbConnection = new OleDbConnection(_connectionString))
        //    {
        //        try
        //        {
        //            var queryString = "Select * From [" + tableName + "];"; //GetSql(queryName,"");
        //            var command = new OleDbCommand(queryString, dbConnection);
        //            dbConnection.Open();
        //            var dataReader = command.ExecuteReader();
        //            if (dataReader != null)
        //            {
        //                dt.Load(dataReader);
        //                dataReader.Close();
        //            }
        //            dbConnection.Close();
        //            command.Dispose();
        //            dataReader?.Dispose();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //        finally
        //        {
        //        }
        //    }
        //    return dt;
        //}

        //public DataTable GetDataTable(string queryName, string queryPara)
        //{
        //    var dt = new DataTable();
        //    using (var dbConnection = new OleDbConnection(_connectionString))
        //    {
        //        try
        //        {
        //            var queryString = GetSql(queryName, queryPara);
        //            var command = new OleDbCommand(queryString, dbConnection);
        //            dbConnection.Open();
        //            var dataReader = command.ExecuteReader();
        //            if (dataReader != null)
        //            {
        //                dt.Load(dataReader);
        //                dataReader.Close();
        //            }
        //            dbConnection.Close();
        //            command.Dispose();
        //            dataReader?.Dispose();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //        finally
        //        {
        //        }
        //    }
        //    return dt;
        //}

        //public string GetFieldValue(string queryName, string queryPara, string fieldName)
        //{
        //    string result = null;
        //    using (var dbConnection = new OleDbConnection(_connectionString))
        //    {
        //        dbConnection.Open();
        //        var reader = GetOleDbDataReader(dbConnection, queryName, queryPara);
        //        if (reader != null && reader.HasRows)
        //        {
        //            reader.Read();
        //            result = reader[fieldName].ToString();
        //        }
        //        reader?.Close();
        //        dbConnection.Close();
        //        reader?.Dispose();
        //    }
        //    return result;
        //}

        // Returns the sql string that results from the query sQueryName after the parameter substitution (in sParmsIn)
        // sParmsIn is assumed to be in the form of:
        // "[parm1] = value1, [parm2] = value2, ... [parmn] = valuen"
        // Note: the square brackets are required and if valuen is a literal string, it must be enclosed in single quotes.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQueryName"></param>
        /// <param name="sParameters"></param>
        /// <returns></returns>
        //public string GetSql(string sQueryName, string sParameters)
        //{
        //    int nCurPos;
        //    int nSqlPos;
        //    var sSQL = "";
        //    string sText;
        //    string sParName;
        //    string sSearch;
        //    string sParVal;
        //    string sSqlLeft;
        //    string sSqlRight;

        //    OleDbCommand cmdQueries = null;
        //    OleDbDataReader drQueries = null;

        //    //----- Find the SQL statement defining the query.
        //    sText = "SELECT DISTINCTROW [SQL.Query Name], [SQL.SQL] " + "FROM [SQL] " + "WHERE [Query Name]=\"" + sQueryName + "\" " + "ORDER BY [SQL.Query Name];";
        //    OleDbConnection dbConnection = null;
        //    try
        //    {
        //        dbConnection = new OleDbConnection(_connectionString);
        //        dbConnection.Open();
        //        cmdQueries = new OleDbCommand(sText, dbConnection);
        //        drQueries = cmdQueries.ExecuteReader();

        //        if (drQueries.HasRows)
        //        {
        //            drQueries.Read();
        //            sSQL = Convert.ToString(drQueries["SQL.SQL"]);

        //            //----- If the query uses parameters, strip off the PARAMETERS declaration
        //            //      (everything up to, and including, the first semicolon and the carriage return)
        //            if (sSQL.Contains("PARAMETERS"))
        //            {
        //                nCurPos = sSQL.IndexOf(";");
        //                sSQL = sSQL.Substring(nCurPos + 3);
        //                //----- Substitute the parameter values into the sql statement.
        //                nCurPos = 1;
        //                sSearch = "=,";
        //                //----- loop through each parameter in the list
        //                //while (nCurPos > 0)  //Zhiqin Wu, to remove Wrapper.ParseString,
        //                {
        //                    //----- get the parameter name and its value
        //                    //sParName = MaxSurvey.Core.ThirdPartyWrapper.Wrapper.ParseString(ref nCurPos, ref sParameters, ref sSearch).Trim();
        //                    //sParVal = MaxSurvey.Core.ThirdPartyWrapper.Wrapper.ParseString(ref nCurPos, ref sParameters, ref sSearch).Trim();
        //                    sParName = sParameters.Split(sSearch.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim();
        //                    sParVal = sParameters.Split(sSearch.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1].Trim();

        //                    if (!string.IsNullOrEmpty(sParName) && !string.IsNullOrEmpty(sParVal))
        //                    {
        //                        nSqlPos = 1;
        //                        //----- loop in case the parameter occurs more than once in the SQL statement
        //                        while (nSqlPos > 0)
        //                        {
        //                            //----- search for the location of the parameter name in the SQL statement
        //                            nSqlPos = sSQL.IndexOf(sParName, nSqlPos);
        //                            if (nSqlPos <= 0)
        //                            {
        //                                break;
        //                            }
        //                            //----- record the sql statement before and after the position of the sParName
        //                            sSqlLeft = sSQL.Substring(0, nSqlPos);
        //                            sSqlRight = sSQL.Substring(nSqlPos + sParName.Length);
        //                            //----- replace the parmeter name with the parameter value in the sql statement
        //                            sSQL = sSqlLeft + sParVal + sSqlRight;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        if (dbConnection != null)
        //        {
        //            drQueries?.Close();
        //            dbConnection.Close();
        //            cmdQueries?.Dispose();
        //            drQueries?.Dispose();
        //        }
        //    }
        //    return sSQL;
        //}
    }
}
