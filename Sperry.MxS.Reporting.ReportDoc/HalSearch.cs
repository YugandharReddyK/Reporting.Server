using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc
{
    public class HalSearch
    {
        #region "Enums"
        public enum SearchResultMessage
        {
          
            Success = 0,
          
            Failed = 1
        }
        #endregion

        #region "Properties"

        public SearchResultMessage ResultMessage{get;set;}

        
        public string Message{get;set;}
        
        public Uri ReturnUri { get;set;}

        #endregion

        #region "Constructors"

        public HalSearch()
        {
        }

        #endregion
    }
}
