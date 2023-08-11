using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class TableGrp
    {
        private string aTableName;
        private string aggregate;

        public string TableName { get; set; }
        public List<TableGrpParam> TableGrpParams { get; set; }

        public class TableGrpParam
        {
            
            public MergeField ATableField { get; set; }
           
            public string Aggregate { get; set; }
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

        
        public string Aggregate
        {
            get { return aggregate; }
            set
            {
                if (value != aggregate)
                {
                    aggregate = value;
                }
            }
        }

        
        public TableGrp()
        {
            TableGrpParams = new List<TableGrpParam>();
        }
       
    }
}
