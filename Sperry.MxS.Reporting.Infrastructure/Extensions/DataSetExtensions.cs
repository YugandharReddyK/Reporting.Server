using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Infrastructure.Extensions
{
    public static class DataSetExtensions
    {
        #region "Public Methods"

        public static bool DataTableExists(this DataSet dataset, string tableName)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException("value");
            }

            if (dataset.Tables.Count == 0)
            {
                return false;
            }

            if (dataset.Tables.Contains(tableName))
            {
                return true;
            }

            return false;
        }

        public static DataTable GetDataTable(this DataSet dataset, string tableName)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException("value");
            }

            if (dataset.Tables.Count == 0)
            {
                return null;
            }

            if (dataset.Tables.Contains(tableName))
            {
                return dataset.Tables[tableName];
            }

            return null;
        }

        public static DataColumnCollection GetColumns(this DataSet dataset, string tableName)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException("value");
            }

            if (dataset.Tables.Count == 0)
            {
                return null;
            }

            if (dataset.Tables.Contains(tableName))
            {
                return dataset.Tables[tableName].Columns;
            }

            return null;
        }
        #endregion

    }
}
