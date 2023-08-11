using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class SavedQuery
    {
        private string _name;
        private string _sqlStatement;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        
        public string SqlStatement
        {
            get { return _sqlStatement; }
            set
            {
                _sqlStatement = value;
            }
        }
    }
}
