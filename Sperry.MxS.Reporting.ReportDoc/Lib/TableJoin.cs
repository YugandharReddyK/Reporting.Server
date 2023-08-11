using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class TableJoin
    {
        private string aTableName;
        private string bTableName;
        private bool isLeftJoin;

        
        public string TableName { get; set; }
        
        public List<TableJoinParam> TableJoinParams { get; set; }

        
        public class TableJoinParam
        {
           
            public MergeField ATableField { get; set; }
           
            public MergeField BTableField { get; set; }
        }

        public string ATableName
        {
            get { return aTableName; }
            set
            {
                if (value != aTableName)
                {
                    aTableName = value;
                }
            }
        }

        public string BTableName
        {
            get { return bTableName; }
            set
            {
                if (value != bTableName)
                {
                    bTableName = value;
                }
            }
        }

       
        public bool IsLeftJoin
        {
            get { return isLeftJoin; }
            set
            {
                if (value != isLeftJoin)
                {
                    isLeftJoin = value;
                }
            }
        }

        
        public TableJoin()
        {
            TableJoinParams = new List<TableJoinParam>();
        }
    }
}
