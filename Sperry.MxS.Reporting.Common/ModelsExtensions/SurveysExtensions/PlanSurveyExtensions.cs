using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.Workbench.AziError;
using Sperry.MxS.Core.Common.Models.Workbench.AziError.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    public static class PlanSurveyExtensions
    {
        public static AziErrorInput MapPlanSurveyToAziErrorInput(this PlanSurvey planSurvey)
        {
            return new AziErrorInput()
            {
                Depth = planSurvey.Depth,
                DateTime = planSurvey.DateTime,
                AziErrorBe = planSurvey.AziErrorBe,
                AziErrorDipe = planSurvey.AziErrorDipe,
                AziErrordDecle = planSurvey.AziErrordDecle,
                AziErrorAzimuth = planSurvey.AziErrorAzimuth,
                AziErrorInclination = planSurvey.AziErrorInclination,
                AziErrorAzimuthMagnetic = planSurvey.AziErrorAzimuthMagnetic
            };
        }

        public static Survey MapPlanSurveyToSurvey(this PlanSurvey planSurvey)
        {
            return new Survey()
            {
                DateTime = planSurvey.DateTime,
                Depth = planSurvey.Depth,
                MWDInclination = planSurvey.MWDInclination ?? 0.0,
                MWDShortCollar = planSurvey.MWDShortCollar ?? 0.0,
                AziErrorAzimuth = planSurvey.AziErrorAzimuth,
                AziErrorInclination = planSurvey.AziErrorInclination,
                AziErrorBe = planSurvey.AziErrorBe,
                AziErrordDecle = planSurvey.AziErrordDecle,
                AziErrorDipe = planSurvey.AziErrorDipe,
                AziErrorAzimuthMagnetic = planSurvey.AziErrorAzimuthMagnetic,
                WellId = planSurvey.Solution?.Run?.Well?.Id ?? Guid.Empty,
                RunId = planSurvey.Solution?.Run?.Id ?? Guid.Empty,
                Run = planSurvey.Solution?.Run.ToString(),
                Id = planSurvey.Id,
            };
        }

        public static CorrectedSurvey MapPlanSurveyToCorrectedSurvey(this PlanSurvey planSurvey)
        {
            return new CorrectedSurvey()
            {
                DateTime = planSurvey.DateTime,
                Depth = planSurvey.Depth,
                MWDInclination = planSurvey.MWDInclination,
                MWDShortCollar = planSurvey.MWDShortCollar,
                AziErrorAzimuth = planSurvey.AziErrorAzimuth,
                AziErrorInclination = planSurvey.AziErrorInclination,
                NomBt = planSurvey.NomBt,
                NomDeclination = planSurvey.NomDeclination,
                NomDip = planSurvey.NomDip,
                NomGrid = planSurvey.NomGrid,
                AziErrorBe = planSurvey.AziErrorBe,
                AziErrorDecle = planSurvey.AziErrorDecle,
                AziErrorConve = planSurvey.AziErrorConve,
                AziErrordDecle = planSurvey.AziErrordDecle,
                AziErrorDipe = planSurvey.AziErrorDipe,
                AziErrorAzimuthMagnetic = planSurvey.AziErrorAzimuthMagnetic,
                SolInc = planSurvey.SolInc,
                SolAzm = planSurvey.SolAzm,
                Deleted = planSurvey.Deleted,
                CreatedBy = planSurvey.CreatedBy,
                CreatedTime = planSurvey.CreatedTime,
                Id = planSurvey.Id,
            };
        }

        public static MxSCalculationType GetCalculationType(this PlanSurvey planSurvey)
        {
            if (planSurvey.Solution != null)
            {
                const int ifr1Flag = 2;
                const int ifr2Flag = 4;
                // if (CheckFlag(planSurvey.Solution.Service, ifr2Flag))  Modified by Naveen Kumar
                if (planSurvey.CheckFlag(planSurvey.Solution.Service, ifr2Flag))
                {
                    return MxSCalculationType.IIFR;
                }
                else if (planSurvey.CheckFlag(planSurvey.Solution.Service, ifr1Flag))
                {
                    return MxSCalculationType.IFR;
                }
            }
            return MxSCalculationType.MFM;
        }

        private static bool CheckFlag(this PlanSurvey planSurvey, MxSSolutionService service, int flag)
        {
            return ((int)service & flag) > 0;
        }

        public static Waypoint GetRelateWaypoint(this PlanSurvey planSurvey, MxSWaypointType waypointType)
        {
            //var waypoints = GetWayPoints(planSurvey);   Modified by Naveen Kumar
            var waypoints = planSurvey.GetWayPoints();
            if (waypoints == null)
            {
                return null;
            }
            return waypoints.FirstOrDefault(
                wp =>
                    wp.StartDepth <= planSurvey.Depth &&
                    wp.EndDepth > planSurvey.Depth &&
                    wp.State != MxSState.Deleted &&
                    wp.Type == waypointType);
        }



        public static List<Waypoint> GetWayPoints(this PlanSurvey planSurvey)
        {
            // var well = GetWell(planSurvey);   Modified by Naveen Kumar
            var well = planSurvey.GetWell();
            if (well == null)
            {
                return null;
            }
            return well.Waypoints;
        }

        public static Well GetWell(this PlanSurvey planSurvey)
        {
            //var run = GetRun(planSurvey); Modified by Naveen Kumar 
            var run = planSurvey.GetRun();
            if (run == null)
            {
                return null;
            }
            return run.Well;
        }

        public static Run GetRun(this PlanSurvey planSurvey)
        {
            if (planSurvey.Solution == null || planSurvey.Solution.Run == null)
            {
                return null;
            }
            return planSurvey.Solution.Run;
        }
        public static void AddForeignKey(this PlanSurvey planSurvey, Solution solution)
        {
            planSurvey.SolutionId = solution.Id;
        }
    }
}
