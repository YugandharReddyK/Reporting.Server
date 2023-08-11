using Sperry.MxS.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.ImportSurveys
{
    public class SurveyRow<T> : IMxSSurveyRow where T : IMxSSurveyValue
    {
        public SurveyRow()
        {
            Values = new List<T>();
        }

        public List<T> Values { get; set; }
    }
}
