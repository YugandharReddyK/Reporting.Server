using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Linq;

namespace Sperry.MxS.Core.Common.Helpers
{
    public class WellPropertyFinder
    {
        public static Waypoint GetRelateWaypoint(CorrectedSurvey correctedSurvey, SurveyProcessingEssentials processingEssentials, MxSWaypointType waypointType)
        {
            if (processingEssentials.AssociatedSolution == null || processingEssentials.AssociatedRun == null || processingEssentials.AssociatedWell == null || processingEssentials.AssociatedWell.Waypoints == null)
            {
                return null;
            }

            return processingEssentials.AssociatedWell.Waypoints.FirstOrDefault((Waypoint wp) => wp.StartDepth <= correctedSurvey.Depth && wp.EndDepth > correctedSurvey.Depth && wp.Type == waypointType);
        }

        public static bool TryGetIFRWaypoint(CorrectedSurvey correctedSurvey, SurveyProcessingEssentials processingEssentials, out Waypoint waypoint, int flag)
        {
            waypoint = GetIFRWaypoint(correctedSurvey, processingEssentials, flag);
            return waypoint != null;
        }

        public static Waypoint GetIFRWaypoint(CorrectedSurvey correctedSurvey, SurveyProcessingEssentials processingEssentials, int flag)
        {
            if (processingEssentials.AssociatedWell.Waypoints == null)
            {
                throw new Exception("waypoints");
            }

            if (correctedSurvey.Depth <= 0.0)
            {
                throw new Exception("Depth must bigger than zero.");
            }

            Waypoint relateWaypoint = GetRelateWaypoint(correctedSurvey, processingEssentials, MxSWaypointType.IFR1);
            if (relateWaypoint == null)
            {
                if (CheckFlag(processingEssentials.AssociatedSolution.Service, flag))
                {
                    throw new Exception("Survey cannot be processed as usable IFR1 waypoint was not found. Add IFR1 waypoint to process the survey.");
                }

                return null;
            }

            return relateWaypoint;
        }

        public static bool CheckFlag(MxSSolutionService service, int flag)
        {
            return (int)((uint)service & (uint)flag) > 0;
        }
    }

}
