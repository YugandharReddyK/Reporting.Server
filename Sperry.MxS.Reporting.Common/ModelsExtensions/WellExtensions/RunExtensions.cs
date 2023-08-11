using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.ModelsExtensions.SurveysExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models
{
    public static class RunExtensions
    {
        public static void CloneForWorkbench(this Run run, Run runToCopy, List<Guid> rawSurveyIds)
        {
            run.UpdateValues(runToCopy);

            foreach (var rawSurvey in runToCopy.RawSurveys)
            {
                if (rawSurveyIds.Contains(rawSurvey.Id))
                {
                    RawSurvey rawSurveyTocopy = new RawSurvey(rawSurvey);
                    run.AddRawSurvey(rawSurveyTocopy);
                }
            }

            foreach (var solution in runToCopy.Solutions)
            {
                Solution solutionTocopy = new Solution(solution);
                run.AddSolution(solutionTocopy);
            }
        }

        public static string FormatRunNumber(this Run run, string runNumber)
        {
            string result = runNumber;
            if (string.IsNullOrWhiteSpace(runNumber) || runNumber.Length >= 4)
            {
                return result;
            }
            int realRunNumber;
            if (int.TryParse(runNumber, out realRunNumber))
            {
                result = string.Format("{0:D4}", realRunNumber);
            }
            return result;
        }

        public static TimeSpan? GetAverageTimeSpan(this Run run)
        {
            long averageTicks = 0;
            TimeSpan? averageTimeSpan = null;
            if (run.RawSurveys.Any())
            {
                var ticks = run.RawSurveys.Where(r => r.CorrectedSurvey?.SurveyReturnTime != null).Select(ts => ts.CorrectedSurvey.SurveyReturnTime.Value.Ticks);
                if (ticks.Any())
                {
                    averageTicks = (long)ticks.Average();
                }
                averageTimeSpan = TimeSpan.FromTicks(averageTicks);
            }
            return averageTimeSpan;
        }

        public static IList<RawSurvey> GetRawSurveysForAnalysis(this Run run)
        {
            var allRawSurveys = GetAllRawSurveys(run);
            var rawSurveysForAnalysis = allRawSurveys.Where(rawSurvey =>
                rawSurvey.ImportSource == MxSImportFileType.ADI || rawSurvey.ImportSource == MxSImportFileType.Realtime ||
                rawSurvey.ImportSource == MxSImportFileType.Excel || rawSurvey.ImportSource == MxSImportFileType.DataGrid || rawSurvey.ImportSource == MxSImportFileType.ThirdParty).ToList();

            if (rawSurveysForAnalysis.Count == 0)
            {
                return rawSurveysForAnalysis;
            }
            //Replaced OrderBy(survey => survey.Depth).ThenBy(survey => survey.DateTime) logic with new SurveyComparer interface to make uniform all over the application for PBI - 325884
            return rawSurveysForAnalysis.OrderBy(survey => survey, new SurveyComparer()).ToList();
        }

        public static List<RawSurvey> GetAllRawSurveys(this Run run)
        {
            List<RawSurvey> allRawSurveys = new List<RawSurvey>(run.RawSurveys);
            allRawSurveys.RemoveAll(r => r.State == MxSState.Deleted);
            return allRawSurveys;
        }

        public static List<ShortSurvey> GetAllShortSurveys(this Run run)
        {
            List<ShortSurvey> allShortSurveys = new List<ShortSurvey>(run.ShortSurveys);
            allShortSurveys.RemoveAll(r => r.State == MxSState.Deleted);
            return allShortSurveys;
        }

        public static List<Solution> GetAllSolutions(this Run run)
        {
            List<Solution> allSolutions = new List<Solution>(run.Solutions);
            allSolutions.RemoveAll(r => r.State == MxSState.Deleted);
            return allSolutions;
        }

        public static List<CorrectedSurvey> GetCorrectedSurveys(this Run run)
        {
            List<CorrectedSurvey> correctedSurveysForAnalysis = run.solutionRawsurveyManager.GetCorrectedSurveys();
            correctedSurveysForAnalysis.RemoveAll(c => c == null || c.State == MxSState.Deleted);
            return correctedSurveysForAnalysis.OrderBy(a => a.Depth).ThenBy(a => a.DateTime).ToList();
        }

        public static ShortSurvey GetPreviousShortSurvey(this Run run, ShortSurvey survey)
        {
            List<ShortSurvey> allShortSurveys = run.GetAllShortSurveys();
            if (allShortSurveys != null && allShortSurveys.Count > 0)
            {
                int shortSurveyIndex = allShortSurveys.IndexOf(survey);
                if (shortSurveyIndex > 0)
                {
                    for (int i = shortSurveyIndex - 1; i >= 0; i--)
                    {
                        return allShortSurveys[i];
                    }
                }
            }
            return null;
        }

        public static CorrectedSurvey GetPreviousSurvey(this Run run, CorrectedSurvey survey, bool skipExcludedIgnoredSurveys = false, bool skipRotationCheckShot = false)
        {
            List<CorrectedSurvey> allCorrectedSurveys = run.GetCorrectedSurveys();
            if (allCorrectedSurveys != null && allCorrectedSurveys.Count > 0)
            {
                int correctedSurveyIndex = allCorrectedSurveys.IndexOf(survey);
                if (correctedSurveyIndex > 0)
                {
                    for (int i = correctedSurveyIndex - 1; i >= 0; i--)
                    {
                        if (skipRotationCheckShot && allCorrectedSurveys[i].SurveyType == MxSSurveyType.RotCheckshot)
                            continue;
                        if (skipExcludedIgnoredSurveys && allCorrectedSurveys[i].IsIgnoreOrExcludeSurvey())
                            continue;
                        return allCorrectedSurveys[i];
                    }
                }
            }
            return null;
        }

        public static List<CorrectedSurvey> GetCorrectedSurveysForSolution(this Run run, Solution solution, bool includeShorSurveys = false)
        {
            List<CorrectedSurvey> result = run.solutionRawsurveyManager.GetCorrectedSurveysForSolution(solution, includeShorSurveys);
            result.RemoveAll(c => c == null || c.State == MxSState.Deleted);
            return result;
        }

        //TODO: need to check for the definitive flag.
        public static void UpdateSolutions(this Run run, List<CorrectedSurvey> correctedSurveys)
        {
            List<RawSurvey> rawSurveys = correctedSurveys.Where(c => c.State != MxSState.Deleted).Select(x => x.RawSurvey).ToList();
            run.UpdateSolutions(rawSurveys);
        }

        public static void UpdateSolutions(this Run run, List<RawSurvey> rawSurveys)
        {
            run.solutionRawsurveyManager.UpdateRawSurveySolutions(rawSurveys);
        }

        /// <summary>
        /// Used mostly when importing surveys to find an existing survey which matches the same values.
        /// </summary>
        /// <param name="surveyDate"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static RawSurvey FindExistingRawSurvey(this Run run, DateTime surveyDate, double depth)
        {
            //try to find the raw survey which matches the given date and depth, using the tolerances.
            RawSurvey result = run.RawSurveys.FirstOrDefault(raw => RoundToSecond(surveyDate).Equals(RoundToSecond(raw.DateTime)) && raw.Depth.CompareDouble(depth));
            return result;
        }

        /// <summary>
        /// Used mostly when importing surveys to find an existing survey which matches the same values.
        /// </summary>
        /// <param name="surveyDate"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static ShortSurvey FindExistingShortSurvey(this Run run, DateTime surveyDate, double depth)
        {
            //try to find the raw survey which matches the given date and depth, using the tolerances.
            return run.ShortSurveys.FirstOrDefault(raw => raw.DateTime == surveyDate && raw.Depth.CompareDouble(depth));
        }

        public static Solution FindSolutionForRawSurvey(this Run run, RawSurvey survey)
        {
            return run.solutionRawsurveyManager.FindSolutionForRawSurvey(survey);
        }

        public static Solution FindSolution(this Run run, double depth, MxSSurveyPumpStatus pumpStatus)
        {
            return run.solutionRawsurveyManager.FindSolution(depth, pumpStatus);
        }

        public static Solution FindSolution(this Run run, double depth, MxSPumpStatusFilter pumpStatusFilter = MxSPumpStatusFilter.All)
        {
            return run.solutionRawsurveyManager.FindSolution(depth, pumpStatusFilter);
        }

        public static Solution CreateSolution(this Run run, List<MaxSurveyRule> defaultMandatoryRules, double depth = MxSConstant.MaximumDepth, MxSPumpStatusFilter pumpStatusFilter = MxSPumpStatusFilter.All)
        {
            return run.solutionRawsurveyManager.CreateSolution(defaultMandatoryRules, depth, pumpStatusFilter);
        }

        public static bool IsIcarusToolType(this Run run)
        {
            return !(run.ToolType == MxSToolTypeOptions.PCD || run.ToolType == MxSToolTypeOptions.NotAvailable);
        }

        public static double GetLatitude(this Run run)
        {
            //this method is used when creating a solution, therefore if the well is not set, it will return 0 for the latitude.
            //then when the well property is set, we need to update the soltions latitude to the latitude of the well.
            return run.Well != null ? run.Well.Latitude : 0;
        }

        public static bool IsEquals(this Run run, Run newrun)
        {
            try
            {
                uint currentRunNumber = 0;
                uint newRunNumber = 0;
                bool isRunNumberSame = false;
                if (UInt32.TryParse(newrun.RunNumber, out newRunNumber) && UInt32.TryParse(run.RunNumber, out currentRunNumber))
                {
                    isRunNumberSame = currentRunNumber == newRunNumber;
                }
                else
                {
                    isRunNumberSame = run.RunNumber.Equals(newrun.RunNumber);
                }
                if (newrun.SerialNumber.Equals(run.SerialNumber) && isRunNumberSame)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static CorrectedSurvey GetPreviousSurveyForCourseLength(IList<CorrectedSurvey> allCorrectedSurveys, CorrectedSurvey survey, bool skipExcludedIgnoredSurveys = false, bool skipRotationCheckShot = false)
        {
            //allCorrectedSurveys has to be sorted by Depth and DateTime to get exact previous survey
            if (allCorrectedSurveys != null && allCorrectedSurveys.Any())
            {
                int correctedSurveyIndex = allCorrectedSurveys.IndexOf(survey);
                if (correctedSurveyIndex > 0)
                {
                    for (int i = correctedSurveyIndex - 1; i >= 0; i--)
                    {
                        if (skipRotationCheckShot && allCorrectedSurveys[i].SurveyType == MxSSurveyType.RotCheckshot)
                            continue;
                        if (skipExcludedIgnoredSurveys && allCorrectedSurveys[i].IsIgnoreOrExcludeSurvey())
                            continue;
                        return allCorrectedSurveys[i];
                    }
                }
            }
            return null;
        }

        public static void AddRawSurvey(this Run run, RawSurvey rawSurvey)
        {
            if (run.solutionRawsurveyManager.AddRawSurvey(rawSurvey))
            {
                rawSurvey.UpdateSolution();
            };
        }

        public static void DeleteRawSurvey(this Run run, RawSurvey rawSurvey)
        {
            run.solutionRawsurveyManager.DeleteRawSurvey(rawSurvey);
        }

        public static void AddShortSurvey(this Run run, ShortSurvey shortSurvey)
        {
            if (!run.ShortSurveys.Any(s => s.Depth.CompareDouble(shortSurvey.Depth) && s.DateTime == shortSurvey.DateTime))
            {
                shortSurvey.Run = run;
                run.ShortSurveys.Add(shortSurvey);
            }
        }

        public static void DeleteShortSurvey(this Run run, ShortSurvey shortSurvey)
        {
            if (run.ShortSurveys.Any(s => s.Depth.CompareDouble(shortSurvey.Depth) && s.DateTime == shortSurvey.DateTime))
            {
                shortSurvey.Run = null;
                run.ShortSurveys.Remove(shortSurvey);
            }
        }

        public static void DeleteShortSurvey(this Run run, RawSurvey rawSurvey)
        {
            var shortSurvey = run.ShortSurveys.FirstOrDefault(s => s.Depth.CompareDouble(rawSurvey.Depth) && s.DateTime == rawSurvey.DateTime);
            if (shortSurvey != null)
            {
                rawSurvey.Run = null;
                run.ShortSurveys.Remove(shortSurvey);
            }
        }

        public static void AddSolution(this Run run, Solution solution)
        {
            run.solutionRawsurveyManager.AddSolution(solution);
        }

        public static void DeleteSolution(this Run run, Solution solution)
        {
            run.solutionRawsurveyManager.DeleteSolution(solution);
        }

        public static Solution GetSolution(this Run run, Guid solutionId)
        {
            return run.GetAllSolutions().FirstOrDefault(x => x.Id == solutionId);
        }

        private static DateTime RoundToSecond(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day,
                                dt.Hour, dt.Minute, dt.Second);
        }

        public static void AddForeignKey(this Run run, Well well)
        {
            run.WellId = well.Id;
            if (run.RawSurveys.Any() && run.RawSurveys.Count > 0)
            {
                foreach (var rawsurvey in run.RawSurveys)
                {
                    rawsurvey.AddForeignKey(run);
                }
            }
            if (run.Solutions.Any() && run.Solutions.Count > 0)
            {
                foreach (var solution in run.Solutions)
                {
                    solution.AddForeignKey(run);
                }
            }
            if (run.ShortSurveys.Any() && run.ShortSurveys.Count > 0)
            {
                foreach (var shortsurvey in run.ShortSurveys)
                {
                    shortsurvey.AddForeignKey(run);
                }
            }
        }
    }
}
