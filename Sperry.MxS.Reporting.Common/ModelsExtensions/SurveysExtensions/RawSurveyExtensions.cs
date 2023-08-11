using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    public static class RawSurveyExtensions
    {
        public static Survey MapRawSurveyToSurvey(this RawSurvey rawSurvey)
        {
            return new Survey()
            {
                Id = rawSurvey.Id,
                Depth = rawSurvey.Depth,
                RunId = rawSurvey.RunId,
                Gx = rawSurvey.Gx ?? 0.0,
                Gy = rawSurvey.Gy ?? 0.0,
                Gz = rawSurvey.Gz ?? 0.0,
                Bx = rawSurvey.Bx ?? 0.0,
                By = rawSurvey.By ?? 0.0,
                Bz = rawSurvey.Bz ?? 0.0,
                Dip = rawSurvey.Dip ?? 0.0,
                Source = rawSurvey.Source,
                Enabled = rawSurvey.Enabled,
                DateTime = rawSurvey.DateTime,
                Run = rawSurvey.Run?.ToString(),
                AzimuthType = rawSurvey.AzimuthType,
                Declination = rawSurvey.Declination ?? 0.0,
                Temperature = rawSurvey.Temperature ?? 0.0,
                MWDLongCollar = rawSurvey.MWDLongCollar ?? 0.0,
                MWDInclination = rawSurvey.MWDInclination ?? 0.0,
                MWDShortCollar = rawSurvey.MWDShortCollar ?? 0.0,
                GxyzInclination = rawSurvey.GxyzInclination ?? 0.0,
                GridConvergence = rawSurvey.GridConvergence ?? 0.0
                //Azimuth = rawSurvey.Azimuth // readonly
            };
        }

        public static bool CompareWithInsiteValue(this RawSurvey rawSurvey, RawSurvey insiteRawSurvey)
        {
            return (insiteRawSurvey != null
                && rawSurvey.DateTime == insiteRawSurvey.DateTime
                && rawSurvey.Depth.CompareDouble(insiteRawSurvey.Depth)
                && rawSurvey.Enabled == insiteRawSurvey.Enabled
                && rawSurvey.GTotal.CompareDouble(insiteRawSurvey.GTotal)
                && rawSurvey.Bx.CompareDouble(insiteRawSurvey.Bx)
                && rawSurvey.By.CompareDouble(insiteRawSurvey.By)
                && rawSurvey.Bz.CompareDouble(insiteRawSurvey.Bz)
                && rawSurvey.Gx.CompareDouble(insiteRawSurvey.Gx)
                && rawSurvey.Gy.CompareDouble(insiteRawSurvey.Gy)
                && rawSurvey.Gz.CompareDouble(insiteRawSurvey.Gz)
                && rawSurvey.PumpStatus == insiteRawSurvey.PumpStatus
                && rawSurvey.Temperature.CompareDouble(insiteRawSurvey.Temperature)
                && rawSurvey.AzimuthType == insiteRawSurvey.AzimuthType
                && rawSurvey.MWDLongCollar.CompareDouble(insiteRawSurvey.MWDLongCollar.Limit360())
                && rawSurvey.MWDShortCollar.CompareDouble(insiteRawSurvey.MWDShortCollar.Limit360())
                && rawSurvey.MWDInclination.CompareDouble(insiteRawSurvey.MWDInclination.Limit360())
                && rawSurvey.Inclination.CompareDouble(insiteRawSurvey.Inclination.Limit360())
                //Bug: 463064- need to review this, to check root cause
                //&& SagInclination.CompareDouble(insiteRawSurvey.SagInclination) 
                );
        }

        // Private access modifier is use in Rawsurveys model for this method .
        public static bool CompareCorrectedSurveyWithInsiteValue(this RawSurvey rawSurvey, RawSurvey insiteRawSurvey)
        {
            if (rawSurvey.CorrectedSurvey != null)
            {
                return rawSurvey.CorrectedSurvey.Bx.CompareDouble(insiteRawSurvey.Bx)
                    && rawSurvey.CorrectedSurvey.By.CompareDouble(insiteRawSurvey.By)
                    && rawSurvey.CorrectedSurvey.Bz.CompareDouble(insiteRawSurvey.Bz)
                    && rawSurvey.CorrectedSurvey.Gx.CompareDouble(insiteRawSurvey.Gx)
                    && rawSurvey.CorrectedSurvey.Gy.CompareDouble(insiteRawSurvey.Gy)
                    && rawSurvey.CorrectedSurvey.Gz.CompareDouble(insiteRawSurvey.Gz)
                    && rawSurvey.CorrectedSurvey.PumpStatus.Equals(insiteRawSurvey.PumpStatus)
                    && rawSurvey.CorrectedSurvey.AzimuthType.Equals(insiteRawSurvey.AzimuthType);
            }
            return true;
        }

        public static bool CompareCorrectedSurveyGsBs(this RawSurvey rawSurvey)
        {
            if (rawSurvey.CorrectedSurvey != null)
            {
                return rawSurvey.CorrectedSurvey.Bx.CompareDouble(rawSurvey.Bx)
                       && rawSurvey.CorrectedSurvey.By.CompareDouble(rawSurvey.By)
                       && rawSurvey.CorrectedSurvey.Bz.CompareDouble(rawSurvey.Bz)
                       && rawSurvey.CorrectedSurvey.Gx.CompareDouble(rawSurvey.Gx)
                       && rawSurvey.CorrectedSurvey.Gy.CompareDouble(rawSurvey.Gy)
                       && rawSurvey.CorrectedSurvey.Gz.CompareDouble(rawSurvey.Gz)
                       && rawSurvey.CorrectedSurvey.SagInclination.CompareDouble(rawSurvey.SagInclination);
            }
            return true;
        }

        public static PMRInput GetPMRInput(this RawSurvey rawSurvey)
        {
            PMRInput input = new PMRInput();

            input.Depth = rawSurvey.Depth;
            input.Gx = rawSurvey.Gx;
            input.Gy = rawSurvey.Gy;
            input.Gz = rawSurvey.Gz;
            input.Bx = rawSurvey.Bx;
            input.By = rawSurvey.By;
            input.Bz = rawSurvey.Bz;

            return input;
        }

        public static void SetSolution(this RawSurvey rawSurvey, Solution newSolution)
        {
            //if the solutions are the same.
            if (rawSurvey.Solution != null && newSolution != null)
            {
                if (rawSurvey.Solution.Id == newSolution.Id)
                {
                    return;
                }
            }

            if (newSolution != null)
            {
                //rawsurvey depth does not fit in the newsolution depth range, then just return.
                if (!newSolution.IsDepthInRange(rawSurvey.Depth))
                {
                    return;
                }
                rawSurvey.SolutionId = newSolution.Id;
            }
            else
            {
                //if the new value is null, then set the solution id to guid.empty
                rawSurvey.SolutionId = Guid.Empty;
            }
            rawSurvey.Solution = newSolution;
        }

        public static bool HasSolution(this RawSurvey rawSurvey)
        {
            return rawSurvey.Solution != null;
        }

        public static ShortSurvey GetShortSurvey(this RawSurvey rawSurvey)
        {
            ShortSurvey shortSurvey = new ShortSurvey(rawSurvey);
            shortSurvey.Id = rawSurvey.Id;
            if (!shortSurvey.Inclination.HasValue)
            {
                shortSurvey.Inclination = rawSurvey.MWDInclination;
            }
            if (!shortSurvey.Azimuth.HasValue)
            {
                if (rawSurvey.AzimuthType == MxSAzimuthTypeEnum.LongCollar)
                {
                    shortSurvey.Azimuth = rawSurvey.MWDLongCollar;
                }
                else
                {
                    shortSurvey.Azimuth = rawSurvey.MWDShortCollar;
                }
            }
            shortSurvey.SurveyType = MxSSurveyType.Undefined;
            return shortSurvey;
        }

        public static bool IsShortSurvey(this RawSurvey rawSurvey)
        {
            return new List<double?> { rawSurvey.Bx, rawSurvey.By, rawSurvey.Bz, rawSurvey.Gx, rawSurvey.Gy, rawSurvey.Gz }.IsAllNull();
        }

        //Rick; Code smell, this code was in the corrected survey extension method.
        //what is the point of having a solution property if we can get a different solution from the rawsurvey.run object.
        public static Solution FindSolution(this RawSurvey rawSurvey, double depth, MxSSurveyPumpStatus pumpStatus)
        {
            if (rawSurvey.Run == null)
            {
                return null;
            }
            return rawSurvey.Run.FindSolution(depth, pumpStatus);
        }

        public static void UpdateValues(this RawSurvey rawSurvey, RawSurvey newRawSurvey)
        {
            try
            {
                //rawSurvey.UpdateRawValues(newRawSurvey);
                //rawSurvey.Run = newRawSurvey.Run;

                // Sandeep singh Added because of some survey getiing yes which already uploaded and Run is getting null
                if (rawSurvey == null || rawSurvey.Run == null || rawSurvey.Solution == null || rawSurvey.CorrectedSurvey == null)
                {
                    rawSurvey.UpdateRawValues(newRawSurvey);
                    rawSurvey.Run = newRawSurvey.Run;
                }
                else
                {
                    rawSurvey.UpdateRawValues(rawSurvey);
                    rawSurvey.Run = rawSurvey.Run;
                }
            }

            finally
            {
                // TODO: Suhail - Related to state
                //SetState(State.Modified);
            }
        }

        public static void UpdateRawSurveyValues(this RawSurvey rawSurvey, RawSurvey newRawSurvey)
        {
            try
            {
                rawSurvey.UpdateRawValues(newRawSurvey);
            }
            finally
            {
                // TODO: Suhail - Related to state
                //SetState(State.Modified);
            }
        }

        public static void UpdateRawValues(this RawSurvey rawSurvey, RawSurvey newRawSurvey)
        {
            rawSurvey.Azimuth = newRawSurvey.Azimuth;
            rawSurvey.BTotal = newRawSurvey.BTotal;
            rawSurvey.Bx = newRawSurvey.Bx;
            rawSurvey.By = newRawSurvey.By;
            rawSurvey.Bz = newRawSurvey.Bz;
            rawSurvey.DateTime = newRawSurvey.DateTime;
            rawSurvey.Depth = newRawSurvey.Depth;
            rawSurvey.Dip = newRawSurvey.Dip;
            rawSurvey.Enabled = newRawSurvey.Enabled;
            rawSurvey.GTotal = newRawSurvey.GTotal;
            rawSurvey.Gx = newRawSurvey.Gx;
            rawSurvey.Gy = newRawSurvey.Gy;
            rawSurvey.Gz = newRawSurvey.Gz;
            rawSurvey.PumpStatus = newRawSurvey.PumpStatus;
            rawSurvey.Source = newRawSurvey.Source;
            rawSurvey.Temperature = newRawSurvey.Temperature;
            rawSurvey.Triac1 = newRawSurvey.Triac1;
            rawSurvey.Triac2 = newRawSurvey.Triac2;
            rawSurvey.Triac3 = newRawSurvey.Triac3;
            rawSurvey.AzimuthType = newRawSurvey.AzimuthType;
            rawSurvey.GridConvergence = newRawSurvey.GridConvergence;
            rawSurvey.Inclination = newRawSurvey.Inclination;
            rawSurvey.MWDInclination = newRawSurvey.MWDInclination;
            rawSurvey.MWDLongCollar = newRawSurvey.MWDLongCollar;
            rawSurvey.MWDShortCollar = newRawSurvey.MWDShortCollar;
            rawSurvey.SagInclination = newRawSurvey.SagInclination;
            rawSurvey.Bg = newRawSurvey.Bg;
            rawSurvey.Bh = newRawSurvey.Bh;
            rawSurvey.BhMeasured = newRawSurvey.BhMeasured;
            rawSurvey.BtMeasured = newRawSurvey.BtMeasured;
            rawSurvey.BTotalQcDelta = newRawSurvey.BTotalQcDelta;
            rawSurvey.BvMeasured = newRawSurvey.BvMeasured;
            rawSurvey.Declination = newRawSurvey.Declination;
            rawSurvey.DipMeasured = newRawSurvey.DipMeasured;
            rawSurvey.DipQcDelta = newRawSurvey.DipQcDelta;
            rawSurvey.Error1 = newRawSurvey.Error1;
            rawSurvey.Error2 = newRawSurvey.Error2;
            rawSurvey.Error3 = newRawSurvey.Error3;
            rawSurvey.GTotalQcDelta = newRawSurvey.GTotalQcDelta;
            rawSurvey.GxyInclination = newRawSurvey.GxyInclination;
            rawSurvey.GxyzInclination = newRawSurvey.GxyzInclination;
            rawSurvey.GzInclination = newRawSurvey.GzInclination;
            rawSurvey.ImportSource = newRawSurvey.ImportSource;
            rawSurvey.RigTimeOffset = newRawSurvey.RigTimeOffset;

        }

        private static double? GetValue(double? value)
        {
            return value == null ? value : Math.Round(value.Value, 8, MidpointRounding.AwayFromZero);
        }

        internal static void UpdateSolution(this RawSurvey rawSurvey)
        {
            rawSurvey.Solution = rawSurvey.Run.FindSolutionForRawSurvey(rawSurvey);
        }
        public static void AddForeignKey(this RawSurvey rawSurvey, Run run)
        {
            rawSurvey.RunId = run.Id;
            rawSurvey.CorrectedSurvey.AddForeignKey(rawSurvey);
        }
    }
}
