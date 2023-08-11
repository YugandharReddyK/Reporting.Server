using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.ImportSurveys
{
    public class ImportSurveyResult
    {
        public Guid ImportId { get; set; }

        public bool ShowPreview { get; set; }

        public SurveyGrid<ResultSurveyValue> ImportGridData { get; set; }

        public SurveyGrid<ResultSurveyValue> ResultsGridData { get; set; }

        public ImportSurveyResult()
        {
            ImportGridData = new SurveyGrid<ResultSurveyValue>();
            ResultsGridData = new SurveyGrid<ResultSurveyValue>();
        }
    }
}
