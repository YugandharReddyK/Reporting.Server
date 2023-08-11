using Sperry.MxS.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.ImportSurveys
{
    public class SurveyGrid<T> where T : IMxSSurveyValue
    {
        public SurveyGrid()
        {
            Rows = new List<SurveyRow<T>>();
            Columns = new List<string>();
        }

        public List<string> Columns { get; set; }

        public List<SurveyRow<T>> Rows { get; set; }

    }
}
