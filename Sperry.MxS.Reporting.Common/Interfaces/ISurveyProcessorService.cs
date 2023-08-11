using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.Models.Workbench;
using System.Collections.Generic;
using System.Threading;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface ISurveyProcessorService
    {
        void ProcessSurveys(IList<CorrectedSurvey> correctedSurveys, IMxSProcessingOptions processingOptions, CancellationTokenSource cancellationTokenSource = null);
        
        void ProcessSurveys(IList<RawSurvey> rawSurveys, IMxSProcessingOptions processingOptions, CancellationTokenSource cancellationTokenSource = null);
        
        WorkBenchCalculationResult ProcessWorkbenchResult(IList<CorrectedSurvey> correctedSurveys, SurveyProcessingEssentials processingEssentials, MxSWorkbenchCalculationType calculationType);
        
        WorkBenchCalculationResult ProcessWorkbenchResultForPlanSurvey(IList<PlanSurvey> planSurveys, SurveyProcessingEssentials processingEssentials, MxSWorkbenchCalculationType calculationType);
        
        CorrectedSurvey RunASAAction(IList<CorrectedSurvey> correctedSurveys);

        CorrectedSurvey CalculateUncertaintyValues(IList<CorrectedSurvey> correctedSurveys);
    }
}
