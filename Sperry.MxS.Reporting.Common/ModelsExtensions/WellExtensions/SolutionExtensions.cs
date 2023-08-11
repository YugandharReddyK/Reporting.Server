using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.Models.DynamicQC;
using Sperry.MxS.Core.Common.Models.Workbench.AziError;
using Sperry.MxS.Core.Common.ModelsExtensions.RulesEngineExtensions;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models
{
    public static class SolutionExtensions
    {
        public static DynamicQCInputParameters MapSolutionToDQCInputParameters(this Solution solution)
        {
            return new DynamicQCInputParameters()
            {
                Sigma = solution.Sigma ?? 0.0,
                AziType = solution.AziType,
                MSA = solution.MSA,
                DBxyz = solution.DBxyz ?? 0.0,
                DGxyz = solution.DGxyz ?? 0.0,
                DBzMod = solution.DBzMod ?? 0.0,
                DDipe = solution.DDipe ?? 0.0,
                DBe = solution.DBe ?? 0.0,
                DEC = solution.DEC ?? 0.0,
                DBH = solution.DBH ?? 0.0,
                DBNoise = solution.DBNoise ?? 0.0,
                DDipNoise = solution.DDipNoise ?? 0.0,
                Sxy = solution.Sxy ?? 0.0,
                MagneticReference = solution.MagneticReference,
                IPMToolCode = solution.IPMToolCode,
                NorthReference = solution.NorthReference,
            };
        }


        public static AziErrorParameters MapSolutionToAziErrorParameters(this Solution solution)
        {
            return new AziErrorParameters()
            {
                AziErrordDipe = solution.AziErrordDipe ?? 0.0, 
                AziErrordBe = solution.AziErrordBe ?? 0.0, 
                AziErrordBz = solution.AziErrordBz ?? 0.0, 
                AziErrorSxy = solution.AziErrorSxy ?? 0.0, 
                AziErrorBGm = solution.AziErrorBGm ?? 0.0

            };
        }

        public static bool SupportsPumpStatus(this Solution solution, MxSSurveyPumpStatus pumpStatus)
        {
            return (solution.AllowedPumpStatus & pumpStatus) == pumpStatus;
        }

        public static void DecideBeforeSetEndDepth(this Solution solution, double value, ref double previousWpEndDepth)
        {
            solution.ShowEnd = value == MxSConstant.MaximumDepth;
            if (solution.EndDepth != MxSConstant.MaximumDepth && solution.EndDepth != 0)
            {
                previousWpEndDepth = solution.EndDepth;
            }
        }

        public static void SetUseIcarusGZ(this Solution solution)
        {
            if (!new List<MxSSolutionService> { MxSSolutionService.MFMCazIca, MxSSolutionService.MFMIFRCazIca, MxSSolutionService.MFMIIFRCazIca }.Contains(solution.Service))
            {
                solution.UseIcarusGz = false;
            }
        }

        public static void LoadDynamicQCValuesFromSolutionService(this Solution solution)
        {
            if (solution.Mode == MxSDynamicQCModes.BasicMode || solution.Mode == MxSDynamicQCModes.AdvancedMode)
            {
                LoadDefaultDynamicQCValueFromSolutionProperties(solution);
            }
        }

        public static void LoadDefaultDynamicQCValueFromSolutionProperties(this Solution solution)
        {
            MxSSolutionService service = solution.Service;
            switch (service)
            {
                case MxSSolutionService.MFM:
                    solution.MagneticReference = MxSMagneticModelType.BGGM;
                    if (solution.SolutionType == MxSSolutionType.LongCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.SolutionType == MxSSolutionType.ShortCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.No;
                    break;
                case MxSSolutionService.MFMCaz:
                case MxSSolutionService.MFMCazIca:
                    solution.MagneticReference = MxSMagneticModelType.BGGM;
                    if (solution.CazandraSolution.ToString().Contains("L"))
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.CazandraSolution.ToString().Contains("S"))
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.Yes;
                    break;
                case MxSSolutionService.MFMIca:
                    solution.MagneticReference = MxSMagneticModelType.BGGM;
                    if (solution.SolutionType == MxSSolutionType.LongCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.SolutionType == MxSSolutionType.ShortCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.Yes;
                    break;
                case MxSSolutionService.MFMIFR:
                    solution.MagneticReference = MxSMagneticModelType.IFR1;
                    if (solution.SolutionType == MxSSolutionType.LongCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.SolutionType == MxSSolutionType.ShortCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.No;
                    break;
                case MxSSolutionService.MFMIFRCaz:
                case MxSSolutionService.MFMIFRCazIca:
                    solution.MagneticReference = MxSMagneticModelType.IFR1;
                    if (solution.CazandraSolution.ToString().Contains("L"))
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.CazandraSolution.ToString().Contains("S"))
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.Yes;
                    break;
                case MxSSolutionService.MFMIFRIca:
                    solution.MagneticReference = MxSMagneticModelType.IFR1;
                    if (solution.SolutionType == MxSSolutionType.LongCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.SolutionType == MxSSolutionType.ShortCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.Yes;
                    break;
                case MxSSolutionService.MFMIIFR:
                    solution.MagneticReference = MxSMagneticModelType.IFR2;
                    if (solution.SolutionType == MxSSolutionType.LongCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.SolutionType == MxSSolutionType.ShortCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.No;
                    break;
                case MxSSolutionService.MFMIIFRCaz:
                case MxSSolutionService.MFMIIFRCazIca:
                    solution.MagneticReference = MxSMagneticModelType.IFR2;
                    if (solution.CazandraSolution.ToString().Contains("L"))
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.CazandraSolution.ToString().Contains("S"))
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.Yes;
                    break;
                case MxSSolutionService.MFMIIFRIca:
                    solution.MagneticReference = MxSMagneticModelType.IFR2;
                    if (solution.SolutionType == MxSSolutionType.LongCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    else if (solution.SolutionType == MxSSolutionType.ShortCollar)
                    {
                        solution.AziType = MxSAzimuthTypeEnum.ShortCollar;
                    }
                    solution.MSA = MxSMSA.Yes;
                    break;
                default:
                    return;
            }
        }

        public static void ReadDynamicQCValuesFromXML(this Solution solution)
        {
            //TODO - Kiran - Need to be in a different class
            //todo: from Lijun why not use automapper for the below logic
            DynamicQCInputParameters dynamicQCParameters = DynamicQCParameterHelper.GetDynamicQCParamters(solution.MagneticReference, solution.AziType, solution.MSA);
            solution.IPMToolCode = dynamicQCParameters.IPMToolCode;
            solution.DGxyz = dynamicQCParameters.DGxyz;
            solution.DBxyz = dynamicQCParameters.DBxyz;
            solution.DBzMod = dynamicQCParameters.DBzMod;
            solution.DEC = dynamicQCParameters.DEC;
            solution.DBH = dynamicQCParameters.DBH;
            solution.DDipe = dynamicQCParameters.DDipe;
            solution.DBe = dynamicQCParameters.DBe;
            solution.Sxy = dynamicQCParameters.Sxy;
            solution.DBNoise = dynamicQCParameters.DBNoise;
            solution.DDipNoise = dynamicQCParameters.DDipNoise;
        }

        public static void FillDefaultDynamicQCParameter(this Solution solution)
        {
            solution.MagneticReference = MxSMagneticModelType.BGGM;
            solution.Mode = MxSDynamicQCModes.BasicMode;
            solution.Sigma = 2;
            solution.AziType = MxSAzimuthTypeEnum.LongCollar;
            solution.MSA = MxSMSA.No;
            solution.RunDynamicQC = false;
            //rick; values taken from the client RunSolutionPropertiesViewModel method AddSolution.
            solution.SolutionType = MxSSolutionType.ShortCollar;
            solution.QCType = MxSQCType.Static;
            solution.IsQCTypeEnabled = true;
            solution.InclinationSolution = MxSInclinationSolutionType.Raw;
            solution.StartTime = DateTime.Now;
            solution.EndTime = DateTime.Now;
            solution.IcarusStationsCount = 9;
            solution.ConsecutiveStation = 9;
            solution.Service = MxSSolutionService.MFM;
            solution.CazandraSolution = MxSCazandraSolution.AzimuthLongCollarMavRaw;
            solution.IcarusSolution = MxSIcarusSolution.RAW;
            solution.TheoreticGravity = 1;
        }

        public static List<CorrectedSurvey> GetCorrectedSurveys(this Solution solution, bool includeShorSurveys = false) //allCorrectedSurveys-> Full and Short surveys
        {
            return solution.Run.GetCorrectedSurveysForSolution(solution, includeShorSurveys);
        }

        public static void AddPlanSurvey(this Solution solution, PlanSurvey planSurvey)
        {
            planSurvey.Solution = solution;
            solution.PlanSurveys.Add(planSurvey);
        }

        public static void DeletePlanSurvey(this Solution solution, PlanSurvey planSurvey)
        {
            if (solution.PlanSurveys.Remove(planSurvey))
            {
                planSurvey.Solution = null;
            }
        }

        public static void AddMaxSurveyRule(this Solution solution, MaxSurveyRule rule)
        {
            rule.Solution = solution;
            MaxSurveyPreConditionRule nonePreConditionRule = solution.ASAPreconditionRules.FirstOrDefault(p => p.RuleName.Equals("None"));
            if (nonePreConditionRule == null)
            {
                AddMaxSurveyPreConditionRule(solution, new MaxSurveyPreConditionRule() { RuleName = "None", LHS = "1", Operator = MxSOperatorsEnum.Equals, RHS = "1", IsValidRule = true });
            }
            if (rule.PreConditionRule == null)
            {
                rule.PreConditionRule = solution.ASAPreconditionRules.FirstOrDefault(p => p.RuleName.Equals("None"));
            }
            solution.ASARules.Add(rule);
        }

        public static void DeleteMaxSurveyRule(this Solution solution, MaxSurveyRule rule)
        {
            if (solution.ASARules.Remove(rule))
            {
                rule.Solution = null;
                rule.PreConditionMapId = Guid.Empty;
            }
        }

        public static void AddMaxSurveyPreConditionRule(this Solution solution, MaxSurveyPreConditionRule rule)
        {
            rule.Solution = solution;
            solution.ASAPreconditionRules.Add(rule);
        }

        public static void DeleteMaxSurveyPreConditionRule(this Solution solution, MaxSurveyPreConditionRule rule)
        {
            if (solution.ASAPreconditionRules.Remove(rule))
            {
                rule.Solution = null;
            }
        }

        public static bool AreDepthsValid(this Solution solution)
        {
            if (solution.EndDepth > solution.StartDepth)
            {
                solution.IsStartDepthValid = true;
                solution.IsEndDepthValid = true;
                return true;
            }
            solution.IsStartDepthValid = false;
            solution.IsEndDepthValid = false;
            return false;
        }

        public static Waypoint GetIFRWaypoint(this Solution solution)
        {
            if (solution.Run == null || solution.Run.Well == null)
            {
                return null;
            }
            var waypoint = solution.Run.Well.Waypoints.FirstOrDefault(p =>
                p.State != MxSState.Deleted &&
                p.StartDepth <= solution.StartDepth &&
                p.EndDepth >= solution.EndDepth &&
                p.Type == MxSWaypointType.IFR1
            );
            if (waypoint == null)
            {
                waypoint = solution.Run.Well.Waypoints.FirstOrDefault(p =>
                    (p.State != MxSState.Deleted && p.EndDepth >= solution.StartDepth && p.EndDepth <= solution.EndDepth && p.Type == MxSWaypointType.IFR1) ||
                    (p.State != MxSState.Deleted && p.StartDepth >= solution.StartDepth && p.StartDepth <= solution.EndDepth && p.Type == MxSWaypointType.IFR1));
            }
            return waypoint;
        }

        public static bool IsDepthInRange(this Solution solution, double depth)
        {
            return depth >= solution.StartDepth && depth < solution.EndDepth;
        }

        public static bool IsValuesEquals(this Solution solution, Solution otherSolution)
        {
            return solution.Run?.RunNumber == otherSolution.Run?.RunNumber &&
                    solution.Run?.SerialNumber == otherSolution.Run?.SerialNumber &&
                    solution.Run?.PrimaryOrSecondary == otherSolution.Run?.PrimaryOrSecondary &&
                    solution.StartDepth.CompareDouble(otherSolution.StartDepth) &&
                    solution.EndDepth.CompareDouble(otherSolution.EndDepth) &&
                    solution.Service == otherSolution.Service &&
                    solution.InclinationSolution == otherSolution.InclinationSolution &&
                    solution.SolutionType == otherSolution.SolutionType &&
                    solution.PumpStatusFilter == otherSolution.PumpStatusFilter &&
                    solution.AllowedPumpStatus == otherSolution.AllowedPumpStatus;
        }

        private static double Rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static double CalculateDistance(this Solution solution, double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLat2 = Rad(lat2);
            double a = radLat1 - radLat2;
            double b = Rad(lng1) - Rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                                               Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * MxSConstants.EarthRadius;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        public static void CopyRules(this Solution solution, Solution solutionToCopy)
        {
            Dictionary<Guid, Guid> preConditionIds = new Dictionary<Guid, Guid>();
            foreach (var asaRule in solutionToCopy.ASAPreconditionRules)
            {
                var asaRuleToCopy = new MaxSurveyPreConditionRule(asaRule);
                asaRuleToCopy.Solution = solution;
                solution.ASAPreconditionRules.Add(asaRuleToCopy);
                preConditionIds.Add(asaRule.PreConditionMapId, asaRuleToCopy.Id);
            }

            foreach (var asaRule in solutionToCopy.ASARules)
            {
                var asaRuleToCopy = new MaxSurveyRule(asaRule) { Solution = solution };
                if (preConditionIds.ContainsKey(asaRule.PreConditionMapId))
                {
                    asaRuleToCopy.PreConditionRule = solution.ASAPreconditionRules.FirstOrDefault(p => p.Id == preConditionIds[asaRule.PreConditionMapId]);
                }
                else
                {
                    asaRuleToCopy.PreConditionRule = null;
                }

                solution.ASARules.Add(asaRuleToCopy);
            }
        }

        //public override int GetHashCode()
        //{
        //    int hash = 23;
        //    hash = hash * 31 + Id.GetHashCode();
        //    hash = hash * 31 + StartDepth.GetHashCode();
        //    hash = hash * 31 + EndDepth.GetHashCode();
        //    hash = hash * 31 + PumpStatusFilter.GetHashCode();
        //    return hash;
        //}

        #region Related to State

        // TODO: Suhail - Related to state
        //public override void ResetState()
        //{
        //    base.ResetState();
        //    if (ObservatoryStation != null)
        //    {
        //        ObservatoryStation.ResetState();
        //    }

        //    if (ASARules != null && ASAPreconditionRules != null)
        //    {
        //        foreach (MaxSurveyRule rule in ASARules)
        //        {
        //            rule.PreConditionRule = ASAPreconditionRules.FirstOrDefault(r => r.PreConditionMapId == rule.PreConditionMapId);
        //        }
        //    }
        //}

        //public static void SetStateAsModified()
        //{
        //    if (State != State.Added && State != State.Deleted)
        //        SetState(State.Modified);
        //}

        #endregion

        public static void AddForeignKey(this Solution solut, Run run)
        {          
            if (run.Solutions.Any() && run.Solutions.Count > 0)
            {
                foreach (var solution in run.Solutions)
                {
                    solution.RunId = run.Id;
                    if (solution.ASAPreconditionRules.Any() && solution.ASAPreconditionRules.Count > 0)
                    {
                        foreach (var rule in solution.ASAPreconditionRules)
                        {
                            rule.AddForeignKey(solution);
                        }
                    }
                    if (solution.ASARules.Any() && solution.ASARules.Count > 0)
                    {
                        foreach (var asarule in solution.ASARules)
                        {
                            asarule.AddForeignKey(solution);
                        }
                    }
                    if (solution.ObservatoryStation != null)
                    {
                        solution.ObservatoryStationId = solution.ObservatoryStation.Id;
                    }
                    if (solution.PlanSurveys.Any() && solution.PlanSurveys.Count > 0)
                    {
                        foreach (var plansurvey in solution.PlanSurveys)
                        {
                            plansurvey.AddForeignKey(solution);
                        }
                    }
                }
            }
        }
    }
}
