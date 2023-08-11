using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc
{
    public class HalQuery
    {
        #region "Properties"


        public IEnumerable<QueryTableColumn> TableDefinition { get;set; }

        public IEnumerable<QueryImage> ImageDefinition{get;set; }
        
        public IEnumerable<QueryConditionalExpressions> ConditionalExpressions { get; set; }
        
        public IEnumerable<QuerySubReport> SubReports{get;set; }
        #endregion

        #region "Constructors"

        public HalQuery()
        {
        }

        #endregion
    }
}
