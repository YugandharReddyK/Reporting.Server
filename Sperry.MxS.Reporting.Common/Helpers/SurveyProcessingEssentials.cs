using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public class SurveyProcessingEssentials
    {

        public SurveyProcessingEssentials()
        {

        }
        
        public Well AssociatedWell { get; set; }
        
        public Run AssociatedRun { get; set; }
        
        public Solution AssociatedSolution { get; set; }
        
        public IList<CorrectedSurvey> ReferenceSurveys { get; set; }
        
        public IList<PlanSurvey> ReferencePlanSurveys { get; set; }
        
        public IList<CorrectedSurvey> SelectedSurveys { get; set; }
        
        public IList<PlanSurvey> SelectedPlanSurveys { get; set; }
        
        public IMxSProcessingOptions ProcessingOptions { get; set; }
    }
}
