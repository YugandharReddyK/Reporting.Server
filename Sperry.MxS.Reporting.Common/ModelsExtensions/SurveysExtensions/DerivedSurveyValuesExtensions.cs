using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    public static class DerivedSurveyValuesExtensions
    {
        public static Waypoint GetRelateWaypoint(this DerivedSurveyValues values)
        {
            //Rick, this is going to throw null reference excpetion when the ctor creates a new solution.
            //the solution will not have any of the properties which are being accessed.
            var waypoint = values.CorrectedSurvey.Run.Well.Waypoints.FirstOrDefault(
                wp =>
                    wp.StartDepth <= values.CorrectedSurvey.RawSurvey.Depth &&
                    wp.EndDepth > values.CorrectedSurvey.RawSurvey.Depth);
            return waypoint;
        }
    }
}
