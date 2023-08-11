using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Infrastructure.ReportingDatabase.Lib
{
    public class SqLiteDatabase
    {
        String dbConnection;
        private SQLiteConnection cnn;
        private SQLiteCommand mycommand;

         
        public SqLiteDatabase(bool enableLog)
        {
            dbConnection = "Data Source=ReportDb.s3db";
            Connect(enableLog);
        }

        
        public SqLiteDatabase(String inputFile, bool enableLog)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
            Connect(enableLog);
        }
  
        public SqLiteDatabase(Dictionary<String, String> connectionOpts, bool enableLog)
        {
            String str = "";
            foreach (KeyValuePair<String, String> row in connectionOpts)
            {
                str += String.Format("{0}={1}; ", row.Key, row.Value);
            }
            str = str.Trim().Substring(0, str.Length - 1);
            dbConnection = str;
            Connect(enableLog);
        }

        private void Connect(bool enableLog)
        {
            int connectCount = 10;
            bool isConnected = false;

            while (!isConnected || connectCount == 0)
            {
                try
                {
                    cnn = new SQLiteConnection(dbConnection);
                    cnn.Open();
                    mycommand = new SQLiteCommand(cnn);
                }
                catch (Exception exception)
                {
                    --connectCount;

                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);
                }

                isConnected = true;
            }
        }

         
        public DataTable GetDataTable(string sql, bool enableLog)
        {
            DataTable dt = new DataTable();
            try
            {
                mycommand.CommandText = sql;
                SQLiteDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                throw new Exception(exception.Message);
            }
            return dt;
        }

         
        public int ExecuteNonQuery(string sql)
        {
            //SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            //cnn.Open();
            SQLiteCommand mmycommand = new SQLiteCommand(cnn);
            mmycommand.CommandText = sql;
            int rowsUpdated = mmycommand.ExecuteNonQuery();
            //cnn.Close();
            return rowsUpdated;
        }

       
        public string ExecuteScalar(string sql)
        {
            //SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            //cnn.Open();
            //SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            object value = mycommand.ExecuteScalar();
            //cnn.Close();
            if (value != null)
            {
                return value.ToString();
            }
            return "";
        }

         
        public bool Update(String tableName, Dictionary<String, String> data, String where, bool enableLog)
        {
            String vals = "";
            Boolean returnCode = true;
            if (data.Count >= 1)
            {
                foreach (KeyValuePair<String, String> val in data)
                {
                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
                }
                vals = vals.Substring(0, vals.Length - 1);
            }
            try
            {
                this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                returnCode = false;
            }
            return returnCode;
        }

         
        public bool Delete(String tableName, String where, bool enableLog)
        {
            Boolean returnCode = true;
            try
            {
                this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                returnCode = false;
            }
            return returnCode;
        }

         
        public bool Insert(String tableName, Dictionary<String, String> data, bool enableLog)
        {
            String columns = "";
            String values = "";
            Boolean returnCode = true;
            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key.ToString());
                values += String.Format(" '{0}',", val.Value.Replace("'", "''"));
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            try
            {
                this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                returnCode = false;
            }
            return returnCode;
        }

       
        public bool ClearDB(bool enableLog)
        {
            DataTable tables;
            try
            {
                tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;", enableLog);
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString(), enableLog);
                }
                return true;
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                return false;
            }
        }

      
        public bool DropTables(bool enableLog)
        {
            DataTable tables;
            try
            {
                ExecuteNonQuery("BEGIN TRANSACTION");

                tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;", enableLog);
                Parallel.ForEach(tables.AsEnumerable(), table =>
                {
                    try
                    {
                        DropTable(table["NAME"].ToString(), enableLog);
                    }
                    catch (Exception exception)
                    {
                        //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                        throw;
                    }
                });

                //foreach (DataRow table in tables.Rows)
                //{
                //    DropTable(table["NAME"].ToString());
                //}

                ExecuteNonQuery("END TRANSACTION");

                return true;
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                return false;
            }
        }

       
        public bool DropTable(string table, bool enableLog)
        {
            try
            {

                this.ExecuteNonQuery(String.Format("drop table {0};", table));
                return true;
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                return false;
            }
        }

        
        public bool ClearTable(String table, bool enableLog)
        {
            try
            {

                this.ExecuteNonQuery(String.Format("delete from {0};", table));
                return true;
            }
            catch (Exception exception)
            {
                //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                return false;
            }
        }
    }
}
