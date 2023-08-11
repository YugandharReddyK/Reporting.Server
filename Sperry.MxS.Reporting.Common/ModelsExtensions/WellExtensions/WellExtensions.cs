using System.Linq.Expressions;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.Models.Odisseus;
using Sperry.MxS.Core.Common.Models.CoordSys;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models
{
    public static class WellExtensions
    {
        //Rick: hack method to add missing observatory readings to support IFR2 calcs.
        //Added to support code in the WaitObservatoryProcessor.cs for the MaxSurvey.Server.ObservatoryService
        //The well object is not the place to be storing observatory readings to support calcualtions.
        public static void AddObservatoryReadings(this Well well, Guid observatoryStationId, List<ObservatoryReading> observatoryReadings)
        {
            if (!well.Observatories.ContainsKey(observatoryStationId))
            {
                well.Observatories.Add(observatoryStationId, new List<ObservatoryReading>());
            }

            var readings = well.Observatories[observatoryStationId];
            var existingReadingDateHash = new HashSet<DateTime>(readings.Select(i => i.DateTime));
            foreach (var observatoryReading in observatoryReadings)
            {
                if (!existingReadingDateHash.Contains(observatoryReading.DateTime))
                {
                    readings.Add(observatoryReading);
                }
            }
        }

        public static void ReplaceRun(this Well well, Run existingRun, Run newRun)
        {
            well.Runs.Replace(existingRun, newRun);
        }

        public static void UpdateScaleFactorValue(this Well well, double? previousScaleValue)
        {
            if (!well.OverrideGeodeticScaleFactor)
            {
                if (well.GeodeticScaleFactorValue.Equals(previousScaleValue))
                    return;
                well.GeodeticScaleFactorValue = previousScaleValue;
            }
        }

        public static TimeSpan? GetAverageTimeSpanForWell(this Well well)
        {
            long averageTicks = 0;
            TimeSpan? averageTimeSpan = null;
            if (well.Runs.Any())
            {
                var ticks = well.Runs.Where(r => r.AverageReturnTimeForRun.HasValue).Select(ts => ts.AverageReturnTimeForRun.Value.Ticks);
                if (ticks.Any())
                {
                    averageTicks = (long)ticks.Average();
                }
                averageTimeSpan = TimeSpan.FromTicks(averageTicks);
            }
            return averageTimeSpan;
        }

        public static void AddRun(this Well well, Run run)
        {
            run.Well = well;
            well.Runs.Add(run);
            // ReSharper disable once ExplicitCallerInfoArgument
        }

        public static bool DeleteRun(this Well well, Run run)
        {
            run.Well = null;
            return well.Runs.Remove(run);
        }

        public static void AddWaypoint(this Well well, Waypoint waypoint)
        {
            waypoint.Well = well;
            well.Waypoints.Add(waypoint);
            // ReSharper disable once ExplicitCallerInfoArgument
        }

        public static bool DeleteWaypoint(this Well well, Waypoint waypoint)
        {
            //waypoint.Well = null;
            return well.Waypoints.Remove(waypoint);
        }

        public static bool DeleteOdisseusToolCodeSection(this Well well, OdisseusToolCodeSection odisseusToolCodeSection)
        {
            odisseusToolCodeSection.Well = null;
            return well.OdisseusToolCodeSections.Remove(odisseusToolCodeSection);
        }

        public static Run GetRun(this Well well, string runNumber)
        {
            return well.Runs.Where(run => run.State != MxSState.Deleted && string.Compare(run.RunNumber, runNumber, StringComparison.OrdinalIgnoreCase) == 0)
                .FirstOrDefault();
        }

        //moved external domain logic back into domain object.
        public static List<CorrectedSurvey> GetCorrectedSurveys(this Well well)
        {
            List<CorrectedSurvey> correctedSurveys = new List<CorrectedSurvey>();
            foreach (Run run in well.Runs)
            {
                correctedSurveys.AddRange(run.GetCorrectedSurveys());
            }
            return correctedSurveys.OrderBy(a => a.Depth).ThenBy(a => a.DateTime).ToList();
        }

        public static CorrectedSurvey GetPreviousSurvey(this Well well, CorrectedSurvey survey, bool skipExcludedIgnoredSurveys = false, bool skipRotationCheckShot = false, bool includeOnlyProcessesSurvey = false)
        {
            List<CorrectedSurvey> allCorrectedSurveys = well.GetCorrectedSurveys();
            if (allCorrectedSurveys != null && allCorrectedSurveys.Count > 0)
            {
                int correctedSurveyIndex = GetNearestIndex(allCorrectedSurveys, survey);
                if (correctedSurveyIndex > 0)
                {
                    for (int i = correctedSurveyIndex - 1; i >= 0; i--)
                    {
                        if (skipRotationCheckShot && allCorrectedSurveys[i].SurveyType == MxSSurveyType.RotCheckshot)
                            continue;
                        if (skipExcludedIgnoredSurveys && allCorrectedSurveys[i].IsIgnoreOrExcludeSurvey())
                            continue;
                        if (includeOnlyProcessesSurvey && (allCorrectedSurveys[i].SurveyStatus != MxSSurveyStatus.Processed || allCorrectedSurveys[i].SurveyStatus != MxSSurveyStatus.BackCorrected))
                            continue;
                        return allCorrectedSurveys[i];
                    }
                }
            }
            return null;
        }

        public static CorrectedSurvey GetPreviousDefinitiveSurvey(this Well well, CorrectedSurvey survey)
        {
            List<CorrectedSurvey> allCorrectedSurveys = well.GetCorrectedSurveys();
            if (allCorrectedSurveys != null && allCorrectedSurveys.Count > 0)
            {
                int correctedSurveyIndex = GetNearestIndex(allCorrectedSurveys, survey);
                if (correctedSurveyIndex > 0)
                {
                    for (int i = correctedSurveyIndex - 1; i >= 0; i--)
                    {
                        if (allCorrectedSurveys[i].SurveyType == MxSSurveyType.Definitive)
                            return allCorrectedSurveys[i];
                    }
                }
            }
            return null;
        }

        public static CorrectedSurvey GetPreviousSurveyIncludingShortSurvey(this Well well, CorrectedSurvey correctedSurvey, bool skipExcludedIgnoredSurveys = false, bool skipRotationCheckShot = false, bool includeOnlyProcessesSurvey = false)
        {
            CorrectedSurvey previousSurvey = well.GetPreviousSurvey(correctedSurvey, skipExcludedIgnoredSurveys, skipRotationCheckShot, includeOnlyProcessesSurvey);
            return well.HandleShortSurveysForPreviousSurvey(correctedSurvey, previousSurvey);
        }

        public static CorrectedSurvey GetPreviousDefinitiveSurveyIncludingShortSurvey(this Well well, CorrectedSurvey correctedSurvey)
        {
            CorrectedSurvey previousSurvey = well.GetPreviousDefinitiveSurvey(correctedSurvey);
            return well.HandleShortSurveysForPreviousSurvey(correctedSurvey, previousSurvey);
        }

        public static List<ShortSurvey> GetShortSurveys(this Well well)
        {
            var shortSurvey = new List<ShortSurvey>();
            foreach (var run in well.Runs)
            {
                shortSurvey.AddRange(run.GetAllShortSurveys());
            }
            return shortSurvey.OrderBy(a => a.Depth).ThenBy(a => a.DateTime).ToList();
        }

        public static Run CreateRun(this Well well, string runNumber = MxSConstant.DefaultRunNumber, MxSPrimaryOrSecondary primaryOrSecondary = MxSPrimaryOrSecondary.Primary, MxSToolTypeOptions toolType = MxSToolTypeOptions.DM)
        {
            Run run = well.GetRun(runNumber);
            if (run == null)
            {
                run = new Run();
                run.RunNumber = runNumber;
                run.PrimaryOrSecondary = primaryOrSecondary;
                run.ToolType = toolType;
                run.StartDepth = 0;
                run.EndDepth = MxSConstant.MaximumDepth;
                run.ShowEnd = true;
                well.AddRun(run);
            }
            return run;
        }

        public static Waypoint CreateEmptyDefaultWaypoint(this Well well)
        {
            Waypoint wayPoint = new Waypoint
            {
                Well = well,
                Source = "Operator",
                Type = MxSWaypointType.MFM,
                EndDepth = MxSConstant.MaximumDepth,
                ShowEnd = true,
                Elevation = well.Elevation,
                Latitude = well.Latitude,
                Longitude = well.Longitude,
                CalculatedDate = well.MagneticCalcDate,
                MagneticModel = well.MagneticModel,
                BGGMBTotal = well.BTotal,
                BGGMDip = well.Dip,
                BGGMDeclination = well.Declination,
                GridConvergence = well.GridConvergence,
                WaypointMode = MxSWaypointMode.WayPoint,
                IFR1WaypointMode = MxSIFR1WaypointMode.WayPoint
            };
            wayPoint.Name = GenerateWayPointName(well, MxSConstant.NewWaypointPrefix);
            wayPoint.CalculateTotalCorrection(well.NorthReference);
            well.Waypoints.Add(wayPoint);
            return wayPoint;
        }

        public static OdisseusToolCodeSection CreateEmptyDefaultOdisseusToolCodeSection(this Well well)
        {
            var toolCodeSection = new OdisseusToolCodeSection
            {
                Well = well,
                StartDepth = 0.0,
                ShowEnd = true,
                EndDepth = MxSConstant.MaximumDepth,
                OdisseusToolCodeParams = null,
                IsEndDepthEnabled = true,
            };
            toolCodeSection.Customer = well.GenerateOdisseusToolCodeSectionName(MxSConstant.NewOdisseusToolCodeSectionPrefix);
            well.OdisseusToolCodeSections.Add(toolCodeSection);
            return toolCodeSection;
        }

        public static void SetRunFilters(this Well well)
        {
            SortedSet<string> runs = new SortedSet<string>();

            well.Runs.Where(r => r.RunNumber != MxSConstant.DefaultPlanRunNumber && r.State != MxSState.Deleted)
             .ToList().ForEach(run =>
             {
                 runs.Add(well.GetRunFilter(run));
             });

            well.RunsFilters = new List<string>(runs);
            well.AdjustFilterIndex();
        }

        public static void AdjustFilterIndex(this Well well)
        {
            if (well.RunsFilters.Count <= well.FilterIndex)
            {
                well.FilterIndex = 0;
            }
            if (!well.RunsFilters.Contains(well.SelectedRunFilter))
            {
                well.FilterIndex = 0;
            }
            else
            {
                well.FilterIndex = well.RunsFilters.IndexOf(well.SelectedRunFilter);
            }
        }

        public static void UpdateStationIdsForSolutions(this Well well, List<ObservatoryStation> allDestinationObservatoryStaions)
        {
            if (allDestinationObservatoryStaions != null)
            {
                well.Runs.ForEach(run => run.Solutions.ForEach(solution =>
                {
                    if (solution != null && solution.ObservatoryStation != null)
                    {

                        ObservatoryStation destinationStation = allDestinationObservatoryStaions.FirstOrDefault(
                                             obs => obs != null && !string.IsNullOrWhiteSpace(obs.Description) && obs.Description.Equals(solution.ObservatoryStation.Description));


                        solution.ObservatoryStationId = destinationStation != null ? destinationStation.Id : (Guid?)null;
                        solution.ObservatoryStation = destinationStation;

                    }
                }));
            }

        }

        //HACK: rick; very bad hack to get around the mag calcs being called multiple times
        //Pulled code from MagneticValuesControl for the implementation.
        //so much wrong with this implementation.
        public static void CalculateMagneticInfomation(this Well well)
        {
            // Commented In MxS_Core
            //if (!MagneticValuesNeedsUpdating)
            //{
            //    return;
            //}

            if (string.IsNullOrWhiteSpace(well.ProjectionGridSystem) || string.IsNullOrWhiteSpace(well.MagneticModel))
            {
                return;
            }

            if (well.ValidateNorthingEasting() && ValidateDateTime(well.MagneticCalcDate))
            {
                var clientAci = new AdvancedCoordInput(well.ProjectionGridSystem, well.MagneticModel, well.MagneticCalcDate, -well.Elevation * MxSConstant.FEETTOMETRIC, well.Latitude, well.Longitude, well.Northing, well.Easting);
                //TODO: Suhail - Removed TypeConverter
                //MagCoordResults magR = TypeConverter.DynamicMap<MagCoordResults>(CoordService.CalculateMagneticParameters(clientAci));
                MagCoordResults magR = CoordService.CalculateMagneticParameters(clientAci);
                well.MagneticFieldStrength = magR.MagFieldStrength;
                well.MagneticDipAngle = Math.Round(magR.MagDip, 3, MidpointRounding.AwayFromZero);
                well.MagneticDeclination = Math.Round(magR.MagDeclination, 3, MidpointRounding.AwayFromZero);
                well.MagneticValuesNeedsUpdating = false;
            }

        }

        //TODO: Suhail - Related to State
        //Kiran - This is not the best implementation but at the moment this is the only way to resolve the save issue.
        public static void PreProcessWellBeforeSave(this Well well, MxSSaveCallerEnum saveCallerEnum = MxSSaveCallerEnum.Client)
        {
            try
            {
                if (well.Runs != null)
                {
                    foreach (Run run in well.Runs)
                    {
                        if (run.RawSurveys != null)
                        {
                            foreach (RawSurvey rawSurvey in run.RawSurveys)
                            {
                                if (rawSurvey != null && rawSurvey.CorrectedSurvey != null)
                                {
                                    if (rawSurvey.CorrectedSurvey.State == MxSState.Deleted)
                                    {
                                        //rawSurvey.CorrectedSurvey.Values.Delete();
                                        //rawSurvey.CorrectedSurvey.UncertaintyValues.Delete();
                                        rawSurvey.DeleteCorrectedSurvey();
                                    }
                                }
                            }
                        }
                    }
                }
                if (well.OdisseusToolCodeSections != null)
                {
                    foreach (OdisseusToolCodeSection section in well.OdisseusToolCodeSections)
                    {
                        if (section != null)
                        {
                            section.Well?.UpdateToolCodeParamsToSameReference();
                            if (section.State == MxSState.Deleted)
                            {
                                //section.Delete();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public static Expression<Func<Well, object>>[] GetIncludes()
        {
            Expression<Func<Well, object>>[] result =
            {
                well => well.Runs,
                well => well.Waypoints,
                well=>well.OdisseusToolCodeSections,

                well=>well.OdisseusToolCodeSections.Select(p=>p.OdisseusToolCodeParams),
                well => well.Runs.Select(r => r.Solutions),
                well => well.Runs.Select(r => r.Solutions.Select(s => s.ASAPreconditionRules)),
                well => well.Runs.Select(r => r.Solutions.Select(s => s.ASARules)),
                well => well.Runs.Select(r => r.Solutions.Select(s => s.PlanSurveys)),
                well => well.Runs.Select(r => r.Solutions.Select(s => s.ObservatoryStation)),

                well => well.Runs.Select(r => r.ShortSurveys),
                well => well.Runs.Select(r => r.RawSurveys),
                well => well.Runs.Select(r => r.RawSurveys.Select(x => x.CorrectedSurvey)),
                well => well.Runs.Select(r => r.RawSurveys.Select(x => x.CorrectedSurvey.BgsDataPoints)),

                well => well.Runs.Select(r => r.RawSurveys.Select(x => x.CorrectedSurvey.Values)),
                well => well.Runs.Select(r => r.RawSurveys.Select(x => x.CorrectedSurvey.UncertaintyValues)),
                well => well.Runs.Select(r => r.RawSurveys.Select(x => x.CorrectedSurvey.MaxSurveyRuleSetResponse.Select(z => z.ActionResults))),
                well => well.Runs.Select(r => r.RawSurveys.Select(x => x.CorrectedSurvey.MaxSurveyRuleSetResponse.Select(z => z.RulesResponse))),

            };
            return result;
        }

        public static void UpdateToolCodeParamsToSameReference(this Well well)
        {
            Dictionary<Guid, OdisseusToolCodeParams> toolCodeParams = new Dictionary<Guid, OdisseusToolCodeParams>();
            foreach (OdisseusToolCodeSection section in well.OdisseusToolCodeSections)
            {
                if (section.OdisseusToolCodeParams != null)
                {
                    if (toolCodeParams.ContainsKey(section.OdisseusToolCodeParams.Id))
                    {
                        section.OdisseusToolCodeParams = toolCodeParams[section.OdisseusToolCodeParams.Id];
                    }
                    else
                    {
                        toolCodeParams.Add(section.OdisseusToolCodeParams.Id, section.OdisseusToolCodeParams);
                    }
                }
            }
        }

        public static void UpdateObservatoryStationToSameReference(this Well well)
        {
            Dictionary<Guid, ObservatoryStation> stations = new Dictionary<Guid, ObservatoryStation>();
            foreach (Solution solution in well.AllSolutions)
            {
                if (solution.ObservatoryStation != null)
                {
                    if (stations.ContainsKey(solution.ObservatoryStation.Id))
                    {
                        solution.ObservatoryStation = stations[solution.ObservatoryStation.Id];
                    }
                    else
                    {
                        stations.Add(solution.ObservatoryStation.Id, solution.ObservatoryStation);
                    }
                }
            }
        }

        private static string GetRunFilter(this Well well, Run run)
        {
            if (string.IsNullOrEmpty(run.SerialNumber))
            {
                return run.RunNumber;
            }
            return run.RunNumber + MxSConstants.RunFilterSplitterChar + run.SerialNumber;
        }

        private static string GenerateWayPointName(this Well well, string prefix)
        {
            string fullname;
            bool found = false;
            int index = 1;

            do
            {
                fullname = string.Format("{0} {1}", prefix, index);
                Waypoint wayPoint = well.Waypoints.Find(x => x.Name == fullname);
                found = wayPoint != null;
                index++;
            } while (found);
            string result = !found ? fullname : string.Empty;
            return result;
        }

        private static int GetNearestIndex(List<CorrectedSurvey> correctedSurveys, CorrectedSurvey survey)
        {
            List<CorrectedSurvey> filteredList = correctedSurveys.Where(c => c.Depth < survey.Depth).ToList();
            if (filteredList.Count > 0)
            {
                return filteredList.Count;
            }
            return -1;
        }

        private static CorrectedSurvey HandleShortSurveysForPreviousSurvey(this Well well, CorrectedSurvey correctedSurvey, CorrectedSurvey previousSurvey)
        {
            double startDepth = previousSurvey == null ? double.MinValue : previousSurvey.Depth;
            List<ShortSurvey> potentialShortSurveys = well.AllShortSurveys.Where(s => s.Depth > startDepth &&
                                                                                    s.Depth < correctedSurvey.Depth &&
                                                                                        s.Latitude.HasValue && s.Longitude.HasValue &&
                                                                                            s.SurveyType == MxSSurveyType.Definitive).OrderBy(x => x.Depth).ToList();
            if (potentialShortSurveys.Any())
            {
                previousSurvey = new CorrectedSurvey(potentialShortSurveys.Last());
                previousSurvey.AziErrorAzimuth = previousSurvey.SolAzm.Value;
                previousSurvey.AziErrorInclination = previousSurvey.SolInc.Value;
            }

            return previousSurvey;
        }

        private static string GenerateOdisseusToolCodeSectionName(this Well well, string prefix)
        {
            string fullname;
            bool found = false;
            int index = 1;

            do
            {
                fullname = string.Format("{0} {1}", prefix, index);
                OdisseusToolCodeSection wayPoint = well.OdisseusToolCodeSections.Find(x => x.Customer == fullname && x.State != MxSState.Deleted);
                found = wayPoint != null;
                index++;
            } while (found);
            string result = !found ? fullname : string.Empty;
            return result;
        }

        private static bool ValidateNorthingEasting(this Well well)
        {
            return !double.IsNaN(well.Northing) && !double.IsNaN(well.Easting);
        }

        private static bool ValidateDateTime(DateTime toVali)
        {
            return Math.Abs(toVali.Year - DateTime.Now.Year) < 150;
        }

        public static void AddForeignKey(this Well well)
        {
            if(well.Waypoints.Any() && well.Waypoints.Count > 0)
            {
                foreach (var waypoint in well.Waypoints)
                {                    
                    waypoint.AddForeignKey(well);
                }
            }
            if(well.Runs.Any() && well.Runs.Count > 0)
            {
                foreach (var run in well.Runs)
                {
                    run.AddForeignKey(well);
                }
            }
            if(well.OdisseusToolCodeSections.Any() && well.OdisseusToolCodeSections.Count > 0)
            {
                foreach (var odisseus in well.OdisseusToolCodeSections)
                {
                    odisseus.AddForeignKey(well);                    
                }
            }            
        }
    }
}
