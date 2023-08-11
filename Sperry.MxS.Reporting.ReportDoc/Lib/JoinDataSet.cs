using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class JoinDataSet
    {
        #region "Private Variables"
        private static DataSet ds;
        private List<ColumnInfo> columnInfo;
        private string fieldList;
        #endregion

        #region "Properties"
        public DataSet DS
        {
            get { return ds; }
            private set { ds = value; }
        }

        public string ColumnList
        {
            get { return fieldList; }
            set { fieldList = value; }
        }
        #endregion
       
        #region "Constructors"
        public JoinDataSet()
        {
            ds = null;
        }

        public JoinDataSet(ref DataSet dataSet)
        {
            ds = dataSet;
        }
        #endregion

        #region "Public Methods"

        public DataTable CreateJoinTable(string tableName, DataTable sourceTable, string columnList)
        {
            if (columnList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
            }
            else
            {
                DataTable dt = new DataTable(tableName);
                ParseFieldList(columnList, true);

                foreach (ColumnInfo field in columnInfo)
                {
                    if (field.RelationName == null)
                    {
                        DataColumn dc = sourceTable.Columns[field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                    else
                    {
                        DataColumn dc = sourceTable.ParentRelations[field.RelationName].ParentTable.Columns[field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                }

                if (ds != null)
                {
                    ds.Tables.Add(dt);
                }

                return dt;
            }
        }

        
        public void InsertJoinInto(DataTable destTable, DataTable sourceTable, string columnList, string rowFilter, string sort)
        {
            if (columnList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
            }
            else
            {
                ParseFieldList(columnList, true);
                DataRow[] rows = sourceTable.Select(rowFilter, sort);

                foreach (DataRow row in rows)
                {
                    DataRow destRow = destTable.NewRow();

                    foreach (ColumnInfo col in columnInfo)
                    {
                        if (col.RelationName == null)
                        {
                            destRow[col.FieldName] = row[col.FieldName];
                        }
                        else
                        {
                            DataRow parentRow = row.GetParentRow(col.RelationName);
                            destRow[col.FieldName] = parentRow[col.FieldName];
                        }
                    }

                    destTable.Rows.Add(destRow);
                }
            }
        }

        public DataTable SelectJoinInto(string tableName, DataTable sourceTable, string columnList, string rowFilter, string sort)
        {
            DataTable dt = CreateJoinTable(tableName, sourceTable, columnList);
            InsertJoinInto(dt, sourceTable, columnList, rowFilter, sort);

            return dt;
        }

       
        public DataTable JoinParentToChild(string newTableName, string parentName, string childName, bool leftJoin)
        {
            if (string.IsNullOrEmpty(parentName) || string.IsNullOrEmpty(childName))
            {
                return null;
            }
            DataTable metadata = ds.Tables["meta_data"];

            DataColumnCollection dataColumnCollection = ds.Tables[parentName].Columns;
            DataColumn[] parentColumns = new DataColumn[dataColumnCollection.Count];
            dataColumnCollection.CopyTo(parentColumns, 0);

            dataColumnCollection = ds.Tables[parentName].Columns;
            DataColumn[] childColumns = new DataColumn[dataColumnCollection.Count];
            dataColumnCollection.CopyTo(childColumns, 0);

            DataTable parentDataTable = ds.Tables[parentName];
            DataTable childDataTable = ds.Tables[childName];

            DataTable newTable = new DataTable(newTableName);
            newTable.Namespace = newTableName;
            // Use a DataSet to leverage DataRelation

            //DataRow[] metadataRows = metadata.Select("table = '" + newTableName + "'");
            var metadataRows = (from rows in metadata.AsEnumerable()
                                where (parentDataTable.TableName.StartsWith("t1") || parentDataTable.TableName.StartsWith("t2")) &&
                                rows.Field<string>("table") == parentDataTable.TableName
                                select rows).ToList();

            if (metadataRows.Any())
            {
                for (int rowIndex = metadataRows.Count() - 1; rowIndex >= 0; --rowIndex)
                {
                    metadata.Rows.Remove(metadataRows[rowIndex]);
                }
            }

            //Create Columns for JOIN table
            for (int i = 0; i < parentDataTable.Columns.Count; i++)
            {
                DataColumn column = new DataColumn("t1." + parentDataTable.Columns[i].ColumnName, parentDataTable.Columns[i].DataType);
                column.Caption = "t1." + parentDataTable.Columns[i].Caption;
                column.Namespace = parentName;
                newTable.Columns.Add(column);

                var localMetadataRows = (from rows in metadata.AsEnumerable()
                                         where rows.Field<string>("table") == parentDataTable.TableName &&
                                               rows.Field<string>("field") == parentDataTable.Columns[i].ColumnName
                                         select rows);
                //metadataRows = metadata.Select("table = '" + parentDataTable.TableName + "' AND field = '" + parentDataTable.Columns[i].ColumnName + "'");
                if (localMetadataRows.Any())
                //if ((metadataRows != null) && (metadataRows.GetUpperBound(0) > -1))
                {
                    object[] newMetadataRowArray = localMetadataRows.First().ItemArray;

                    if (metadata.Columns.Contains("table"))
                        newMetadataRowArray[metadata.Columns.IndexOf("table")] = newTableName;
                    if (metadata.Columns.Contains("table_label"))
                        newMetadataRowArray[metadata.Columns.IndexOf("table_label")] = newTableName;
                    if (metadata.Columns.Contains("field"))
                        newMetadataRowArray[metadata.Columns.IndexOf("field")] = column.ColumnName;
                    if (metadata.Columns.Contains("label"))
                        newMetadataRowArray[metadata.Columns.IndexOf("label")] = column.Caption;
                    metadata.Rows.Add(newMetadataRowArray);
                }

            }

            for (int i = 0; i < childDataTable.Columns.Count; i++)
            {
                DataColumn column = null;
                //Beware Duplicates

                if (!newTable.Columns.Contains(childName + "." + childDataTable.Columns[i].ColumnName))
                {
                    column = new DataColumn("t2." + childDataTable.Columns[i].ColumnName, childDataTable.Columns[i].DataType);
                    column.Caption = "t2." + childDataTable.Columns[i].Caption;
                }
                else
                {
                    column = new DataColumn("t2." + childDataTable.Columns[i].ColumnName + "_1", childDataTable.Columns[i].DataType);
                    column.Caption = "t2." + childDataTable.Columns[i].Caption;
                }

                column.Namespace = childName;
                newTable.Columns.Add(column);

                var localMetadataRows = (from rows in metadata.AsEnumerable()
                                         where rows.Field<string>("table") == childDataTable.TableName &&
                                               rows.Field<string>("field") == childDataTable.Columns[i].ColumnName
                                         select rows);


                //  metadataRows = metadata.Select("table = '" + childDataTable.TableName + "' AND field = '" + childDataTable.Columns[i].ColumnName + "'");
                if (localMetadataRows.Any())
                {
                    object[] newMetadataRowArray = localMetadataRows.First().ItemArray;

                    if (metadata.Columns.Contains("table"))
                        newMetadataRowArray[metadata.Columns.IndexOf("table")] = newTableName;
                    if (metadata.Columns.Contains("table_label"))
                        newMetadataRowArray[metadata.Columns.IndexOf("table_label")] = newTableName;
                    if (metadata.Columns.Contains("field"))
                        newMetadataRowArray[metadata.Columns.IndexOf("field")] = column.ColumnName;
                    if (metadata.Columns.Contains("label"))
                        newMetadataRowArray[metadata.Columns.IndexOf("label")] = column.Caption;
                    metadata.Rows.Add(newMetadataRowArray);
                }
            }
            //Loop through First table
            newTable.BeginLoadData();

            DataRelation relation = ds.Relations[newTableName];
            foreach (DataRow dRow in ds.Tables[parentName].Rows)
            {
                //Get "joined" rows
                DataRow[] childrows = dRow.GetChildRows(relation);
                if (childrows.Count() > 0 && childrows.Length > 0)
                {
                    object[] parentarray = dRow.ItemArray;
                    foreach (DataRow secondrow in childrows)
                    {
                        object[] secondarray = secondrow.ItemArray;
                        object[] joinarray = new object[parentarray.Length + secondarray.Length];
                        Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                        Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);
                        newTable.LoadDataRow(joinarray, true);
                    }
                }
                else if (leftJoin)
                {
                    object[] parentarray = dRow.ItemArray;
                    object[] joinarray = new object[parentarray.Length];
                    Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                    newTable.LoadDataRow(joinarray, true);
                }
            }

            ds.Relations.Clear();

            newTable.EndLoadData();

            return newTable;
        }


        #endregion
       
        #region "Private Methods"
       
        private void ParseFieldList(string columnlist, bool allowrelation)
        {
            if (fieldList == columnlist)
            {
                return;
            }

            columnInfo = new List<ColumnInfo>();
            fieldList = columnlist;
            ColumnInfo column;
            string[] fieldParts;
            string[] fields = columnlist.Split(',');
            int i;

            for (i = 0; i <= fields.Length - 1; i++)
            {
                column = new ColumnInfo();

                // Split FieldAlias apart
                fieldParts = fields[i].Trim().Split(' ');

                switch (fieldParts.Length)
                {
                    case 1: 		// To be set at the end of the loop
                        break;
                    case 2:
                        column.FieldAlias = fieldParts[1];
                        break;
                    default:
                        throw new Exception("Too many spaces in field definition: '" + fields[i] + "'.");
                }

                // Split out the FieldName and RelationName
                fieldParts = fieldParts[0].Split('.');

                switch (fieldParts.Length)
                {
                    case 1:
                        column.FieldName = fieldParts[0];
                        break;
                    case 2:
                        if (allowrelation == false)
                        {
                            throw new Exception("Relation specifiers not permitted in field list: '" + fields[i] + "'.");
                        }

                        column.RelationName = fieldParts[0].Trim();
                        column.FieldName = fieldParts[1].Trim();
                        break;
                    default:
                        throw new Exception("Invalid field definition: " + fields[i] + "'.");
                }

                if (column.FieldAlias == null)
                {
                    column.FieldAlias = column.FieldName;
                }

                columnInfo.Add(column);
            }
        }
        #endregion

        #region "Internal Classes"
        private class ColumnInfo
        {
            private string relationName;
            private string fieldName;	//source table field name
            private string fieldAlias;	//destination table field name
            private string aggregate;

            public string RelationName
            {
                get { return relationName; }
                set { relationName = value; }
            }

            public string FieldName
            {
                get { return fieldName; }
                set { fieldName = value; }
            }

            public string FieldAlias
            {
                get { return fieldAlias; }
                set { fieldAlias = value; }
            }

            public string Aggregate
            {
                get { return aggregate; }
                set { aggregate = value; }
            }
        }
        #endregion

    }
}
