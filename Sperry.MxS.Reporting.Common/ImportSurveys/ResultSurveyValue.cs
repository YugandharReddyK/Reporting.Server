using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.ImportSurveys
{
    public class ResultSurveyValue : IMxSSurveyValue
    {
        public object Value { get; set; }
        
        public string ToolTip { get; set; }
        
        public MxSImportState State { get; set; }
    }
}
