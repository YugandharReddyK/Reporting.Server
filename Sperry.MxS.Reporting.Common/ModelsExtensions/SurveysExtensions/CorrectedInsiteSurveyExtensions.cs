using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    public static class CorrectedInsiteSurveyExtensions
    {
        public static CorrectedInsiteSurvey FromCorrectedSurvey(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.RawSurvey == null)
            {
                //throw new NullReferenceException(GenerateNullPropertyErrorMessage("RawSurvey")); Modified by Naveen Kumar
                throw new NullReferenceException(GenerateNullPropertyErrorMessage("RawSurvey"));
            }

            CorrectedInsiteSurvey result = new CorrectedInsiteSurvey();
            result.AssignGeneralSurveyValues(correctedSurvey);
            result.AssignMagneticsSurveyValues(correctedSurvey);
            result.AssignSolutionSurveyValues(correctedSurvey);
            result.AssignNominalSurveyValues(correctedSurvey);
            result.AssignQCSurveyValues(correctedSurvey);
            result.AssignSolutionTypeSurveyValues(correctedSurvey);

            return result;
        }

        private static string GenerateNullPropertyErrorMessage(string propertyName)
        {
            return $"{propertyName} property can not be null on CorrectedSurvey";
        }


        private static void AssignSolutionTypeSurveyValues(this CorrectedInsiteSurvey correctedInsiteSurvey, CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey.AppliedCazandraSolution != null)
            {

                switch (correctedSurvey.AppliedCazandraSolution)
                {
                    case MxSConstant.LongCollar:
                        correctedInsiteSurvey.AzimuthType = MxSAzimuthTypeEnum.LongCollar;
                        break;
                    case MxSConstant.ShortCollar:
                        correctedInsiteSurvey.AzimuthType = MxSAzimuthTypeEnum.ShortCollar;
                        break;
                    default:
                        correctedInsiteSurvey.AzimuthType = MxSAzimuthTypeEnum.NA;
                        break;
                }
            }

            correctedInsiteSurvey.Service = correctedSurvey.AppliedService;
            correctedInsiteSurvey.IcarusSolution = correctedSurvey.AppliedIcarusSolution;
            correctedInsiteSurvey.CazandraSolution = correctedSurvey.AppliedCazandraSolution;
        }

        private static void AssignQCSurveyValues(this CorrectedInsiteSurvey correctedInsiteSurvey, CorrectedSurvey correctedSurvey)
        {
            correctedInsiteSurvey.DipQcLimit = correctedSurvey.Run.Well.DipQcLimit;
            correctedInsiteSurvey.BtQcLimit = correctedSurvey.Run.Well.BtQcLimit;
            correctedInsiteSurvey.GtQcLimit = correctedSurvey.Run.Well.GtQcLimit;
            correctedInsiteSurvey.GtQcDelta = -(correctedSurvey.NomGt - correctedSurvey.SolGt);
            correctedInsiteSurvey.DipQcDelta = -(correctedSurvey.NomDip - correctedSurvey.SolDip);
            correctedInsiteSurvey.BtQcDelta = -(correctedSurvey.NomBt - correctedSurvey.SolBt);
        }

        private static void AssignNominalSurveyValues(this CorrectedInsiteSurvey correctedInsiteSurvey, CorrectedSurvey correctedSurvey)
        {
            correctedInsiteSurvey.NomBt = correctedSurvey.NomBt;
            correctedInsiteSurvey.NomBtDip = correctedSurvey.NomBtDip;
            correctedInsiteSurvey.NomDip = correctedSurvey.NomDip;
            correctedInsiteSurvey.NomGt = correctedSurvey.NomGt;
            correctedInsiteSurvey.NomGrid = correctedSurvey.NomGrid;
            correctedInsiteSurvey.Declination = correctedSurvey.NomDeclination;


        }

        private static void AssignSolutionSurveyValues(this CorrectedInsiteSurvey correctedInsiteSurvey, CorrectedSurvey correctedSurvey)
        {
            correctedInsiteSurvey.SolAzm = correctedSurvey.SolAzm;
            correctedInsiteSurvey.SolAzmLc = correctedSurvey.SolAzmLc;
            correctedInsiteSurvey.SolAzmSc = correctedSurvey.SolAzmSc;
            correctedInsiteSurvey.SolBt = correctedSurvey.SolBt;
            correctedInsiteSurvey.SolBtDip = correctedSurvey.SolBtDip;
            correctedInsiteSurvey.SolBz = correctedSurvey.SolBz;
            correctedInsiteSurvey.SolDec = correctedSurvey.SolDec;
            correctedInsiteSurvey.SolDip = correctedSurvey.SolDip;
            correctedInsiteSurvey.SolGridConv = correctedSurvey.SolGridConv;
            correctedInsiteSurvey.SolGt = correctedSurvey.SolGt;
            correctedInsiteSurvey.SolInc = correctedSurvey.SolInc;
            correctedInsiteSurvey.IcaUsed = correctedSurvey.IcaUsed;
            correctedInsiteSurvey.CazUsed = correctedSurvey.CazUsed;
        }

        private static void AssignMagneticsSurveyValues(this CorrectedInsiteSurvey correctedInsiteSurvey, CorrectedSurvey correctedSurvey)
        {
            correctedInsiteSurvey.DipMeasured = correctedSurvey.DipMeasured;
            correctedInsiteSurvey.BhMeasured = correctedSurvey.BhMeasured;
            correctedInsiteSurvey.BtMeasured = correctedSurvey.BtMeasured;
            correctedInsiteSurvey.BvMeasured = correctedSurvey.BvMeasured;
        }

        private static void AssignGeneralSurveyValues(this CorrectedInsiteSurvey correctedInsiteSurvey, CorrectedSurvey correctedSurvey)
        {
            correctedInsiteSurvey.DateTime = correctedSurvey.DateTime;
            correctedInsiteSurvey.Depth = correctedSurvey.Depth;
            correctedInsiteSurvey.Temperature = correctedSurvey.RawSurvey.Temperature;
            correctedInsiteSurvey.PumpStatus = correctedSurvey.PumpStatus;
            correctedInsiteSurvey.GravityToolFace = correctedSurvey.GravityToolFace;
            correctedInsiteSurvey.ToolfaceOffset = correctedSurvey.Run.ToolfaceOffset;
            correctedInsiteSurvey.AzimuthSolution = correctedSurvey.AzimuthSolution;
            if (correctedSurvey.Solution != null)
                correctedInsiteSurvey.InclinationSolution = (MxSInclinationSolutionType)Enum.ToObject(typeof(MxSInclinationSolutionType), correctedSurvey.Solution.InclinationSolution);
            correctedInsiteSurvey.RawInclination = correctedSurvey.GxyzInclination;
            correctedInsiteSurvey.NorthReference = correctedSurvey.NorthReference;
            correctedInsiteSurvey.DistanceToBit = correctedSurvey.Run.DistanceToBit;
            correctedInsiteSurvey.Enabled = correctedSurvey.Enabled;
            correctedInsiteSurvey.Bx = correctedSurvey.Bx;
            correctedInsiteSurvey.By = correctedSurvey.By;
            correctedInsiteSurvey.Bz = correctedSurvey.Bz;
            correctedInsiteSurvey.Gx = correctedSurvey.Gx;
            correctedInsiteSurvey.Gy = correctedSurvey.Gy;
            correctedInsiteSurvey.Gz = correctedSurvey.Gz;
            correctedInsiteSurvey.RunNumber = correctedSurvey.Run.RunNumber;
            correctedInsiteSurvey.LastEditedTime = correctedSurvey.LastEditedTime;
            correctedInsiteSurvey.SurveyStatus = (MxSSurveyStatus)Enum.ToObject(typeof(MxSSurveyStatus), correctedSurvey.SurveyStatus);
            correctedInsiteSurvey.SurveyType = (MxSSurveyType)Enum.ToObject(typeof(MxSSurveyType), correctedSurvey.SurveyType);
            if (correctedSurvey.Solution != null)
            {
                correctedInsiteSurvey.Ipm = correctedSurvey.Solution.QCType.IsIncludeDynamic() ? correctedSurvey.Solution.IPMToolCode : string.Empty;
                correctedInsiteSurvey.Sigma = correctedSurvey.Solution.Sigma;
            }
            correctedInsiteSurvey.Deleted = correctedSurvey.Deleted;
        }
    }
}
