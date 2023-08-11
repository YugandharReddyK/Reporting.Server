using Sperry.MxS.Core.Common.Enums;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class PumpStatusFilterExtensions
    {
        public static MxSSurveyPumpStatus AllowedPumpStatus(this MxSPumpStatusFilter filter)
        {
            switch (filter)
            {
                case MxSPumpStatusFilter.All:
                    return MxSSurveyPumpStatus.NA | MxSSurveyPumpStatus.Off | MxSSurveyPumpStatus.On;
                case MxSPumpStatusFilter.On:
                    return MxSSurveyPumpStatus.On;
                case MxSPumpStatusFilter.Off:
                    return MxSSurveyPumpStatus.Off;
                case MxSPumpStatusFilter.NAandOff:
                    return MxSSurveyPumpStatus.NA | MxSSurveyPumpStatus.Off;
                default:
                    return MxSSurveyPumpStatus.NA | MxSSurveyPumpStatus.Off | MxSSurveyPumpStatus.On;
            }
        }
    }

}
