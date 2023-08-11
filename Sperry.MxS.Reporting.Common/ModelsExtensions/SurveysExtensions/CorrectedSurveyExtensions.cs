using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.DynamicQC;
using Sperry.MxS.Core.Common.Models.Workbench.AziError;
using Sperry.MxS.Core.Common.ModelsExtensions.SurveysExtensions;
using Sperry.MxS.Core.Common.Models.RulesEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    public static class CorrectedSurveyExtensions
    {
        public static DynamicQCSurveyInput MapCorrectedSurveyToDQCSurveyInput(this CorrectedSurvey survey)
        {
            return new DynamicQCSurveyInput()
            {
                Depth = survey.Depth,
                SolAzm = survey.SolAzm,
                SolInc = survey.SolInc,
                NomDip = survey.NomDip ?? 0.0,
                NomBt = survey.NomBt ?? 0.0,
                NomGrid = survey.NomGrid ?? 0.0,
                NomDeclination = survey.NomDeclination ?? 0.0
            };
        }

        public static AziErrorInput MapCorrectedSurveyToAziErrorInput(this CorrectedSurvey survey)
        {
            return new AziErrorInput()
            {
                DateTime = survey.DateTime, 
                Depth = survey.Depth, 
                AziErrorAzimuth = survey.AziErrorAzimuth, 
                AziErrorInclination = survey.AziErrorInclination, 
                AziErrorDipe = survey.AziErrorDipe, 
                AziErrorBe = survey.AziErrorBe, 
                AziErrorAzimuthMagnetic = survey.AziErrorAzimuthMagnetic, 
                AziErrordDecle = survey.AziErrordDecle
            };
        }

        public static PlanSurvey MapCorrectedSurveyToPlanSurvey(this CorrectedSurvey survey)
        {
            return new PlanSurvey()
            {
                //RunNo = survey.RunNo, readonly
                SolutionId = survey.Solution?.Id ?? Guid.Empty,
                Solution = survey.Solution,
                DateTime = survey.DateTime,
                Depth = survey.Depth,
                MWDInclination = survey.MWDInclination,
                MWDShortCollar = survey.MWDShortCollar,
                SolAzm = survey.SolAzm,
                SolInc = survey.SolInc,
                NomBt = survey.NomBt,
                NomDip = survey.NomDip,
                NomDeclination = survey.NomDeclination,
                NomGrid = survey.NomGrid,
                AziErrorAzimuth = survey.AziErrorAzimuth,
                AziErrorAzimuthMagnetic = survey.AziErrorAzimuthMagnetic,
                AziErrorInclination = survey.AziErrorInclination,
                AziErrorDipe = survey.AziErrorDipe,
                AziErrorBe = survey.AziErrorBe,
                AziErrorDecle = survey.AziErrorDecle,
                AziErrorConve = survey.AziErrorConve,
                AziErrordDecle = survey.AziErrordDecle,
                Deleted = survey.Deleted,
                CreatedBy = survey.CreatedBy,
                CreatedTime = survey.CreatedTime,
                Id = survey.Id,
            };
        }

        public static Survey MapCorrectedSurveyToSurvey(this CorrectedSurvey surveyToMap)
        {
            return new Survey()
            {
                Id = surveyToMap.Id,
                Depth = surveyToMap.Depth,
                Gx = surveyToMap.Gx ?? 0.0,
                Gy = surveyToMap.Gy ?? 0.0,
                Gz = surveyToMap.Gz ?? 0.0,
                Bx = surveyToMap.Bx ?? 0.0,
                By = surveyToMap.By ?? 0.0,
                Bz = surveyToMap.Bz ?? 0.0,
                Enabled = surveyToMap.Enabled,
                DateTime = surveyToMap.DateTime,
                Run = surveyToMap.Run?.ToString(),
                AziErrorBe = surveyToMap.AziErrorBe,
                AzimuthType = surveyToMap.AzimuthType,
                AziErrorDipe = surveyToMap.AziErrorDipe,
                RunId = surveyToMap.Run?.Id ?? Guid.Empty,
                AziErrordDecle = surveyToMap.AziErrordDecle,
                SurveyRecorded = surveyToMap.SurveyRecorded,
                Temperature = surveyToMap.Temperature ?? 0.0,
                AziErrorAzimuth = surveyToMap.AziErrorAzimuth,
                MWDLongCollar = surveyToMap.MWDLongCollar ?? 0.0,
                WellId = surveyToMap.Run?.Well?.Id ?? Guid.Empty,
                MWDInclination = surveyToMap.MWDInclination ?? 0.0,
                MWDShortCollar = surveyToMap.MWDShortCollar ?? 0.0,
                GxyzInclination = surveyToMap.GxyzInclination ?? 0.0,
                AziErrorInclination = surveyToMap.AziErrorInclination,
                ReferenceInclination = surveyToMap.ReferenceInclination ?? 0.0,
                AziErrorAzimuthMagnetic = surveyToMap.AziErrorAzimuthMagnetic,
                //Azimuth = surveyToMap.Azimuth
            };
        }

        public static void ResetValues(this CorrectedSurvey correctedSurvey, RawSurvey survey)
        {
            if (survey == null)
            {
                return;
            }
            //Listen = false;  Not Using.
            try
            {
                //use field level accessors to avoid multiple property change events.
                //will trigger the change event once at the end.
                correctedSurvey.RawSurvey = survey;
                correctedSurvey.DateTime = survey.DateTime;
                correctedSurvey.Depth = survey.Depth;
                correctedSurvey.Enabled = survey.Enabled;
                correctedSurvey.PumpStatus = survey.PumpStatus;
                correctedSurvey.Gx = survey.Gx;
                correctedSurvey.Gy = survey.Gy;
                correctedSurvey.Gz = survey.Gz;
                correctedSurvey.Bx = survey.Bx;
                correctedSurvey.By = survey.By;
                correctedSurvey.Bz = survey.Bz;
                correctedSurvey.MWDInclination = survey.MWDInclination;
                correctedSurvey.MWDLongCollar = survey.MWDLongCollar;
                correctedSurvey.MWDShortCollar = survey.MWDShortCollar;
                correctedSurvey.AzimuthType = survey.AzimuthType;
                correctedSurvey.SagInclination = survey.SagInclination;
                correctedSurvey.RigTimeOffset = survey.RigTimeOffset;
                correctedSurvey.TypeEditedBy = survey.LastEditedBy;
                correctedSurvey.GravityToolFace = null;
                correctedSurvey.GxyzInclination = null;
                correctedSurvey.GxyInclination = null;
                correctedSurvey.GzInclination = null;
                correctedSurvey.ReferenceInclination = null;
                correctedSurvey.GTotal = null;
                correctedSurvey.Goxy = null;
                correctedSurvey.RigInclination = null;
                correctedSurvey.RigAzimuthSC = null;
                correctedSurvey.RigAzimuthLC = null;
                correctedSurvey.BtMeasured = null;
                correctedSurvey.DipMeasured = null;
                correctedSurvey.BvMeasured = null;
                correctedSurvey.BhMeasured = null;
                correctedSurvey.SolAzm = null;
                correctedSurvey.SolAzmSc = null;
                correctedSurvey.SolAzmLc = null;
                correctedSurvey.SolDec = null;
                correctedSurvey.SolGridConv = null;
                correctedSurvey.SolInc = null;
                correctedSurvey.SolBz = null;
                correctedSurvey.SolGt = null;
                correctedSurvey.SolBt = null;
                correctedSurvey.SolDip = null;
                correctedSurvey.SolBtDip = null;
                correctedSurvey.SolBv = null;
                correctedSurvey.SolBh = null;
                correctedSurvey.NomBv = null;
                correctedSurvey.NomBh = null;
                correctedSurvey.NomGt = null;
                correctedSurvey.NomBt = null;
                correctedSurvey.NomDip = null;
                correctedSurvey.NomBtDip = null;
                correctedSurvey.ObservatoryDipRaw = null;
                correctedSurvey.ObservatoryBTRaw = null;
                correctedSurvey.ObservatoryDecRaw = null;
                correctedSurvey.ObservatoryTimeOffset = null;
                correctedSurvey.ObservatoryDateTimeRaw = null;
                correctedSurvey.AppliedIcarusSolution = null;
                correctedSurvey.AppliedCazandraSolution = null;
                correctedSurvey.AppliedService = null;
                correctedSurvey.SurveyStatus = MxSSurveyStatus.New;
                correctedSurvey.ErrorMessage = string.Empty;
                correctedSurvey.NomDeclination = null;
                correctedSurvey.NomGrid = null;
                correctedSurvey.IcaUsed = MxSConstants.ServiceNotUsedFlagValue;
                correctedSurvey.CazUsed = MxSConstants.ServiceNotUsedFlagValue;
                correctedSurvey.BtLimit = null;
                correctedSurvey.BtRssDynamicLimit = null;
                correctedSurvey.DipLimit = null;
                correctedSurvey.DipRssDynamicLimit = null;
                correctedSurvey.BtDipLimit = null;
                correctedSurvey.BtDipRssDynamicLimit = null;
                correctedSurvey.AzRssDynamicLimit = null;
                correctedSurvey.Comment = string.Empty;
                correctedSurvey.ASAResult = MxSRuleResultEnum.NotExecuted;
                correctedSurvey.SISA = null;
                correctedSurvey.IcadGx = null;
                correctedSurvey.IcadGy = null;
                correctedSurvey.IcadGz = null;
                correctedSurvey.IcaSFGx = null;
                correctedSurvey.IcaSFGy = null;
                correctedSurvey.CazdBx = null;
                correctedSurvey.CazdBy = null;
                correctedSurvey.CazdBz = null;
                correctedSurvey.CazSFBx = null;
                correctedSurvey.CazSFBy = null;
                correctedSurvey.CourseLength = null;

                correctedSurvey.SoldBx = null;
                correctedSurvey.SoldBy = null;
                correctedSurvey.SoldBz = null;

                correctedSurvey.LCBtQCDelta = null;
                correctedSurvey.SCBtQCDelta = null;
                correctedSurvey.CazBtQCDelta = null;
                correctedSurvey.LCDipQCDelta = null;
                correctedSurvey.SCDipQCDelta = null;
                correctedSurvey.CazDipQCDelta = null;

                correctedSurvey.NorthSouth = null;
                correctedSurvey.EastWest = null;
                correctedSurvey.Northing = null;
                correctedSurvey.Easting = null;
                correctedSurvey.TVD = null;
                correctedSurvey.TVDss = null;
                correctedSurvey.Latitude = null;
                correctedSurvey.Longitude = null;
                correctedSurvey.DCLatitude = null;
                correctedSurvey.DCLongitude = null;
                correctedSurvey.DCTVDss = null;
                correctedSurvey.NorthReference = null;

                if (correctedSurvey.UncertaintyValue != null)
                {
                    correctedSurvey.UncertaintyValue.SigmaN = null;
                    correctedSurvey.UncertaintyValue.SigmaE = null;
                    correctedSurvey.UncertaintyValue.SigmaV = null;
                    correctedSurvey.UncertaintyValue.SigmaL = null;
                    correctedSurvey.UncertaintyValue.SigmaH = null;
                    correctedSurvey.UncertaintyValue.SigmaA = null;
                    correctedSurvey.UncertaintyValue.BiasN = null;
                    correctedSurvey.UncertaintyValue.BiasE = null;
                    correctedSurvey.UncertaintyValue.BiasV = null;
                    correctedSurvey.UncertaintyValue.BiasH = null;
                    correctedSurvey.UncertaintyValue.BiasL = null;
                    correctedSurvey.UncertaintyValue.BiasA = null;
                    correctedSurvey.UncertaintyValue.CorrHL = null;
                    correctedSurvey.UncertaintyValue.CorrHA = null;
                    correctedSurvey.UncertaintyValue.CorrLA = null;
                    correctedSurvey.UncertaintyValue.HMajSA = null;
                    correctedSurvey.UncertaintyValue.HMinSA = null;
                    correctedSurvey.UncertaintyValue.RotAng = null;
                    correctedSurvey.UncertaintyValue.SemiAx1 = null;
                    correctedSurvey.UncertaintyValue.SemiAx2 = null;
                    correctedSurvey.UncertaintyValue.SemiAx3 = null;
                    correctedSurvey.UncertaintyValue.CovNN = null;
                    correctedSurvey.UncertaintyValue.CovNE = null;
                    correctedSurvey.UncertaintyValue.CovNV = null;
                    correctedSurvey.UncertaintyValue.CovEE = null;
                    correctedSurvey.UncertaintyValue.CovEV = null;
                    correctedSurvey.UncertaintyValue.CovVV = null;
                    correctedSurvey.UncertaintyValue.ToolCode = string.Empty;
                }

                correctedSurvey.SurveyType = MxSSurveyType.Undefined;


                for (int num = correctedSurvey.MaxSurveyRuleSetResponse.Count - 1; num >= 0; num--)
                {
                    //correctedSurvey.MaxSurveyRuleSetResponse.Delete(MaxSurveyRuleSetResponse[i]);
                    correctedSurvey.MaxSurveyRuleSetResponse.RemoveAt(num);
                }
                for (var num2 = correctedSurvey.UncertaintyValues.Count - 1; num2 >= 0; num2--)
                {
                    //correctedSurvey.UncertaintyValues.Delete(UncertaintyValues[i]);
                    correctedSurvey.UncertaintyValues.RemoveAt(num2);
                }
                for (var num3 = correctedSurvey.BgsDataPoints.Count - 1; num3 >= 0; num3--)
                {
                    //correctedSurvey.BgsDataPoints.Delete(BgsDataPoints[i]);
                    correctedSurvey.BgsDataPoints.RemoveAt(num3);
                }
            }
            finally
            {

                //This code was commented in new refactored model by suhail.

                //Listen = true; 
                //if (survey.State == MxSState.Added || State == MxSState.Added)
                //{
                //    SetState(State.Added);
                //    for (var i = Values.Count - 1; i >= 0; i--)
                //    {
                //        Values.Delete(Values[i]);
                //    }
                //}
                //else
                //{
                //    for (var i = Values.Count - 1; i >= 0; i--)
                //    {
                //        if (Values[i].State == State.Added)
                //        {
                //            Values.Delete(Values[i]);
                //        }
                //        else
                //        {
                //            Values[i].SetState(State.Modified);
                //        }
                //    }
                //    SetState(State.Modified);
                //}

                for (int num4 = correctedSurvey.Values.Count - 1; num4 >= 0; num4--)
                {
                    //Values.Delete(Values[num4]);
                    // Changed by Suhail
                    correctedSurvey.Values.RemoveAt(num4);
                }
            }
        }

        public static void AddUncertaintyValues(this CorrectedSurvey correctedSurvey, Uncertainty uncertainty)
        {
            uncertainty.CorrectedSurvey = correctedSurvey;
            uncertainty.CorrectedSurveyId = correctedSurvey.Id;
            correctedSurvey.UncertaintyValues.Add(uncertainty);
        }

        public static void AddCorrectedSurveyValue(this CorrectedSurvey correctedSurvey, CorrectedSurveyValues correctedSurveyValues)
        {
            correctedSurveyValues.CorrectedSurvey = correctedSurvey;
            correctedSurvey.Values.Add(correctedSurveyValues);
        }

        public static void AddBgsDatapoint(this CorrectedSurvey correctedSurvey, BGSDataPoint bgsDataPoint)
        {
            bgsDataPoint.CorrectedSurvey = correctedSurvey;
            bgsDataPoint.CorrectedSurveyId = correctedSurvey.Id;
            correctedSurvey.BgsDataPoints.Add(bgsDataPoint);
        }

        public static void DeleteBgsDatapoint(this CorrectedSurvey correctedSurvey, BGSDataPoint bgsDatapoint)
        {
            bgsDatapoint.CorrectedSurvey = null;
            bgsDatapoint.CorrectedSurveyId = Guid.Empty;
            correctedSurvey.BgsDataPoints.Remove(bgsDatapoint);
        }

        public static void DeleteUncertaintyValue(this CorrectedSurvey correctedSurvey, Uncertainty uncertainty)
        {
            uncertainty.CorrectedSurvey = null;
            uncertainty.CorrectedSurveyId = Guid.Empty;
            correctedSurvey.UncertaintyValues.Remove(uncertainty);
        }

        public static void DeleteUncertaintyValues(this CorrectedSurvey correctedSurvey)
        {
            correctedSurvey.UncertaintyValues.RemoveAll(v => v.Id != Guid.Empty);
        }

        public static void DeleteCorrectedSurveyValue(this CorrectedSurvey correctedSurvey, CorrectedSurveyValues correctedSurveyValues)
        {
            correctedSurveyValues.CorrectedSurvey = null;
            correctedSurvey.Values.Remove(correctedSurveyValues);
        }

        public static void AddMaxSurveyRuleSetResponse(this CorrectedSurvey correctedSurvey, MaxSurveyRuleSetResponse maxSurveyRuleSetResponse)
        {
            maxSurveyRuleSetResponse.CorrectedSurvey = correctedSurvey;
            foreach (MaxSurveyRuleResponse ruleResponse in maxSurveyRuleSetResponse.RulesResponse)
            {
                ruleResponse.MaxSurveyRuleSetResponse = maxSurveyRuleSetResponse;
            }
            foreach (MaxSurveyActionResult actionResult in maxSurveyRuleSetResponse.ActionResults)
            {
                actionResult.MaxSurveyRuleSetResponse = maxSurveyRuleSetResponse;
            }
            //There will be only one rule set response for a given corrected survey. We need to delete the remaining ones.

            foreach (MaxSurveyRuleSetResponse response in correctedSurvey.MaxSurveyRuleSetResponse)
            {
                //TODO: Suhail - Need to find alternate 
                //response.Delete();
            }
            correctedSurvey.MaxSurveyRuleSetResponse.Add(maxSurveyRuleSetResponse);

        }

        public static void Reset(this CorrectedSurvey correctedSurvey)
        {
            correctedSurvey.ResetValues(correctedSurvey.RawSurvey);
        }

        public static void ResetUncertainityValues(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.UncertaintyValue != null)
            {
                // TODO: Suhail - Related to state
                //correctedSurvey.UncertaintyValue.SetState(State.Modified);
                correctedSurvey.UncertaintyValue.Reset();
            }
        }

        public static void ResetPositionValues(this CorrectedSurvey correctedSurvey)
        {
            correctedSurvey.NorthSouth = null;
            correctedSurvey.EastWest = null;
            correctedSurvey.Northing = null;
            correctedSurvey.Easting = null;
            correctedSurvey.TVD = null;
            correctedSurvey.TVDss = null;
            correctedSurvey.Latitude = null;
            correctedSurvey.Longitude = null;
            correctedSurvey.CourseLength = null;
        }



        public static void ResetRigTimeOffset(this CorrectedSurvey correctedSurvey)
        {
            //if (ShouldApplyManualTimeOffset(correctedSurvey))  changed by Naveen Kumar
            if (correctedSurvey.ShouldApplyManualTimeOffset())
            {
                correctedSurvey.RigTimeOffset = correctedSurvey.Solution.RigTimeOffset;
                correctedSurvey.ManualTimeOffsetFlag = true;
            }
            else
            {
                correctedSurvey.RigTimeOffset = correctedSurvey.RawSurvey.RigTimeOffset;
                correctedSurvey.ManualTimeOffsetFlag = false;
            }
        }

        public static bool ShouldApplyManualTimeOffset(this CorrectedSurvey correctedSurvey)
        {
            bool result = false;
            if (correctedSurvey.Solution != null)
            {
                if (correctedSurvey.Solution.ForceManualTimeOffset)
                {
                    result = true;
                }
                else
                {
                    if (correctedSurvey.Solution.RigTimeOffset != null)
                    {
                        //result = !IsAutoTimeOffsetExists(correctedSurvey);  changed by Naveen kumar
                        result = !correctedSurvey.IsAutoTimeOffsetExists();
                    }
                }
            }
            return result;
        }

        public static bool IsAutoTimeOffsetExists(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.RawSurvey != null)
            {
                return correctedSurvey.RawSurvey.RigTimeOffset != null;
            }
            return false;
        }

        public static void UpdateSolution(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.RawSurvey != null)
            {
                correctedSurvey.RawSurvey.UpdateSolution();
            }
        }

        public static bool HasSolution(this CorrectedSurvey correctedSurvey)
        {
            return correctedSurvey.Solution != null;
        }

        public static CorrectedSurveyValues GetMagneticParameters(this CorrectedSurvey correctedSurvey)
        {
            MxSCalculationType service = correctedSurvey.GetMagneticValsTypeFromSolution();
            return correctedSurvey.GetCorrectedSurveyValuesByCalculationType(service) ?? new CorrectedSurveyValues() { CalculationType = service };
        }

        public static MxSCalculationType GetMagneticValsTypeFromSolution(this CorrectedSurvey correctedSurvey)
        {
            const int ifr1Flag = 2;
            const int ifr2Flag = 4;
            if (correctedSurvey.Solution != null)
            {
                if (correctedSurvey.CheckFlag(correctedSurvey.Solution.Service, ifr2Flag))
                {
                    return MxSCalculationType.IIFR;
                }
                if (correctedSurvey.CheckFlag(correctedSurvey.Solution.Service, ifr1Flag))
                {
                    return MxSCalculationType.IFR;
                }
            }
            return MxSCalculationType.MFM;
        }

        public static CorrectedSurveyValues GetCorrectedSurveyValuesByCalculationType(this CorrectedSurvey correctedSurvey, MxSCalculationType type)
        {
            return correctedSurvey.Values.FirstOrDefault(item => item.CalculationType == type && item.State != MxSState.Deleted);
        }

        public static CorrectedSurveyValues GetAzRSSMinValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = new CorrectedSurveyValues();
            correctedSurveyValues.AzRSSMin = Convert.ToDouble(correctedSurvey.SolAzm ?? double.NaN) -
                                             Convert.ToDouble(correctedSurvey.AzRssDynamicLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetAzRSSMaxValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = new CorrectedSurveyValues();
            correctedSurveyValues.AzRSSMax = Convert.ToDouble(correctedSurvey.SolAzm ?? double.NaN) +
                                             Convert.ToDouble(correctedSurvey.AzRssDynamicLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static bool HasCazandraResult(this CorrectedSurvey correctedSurvey)
        {
            //CorrectedSurveyValues item = GetCorrectedSurveyValuesByCalculationType(correctedSurvey,MxSCalculationType.Caz); changed by Naveen Kumar
            CorrectedSurveyValues item = correctedSurvey.GetCorrectedSurveyValuesByCalculationType(MxSCalculationType.Caz);

            return item != null &&
                   item.CorrectedSurvey != null &&
                   (item.CorrectedSurvey.SurveyStatus == MxSSurveyStatus.Processed ||
                    item.CorrectedSurvey.SurveyStatus == MxSSurveyStatus.BackCorrected);
        }

        public static CorrectedSurveyValues GetNomBTotalQCLimitsValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = new CorrectedSurveyValues();
            correctedSurveyValues.BTotalNomMin = Convert.ToDouble(correctedSurvey.NomBt ?? double.NaN) -
                                                 Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            correctedSurveyValues.BTotalNomMax = Convert.ToDouble(correctedSurvey.NomBt ?? double.NaN) +
                                                 Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetMFMBTotalQCLimitsValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = GetCorrectedSurveyValuesByCalculationType(correctedSurvey, MxSCalculationType.MFM) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.MFM };
            correctedSurveyValues.BTotalMFMQCMax = correctedSurveyValues.BTotal +
                                                   Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            correctedSurveyValues.BTotalMFMQCMin = correctedSurveyValues.BTotal -
                                                   Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetMFMDipQCLimitValues(this CorrectedSurvey correctedSurvey)
        {
            // CorrectedSurveyValues correctedSurveyValues = GetCorrectedSurveyValuesByCalculationType(correctedSurvey, MxSCalculationType.MFM) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.MFM }; changed by Naveen kumar
            CorrectedSurveyValues correctedSurveyValues = correctedSurvey.GetCorrectedSurveyValuesByCalculationType(MxSCalculationType.MFM) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.MFM };

            correctedSurveyValues.DipMFMQCMin = correctedSurveyValues.Dip -
                                                Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            correctedSurveyValues.DipMFMQCMax = correctedSurveyValues.Dip +
                                                Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetIFRBTotalQCLimitsValues(this CorrectedSurvey correctedSurvey)
        {
            //CorrectedSurveyValues correctedSurveyValues = GetCorrectedSurveyValuesByCalculationType(correctedSurvey, MxSCalculationType.IFR) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.IFR }; changed by Naveen Kumar
            CorrectedSurveyValues correctedSurveyValues = correctedSurvey.GetCorrectedSurveyValuesByCalculationType(MxSCalculationType.IFR) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.IFR };

            correctedSurveyValues.BTotalIFRQCMax = correctedSurveyValues.BTotal +
                                                   Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            correctedSurveyValues.BTotalIFRQCMin = correctedSurveyValues.BTotal -
                                                   Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetNomDipQCLimitValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = new CorrectedSurveyValues();
            correctedSurveyValues.DipNomMin = Convert.ToDouble(correctedSurvey.NomDip ?? double.NaN) -
                                              Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            correctedSurveyValues.DipNomMax = Convert.ToDouble(correctedSurvey.NomDip ?? double.NaN) +
                                              Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetNomBtDipQCMaxLimitValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = new CorrectedSurveyValues();
            correctedSurveyValues.BtDipNomMax = Convert.ToDouble(correctedSurvey.NomBtDip ?? double.NaN) +
                                                Convert.ToDouble(correctedSurvey.BtDipLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetIFRDipQCLimitValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = correctedSurvey.GetCorrectedSurveyValuesByCalculationType(MxSCalculationType.IFR) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.IFR };
            correctedSurveyValues.DipIFRQCMin = correctedSurveyValues.Dip -
                                                Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            correctedSurveyValues.DipIFRQCMax = correctedSurveyValues.Dip +
                                                Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetIIFRBTotalQCLimitsValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = correctedSurvey.GetCorrectedSurveyValuesByCalculationType(MxSCalculationType.IIFR) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.IIFR };
            correctedSurveyValues.BTotalIIFRQCMax = correctedSurveyValues.BTotal +
                                                    Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            correctedSurveyValues.BTotalIIFRQCMin = correctedSurveyValues.BTotal -
                                                    Convert.ToDouble(correctedSurvey.BtLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static CorrectedSurveyValues GetIIFRDipQCLimitValues(this CorrectedSurvey correctedSurvey)
        {
            CorrectedSurveyValues correctedSurveyValues = correctedSurvey.GetCorrectedSurveyValuesByCalculationType(MxSCalculationType.IIFR) ?? new CorrectedSurveyValues() { CalculationType = MxSCalculationType.IIFR };
            correctedSurveyValues.DipIIFRQCMin = correctedSurveyValues.Dip -
                                                 Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            correctedSurveyValues.DipIIFRQCMax = correctedSurveyValues.Dip +
                                                 Convert.ToDouble(correctedSurvey.DipLimit ?? double.NaN);
            return correctedSurveyValues;
        }

        public static void UpdateRawSurveyForPMRFields(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.RawSurvey == null)
            {
                return;
            }

            correctedSurvey.RawSurvey.BtMeasured = correctedSurvey.BtMeasured;
            correctedSurvey.RawSurvey.DipMeasured = correctedSurvey.DipMeasured;
            correctedSurvey.RawSurvey.GxyInclination = correctedSurvey.GxyInclination;
            correctedSurvey.RawSurvey.GxyzInclination = correctedSurvey.GxyzInclination;
            correctedSurvey.RawSurvey.GzInclination = correctedSurvey.GzInclination;
            correctedSurvey.RawSurvey.GtRawQC = correctedSurvey.CalculateGtRawQc();
        }

        public static double? CalculateGtRawQc(this CorrectedSurvey correctedSurvey)
        {
            return (1 - correctedSurvey.GTotal) * -1;
        }

        public static CorrectedSurvey CloneForWorkbench(this CorrectedSurvey correctedSurvey)
        {
            // var copiedData = new CorrectedSurvey(correctedSurvey);
            var rawSurvey = new RawSurvey(correctedSurvey.RawSurvey);
            return rawSurvey.CorrectedSurvey;
        }

        public static bool IsIgnoreOrExcludeSurvey(this CorrectedSurvey correctedSurvey)
        {
            try
            {
                if (correctedSurvey.Solution != null)
                {
                    string dateTimePreviousSurvey = correctedSurvey.DateTime.ToString(MxSConstants.SurveyIndicationFormat);
                    if (WellPropertyFinder.CheckFlag(correctedSurvey.Solution.Service, MxSConstants.IcaFlag))
                    {
                        string excludedSurvey = correctedSurvey.Solution.IcarusExcludedSurveys;
                        if (!string.IsNullOrEmpty(excludedSurvey) && excludedSurvey.Contains(dateTimePreviousSurvey))
                            return true;
                        string ignoredSurvey = correctedSurvey.Solution.IcarusIngoredSurveys;
                        if (!string.IsNullOrEmpty(ignoredSurvey) && ignoredSurvey.Contains(dateTimePreviousSurvey))
                            return true;
                    }
                    if (WellPropertyFinder.CheckFlag(correctedSurvey.Solution.Service, MxSConstants.CazFlag))
                    {
                        string excludedSurvey = correctedSurvey.Solution.CazandraExcludedSurveys;
                        if (!string.IsNullOrEmpty(excludedSurvey) && excludedSurvey.Contains(dateTimePreviousSurvey))
                            return true;
                        string ignoredSurvey = correctedSurvey.Solution.CazandraIngoredSurveys;
                        if (!string.IsNullOrEmpty(ignoredSurvey) && ignoredSurvey.Contains(dateTimePreviousSurvey))
                            return true;
                    }
                }
            }
            catch { }
            return false;
        }

        public static void ResetCazVariablesToNull(this CorrectedSurvey correctedSurvey)
        {
            correctedSurvey.CazdBx = null;
            correctedSurvey.CazdBy = null;
            correctedSurvey.CazdBz = null;
            correctedSurvey.CazSFBx = null;
            correctedSurvey.CazSFBy = null;
        }

        public static void ResetIcaVariablesToNull(this CorrectedSurvey correctedSurvey)
        {
            correctedSurvey.IcadGx = null;
            correctedSurvey.IcadGy = null;
            correctedSurvey.IcadGz = null;
            correctedSurvey.IcaSFGx = null;
            correctedSurvey.IcaSFGy = null;
        }

        public static Waypoint GetRelateWaypoint(this CorrectedSurvey correctedSurvey, MxSWaypointType waypointType)
        {
            List<Waypoint> waypoints = correctedSurvey.GetWayPoints();
            if (waypoints == null)
            {
                return null;
            }
            return waypoints.FirstOrDefault(
                wp =>
                    wp.StartDepth <= correctedSurvey.Depth &&
                    wp.EndDepth > correctedSurvey.Depth &&
                    wp.State != MxSState.Deleted &&
                    wp.Type == waypointType);
        }

        internal static List<Waypoint> GetWayPoints(this CorrectedSurvey correctedSurvey)
        {
            Well well = correctedSurvey.GetWell();
            if (well == null)
            {
                return null;
            }
            return well.Waypoints;
        }

        public static Well GetWell(this CorrectedSurvey correctedSurvey)
        {
            Run run = correctedSurvey.GetRun();
            if (run == null)
            {
                return null;
            }
            return run.Well;
        }

        public static Run GetRun(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.RawSurvey == null)
            {
                return null;
            }
            return correctedSurvey.RawSurvey.Run;
        }

        public static Tuple<Guid, DateTime> GetObservatoryTimeRange(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.ObservatoryStation == null)
            {
                return null;
            }
            correctedSurvey.ResetRigTimeOffset();
            DateTime? atomicRigTime = correctedSurvey.GetAtomicRigTime();

            //rick; not sure why we convert to localtime to just convert to utc time.
            if (atomicRigTime.HasValue)
            {
                atomicRigTime = atomicRigTime.Value.ToLocalTime();
            }
            else
            {
                return null;
            }
            DateTime utcTime = atomicRigTime.Value.ToUniversalTime();

            if (utcTime == DateTime.MinValue || utcTime == DateTime.MaxValue)
            {
                return null;
            }

            return new Tuple<Guid, DateTime>(correctedSurvey.ObservatoryStation.Id, utcTime);
        }

        public static Solution FindSolution(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.RawSurvey == null)
            {
                return null;
            }
            return correctedSurvey.RawSurvey.FindSolution(correctedSurvey.Depth, correctedSurvey.PumpStatus);
        }

        public static MxSQCLevel GetThreeRanksQCLevel(this CorrectedSurvey correctedSurvey, double toQC, double baseQC, double limitQCLow, double limitQCHigh)
        {
            double delta = Math.Abs(toQC - baseQC);
            if (delta < Math.Abs(limitQCLow))
                return MxSQCLevel.Normal;
            if (delta < Math.Abs(limitQCHigh))
                return MxSQCLevel.Warning;
            return MxSQCLevel.Error;
        }

        private static bool CheckFlag(this CorrectedSurvey correctedSurvey, MxSSolutionService service, int flag)
        {
            return ((int)service & flag) > 0;
        }

        public static void AddForeignKey(this CorrectedSurvey correctedSurvey, RawSurvey rawsurvey)
        {            
            if (rawsurvey.CorrectedSurvey != null)
            {
                rawsurvey.CorrectedSurvey.RawSurveyId = rawsurvey.Id;
                if (rawsurvey.CorrectedSurvey.BgsDataPoints.Any() && rawsurvey.CorrectedSurvey.BgsDataPoints.Count > 0)
                {
                    foreach (var bgsdata in rawsurvey.CorrectedSurvey.BgsDataPoints)
                    {
                        bgsdata.AddForeignKey(rawsurvey.CorrectedSurvey);
                    }
                }
                if (rawsurvey.CorrectedSurvey.Values.Any() && rawsurvey.CorrectedSurvey.Values.Count > 0)
                {
                    foreach (var value in rawsurvey.CorrectedSurvey.Values)
                    {
                        value.AddForeignKey(rawsurvey.CorrectedSurvey);
                    }
                }
                if (rawsurvey.CorrectedSurvey.MaxSurveyRuleSetResponse.Any() && rawsurvey.CorrectedSurvey.MaxSurveyRuleSetResponse.Count > 0)
                {
                    foreach (var maxSurvey in rawsurvey.CorrectedSurvey.MaxSurveyRuleSetResponse)
                    {
                        maxSurvey.AddForeignKey(rawsurvey.CorrectedSurvey);                        
                    }
                }
                if (rawsurvey.CorrectedSurvey.UncertaintyValues.Any() && rawsurvey.CorrectedSurvey.UncertaintyValues.Count > 0)
                {
                    foreach (var uncertaintyValue in rawsurvey.CorrectedSurvey.UncertaintyValues)
                    {
                        uncertaintyValue.AddForeignKey(rawsurvey.CorrectedSurvey);
                    }
                }
            }
        }
    }
}
