using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSSurveySubmissionErrorCodes
    {
        None,

        WellNotExisted,

        RunNotExisted,

        SurveyValuesInvalid,

        SurveyDepthInvalid,

        SolutionNotExisted,

        SurveyAlreadyExisted,

        SurveyIsDisabled,

        SaveSurveyFailed,

        Other
    }
}
