using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.Surveys;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class SolutionRawsurveyManager
    {
        internal Run Run { get; }

        internal List<Solution> Solutions => Run.Solutions;

        internal List<RawSurvey> RawSurveys => Run.RawSurveys;

        public SolutionRawsurveyManager(Run run)
        {
            Run = run;
        }

        //TODO: Suhail - Need to ask Kiran Sir
        //===========================================================================================
        #region Runs

        internal bool AddRawSurvey(RawSurvey rawSurvey)
        {
            if (RawSurveys.Any(r => r.Id == rawSurvey.Id))
            {
                return false;
            }

            var solution = FindSolutionForRawSurvey(rawSurvey);
                rawSurvey.Solution = solution;

            rawSurvey.Run = this.Run;

            RawSurveys.Add(rawSurvey);
            return true;
        }

        internal bool DeleteRawSurvey(RawSurvey rawSurvey)
        {
            if (RawSurveys.Remove(rawSurvey))
            {
                //rawSurvey.Run = null;
                // Hari-Run object in rawsurvey throws null every where refernce exception , commented for now.
                //Corrected Surveys are also deleted , run object needs to available.
                return true;
            }
            return false;
        }

        #endregion

        //===========================================================================================
        #region Solutions

        internal bool AddSolution(Solution solution)
        {

            if (Run.GetAllSolutions().Where(sol => sol.Id == solution.Id).Any())
            {
                return false;
            }
            solution.Run = this.Run;
            Solutions.Add(solution);

            //should we try to match the solution to any and all rawsurveys, or only the raw surveys which do not have a solution?
            ReassociateSolutionsToRawSurveys();

            SubscribeToSolutionEvents(solution);
            return true;
        }


        internal bool DeleteSolution(Solution solution)
        {
            if (Solutions.Remove(solution))
            {
                UnsubscribeToSolutionEvents(solution);
                //if we delete the solution, then we need to update the rawsurveys which were referencing the solution and set the solution property to null.
                solution.Run = null;

                ReassociateSolutionsToRawSurveys();
                return true;
            }
            return false;

        }


        internal Solution CreateSolutionForRawSurvey(RawSurvey rawSurvey)
        {
            var result = FindSolutionForRawSurvey(rawSurvey);
            if (result == null)
            {
                //if a solution was not found for the rawsurvey, then need to create one.
                result = CreateSolution(new List<MaxSurveyRule>());
            }
            rawSurvey.Solution = result;

            return result;
        }


        internal Solution CreateSolution(List<MaxSurveyRule> defaultMandatoryRules, double endDepth = MxSConstant.MaximumDepth, MxSPumpStatusFilter pumpStatusFilter = MxSPumpStatusFilter.All)
        {
            var result = FindSolution(endDepth, pumpStatusFilter);
            if (result == null)
            {
                var depths = DetermineSolutionDepths(endDepth);
                result = new Solution
                {
                    PumpStatusFilter = pumpStatusFilter,
                    IcarusWellLatitude = Run.GetLatitude(),
                    IcarusToolType = Run.IsIcarusToolType(),
                    StartDepth = depths.Item1,
                    EndDepth = depths.Item2
                };
                foreach (MaxSurveyRule rule in defaultMandatoryRules)
                {
                    if (rule.PreConditionRule != null && result.ASAPreconditionRules.FirstOrDefault(p => p.Id == rule.PreConditionRule.Id) == null)
                        result.AddMaxSurveyPreConditionRule(rule.PreConditionRule);
                    result.AddMaxSurveyRule(rule);
                }

                AddSolution(result);
            }
            return result;
        }


        internal void ReassociateSolutionsToRawSurveys()
        {
            UpdateRawSurveySolutions(RawSurveys);
        }

        internal void UpdateRawSurveySolutions(ICollection<RawSurvey> rawSurveys)
        {
            foreach (var rawSurvey in rawSurveys)
            {
                rawSurvey.UpdateSolution();
            }
        }

        internal void UpdateSolutionLatitudes(double wellLatitude)
        {
            foreach (var solution in Solutions.Where(sol => sol.State != MxSState.Deleted))
            {
                solution.IcarusWellLatitude = wellLatitude;
            }
        }


        internal Solution FindSolutionForRawSurvey(RawSurvey rawSurvey)
        {
            var pumpStatus = rawSurvey.CorrectedSurvey != null ? rawSurvey.CorrectedSurvey.PumpStatus : rawSurvey.PumpStatus;
            return FindSolution(rawSurvey.Depth, pumpStatus);

        }

        internal Solution FindSolution(double depth, MxSSurveyPumpStatus pumpStatus)
        {
            Solution result = null;
            var solutions = Solutions.Where(s => s.State != MxSState.Deleted && s.IsDepthInRange(depth) && s.SupportsPumpStatus(pumpStatus)).ToList();

            if (solutions.Count == 1)
            {
                result = solutions[0];

            }
            else if (solutions.Count > 1)
            {
                //found more than one solution, then we need to match on the specific pump status.
                result = FindBestPumpStatusMatch(pumpStatus, solutions);
            }
            return result;
        }



        internal Solution FindSolution(double depth, MxSPumpStatusFilter pumpStatusFilter)
        {
            var solution = Solutions.FirstOrDefault(s => s.State != MxSState.Deleted && s.IsDepthInRange(depth) && s.PumpStatusFilter == pumpStatusFilter);
            return solution;

        }

        private void SubscribeToSolutionEvents(Solution solution)
        {
            UnsubscribeToSolutionEvents(solution);
            solution.StartDepthChanged += Solution_StartDepthChanged;
            solution.EndDepthChanged += Solution_EndDepthChanged;
        }

        private void UnsubscribeToSolutionEvents(Solution solution)
        {
            solution.StartDepthChanged -= Solution_StartDepthChanged;
            solution.EndDepthChanged -= Solution_EndDepthChanged;
        }

        private void Solution_EndDepthChanged(object sender, EventArgs e)
        {
            //reset the rawsurvey solution properties based on depth
        }

        private void Solution_StartDepthChanged(object sender, EventArgs e)
        {

        }

        private Solution FindBestPumpStatusMatch(MxSSurveyPumpStatus pumpStatus, List<Solution> solutions)
        {
            Solution result = null;
            switch (pumpStatus)
            {
                case MxSSurveyPumpStatus.NA:

                    // Precedence = NAandOff , All
                    result = solutions.FirstOrDefault(x => x.PumpStatusFilter == MxSPumpStatusFilter.NAandOff);
                    //assumption is, since there can only be 2 matches for pump status of na, then we only need to find the one with NA and off as it has precedence.
                    if (result == null)
                    {
                        result = solutions.FirstOrDefault(x => x.PumpStatusFilter == MxSPumpStatusFilter.All);
                    }
                    break;

                case MxSSurveyPumpStatus.On:

                    // Precedence = On , All
                    result = solutions.FirstOrDefault(x => x.PumpStatusFilter == MxSPumpStatusFilter.On);
                    //assumption is, since there can only be 2 matches for pump status of On, then we only need to find the one with On and off as it has precedence.
                    if (result == null)
                    {
                        result = solutions.FirstOrDefault(x => x.PumpStatusFilter == MxSPumpStatusFilter.All);
                    }
                    break;

                case MxSSurveyPumpStatus.Off:

                    // Precedence = Off , NAandOff, All
                    result = solutions.FirstOrDefault(x => x.PumpStatusFilter == MxSPumpStatusFilter.Off);
                    if (result == null)
                    {
                        result = solutions.FirstOrDefault(x => x.PumpStatusFilter == MxSPumpStatusFilter.NAandOff);
                    }
                    //assumption is, since there can only be 3 matches for pump status of Off, then we only need to worry about off, and NaAndOff, 
                    if (result == null)
                    {
                        result = solutions.FirstOrDefault(x => x.PumpStatusFilter == MxSPumpStatusFilter.All);
                    }
                    break;
            }

            return result;
        }

        #endregion

        //===========================================================================================
        #region CorrectedSurveys 

        internal List<CorrectedSurvey> GetCorrectedSurveys()
        {
            var result = RawSurveys.Where(x => x.CorrectedSurvey != null && !x.IsShortSurvey() && x.CorrectedSurvey.State != MxSState.Deleted).Select(x => x.CorrectedSurvey).ToList();
            return result;
        }


        internal List<CorrectedSurvey> GetCorrectedSurveysForSolution(Solution solution, bool includeShorSurveys = false)
        {
            var result = RawSurveys.Where(x => x.Solution != null && x.Solution.Equals(solution)).Where(s => s.CorrectedSurvey != null && !s.IsShortSurvey() && s.CorrectedSurvey.State != MxSState.Deleted).Select(x => x.CorrectedSurvey).ToList();
            if (includeShorSurveys)
            {
                solution.Run.GetAllShortSurveys().Where(s => s.Enabled && s.SurveyType == MxSSurveyType.Definitive).ToList()
                             .ForEach(s => result.Add(new CorrectedSurvey(s)));

            }
            return result;
        }

        #endregion

        private Tuple<double, double> DetermineSolutionDepths(double depth)
        {

            double startDepth = 0.0;
            double endDepth = depth != MxSConstant.MaximumDepth ? depth : MxSConstant.MaximumDepth;

            double startDepthDiff = Double.MaxValue;
            double endDepthDiff = Double.MaxValue;
            foreach (var solution in Solutions)
            {
                if (solution.State != MxSState.Deleted)
                {
                    if (solution.EndDepth <= depth && Math.Abs(depth - solution.EndDepth) < startDepthDiff)
                    {
                        startDepth = solution.EndDepth;
                        startDepthDiff = Math.Abs(depth - solution.EndDepth);
                    }

                    if (solution.StartDepth > depth && Math.Abs(depth - solution.StartDepth) < endDepthDiff)
                    {
                        endDepth = solution.StartDepth;
                        endDepthDiff = Math.Abs(depth - solution.StartDepth);
                    }
                }
            }
            return new Tuple<double, double>(startDepth, endDepth);
        }
    }
}
