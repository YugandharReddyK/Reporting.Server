using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Utilities;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.Surveys;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    public class UncertProcessor
    {
        #region Constants

        private const double GTOTALMIN = 0.995;
        private const double GTOTALMAX = 1.005;
        private const double DIPMIN = -89;
        private const double DIPMAX = 89;
        private const double DECLINATIONMIN = -89;
        private const double DECLINATIONMAX = 89;
        private const double LATITUDEMIN = -89;
        private const double LATITUDEMAX = 89;
        private const int SIGMALEVELMIN = 0;
        private const int SIGMALEVELMAX = 5;
        private const double BTOTAL = 1000;
        private const double RAD = 3.141592654 / 180.00;
        private const double DEGREE = 180.00 / 3.141592654;
        private const double ERATE = 15.041;

        #endregion

        #region Fields

        private List<double> _lstKS = new List<double>();

        private bool _isValidLeg = false;

        private static double[] _prevMX = new double[3];
        private static double[] _prevMY = new double[3];
        private static double[] _prevMIS1 = new double[3];
        private static double[] _prevMIS2 = new double[3];
        private static double[] _prevMIS3 = new double[3];
        private static double[] _prevMIS4 = new double[3];
        private static double[] _prevSAG = new double[3];
        private static double[] _prevDREF = new double[3];
        private static double[] _prevDSF = new double[3];
        private static double[] _prevABIx = new double[3];
        private static double[] _prevABIy = new double[3];
        private static double[] _prevABITI1 = new double[3];
        private static double[] _prevABITI2 = new double[3];
        private static double[] _prevABIz = new double[3];
        private static double[] _prevASIx = new double[3];
        private static double[] _prevASIy = new double[3];
        private static double[] _prevASITI1 = new double[3];
        private static double[] _prevASITI2 = new double[3];
        private static double[] _prevASITI3 = new double[3];
        private static double[] _prevASIz = new double[3];
        private static double[] _prevMBIx = new double[3];
        private static double[] _prevMBIy = new double[3];
        private static double[] _prevMBITI1 = new double[3];
        private static double[] _prevMBITI2 = new double[3];
        private static double[] _prevMSIx = new double[3];
        private static double[] _prevMSIy = new double[3];
        private static double[] _prevMSITI1 = new double[3];
        private static double[] _prevMSITI2 = new double[3];
        private static double[] _prevMSITI3 = new double[3];
        private static double[] _prevMMudI = new double[3];

        private static double[] _prevAccB = new double[3];
        private static double[] _prevAccSFE = new double[3];
        private static double[] _prevAccMis = new double[3];
        private static double[] _prevGravB = new double[3];
        private static double[] _prevGxyB1 = new double[3];
        private static double[] _prevGxyB2 = new double[3];
        private static double[] _prevGxyGDep1 = new double[3];
        private static double[] _prevGxyGDep2 = new double[3];
        private static double[] _prevGxyGDep3 = new double[3];
        private static double[] _prevGxyGDep4 = new double[3];
        private static double[] _prevGxySFE = new double[3];
        private static double[] _prevGxyMis = new double[3];

        private static double[] _prevA3Bxy = new double[3];
        private static double[] _prevA3Bz = new double[3];
        private static double[] _prevA3SFE = new double[3];
        private static double[] _prevA3Mis = new double[3];
        private static double[] _prevGy3XYb1 = new double[3];
        private static double[] _prevGy3XYb2 = new double[3];
        private static double[] _prevGy3XYGDep1 = new double[3];
        private static double[] _prevGy3XYGDep2 = new double[3];
        private static double[] _prevGy3XYGDep3 = new double[3];
        private static double[] _prevGy3XYGDep4 = new double[3];
        private static double[] _prevGy3Zb = new double[3];
        private static double[] _prevGy3ZGDep1 = new double[3];
        private static double[] _prevGy3ZGDep2 = new double[3];
        private static double[] _prevGy3SFE = new double[3];
        private static double[] _prevGy3Mis = new double[3];

        private static double[] _prevAZ = new double[3];
        private static double[] _prevDBH = new double[3];
        private static double[] _prevDST = new double[3];
        private static double[] _prevMFI = new double[3];
        private static double[] _prevMDI = new double[3];

        private double[] _prevAZf = new double[3];
        private double[] _prevGYC = new double[3];
        private double[] _prevCNI = new double[3];
        private double[] _prevCNA = new double[3];
        private double[] _prevMUI = new double[3];
        private double[] _prevMUS = new double[3];
        private double[] _prevGDR = new double[3];

        private static double[] _prevAMIC = new double[3];
        private static double[] _prevAMID = new double[3];
        private static double[] _prevAMIL = new double[3];
        private static double[] _prevABx = new double[3];
        private static double[] _prevABy = new double[3];
        private static double[] _prevABTI1 = new double[3];
        private static double[] _prevABTI2 = new double[3];
        private static double[] _prevABz = new double[3];
        private static double[] _prevASx = new double[3];
        private static double[] _prevASy = new double[3];
        private static double[] _prevASTI1 = new double[3];
        private static double[] _prevASTI2 = new double[3];
        private static double[] _prevASTI3 = new double[3];
        private static double[] _prevASz = new double[3];
        private static double[] _prevMBx = new double[3];
        private static double[] _prevMBy = new double[3];
        private static double[] _prevMBTI1 = new double[3];
        private static double[] _prevMBTI2 = new double[3];
        private static double[] _prevMBz = new double[3];
        private static double[] _prevMSx = new double[3];
        private static double[] _prevMSy = new double[3];
        private static double[] _prevMSTI1 = new double[3];
        private static double[] _prevMSTI2 = new double[3];
        private static double[] _prevMSTI3 = new double[3];
        private static double[] _prevMSz = new double[3];
        private static double[] _prevMMud = new double[3];

        private static double[] _prevDSTb = new double[3];
        private static double[] _prevAMIDb = new double[3];
        private static double[] _prevMMudb = new double[3];
        private static double[] _prevMMudIb = new double[3];
        private static double[] _prevDrift = new double[3];

        private static double[,] _cDREFr = new double[3, 3];
        private static double[,] _cMFIr = new double[3, 3];
        private static double[,] _cMDIr = new double[3, 3];
        private static double[,] _cAZr = new double[3, 3];
        private static double[,] _cDBHr = new double[3, 3];
        private static double[,] _cGXYr = new double[3, 3];
        private static double[,] _cGZr = new double[3, 3];
        private double[,] _aDRB = new double[3, 3];
        private double[,] _aDRBMis = new double[3, 3];
        private double[,] _aDR = new double[3, 3];
        private double[,] _aDRMis = new double[3, 3];


        private const double conTF = 0;

        private static double _verticalPGlobalError, _verticalCGlobalError;
        private static double _verticalPSurBiasError, _verticalCSurBiasError;

        #endregion

        #region Properties


        public List<OdisseusToolCodeParams> Params = new List<OdisseusToolCodeParams>();
        public List<double> ToolCodeSectionsEndDepth = new List<double>();
        public List<double> ToolCodeSectionsStartDepth = new List<double>();
        public List<string> ToolCodeSectionsToolCodeName = new List<string>();
        public List<CorrectedSurvey> Surveys = new List<CorrectedSurvey>();
        public List<Uncert> LstUncert = new List<Uncert>();
        public UncertCalc UncertCalcModel = new UncertCalc();
        private bool isValid = false;
        List<double> outHighSide = new List<double>();
        private MxSUnitSystemEnum currentUnitSystem;

        #endregion

        #region Public Methods


        private IList<CorrectedSurvey> GetAllDefinitiveSurveys(Well currentWell)
        {
            var allDefinitiveSurveys = new List<CorrectedSurvey>(currentWell.AllCorrectedSurveys.Where(s => s.SurveyType == MxSSurveyType.Definitive));
            currentWell.AllShortSurveys.Where(s => s.SurveyType == MxSSurveyType.Definitive).ToList().ForEach(s => allDefinitiveSurveys.Add(new CorrectedSurvey(s)));
            return allDefinitiveSurveys;
        }

        public List<Uncert> CalculateUncert(IList<CorrectedSurvey> correctedSurveys)
        {
            int SurveyCount = correctedSurveys.Count;
            Well currentWell = correctedSurveys.FirstOrDefault()?.GetWell();
            if (currentWell != null)
            {
                Surveys = GetAllDefinitiveSurveys(currentWell).OrderBy(d => d.Depth).ToList();
                SurveyCount = Surveys.Count;

                CorrectedSurvey tieInSurvey = GetTieInSurvey(currentWell);
                if (tieInSurvey != null)
                {
                    Surveys.Insert(0, tieInSurvey);
                    SurveyCount++;
                }
            }

            currentUnitSystem = currentWell.UnitSystem;
            Params.Clear();
            LstUncert.Clear();
            ToolCodeSectionsEndDepth.Clear();
            ToolCodeSectionsStartDepth.Clear();
            outHighSide.Clear();
            ToolCodeSectionsToolCodeName.Clear();
            var toolCodeSections = currentWell.OdisseusToolCodeSections.Where(t => t.State != MxSState.Deleted).OrderBy(x => x.StartDepth);
            foreach (var odisseusToolCodeSection in toolCodeSections)
            {
                Params.Add(odisseusToolCodeSection.OdisseusToolCodeParams);
                ToolCodeSectionsEndDepth.Add(odisseusToolCodeSection.EndDepth);
                ToolCodeSectionsStartDepth.Add(odisseusToolCodeSection.StartDepth);
                ToolCodeSectionsToolCodeName.Add(odisseusToolCodeSection.ToolCodeName);
            }

            //Set Input from Well info for calculation            
            UncertCalcModel.Latitude = currentWell.Latitude;

            if (currentWell != null)
            {
                if (currentWell.OdisseusToolCodeSections != null)
                    UncertCalc.Nsl = toolCodeSections.Count();

                // Set Input from Odisseus general properties for calculation
                // Taking SigmaValue and Toolface value from the first param as it is common for all sections
                UncertCalcModel.SigmaLevel = currentWell.SigmaValue;
                UncertCalcModel.Toolface = (int)currentWell.Toolface;

                #region Toolface
                UncertCalcModel.IsToolFaceIndependent = false;
                if ((currentWell.Toolface == MxSToolface.Calculated) ||
                    (currentWell.Toolface == MxSToolface.TFIndependent))
                {

                    if (currentWell.Toolface == MxSToolface.TFIndependent)
                    {
                        UncertCalcModel.IsToolFaceIndependent = true;
                    }

                    for (int i = 0; i < SurveyCount; i++)
                    {
                        outHighSide.Add(conTF);
                    }
                    isValid = true;
                }
                else
                {
                    throw new Exception("Undefined toolface type");
                }
            }

            #endregion

            if (isValid)
            {
                if (LstUncert != null)
                    ClearUncertData();
                ActualCovar(SurveyCount, currentWell, Surveys);
            }
            //ToDo:This is temp code to handle uncertainty Calculation for Short survey,needs to revisit this in future version.
            AssignUncertaintyValues(correctedSurveys);
            return LstUncert;
        }

        private void AssignUncertaintyValues(IList<CorrectedSurvey> correctedSurveys)
        {
            foreach (var correctedSurvey in correctedSurveys)
            {
                if (correctedSurvey.RawSurvey.IsShortSurvey())
                {
                    var calculatedCorrectedSurvey = Surveys.FirstOrDefault(s => s.Depth.CompareDouble(correctedSurvey.Depth) && s.DateTime == correctedSurvey.DateTime);
                    if (calculatedCorrectedSurvey != null)
                    {
                        correctedSurvey.DeleteUncertaintyValues();
                        correctedSurvey.AddUncertaintyValues(calculatedCorrectedSurvey.UncertaintyValue);
                    }
                }


            }
        }

        private CorrectedSurvey GetTieInSurvey(Well currentWell)
        {
            CorrectedSurvey firstSurvey = Surveys.FirstOrDefault();
            CorrectedSurvey previousSurvey = currentWell.GetPreviousDefinitiveSurveyIncludingShortSurvey(firstSurvey);
            CorrectedSurvey tieInSurvey = null;
            if (previousSurvey == null)
            {
                double inc = 0.0;
                double.TryParse(currentWell.Inclination, out inc);
                double az = 0.0;
                double.TryParse(currentWell.Azimuth, out az);
                tieInSurvey = new CorrectedSurvey()
                {
                    Depth = currentWell.MeasuredDepth,
                    SolInc = inc,
                    SolAzm = az,
                };
            }
            else
            {
                tieInSurvey = new CorrectedSurvey()
                {
                    Depth = previousSurvey.Depth,
                    SolInc = previousSurvey.SolInc.Value,
                    SolAzm = previousSurvey.SolAzm.Value,
                };
            }

            return tieInSurvey;
        }

        #endregion

        #region Private Methods

        private void ActualCovar(int surveyCount, Well currentWell, IList<CorrectedSurvey> correctedSurveys)
        {
            double md0 = 0, inc0 = 0, az0 = 0, tau0 = 0;
            double md1 = 0, inc1 = 0, az1 = 0, tau1 = 0;
            double md2 = 0, inc2 = 0, az2 = 0, tau2 = 0;
            double denominator;
            double numerator;
            double endDepth;
            double startDepth;
            double sigmaN, sigmaE, sigmaV;
            double sigmaL, sigmaH, sigmaA;
            double sigmaP, sigmaQ, sigmaR;
            double varN, varE, varV;
            double varNE, varEV, varNV;
            double rotA, sinRA, cosRA, sigmaHMax, sigmaHMin;
            double trace, q, r, eta;
            double corHL = 0, corHA = 0, corLA = 0;

            bool isAssignDepth;



            double[,] arrCovFinal = new double[3, 3];
            double[,] arrCovHla = new double[3, 3];
            double[,] arrCov = new double[3, 3];
            double[,] arrCovG = new double[3, 3];
            double[,] arrCovS = new double[3, 3];
            double[,] arrCovR = new double[3, 3];

            double[] arrBias = new double[3];
            double[] arrBiasHla = new double[3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrCovR[i, j] = 0;
                    arrCovS[i, j] = 0;
                    arrCovG[i, j] = 0;
                }
            }

            _lstKS = new List<double>(UncertCalc.Nsl);

            _lstKS.Add(0);

            CorrectedSurvey firstSurvey = Surveys.FirstOrDefault();


            inc1 = 0;
            az1 = 0;
            md1 = 0;
            tau1 = 0;


            List<double> outHighSide = new List<double>();
            outHighSide.Add(0);

            if (firstSurvey != null && currentWell != null)
            {
                md2 = firstSurvey.Depth;

                if (firstSurvey.SolInc == null || firstSurvey.SolAzm == null)
                    return;

                inc2 = firstSurvey.SolInc.Value; // Inclination.Value;
                az2 = firstSurvey.SolAzm.Value; // Azimuth.Value;

                if (currentWell.Toolface == MxSToolface.Calculated)
                    tau2 = 0;
                else
                    tau2 = outHighSide[0];// firstSurvey.HighSide;
            }

            if (LstUncert != null && LstUncert.Count > 0)
            {
                Uncert firstUncert = LstUncert.FirstOrDefault();
                firstUncert.Depth = md2;
                ResetUncertData(firstUncert);
            }
            else
            {
                LstUncert = Enumerable.Repeat(new Uncert(), surveyCount).ToList();
            }



            for (int leg = 1; leg <= UncertCalc.Nsl; leg++)
            {
                _isValidLeg = true;

                double[] dblDepth = Surveys.Select(s => s.Depth).ToArray();
                endDepth = ToolCodeSectionsEndDepth[leg - 1];
                startDepth = ToolCodeSectionsStartDepth[leg - 1];
                double lessThanendDepth;
                double greaterThanStartDepth;
                double[] sectionDepths = (Surveys.Where(s => s.Depth <= endDepth && s.Depth >= startDepth).Select(l => l.Depth)).ToArray();
                if (sectionDepths.Any())
                {
                    lessThanendDepth = dblDepth.Last(p => p <= endDepth);
                    greaterThanStartDepth = dblDepth.FirstOrDefault(p => p >= startDepth);
                }
                else
                {
                    _lstKS.Add(0);
                    continue;
                }
                double closestIndex = Array.IndexOf(dblDepth, lessThanendDepth);
                double startDepthClosesetIndex = Array.IndexOf(dblDepth, greaterThanStartDepth);
                _lstKS.Add(closestIndex);
                if (_lstKS.Count > 2)
                {
                    _lstKS[leg - 1] = startDepthClosesetIndex - 1;
                }

                surveyCount = (int)closestIndex + 1;

                //Removing ModelType Code and initialize the params to false
                UncertCalcModel.IscXyGyro = false;
                UncertCalcModel.IscXyzGyro = false;
                UncertCalcModel.IsBpErrorModel = false;
                UncertCalcModel.IsBpGcSS = false;
                UncertCalcModel.IsBpCtCms = false;
                UncertCalcModel.IsBpCtDms = false;


                GetErrorVals(leg, Params, currentWell);

                if (leg > 1)
                {
                    md2 = md1;
                    inc2 = inc1;
                    az2 = az1;
                    tau2 = tau1;
                }

                for (double k = _lstKS[leg - 1] + 1; k <= _lstKS[leg] + 1; k++)
                {
                    CorrectedSurvey currentSurvey = Surveys.ElementAtOrDefault((int)k);

                    if (currentSurvey != null && currentWell != null)
                    {
                        #region Set Input from surveys for calculation 
                        UncertCalcModel.Gtot = (double)currentWell.GTotal;
                        UncertCalcModel.Btot = currentWell.BTotal;

                        if (UncertCalcModel.Btot < 1000)
                        {
                            UncertCalcModel.Btot = UncertCalcModel.Btot * 1000;
                        }

                        UncertCalcModel.Dip = currentWell.Dip;
                        if (currentWell.NorthReference == MxSNorthReference.Grid)
                        {
                            UncertCalcModel.Declination = currentWell.Declination - currentWell.GridConvergence;
                        }
                        else if (currentWell.NorthReference == MxSNorthReference.Magnetic)
                        {
                            UncertCalcModel.Declination = 0.0;
                        }
                        else
                        {
                            UncertCalcModel.Declination = currentWell.Declination;
                        }
                        UncertCalcModel.Nst = surveyCount - 1;

                        #endregion

                        md0 = md1;
                        inc0 = inc1;
                        az0 = az1;
                        tau0 = tau1;
                        md1 = md2;
                        inc1 = inc2;
                        az1 = az2;
                        tau1 = tau2;


                        md2 = currentSurvey.Depth;

                        if (currentSurvey.SolInc == null || currentSurvey.SolAzm == null)
                            return;

                        inc2 = currentSurvey.SolInc.Value;
                        az2 = currentSurvey.SolAzm.Value;

                        if (currentWell.Toolface == MxSToolface.Calculated)
                        {
                            denominator = Math.Sin(inc2 * RAD) * Math.Cos(inc1 * RAD) - Math.Cos((az2 - az1) * RAD) * Math.Sin(inc1 * RAD) * Math.Cos(inc2 * RAD);
                            numerator = Math.Sin(inc1 * RAD) * Math.Sin((az2 - az1) * RAD);
                            tau2 = AdjustRange(Atanc(denominator, numerator) * DEGREE);
                            //Check 
                            //currentSurvey.HighSide = tau2;                            
                            outHighSide.Add(tau2);
                        }
                        else if (currentWell.Toolface == MxSToolface.TFIndependent)
                        {
                            tau2 = 0;
                        }
                        else
                        {
                            tau2 = currentSurvey.HighSide;
                        }

                        if (UncertCalcModel.IsAssignDepth)
                        {
                            BtAssignDepthDiffernce(md1, inc1, az1, md2, inc2, az2);
                        }
                        else
                        {
                            BtDifferenceB(md1, inc1, az1, md2, inc2, az2);
                        }

                        if (k > _lstKS[leg - 1] + 1)
                        {
                            BtDiffernce(md0, inc0, az0, md1, inc1, az1, md2, inc2, az2);
                        }

                        if (UncertCalcModel.IsSccAz)
                        {
                            SysCovSCC(leg, (int)k, md1, tau1, inc1, az1, md2, tau2, inc2, az2, arrCov);
                        }
                        else
                        {
                            SysCovLC(leg, (int)k, md1, tau1, inc1, az1, md2, tau2, inc2, az2, arrCov);
                        }

                        GlobCovErr(leg, (int)k, md1, inc1, az1, md2, inc2, az2, arrCovG);
                        SurBiasErr(leg, (int)k, md1, tau1, inc1, az1, md2, tau2, inc2, az2, arrBias);


                        if (k == _lstKS[leg] + 1 && k != UncertCalcModel.Nst)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    arrCovS[i, j] = arrCovS[i, j] + arrCov[i, j];
                                }
                            }
                        }
                        else
                        {
                            double tempVariable;

                            Random(leg, (int)k, md1, inc1, az1, md2, inc2, az2, arrCovR);

                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    arrCovFinal[i, j] = arrCovR[i, j] + arrCovG[i, j] + arrCovS[i, j] + arrCov[i, j];
                                }
                            }
                            int row = (int)k;

                            LstUncert[row].CovNN = arrCovFinal[0, 0];
                            LstUncert[row].CovNE = arrCovFinal[0, 1];
                            LstUncert[row].CovNV = arrCovFinal[0, 2];
                            LstUncert[row].CovEE = arrCovFinal[1, 1];
                            LstUncert[row].CovEV = arrCovFinal[1, 2];
                            LstUncert[row].CovVV = arrCovFinal[2, 2];

                            sigmaN = Math.Sqrt(arrCovFinal[0, 0]);
                            sigmaE = Math.Sqrt(arrCovFinal[1, 1]);
                            sigmaV = Math.Sqrt(arrCovFinal[2, 2]);

                            LstUncert[row].Depth = md2;
                            LstUncert[row].SigmaN = sigmaN;
                            LstUncert[row].SigmaE = sigmaE;
                            LstUncert[row].SigmaV = sigmaV;
                            LstUncert[row].BiasN = arrBias[0];
                            LstUncert[row].BiasE = arrBias[1];
                            LstUncert[row].BiasV = arrBias[2];

                            varN = arrCovFinal[0, 0];
                            varE = arrCovFinal[1, 1];
                            varV = arrCovFinal[2, 2];
                            varNE = arrCovFinal[0, 1];
                            varEV = arrCovFinal[1, 2];
                            varNV = arrCovFinal[0, 2];

                            rotA = 0.5 * Atanc(varN - varE, 2 * varNE);
                            sinRA = Math.Sin(rotA);
                            cosRA = Math.Cos(rotA);
                            sigmaHMax = (Math.Pow(sigmaN * cosRA, 2) + 2 * varNE * sinRA * cosRA + Math.Pow((sigmaE * sinRA), 2));

                            if (sigmaHMax > 0)
                            {
                                sigmaHMax = Math.Sqrt(sigmaHMax);
                            }
                            else
                            {
                                sigmaHMax = 0;
                            }

                            sigmaHMin = (Math.Pow(sigmaN * sinRA, 2) - 2 * varNE * sinRA * cosRA + Math.Pow((sigmaE * cosRA), 2));
                            if (sigmaHMin > 0)
                            {
                                sigmaHMin = Math.Sqrt(sigmaHMin);
                            }
                            else
                            {
                                sigmaHMin = 0;
                            }
                            rotA = rotA * DEGREE;

                            trace = varN + varE + varV;

                            q = (1.0 / 3.0) * trace * (varN * varE + varN * varV + varE * varV - Math.Pow(varNE, 2) - Math.Pow(varNV, 2) - Math.Pow(varEV, 2));
                            q = q - (2.0 / 27.0) * Math.Pow(trace, 3) + varN * Math.Pow(varEV, 2) + varE * Math.Pow(varNV, 2) + varV * (Math.Pow(varNE, 2));
                            q = q - varN * varE * varV - 2 * varNE * varNV * varEV;
                            r = Math.Pow(varNE, 2) + Math.Pow(varNV, 2) + Math.Pow(varEV, 2) - varN * varE - varN * varV - varE * varV;
                            r = (1.0 / 9.0) * Math.Pow(trace, 2) + (1.0 / 3.0) * r;

                            if (r > 0)
                            {
                                r = Math.Sqrt(r);
                                tempVariable = -0.5 * q / Math.Pow(r, 3);
                                if (tempVariable > 1)
                                {
                                    tempVariable = 1;
                                }
                                if (tempVariable < -1)
                                {
                                    tempVariable = -1;
                                }
                                eta = Math.Acos(tempVariable);
                            }
                            else
                            {
                                r = 0;
                                eta = 0;
                            }

                            sigmaP = 2 * r * Math.Cos(eta / 3.0) + (1.0 / 3.0) * trace;
                            if (sigmaP < 0)
                            {
                                sigmaP = 0;
                            }
                            else
                            {
                                sigmaP = Math.Sqrt(sigmaP);
                            }

                            sigmaQ = 2 * r * Math.Cos((eta + 2 * Math.PI) / 3.0) + (1.0 / 3.0) * trace;
                            if (sigmaQ < 0)
                                sigmaQ = 0;
                            else
                                sigmaQ = Math.Sqrt(sigmaQ);

                            sigmaR = 2 * r * Math.Cos((eta + 4 * Math.PI) / 3.0) + (1.0 / 3.0) * trace;
                            if (sigmaR < 0)
                                sigmaR = 0;
                            else
                                sigmaR = Math.Sqrt(sigmaR);


                            if (sigmaP < sigmaQ)
                            {
                                tempVariable = sigmaP;
                                sigmaP = sigmaQ;
                                sigmaQ = tempVariable;
                            }
                            if (sigmaQ < sigmaR)
                            {
                                tempVariable = sigmaQ;
                                sigmaQ = sigmaR;
                                sigmaR = tempVariable;
                            }
                            if (sigmaP < sigmaQ)
                            {
                                tempVariable = sigmaP;
                                sigmaP = sigmaQ;
                                sigmaQ = tempVariable;
                            }

                            NevHla(inc2, az2, arrCovFinal, arrCovHla);
                            BnevHla(inc2, az2, arrBias, arrBiasHla);

                            if (arrCovHla[0, 0] < 0)
                                sigmaH = 0;
                            else
                                sigmaH = Math.Sqrt(arrCovHla[0, 0]);


                            if (arrCovHla[1, 1] < 0)
                                sigmaL = 0;
                            else
                                sigmaL = Math.Sqrt(arrCovHla[1, 1]);


                            if (arrCovHla[2, 2] < 0)
                                sigmaA = 0;
                            else
                                sigmaA = Math.Sqrt(arrCovHla[2, 2]);

                            if (sigmaH == 0)
                            {
                                corHL = 0;
                                corHA = 0;
                            }
                            else if (sigmaL == 0)
                            {
                                corHL = 0;
                                corLA = 0;
                            }
                            else if (sigmaA == 0)
                            {
                                corLA = 0;
                                corHA = 0;
                            }
                            else
                            {
                                corHL = arrCovHla[0, 1] / (sigmaH * sigmaL);
                                corHA = arrCovHla[0, 2] / (sigmaH * sigmaA);
                                corLA = arrCovHla[1, 2] / (sigmaL * sigmaA);
                            }

                            LstUncert[row].SigmaH = sigmaH;
                            LstUncert[row].SigmaL = sigmaL;
                            LstUncert[row].SigmaA = sigmaA;
                            LstUncert[row].BiasH = arrBiasHla[0];
                            LstUncert[row].BiasL = arrBiasHla[1];
                            LstUncert[row].BiasA = arrBiasHla[2];

                            LstUncert[row].CorrHL = corHL;
                            LstUncert[row].CorrHA = corHA;
                            LstUncert[row].CorrLA = corLA;
                            LstUncert[row].HMajSA = sigmaHMax;
                            LstUncert[row].HMinSA = sigmaHMin;

                            LstUncert[row].RotAng = rotA;
                            LstUncert[row].SemiAx1 = sigmaP;
                            LstUncert[row].SemiAx2 = sigmaQ;
                            LstUncert[row].SemiAx3 = sigmaR;

                            correctedSurveys[row].DeleteUncertaintyValues();
                            var uncertaintyValue = new Uncertainty();
                            uncertaintyValue.SigmaN = sigmaN;
                            uncertaintyValue.SigmaE = sigmaE;
                            uncertaintyValue.SigmaV = sigmaV;
                            uncertaintyValue.BiasN = arrBias[0];
                            uncertaintyValue.BiasE = arrBias[1];
                            uncertaintyValue.BiasV = arrBias[2];

                            uncertaintyValue.CovNN = arrCovFinal[0, 0];
                            uncertaintyValue.CovNE = arrCovFinal[0, 1];
                            uncertaintyValue.CovNV = arrCovFinal[0, 2];
                            uncertaintyValue.CovEE = arrCovFinal[1, 1];
                            uncertaintyValue.CovEV = arrCovFinal[1, 2];
                            uncertaintyValue.CovVV = arrCovFinal[2, 2];

                            uncertaintyValue.SigmaH = sigmaH;
                            uncertaintyValue.SigmaL = sigmaL;
                            uncertaintyValue.SigmaA = sigmaA;
                            uncertaintyValue.BiasH = arrBiasHla[0];
                            uncertaintyValue.BiasL = arrBiasHla[1];
                            uncertaintyValue.BiasA = arrBiasHla[2];
                            uncertaintyValue.CorrHL = corHL;
                            uncertaintyValue.CorrHA = corHA;
                            uncertaintyValue.CorrLA = corLA;
                            uncertaintyValue.HMajSA = sigmaHMax;
                            uncertaintyValue.HMinSA = sigmaHMin;
                            uncertaintyValue.RotAng = rotA;
                            uncertaintyValue.SemiAx1 = sigmaP;
                            uncertaintyValue.SemiAx2 = sigmaQ;
                            uncertaintyValue.SemiAx3 = sigmaR;
                            uncertaintyValue.ToolCode = ToolCodeSectionsToolCodeName[leg - 1];

                            correctedSurveys[row].AddUncertaintyValues(uncertaintyValue);

                        }
                    }
                }
            }
        }

        private void SysCovSCC(int leg, int ist, double s1, double t1, double i1, double a1, double s2, double t2, double i2, double a2, double[,] covMat)
        {
            double[,] covMX = new double[3, 3];
            double[,] covMY = new double[3, 3];
            double[,] covMIS1 = new double[3, 3];
            double[,] covMIS2 = new double[3, 3];
            double[,] covMIS3 = new double[3, 3];
            double[,] covMIS4 = new double[3, 3];
            double[,] covSAG = new double[3, 3];
            double[,] covDREF = new double[3, 3];
            double[,] covDSF = new double[3, 3];

            double[,] covABIx = new double[3, 3];
            double[,] covABIy = new double[3, 3];
            double[,] covABITI1 = new double[3, 3];
            double[,] covABITI2 = new double[3, 3];
            double[,] covABIz = new double[3, 3];
            double[,] covASIx = new double[3, 3];
            double[,] covASIy = new double[3, 3];
            double[,] covASITI1 = new double[3, 3];
            double[,] covASITI2 = new double[3, 3];
            double[,] covASITI3 = new double[3, 3];
            double[,] covASIz = new double[3, 3];
            double[,] covMBIx = new double[3, 3];
            double[,] covMBIy = new double[3, 3];
            double[,] covMBITI1 = new double[3, 3];
            double[,] covMBITI2 = new double[3, 3];
            double[,] covMSIx = new double[3, 3];
            double[,] covMSIy = new double[3, 3];
            double[,] covMSITI1 = new double[3, 3];
            double[,] covMSITI2 = new double[3, 3];
            double[,] covMSITI3 = new double[3, 3];

            double[,] covMMudI = new double[3, 3];

            if (_isValidLeg)
            {
                for (int i = 0; i < 3; i++)
                {
                    _prevMX[i] = 0;
                    _prevMY[i] = 0;
                    _prevMIS1[i] = 0;
                    _prevMIS2[i] = 0;
                    _prevMIS3[i] = 0;
                    _prevMIS4[i] = 0;
                    _prevSAG[i] = 0;
                    _prevDSF[i] = 0;
                    _prevDREF[i] = 0;
                    _prevABIx[i] = 0;
                    _prevABIy[i] = 0;
                    _prevABITI1[i] = 0;
                    _prevABITI2[i] = 0;
                    _prevABIz[i] = 0;
                    _prevASIx[i] = 0;
                    _prevASIy[i] = 0;
                    _prevASITI1[i] = 0;
                    _prevASITI2[i] = 0;
                    _prevASITI3[i] = 0;
                    _prevASIz[i] = 0;
                    _prevMBIx[i] = 0;
                    _prevMBIy[i] = 0;
                    _prevMBITI1[i] = 0;
                    _prevMBITI2[i] = 0;
                    _prevMSIx[i] = 0;
                    _prevMSIy[i] = 0;
                    _prevMSITI1[i] = 0;
                    _prevMSITI2[i] = 0;
                    _prevMSITI3[i] = 0;
                    _prevMMudI[i] = 0;
                }
                _isValidLeg = false;
            }
            SysCov(leg, ist, WtMIS1(s1, t1, i1, a1), WtMIS1(s2, t2, i2, a2), _prevMIS1, covMIS1);
            SysCov(leg, ist, WtMIS2(s1, t1, i1, a1), WtMIS2(s2, t2, i2, a2), _prevMIS2, covMIS2);
            SysCovMis(leg, ist, WtMIS3(s1, t1, i1, a1), WtMIS3(s2, t2, i2, a2), _prevMIS3, covMIS3);
            SysCovMis(leg, ist, WtMIS4(s1, t1, i1, a1), WtMIS4(s2, t2, i2, a2), _prevMIS4, covMIS4);
            SysCov(leg, ist, WtSAG(s1, t1, i1, a1), WtSAG(s2, t2, i2, a2), _prevSAG, covSAG);
            SysCov(leg, ist, WtDREF(s1, i1, a1), WtDREF(s2, i2, a2), _prevDREF, covDREF);
            SysCov(leg, ist, WtDsf(s1, t1, i1, a1), WtDsf(s2, t2, i2, a2), _prevDSF, covDSF);

            double a1M, b1P, b2P, b3P, b4P, a2M, b1C, b2C, b3C, b4C;






            a1M = a1 - UncertCalcModel.Declination;
            b1P = Math.Pow(Math.Cos(i1 * RAD), 2) * Math.Sin(i1 * RAD) * Math.Sin(a1M * RAD);
            b2P = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i1 * RAD) + Math.Sin(i1 * RAD) * Math.Cos(a1M * RAD);
            b3P = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(i1 * RAD) * Math.Cos(a1M * RAD) - Math.Cos(i1 * RAD);
            b4P = 1 - Math.Pow((Math.Sin(a1M * RAD) * Math.Sin(i1 * RAD)), 2);

            a2M = a2 - UncertCalcModel.Declination;
            b1C = Math.Pow(Math.Cos(i2 * RAD), 2) * Math.Sin(i2 * RAD) * Math.Sin(a2M * RAD);
            b2C = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i2 * RAD) + Math.Sin(i2 * RAD) * Math.Cos(a2M * RAD);
            b3C = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(i2 * RAD) * Math.Cos(a2M * RAD) - Math.Cos(i2 * RAD);
            b4C = 1 - Math.Pow((Math.Sin(a2M * RAD) * Math.Sin(i2 * RAD)), 2);

            if (UncertCalcModel.IsToolFaceIndependent)
            {
                SysCov(leg, ist, WtABITI1(b1P, b2P, b3P, b4P, i1, a1M), WtABITI1(b1C, b2C, b3C, b4C, i2, a2M), _prevABITI1, covABITI1);
                SysCovMis(leg, ist, WtABITI2(b1P, b2P, b3P, b4P, i1, a1M), WtABITI2(b1C, b2C, b3C, b4C, i2, a2M), _prevABITI2, covABITI2);
                SysCov(leg, ist, WtASITI1(b1P, b2P, b3P, b4P, i1, a1M), WtASITI1(b1C, b2C, b3C, b4C, i2, a2M), _prevASITI1, covASITI1);
                SysCov(leg, ist, WtASITI2(b1P, b2P, b3P, b4P, i1, a1M), WtASITI2(b1C, b2C, b3C, b4C, i2, a2M), _prevASITI2, covASITI2);
                SysCov(leg, ist, WtASITI3(b1P, b2P, b3P, b4P, i1, a1M), WtASITI3(b1C, b2C, b3C, b4C, i2, a2M), _prevASITI3, covASITI3);
                SysCov(leg, ist, WtMBITI1(b1P, b2P, b3P, b4P, i1, a1M), WtMBITI1(b1C, b2C, b3C, b4C, i2, a2M), _prevMBITI1, covMBITI1);
                SysCov(leg, ist, WtMBITI2(b1P, b2P, b3P, b4P, i1, a1M), WtMBITI2(b1C, b2C, b3C, b4C, i2, a2M), _prevMBITI2, covMBITI2);
                SysCov(leg, ist, WtMSITI1(b1P, b2P, b3P, b4P, i1, a1M), WtMSITI1(b1C, b2C, b3C, b4C, i2, a2M), _prevMSITI1, covMSITI1);
                SysCov(leg, ist, WtMSITI2(b1P, b2P, b3P, b4P, i1, a1M), WtMSITI2(b1C, b2C, b3C, b4C, i2, a2M), _prevMSITI2, covMSITI2);
                SysCov(leg, ist, WtMSITI3(b1P, b2P, b3P, b4P, i1, a1M), WtMSITI3(b1C, b2C, b3C, b4C, i2, a2M), _prevMSITI3, covMSITI3);
            }
            else
            {
                SysCovMis(leg, ist, WtABIx(b1P, b2P, b3P, b4P, t1, i1, a1M), WtABIx(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevABIx, covABIx);
                SysCovMis(leg, ist, WtABIy(b1P, b2P, b3P, b4P, t1, i1, a1M), WtABIy(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevABIy, covABIy);
                SysCov(leg, ist, WtASIx(b1P, b2P, b3P, b4P, t1, i1, a1M), WtASIx(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevASIx, covASIx);
                SysCov(leg, ist, WtASIy(b1P, b2P, b3P, b4P, t1, i1, a1M), WtASIy(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevASIy, covASIy);
                SysCov(leg, ist, WtMBIx(b1P, b2P, b3P, b4P, t1, i1, a1M), WtMBIx(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevMBIx, covMBIx);
                SysCov(leg, ist, WtMBIy(b1P, b2P, b3P, b4P, t1, i1, a1M), WtMBIy(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevMBIy, covMBIy);
                SysCov(leg, ist, WtMSIx(b1P, b2P, b3P, b4P, t1, i1, a1M), WtMSIx(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevMSIx, covMSIx);
                SysCov(leg, ist, WtMSIy(b1P, b2P, b3P, b4P, t1, i1, a1M), WtMSIy(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevMSIy, covMSIy);
            }

            SysCov(leg, ist, WtABIz(b1P, b2P, b3P, b4P, t1, i1, a1M), WtABIz(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevABIz, covABIz);
            SysCov(leg, ist, WtASIz(b1P, b2P, b3P, b4P, t1, i1, a1M), WtASIz(b1C, b2C, b3C, b4C, t2, i2, a2M), _prevASIz, covASIz);
            SysCov(leg, ist, WtMMudI(b2P, b4P, i1, a1M, UncertCalcModel.SdMM), WtMMudI(b2C, b4C, i2, a2M, UncertCalcModel.SdMM), _prevMMudI, covMMudI);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    covMat[i, j] = covMIS1[i, j] + covMIS2[i, j] + covMIS3[i, j] + covMIS4[i, j] + covSAG[i, j];
                    covMat[i, j] = covMat[i, j] + covDREF[i, j] + covDSF[i, j];

                    if (UncertCalcModel.IsToolFaceIndependent)
                    {
                        covMat[i, j] = covMat[i, j] + covABITI1[i, j] + covABITI2[i, j] + covABIz[i, j];
                        covMat[i, j] = covMat[i, j] + covASITI1[i, j] + covASITI2[i, j] + covASITI3[i, j] + covASIz[i, j];
                        covMat[i, j] = covMat[i, j] + covMBITI1[i, j] + covMBITI2[i, j];
                        covMat[i, j] = covMat[i, j] + covMSITI1[i, j] + covMSITI2[i, j] + covMSITI3[i, j] + covMMudI[i, j];
                    }
                    else
                    {
                        covMat[i, j] = covMat[i, j] + covABIx[i, j] + covABIy[i, j] + covABIz[i, j];
                        covMat[i, j] = covMat[i, j] + covASIx[i, j] + covASIy[i, j] + covASIz[i, j];
                        covMat[i, j] = covMat[i, j] + covMBIx[i, j] + covMBIy[i, j];
                        covMat[i, j] = covMat[i, j] + covMSIx[i, j] + covMSIy[i, j] + covMMudI[i, j];
                    }
                }
            }
        }

        private void GetErrorVals(int legNo, List<OdisseusToolCodeParams> Params, Well currentWell)
        {

            OdisseusToolCodeParams currentParam = Params.ElementAtOrDefault(legNo - 1);
            if (currentParam != null)
            {
                if (currentWell?.RigType == MxSRigType.Float)
                {
                    UncertCalcModel.SdDREF = currentWell.SigmaValue * GetUnitConvertedValue(currentParam.DREF_Float);
                    UncertCalcModel.SdDREFr = currentWell.SigmaValue * GetUnitConvertedValue(currentParam.DREFr_Float);
                }
                else
                {
                    UncertCalcModel.SdDREF = currentWell.SigmaValue * GetUnitConvertedValue(currentParam.DREF_Fix);
                    UncertCalcModel.SdDREFr = currentWell.SigmaValue * GetUnitConvertedValue(currentParam.DREFr_Fix);
                }

                UncertCalcModel.IsSccAz = IsShortCollar(currentParam);
                UncertCalcModel.IsAssignDepth = IsAssignDepth(currentParam);

                UncertCalcModel.SdDSF = currentWell.SigmaValue * currentParam.DSF;
                UncertCalcModel.SdDST = currentWell.SigmaValue * currentParam.DST * MxSConstant.FEETTOMETRIC;//Inverse unit conversion logic same as in VBA; 
                UncertCalcModel.MnDST = currentParam.DSTB * MxSConstant.FEETTOMETRIC;//Inverse unit conversion logic same as in VBA; 
                UncertCalcModel.SdSAG = currentWell.SigmaValue * currentParam.SAG;
                UncertCalcModel.SdMXY1 = currentWell.SigmaValue * currentParam.MXY1;
                UncertCalcModel.SdMXY2 = currentWell.SigmaValue * currentParam.MXY2;
                UncertCalcModel.SdAB = currentWell.SigmaValue * currentParam.AB;
                UncertCalcModel.SdAS = currentWell.SigmaValue * currentParam.AS;
                UncertCalcModel.SdMB = currentWell.SigmaValue * currentParam.MB;
                UncertCalcModel.SdMS = currentWell.SigmaValue * currentParam.MS;
                UncertCalcModel.SdAMIC = currentWell.SigmaValue * currentParam.AMIC;
                UncertCalcModel.SdAMID = currentWell.SigmaValue * currentParam.AMID;
                UncertCalcModel.SdAMIL = currentWell.SigmaValue * currentParam.AMIL;
                UncertCalcModel.MnAMID = currentWell.SigmaValue * currentParam.AMIB;
                UncertCalcModel.SdAZ = currentWell.SigmaValue * currentParam.AZ;
                UncertCalcModel.SdAZr = currentWell.SigmaValue * currentParam.AZrandom;
                UncertCalcModel.SdDBH = currentWell.SigmaValue * currentParam.DBH;
                UncertCalcModel.SdDBHr = currentWell.SigmaValue * currentParam.DBHrandom;
                UncertCalcModel.SdMFI = currentWell.SigmaValue * currentParam.MFI;
                UncertCalcModel.SdMFIr = currentWell.SigmaValue * currentParam.MFIrandom;
                UncertCalcModel.SdMDI = currentWell.SigmaValue * currentParam.MDI;
                UncertCalcModel.SdMDIr = currentWell.SigmaValue * currentParam.MDIrandom;
                UncertCalcModel.SdMM = 0;
                UncertCalcModel.MnMM = currentWell.SigmaValue * currentParam.MM;
            }

        }

        private bool IsAssignDepth(OdisseusToolCodeParams currentParam)
        {
            return (currentParam.DepthStation == MxSDepthStation.AssignD);
        }

        private bool IsShortCollar(OdisseusToolCodeParams currentParam)
        {
            return (currentParam.LongSCC == MxSLongScc.SCC);
        }

        private double GetUnitConvertedValue(double paramValue)
        {
            return UnitConverter.Convert(MxSUnitSystemEnum.Metric, MxSUnitSystemEnum.English, paramValue, MxSFormatType.Uncertainty);
        }

        private void BnevHla(double inc, double az, double[] pEnev, double[] pEhla)
        {

            double[,] aCalculation = new double[3, 3];

            aCalculation[0, 0] = Math.Cos(inc * RAD) * Math.Cos(az * RAD);
            aCalculation[0, 1] = Math.Cos(inc * RAD) * Math.Sin(az * RAD);
            aCalculation[0, 2] = -Math.Sin(inc * RAD);
            aCalculation[1, 0] = -Math.Sin(az * RAD);
            aCalculation[1, 1] = Math.Cos(az * RAD);
            aCalculation[1, 2] = 0;
            aCalculation[2, 0] = Math.Sin(inc * RAD) * Math.Cos(az * RAD);
            aCalculation[2, 1] = Math.Sin(inc * RAD) * Math.Sin(az * RAD);
            aCalculation[2, 2] = Math.Cos(inc * RAD);

            Tuple<double, double, double, double> tPEnev = new Tuple<double, double, double, double>(0, pEnev[0], pEnev[1], pEnev[2]);

            MVmult(aCalculation, tPEnev, pEhla);

        }

        private void NevHla(double inc, double az, double[,] cNev, double[,] cHla)
        {
            double[,] t = new double[3, 3];
            double[,] trans = new double[3, 3];
            double[,] temp = new double[3, 3];

            t[0, 0] = Math.Cos(inc * RAD) * Math.Cos(az * RAD);
            t[1, 0] = Math.Cos(inc * RAD) * Math.Sin(az * RAD);
            t[2, 0] = -Math.Sin(inc * RAD);
            t[0, 1] = -Math.Sin(az * RAD);
            t[1, 1] = Math.Cos(az * RAD);
            t[2, 1] = 0;
            t[0, 2] = Math.Sin(inc * RAD) * Math.Cos(az * RAD);
            t[1, 2] = Math.Sin(inc * RAD) * Math.Sin(az * RAD);
            t[2, 2] = Math.Cos(inc * RAD);
            trans[0, 0] = t[0, 0];
            trans[1, 0] = t[0, 1];
            trans[2, 0] = t[0, 2];
            trans[0, 1] = t[1, 0];
            trans[1, 1] = t[1, 1];
            trans[2, 1] = t[1, 2];
            trans[0, 2] = t[2, 0];
            trans[1, 2] = t[2, 1];
            trans[2, 2] = t[2, 2];

            MatMult(trans, 3, 3, cNev, 3, temp);
            MatMult(temp, 3, 3, t, 3, cHla);
        }

        private void MatMult(double[,] a, int aRow, int aCol,
                             double[,] b, int bCol, double[,] c)
        {
            MatZ(c, aRow, bCol);

            for (int i = 0; i < aRow; i++)
            {
                for (int j = 0; j < aCol; j++)
                {
                    for (int k = 0; k < bCol; k++)
                    {
                        c[i, k] = c[i, k] + a[i, j] * b[j, k];
                    }
                }
            }
        }
        private void Random(int leg, int ist, double s1, double i1, double a1,
                            double s2, double i2, double a2, double[,] covR)
        {
            double b1p, b2p, b3p, b4p;
            double b1c, b2c, b3c, b4c;

            double[,] covDREFr = new double[3, 3];
            double[,] covMFIr = new double[3, 3];
            double[,] covMDIr = new double[3, 3];
            double[,] covAZr = new double[3, 3];
            double[,] covDBHr = new double[3, 3];
            double[,] covGXYr = new double[3, 3];
            double[,] covGZr = new double[3, 3];

            if (ist == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _cDREFr[i, j] = 0;
                        _cMFIr[i, j] = 0;
                        _cMDIr[i, j] = 0;
                        _cAZr[i, j] = 0;
                        _cDBHr[i, j] = 0;
                        _cGXYr[i, j] = 0;
                        _cGZr[i, j] = 0;
                    }
                }
            }

            RanCov(ist, WtDREFr(s1, i1, a1), WtDREFr(s2, i2, a2), _cDREFr, covDREFr);

            if (UncertCalcModel.IscXyGyro)
            {
                RanCov(ist, WtGxyRanN(s1, i1, a1), WtGxyRanN(s2, i2, a2), _cGXYr, covGXYr);
            }
            else if (UncertCalcModel.IscXyzGyro)
            {
                RanCov(ist, WtGxyzRanNxy(s1, i1, a1), WtGxyzRanNxy(s2, i2, a2), _cGXYr, covGXYr);
                RanCov(ist, WtGxyzRanNz(s1, i1, a1), WtGxyzRanNz(s2, i2, a2), _cGZr, covGZr);
            }
            else
            {
                RanCov(ist, WtAZ(UncertCalcModel.SdAZr), WtAZ(UncertCalcModel.SdAZr), _cAZr, covAZr);
                RanCov(ist, WtDBH(UncertCalcModel.SdDBHr), WtDBH(UncertCalcModel.SdDBHr), _cDBHr, covDBHr);

                if (UncertCalcModel.IsSccAz)
                {
                    double a1m, a2m;
                    a1m = a1 - UncertCalcModel.Declination;
                    b1p = ((Math.Cos(i1 * RAD)) * (Math.Cos(i1 * RAD))) * Math.Sin(i1 * RAD) * Math.Sin(a1m * RAD);
                    b2p = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i1 * RAD) + Math.Sin(i1 * RAD) * Math.Cos(a1m * RAD);
                    b3p = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(i1 * RAD) * Math.Cos(a1m * RAD) - Math.Cos(i1 * RAD);
                    b4p = 1 - ((Math.Sin(a1m * RAD) * Math.Sin(i1 * RAD)) * (Math.Sin(a1m * RAD) * Math.Sin(i1 * RAD)));
                    a2m = a2 - UncertCalcModel.Declination;
                    b1c = ((Math.Cos(i2 * RAD)) * (Math.Cos(i2 * RAD))) * Math.Sin(i2 * RAD) * Math.Sin(a2m * RAD);
                    b2c = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i2 * RAD) + Math.Sin(i2 * RAD) * Math.Cos(a2m * RAD);
                    b3c = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(i2 * RAD) * Math.Cos(a2m * RAD) - Math.Cos(i2 * RAD);
                    b4c = 1 - ((Math.Sin(a2m * RAD) * Math.Sin(i2 * RAD)) * (Math.Sin(a2m * RAD) * Math.Sin(i2 * RAD)));

                    RanCov(ist, WtMFI(b2p, b4p, i1, a1, UncertCalcModel.SdMFIr), WtMFI(b2c, b4c, i2, a2, UncertCalcModel.SdMFIr), _cMFIr, covMFIr);
                    RanCov(ist, WtMDI(b3p, b4p, i1, a1, UncertCalcModel.SdMDIr), WtMDI(b3c, b4c, i2, a2, UncertCalcModel.SdMDIr), _cMDIr, covMDIr);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    covR[i, j] = covDREFr[i, j] + covGXYr[i, j] + covGZr[i, j];
                    covR[i, j] = covR[i, j] + covMFIr[i, j] + covMDIr[i, j] + covAZr[i, j] + covDBHr[i, j];
                }
            }
        }

        private Tuple<double, double, double, double> WtGxyzRanNz(double md, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Sin(az * RAD) * Math.Sin(inc * RAD) / (ERATE * Math.Cos(UncertCalcModel.Latitude * RAD)) * UncertCalcModel.SdGzrN;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }


        private Tuple<double, double, double, double> WtGxyzRanNxy(double md, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Sqrt(1 - Math.Pow((Math.Sin(az * RAD) * Math.Sin(inc * RAD)), 2));
            wt3 = wt3 / (ERATE * Math.Cos(UncertCalcModel.Latitude * RAD)) * UncertCalcModel.SdGxyrN;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtGxyRanN(double md, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Sqrt(1 - Math.Pow((Math.Cos(az * RAD) * Math.Sin(inc * RAD)), 2));
            wt3 = wt3 / (ERATE * Math.Cos(UncertCalcModel.Latitude * RAD) * Math.Cos(inc * RAD)) * UncertCalcModel.SdGxyrN;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }


        private Tuple<double, double, double, double> WtDREFr(double md, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = UncertCalcModel.SdDREFr;
            wt2 = 0;
            wt3 = 0;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private void RanCov(int ist, Tuple<double, double, double, double> wPrev, Tuple<double, double, double, double> wCurr,
                                        double[,] cov, double[,] covar)
        {
            double[] pErr = new double[3];
            double[] pErrB = new double[3];
            double[,] cincr = new double[3, 3];
            double[,] covB = new double[3, 3];

            if (ist > 1)
            {
                MVmult(_aDR, wPrev, pErr);
                VVmult(pErr, pErr, cincr);
                MatAdd(cov, cincr, 3, 3, cov);
            }
            else
            {
                MatZ(cov, 3, 3);
            }

            MVmult(_aDRB, wCurr, pErrB);
            VVmult(pErrB, pErrB, covB);
            MatAdd(cov, covB, 3, 3, covar);
        }

        private void MatZ(double[,] a, int nRow, int nCol)
        {
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    a[i, j] = 0;
                }
            }
        }

        private void MatAdd(double[,] a, double[,] b, int nRow, int nCol, double[,] c)
        {
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
        }

        private void SurBiasErr(int leg, int ist, double s1, double t1, double i1, double a1,
                                    double s2, double t2, double i2, double a2, double[] bias)
        {
            double vertP, vertC;
            double b2p, b4p, b2c, b4c;
            double a1m, a2m;

            double[] biasDST = new double[3];
            double[] biasAMID = new double[3];
            double[] biasMMud = new double[3];
            double[] biasMMudI = new double[3];

            if (ist == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    _prevDSTb[i] = 0;
                    _prevAMIDb[i] = 0;
                    _prevMMudb[i] = 0;
                    _prevMMudIb[i] = 0;
                }

                _verticalPSurBiasError = 0;
                _verticalCSurBiasError = 0;
            }

            a1m = a1 - UncertCalcModel.Declination;
            b2p = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i1 * RAD) + Math.Sin(i1 * RAD) * Math.Cos(a1m * RAD);
            b4p = 1 - ((Math.Sin(a1m * RAD) * Math.Sin(i1 * RAD)) * (Math.Sin(a1m * RAD) * Math.Sin(i1 * RAD)));
            a2m = a2 - UncertCalcModel.Declination;
            b2c = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i2 * RAD) + Math.Sin(i2 * RAD) * Math.Cos(a2m * RAD);
            b4c = 1 - ((Math.Sin(a2m * RAD) * Math.Sin(i2 * RAD)) * (Math.Sin(a2m * RAD) * Math.Sin(i2 * RAD)));

            if (ist == _lstKS[leg] + 1 && ist != UncertCalcModel.Nst)
            {

                SurBias(leg, ist, BDst(s1, _verticalCSurBiasError), BDst(s2, _verticalCSurBiasError), _prevDSTb, biasDST);
                SurBias(leg, ist, BAmid(s1, t1, i1, a1), BAmid(s2, t2, i2, a2), _prevAMIDb, biasAMID);
                SurBias(leg, ist, BMMud(s1, t1, i1, a1), BMMud(s2, t2, i2, a2), _prevMMudb, biasMMud);
                SurBias(leg, ist, BMMudI(b2p, b4p, i1, a1, UncertCalcModel.MnMM), BMMudI(b2c, b4c, i2, a2, UncertCalcModel.MnMM), _prevMMudIb, biasMMudI);
            }
            else
            {
                double dinc, daz, dl, f;

                _verticalPSurBiasError = _verticalCSurBiasError;
                dinc = (i2 - i1) * Math.PI / 180;
                daz = (a2 - a1) * Math.PI / 180;
                dl = Math.Acos(Math.Cos(dinc) - Math.Sin(i2 * Math.PI / 180) * Math.Sin(i1 * Math.PI / 180) * (1 - Math.Cos(daz)));

                if (dl > 0.001)
                {
                    f = Math.Tan(0.5 * dl) / dl;
                }
                else
                {
                    f = 0.5;
                }

                _verticalCSurBiasError = _verticalCSurBiasError + f * (s2 - s1) * (Math.Cos(i2 * RAD) + Math.Cos(i1 * RAD));
                SurBias(leg, ist, BDst(s1, _verticalPSurBiasError), BDst(s2, _verticalCSurBiasError), _prevDSTb, biasDST);
                SurBias(leg, ist, BAmid(s1, t1, i1, a1), BAmid(s2, t2, i2, a2), _prevAMIDb, biasAMID);

                if (UncertCalcModel.IsSccAz)
                {
                    SurBias(leg, ist, BMMudI(b2p, b4p, i1, a1, UncertCalcModel.MnMM), BMMudI(b2c, b4c, i2, a2, UncertCalcModel.MnMM), _prevMMudIb, biasMMudI);

                    for (int i = 0; i < 3; i++)
                    {
                        bias[i] = biasDST[i] + biasAMID[i] + biasMMudI[i];
                    }
                }
                else
                {
                    SurBias(leg, ist, BMMud(s1, t1, i1, a1), BMMud(s2, t2, i2, a2), _prevMMudb, biasMMud);

                    for (int i = 0; i < 3; i++)
                    {
                        bias[i] = biasDST[i] + biasAMID[i] + biasMMud[i];
                    }
                }
            }

        }
        private Tuple<double, double, double, double> BMMudI(double b2, double b4, double inc, double az, double mn)
        {
            double wt3, hAinc = 0, hA70 = 0, am;
            double std70;

            am = az - UncertCalcModel.Declination;

            if ((80 < inc) && (inc < 100) && ((((70 < am) && (am < 110))) || (((250 < am) && (am < 290)))))
            {
                HAapprox(am, mn, out hAinc);
                HAapprox(70, mn, out hA70);

                std70 = (Math.Sin(inc * RAD) * Math.Sin(70 * RAD)) / (1 - ((Math.Sin(inc * RAD) * Math.Sin(70 * RAD)) * (Math.Sin(inc * RAD) * Math.Sin(70 * RAD))));
                std70 = std70 * (Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(inc * RAD) + Math.Sin(inc * RAD) * Math.Cos(70 * RAD)) * mn;
                wt3 = ((hAinc - hA70) * RAD + std70);
            }
            else
            {
                wt3 = Math.Sin(inc * RAD) * Math.Sin(am * RAD) * b2 * mn / b4;
            }

            return Tuple.Create(0D, 0D, 0D, wt3);
        }
        private Tuple<double, double, double, double> BMMud(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, mAz;

            mAz = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = (Math.Sin(inc * RAD) * Math.Cos(mAz * RAD) + Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(inc * RAD));
            wt3 = wt3 * Math.Sin(inc * RAD) * Math.Sin(mAz * RAD) * UncertCalcModel.MnMM;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> BDst(double md, double vert)
        {

            double wt1, wt2, wt3;
            wt1 = vert * md * UncertCalcModel.MnDST;
            wt2 = 0;
            wt3 = 0;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private void SurBias(int leg, int ist, Tuple<double, double, double, double> wPrev,
                                Tuple<double, double, double, double> wCurr, double[] prevPE, double[] posErr)
        {
            double[] mat = new double[3];

            if (ist > _lstKS[leg - 1] + 1)
            {
                MVmult(_aDR, wPrev, mat);
                VecAdd(prevPE, mat, 3, prevPE);
            }

            if ((ist == (_lstKS[leg] + 1)) && (ist != UncertCalcModel.Nst))
            {
                VecZ(posErr, 3);
            }
            else
            {
                MVmult(_aDRB, wCurr, posErr);
            }
            VecAdd(posErr, prevPE, 3, posErr);
        }

        private void VecZ(double[] a, int nRow)
        {
            for (int i = 0; i < 3; i++)
            {
                a[i] = 0;
            }
        }

        private void GlobCovErr(int leg, int ist, double s1, double i1, double a1,
                                    double s2, double i2, double a2, double[,] covG)
        {
            double b1P, b2P, b3P, b4P;
            double b1C, b2C, b3C, b4C;
            double a1m, a2m;

            double[,] covAZ = new double[3, 3];
            double[,] covDBH = new double[3, 3];
            double[,] covDST = new double[3, 3];
            double[,] covMFI = new double[3, 3];
            double[,] covMDI = new double[3, 3];

            if (ist == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    _prevAZ[i] = 0;
                    _prevDBH[i] = 0;
                    _prevDST[i] = 0;
                    _prevMFI[i] = 0;
                    _prevMDI[i] = 0;
                }

                _verticalPGlobalError = 0;
                _verticalCGlobalError = 0;
            }

            a1m = a1 - UncertCalcModel.Declination;
            b1P = (Math.Pow(Math.Cos(i1 * RAD), 2)) * Math.Sin(i1 * RAD) * Math.Sin(a1m * RAD);
            b2P = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i1 * RAD) + Math.Sin(i1 * RAD) * Math.Cos(a1m * RAD);
            b3P = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(i1 * RAD) * Math.Cos(a1m * RAD) - Math.Cos(i1 * RAD);
            b4P = 1 - (Math.Pow((Math.Sin(a1m * RAD) * Math.Sin(i1 * RAD)), 2));
            a2m = a2 - UncertCalcModel.Declination;
            b1C = (Math.Pow(Math.Cos(i2 * RAD), 2) * Math.Sin(i2 * RAD) * Math.Sin(a2m * RAD));
            b2C = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(i2 * RAD) + Math.Sin(i2 * RAD) * Math.Cos(a2m * RAD);
            b3C = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(i2 * RAD) * Math.Cos(a2m * RAD) - Math.Cos(i2 * RAD);
            b4C = 1 - (Math.Pow((Math.Sin(a2m * RAD) * Math.Sin(i2 * RAD)), 2));

            if (ist == _lstKS[leg] + 1 && ist != UncertCalcModel.Nst)
            {

                GlobCov(leg, ist, WtAZ(UncertCalcModel.SdAZ), WtAZ(UncertCalcModel.SdAZ), _prevAZ, covAZ);
                GlobCov(leg, ist, WtDBH(UncertCalcModel.SdDBH), WtDBH(UncertCalcModel.SdDBH), _prevDBH, covDBH);
                GlobCov(leg, ist, WtDST(s1, _verticalCGlobalError), WtDST(s2, _verticalCGlobalError), _prevDST, covDST);
                GlobCov(leg, ist, WtMFI(b2P, b4P, i1, a1, UncertCalcModel.SdMFI), WtMFI(b2C, b4C, i2, a2, UncertCalcModel.SdMFI), _prevMFI, covMFI);
                GlobCov(leg, ist, WtMDI(b3P, b4P, i1, a1, UncertCalcModel.SdMDI), WtMDI(b3C, b4C, i2, a2, UncertCalcModel.SdMDI), _prevMDI, covMDI);
            }
            else
            {
                double dI, daz, dl, f;

                _verticalPGlobalError = _verticalCGlobalError;
                dI = (i2 - i1) * Math.PI / 180;
                daz = (a2 - a1) * Math.PI / 180;
                dl = Math.Acos(Math.Cos(dI) - Math.Sin(i2 * Math.PI / 180) * Math.Sin(i1 * Math.PI / 180) * (1 - Math.Cos(daz)));

                if (dl > 0.001)
                {
                    f = Math.Tan(0.5 * dl) / dl;
                }
                else
                {
                    f = 0.5;
                }

                _verticalCGlobalError = _verticalCGlobalError + f * (s2 - s1) * (Math.Cos(i2 * RAD) + Math.Cos(i1 * RAD));
                GlobCov(leg, ist, WtAZ(UncertCalcModel.SdAZ), WtAZ(UncertCalcModel.SdAZ), _prevAZ, covAZ);
                GlobCov(leg, ist, WtDBH(UncertCalcModel.SdDBH), WtDBH(UncertCalcModel.SdDBH), _prevDBH, covDBH);
                GlobCov(leg, ist, WtDST(s1, _verticalPGlobalError), WtDST(s2, _verticalCGlobalError), _prevDST, covDST);
                GlobCov(leg, ist, WtMFI(b2P, b4P, i1, a1, UncertCalcModel.SdMFI), WtMFI(b2C, b4C, i2, a2, UncertCalcModel.SdMFI), _prevMFI, covMFI);
                GlobCov(leg, ist, WtMDI(b3P, b4P, i1, a1, UncertCalcModel.SdMDI), WtMDI(b3C, b4C, i2, a2, UncertCalcModel.SdMDI), _prevMDI, covMDI);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        covG[i, j] = covAZ[i, j] + covDBH[i, j] + covDST[i, j] + covMFI[i, j] + covMDI[i, j];
                    }
                }
            }

        }

        private void MDIamp(double mAz, out double ampErr, double sd)
        {
            double kerr, azq = 0, minErr;

            kerr = Math.Asin(Math.Abs(Math.Sin(mAz * RAD)) - Math.Abs(Math.Tan(UncertCalcModel.Dip * RAD) * sd * RAD)) * DEGREE;
            Quad(mAz, out azq);
            minErr = Math.Min(azq - kerr, 90 - azq);
            ampErr = Math.Sqrt(0.5 * (((azq - kerr) * (azq - kerr)) + (minErr * minErr))) * Math.Sign(Math.Sin(2 * mAz * RAD));

        }

        private Tuple<double, double, double, double> WtMDI(double b3, double b4, double inc, double az, double sd)
        {
            double wt3, amp = 0.0, am;

            am = az - UncertCalcModel.Declination;

            if ((89 < inc) && (inc < 91) && (((80 < am) && (am < 100)) || ((260 < am) && (am < 280))))
            {
                MDIamp(am, out amp, sd);
                wt3 = amp * RAD;
            }
            else
            {
                wt3 = Math.Sin(inc * RAD) * Math.Sin(am * RAD) * b3 * sd * RAD / b4;
            }

            return Tuple.Create(0D, 0D, 0D, wt3);
        }


        private void MFIamp(double mAz, out double ampErr, double sd)
        {
            double kerr, azq = 0.0, minErr, bn;


            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            kerr = Math.Asin(Math.Abs(Math.Sin(mAz * RAD)) - Math.Abs(sd / bn)) * DEGREE;
            Quad(mAz, out azq);
            minErr = Math.Min(azq - kerr, 90 - azq);
            ampErr = Math.Sqrt(0.5 * (((azq - kerr) * (azq - kerr)) + (minErr * minErr))) * Math.Sign(Math.Sin(2 * mAz * RAD));
        }

        private Tuple<double, double, double, double> WtMFI(double b2, double b4, double inc, double az, double sd)
        {
            double wt3, amp = 0.0, am;

            am = az - UncertCalcModel.Declination;

            if ((89 < inc) && (inc < 91) && (((80 < am) && (am < 100)) || ((260 < am) && (am < 280))))
            {
                MFIamp(am, out amp, sd);
                wt3 = -amp * RAD;
            }
            else
            {
                wt3 = -Math.Sin(inc * RAD) * Math.Sin(am * RAD) * b2 * sd / (UncertCalcModel.Btot * b4);
            }

            return Tuple.Create(0D, 0D, 0D, wt3);
        }

        private Tuple<double, double, double, double> WtDST(double md, double vert)
        {
            double wt1, wt2, wt3;

            wt1 = vert * md * UncertCalcModel.SdDST;
            wt2 = 0;
            wt3 = 0;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtDBH(double sd)
        {
            double wt1, wt2, wt3, bn;


            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            wt1 = 0;
            wt2 = 0;
            wt3 = (1 / bn) * sd * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);

        }

        private Tuple<double, double, double, double> WtAZ(double sd)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = 0;
            wt3 = sd * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);

        }

        private void GlobCov(int leg, int ist, Tuple<double, double, double, double> wPrev, Tuple<double, double, double, double> wCurr,
                                double[] prevPE, double[,] covar)
        {
            double[] mat = new double[3];
            double[] posErr = new double[3];

            if (ist > _lstKS[leg - 1] + 1)
            {
                MVmult(_aDR, wPrev, mat);
                VecAdd(prevPE, mat, 3, prevPE);
            }

            if (ist == _lstKS[leg] + 1 && ist != UncertCalcModel.Nst)
            {
                VVmult(prevPE, prevPE, covar);
            }
            else
            {
                MVmult(_aDRB, wCurr, posErr);
                VecAdd(posErr, prevPE, 3, posErr);
                VVmult(posErr, posErr, covar);
            }
        }

        private void SysCovLC(int leg, int ist,
                              double s1, double t1, double i1, double a1,
                              double s2, double t2, double i2, double a2,
                              double[,] covMat)
        {
            double[,] covMX = new double[3, 3];
            double[,] covMY = new double[3, 3];
            double[,] covMIS1 = new double[3, 3];
            double[,] covMIS2 = new double[3, 3];
            double[,] covMIS3 = new double[3, 3];
            double[,] covMIS4 = new double[3, 3];
            double[,] covSAG = new double[3, 3];
            double[,] covDREF = new double[3, 3];
            double[,] covDSF = new double[3, 3];
            double[,] covAMIC = new double[3, 3];
            double[,] covAMID = new double[3, 3];
            double[,] covAMIL = new double[3, 3];
            double[,] covABx = new double[3, 3];
            double[,] covABy = new double[3, 3];
            double[,] covABTI1 = new double[3, 3];
            double[,] covABTI2 = new double[3, 3];
            double[,] covABz = new double[3, 3];
            double[,] covASx = new double[3, 3];
            double[,] covASy = new double[3, 3];
            double[,] covASTI1 = new double[3, 3];
            double[,] covASTI2 = new double[3, 3];
            double[,] covASTI3 = new double[3, 3];
            double[,] covASz = new double[3, 3];
            double[,] covMBx = new double[3, 3];
            double[,] covMBy = new double[3, 3];
            double[,] covMBTI1 = new double[3, 3];
            double[,] covMBTI2 = new double[3, 3];
            double[,] covMBz = new double[3, 3];
            double[,] covMSx = new double[3, 3];
            double[,] covMSy = new double[3, 3];
            double[,] covMSTI1 = new double[3, 3];
            double[,] covMSTI2 = new double[3, 3];
            double[,] covMSTI3 = new double[3, 3];
            double[,] covMSz = new double[3, 3];
            double[,] covMMud = new double[3, 3];

            if (_isValidLeg)
            {
                for (int i = 0; i < 3; i++)
                {
                    _prevMX[i] = 0;
                    _prevMY[i] = 0;
                    _prevMIS1[i] = 0;
                    _prevMIS2[i] = 0;
                    _prevMIS3[i] = 0;
                    _prevMIS4[i] = 0;
                    _prevSAG[i] = 0;
                    _prevDSF[i] = 0;
                    _prevDREF[i] = 0;
                    _prevAMIC[i] = 0;
                    _prevAMID[i] = 0;
                    _prevAMIL[i] = 0;
                    _prevABx[i] = 0;
                    _prevABy[i] = 0;
                    _prevABTI1[i] = 0;
                    _prevABTI2[i] = 0;
                    _prevABz[i] = 0;
                    _prevASx[i] = 0;
                    _prevASy[i] = 0;
                    _prevASTI1[i] = 0;
                    _prevASTI2[i] = 0;
                    _prevASTI3[i] = 0;
                    _prevASz[i] = 0;
                    _prevMBx[i] = 0;
                    _prevMBy[i] = 0;
                    _prevMBTI1[i] = 0;
                    _prevMBTI2[i] = 0;
                    _prevMBz[i] = 0;
                    _prevMSx[i] = 0;
                    _prevMSy[i] = 0;
                    _prevMSTI1[i] = 0;
                    _prevMSTI2[i] = 0;
                    _prevMSTI3[i] = 0;
                    _prevMSz[i] = 0;
                    _prevMMud[i] = 0;
                }
                _isValidLeg = false;
            }
            SysCov(leg, ist, WtMIS1(s1, t1, i1, a1), WtMIS1(s2, t2, i2, a2), _prevMIS1, covMIS1);
            SysCov(leg, ist, WtMIS2(s1, t1, i1, a1), WtMIS2(s2, t2, i2, a2), _prevMIS2, covMIS2);
            SysCovMis(leg, ist, WtMIS3(s1, t1, i1, a1), WtMIS3(s2, t2, i2, a2), _prevMIS3, covMIS3);
            SysCovMis(leg, ist, WtMIS4(s1, t1, i1, a1), WtMIS4(s2, t2, i2, a2), _prevMIS4, covMIS4);
            SysCov(leg, ist, WtSAG(s1, t1, i1, a1), WtSAG(s2, t2, i2, a2), _prevSAG, covSAG);
            SysCov(leg, ist, WtDREF(s1, i1, a1), WtDREF(s2, i2, a2), _prevDREF, covDREF);
            SysCov(leg, ist, WtDsf(s1, t1, i1, a1), WtDsf(s2, t2, i2, a2), _prevDSF, covDSF);
            SysCov(leg, ist, WtAMIC(s1, t1, i1, a1), WtAMIC(s2, t2, i2, a2), _prevAMIC, covAMIC);
            SysCov(leg, ist, WtAMID(s1, t1, i1, a1), WtAMID(s2, t2, i2, a2), _prevAMID, covAMID);
            SysCov(leg, ist, WtAMIL(s1, t1, i1, a1), WtAMIL(s2, t2, i2, a2), _prevAMIL, covAMIL);

            if (UncertCalcModel.IsToolFaceIndependent)
            {
                SysCov(leg, ist, WtABTI1(s1, i1, a1), WtABTI1(s2, i2, a2), _prevABTI1, covABTI1);
                SysCovMis(leg, ist, WtABTI2(s1, i1, a1), WtABTI2(s2, i2, a2), _prevABTI2, covABTI2);
                SysCov(leg, ist, WtASTI1(s1, i1, a1), WtASTI1(s2, i2, a2), _prevASTI1, covASTI1);
                SysCov(leg, ist, WtASTI2(s1, i1, a1), WtASTI2(s2, i2, a2), _prevASTI2, covASTI2);
                SysCov(leg, ist, WtASTI3(s1, i1, a1), WtASTI3(s2, i2, a2), _prevASTI3, covASTI3);
                SysCov(leg, ist, WtMBTI1(s1, i1, a1), WtMBTI1(s2, i2, a2), _prevMBTI1, covMBTI1);
                SysCov(leg, ist, WtMBTI2(s1, i1, a1), WtMBTI2(s2, i2, a2), _prevMBTI2, covMBTI2);
                SysCov(leg, ist, WtMSTI1(s1, i1, a1), WtMSTI1(s2, i2, a2), _prevMSTI1, covMSTI1);
                SysCov(leg, ist, WtMSTI2(s1, i1, a1), WtMSTI2(s2, i2, a2), _prevMSTI2, covMSTI2);
                SysCov(leg, ist, WtMSTI3(s1, i1, a1), WtMSTI3(s2, i2, a2), _prevMSTI3, covMSTI3);
            }
            else
            {
                SysCovMis(leg, ist, WtABx(s1, t1, i1, a1), WtABx(s2, t2, i2, a2), _prevABx, covABx);
                SysCovMis(leg, ist, WtABy(s1, t1, i1, a1), WtABy(s2, t2, i2, a2), _prevABy, covABy);
                SysCov(leg, ist, WtASx(s1, t1, i1, a1), WtASx(s2, t2, i2, a2), _prevASx, covASx);
                SysCov(leg, ist, WtASy(s1, t1, i1, a1), WtASy(s2, t2, i2, a2), _prevASy, covASy);
                SysCov(leg, ist, WtMBx(s1, t1, i1, a1), WtMBx(s2, t2, i2, a2), _prevMBx, covMBx);
                SysCov(leg, ist, WtMBy(s1, t1, i1, a1), WtMBy(s2, t2, i2, a2), _prevMBy, covMBy);
                SysCov(leg, ist, WtMSx(s1, t1, i1, a1), WtMSx(s2, t2, i2, a2), _prevMSx, covMSx);
                SysCov(leg, ist, WtMSy(s1, t1, i1, a1), WtMSy(s2, t2, i2, a2), _prevMSy, covMSy);
            }

            SysCov(leg, ist, WtABz(s1, t1, i1, a1), WtABz(s2, t2, i2, a2), _prevABz, covABz);
            SysCov(leg, ist, WtASz(s1, t1, i1, a1), WtASz(s2, t2, i2, a2), _prevASz, covASz);
            SysCov(leg, ist, WtMBz(s1, t1, i1, a1), WtMBz(s2, t2, i2, a2), _prevMBz, covMBz);
            SysCov(leg, ist, WtMSz(s1, t1, i1, a1), WtMSz(s2, t2, i2, a2), _prevMSz, covMSz);
            SysCov(leg, ist, WtMMud(s1, t1, i1, a1), WtMMud(s2, t2, i2, a2), _prevMMud, covMMud);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    covMat[i, j] = covMIS1[i, j] + covMIS2[i, j] + covMIS3[i, j] + covMIS4[i, j] + covSAG[i, j];
                    covMat[i, j] = covMat[i, j] + covDREF[i, j] + covDSF[i, j];
                    covMat[i, j] = covMat[i, j] + covAMIC[i, j] + covAMID[i, j] + covAMIL[i, j];

                    if (UncertCalcModel.IsToolFaceIndependent)
                    {
                        covMat[i, j] = covMat[i, j] + covABTI1[i, j] + covABTI2[i, j] + covABz[i, j];
                        covMat[i, j] = covMat[i, j] + covASTI1[i, j] + covASTI2[i, j] + covASTI3[i, j] + covASz[i, j];
                        covMat[i, j] = covMat[i, j] + covMBTI1[i, j] + covMBTI2[i, j] + covMBz[i, j];
                        covMat[i, j] = covMat[i, j] + covMSTI1[i, j] + covMSTI2[i, j] + covMSTI3[i, j] + covMSz[i, j];
                    }
                    else
                    {
                        covMat[i, j] = covMat[i, j] + covABx[i, j] + covABy[i, j] + covABz[i, j];
                        covMat[i, j] = covMat[i, j] + covASx[i, j] + covASy[i, j] + covASz[i, j];
                        covMat[i, j] = covMat[i, j] + covMBx[i, j] + covMBy[i, j] + covMBz[i, j];
                        covMat[i, j] = covMat[i, j] + covMSx[i, j] + covMSy[i, j] + covMSz[i, j];
                    }
                    covMat[i, j] = covMat[i, j] + covMMud[i, j];
                }
            }
        }

        private Tuple<double, double, double, double> WtMMud(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = (Math.Sin(inc * RAD) * Math.Cos(magAzimuth * RAD) + Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(inc * RAD));
            wt3 = wt3 * Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdMM;


            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMSz(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = -(Math.Sin(inc * RAD) * Math.Cos(magAzimuth * RAD) + Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(inc * RAD));
            wt3 = wt3 * Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdMS;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMBz(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = -Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdMB / bn;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtASz(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = -Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * UncertCalcModel.SdAS;
            wt3 = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdAS;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtABz(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = -Math.Sin(inc * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            wt3 = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMSy(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Cos(inc * RAD) * Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD) - Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(tau * RAD) - Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD);
            wt3 = -wt3 * (Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD) + Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD)) * UncertCalcModel.SdMS;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMSx(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Cos(inc * RAD) * Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD) - Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Sin(tau * RAD) + Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD);
            wt3 = wt3 * (Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD) - Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD)) * UncertCalcModel.SdMS;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMBy(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD) + Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD);
            wt3 = -wt3 * UncertCalcModel.SdMB / bn;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMBx(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD) - Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD);
            wt3 = wt3 * UncertCalcModel.SdMB / bn;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtASy(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;

            wt2 = Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * ((Math.Cos(tau * RAD)) * (Math.Cos(tau * RAD))) * UncertCalcModel.SdAS;
            wt3 = Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD) + Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD);
            wt3 = -(wt3 * Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) - Math.Cos(inc * RAD) * Math.Sin(tau * RAD)) * Math.Cos(tau * RAD) * UncertCalcModel.SdAS;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtASx(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * ((Math.Sin(tau * RAD)) * (Math.Sin(tau * RAD))) * UncertCalcModel.SdAS;
            wt3 = Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD) - Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD);
            wt3 = -(wt3 * Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) + Math.Cos(inc * RAD) * Math.Cos(tau * RAD)) * Math.Sin(tau * RAD) * UncertCalcModel.SdAS;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtABy(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = -Math.Cos(inc * RAD) * Math.Cos(tau * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            wt3 = Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD) + Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD);
            wt3 = (wt3 * Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) - Math.Cos(inc * RAD) * Math.Sin(tau * RAD)) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtABx(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = -Math.Cos(inc * RAD) * Math.Sin(tau * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            wt3 = Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD) - Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD);
            wt3 = (wt3 * Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) + Math.Cos(inc * RAD) * Math.Cos(tau * RAD)) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMSTI3(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Cos(inc * RAD) * (Math.Cos(magAzimuth * RAD)) * (Math.Cos(magAzimuth * RAD));
            wt3 = wt3 - Math.Cos(inc * RAD) * (Math.Sin(magAzimuth * RAD)) * (Math.Sin(magAzimuth * RAD));
            wt3 = (wt3 - Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(magAzimuth * RAD)) * UncertCalcModel.SdMS / 2;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMSTI2(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(inc * RAD);
            wt3 = wt3 - (Math.Cos(inc * RAD) * Math.Cos(inc * RAD)) * Math.Cos(magAzimuth * RAD) - Math.Cos(magAzimuth * RAD);
            wt3 = wt3 * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdMS / 2;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMSTI1(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD);
            wt3 = wt3 * (Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(inc * RAD) + Math.Sin(inc * RAD) * Math.Cos(magAzimuth * RAD)) * UncertCalcModel.SdMS / Math.Sqrt(2);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMBTI2(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;


            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Cos(magAzimuth * RAD) * UncertCalcModel.SdMB / bn;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMBTI1(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = -Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdMB / bn;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtASTI3(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;

            wt1 = 0;
            wt2 = 0;
            wt3 = (Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(magAzimuth * RAD) - Math.Cos(inc * RAD)) * UncertCalcModel.SdAS / 2;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtASTI2(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;

            wt1 = 0;
            wt2 = Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * UncertCalcModel.SdAS / 2;
            wt3 = -Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdAS / 2;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtASTI1(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;

            wt1 = 0;
            wt2 = Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * UncertCalcModel.SdAS / Math.Sqrt(2);
            wt3 = -Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdAS / Math.Sqrt(2);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtABTI2(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;

            wt1 = 0;
            wt2 = 0;
            wt3 = (Math.Cos(inc * RAD) - Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(magAzimuth * RAD)) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtABTI1(double md, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;

            wt1 = 0;
            wt2 = -Math.Cos(inc * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            wt3 = (Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD)) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtAMIL(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            magAzimuth = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) / bn * UncertCalcModel.SdAMIL;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtABITI1(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = -Math.Cos(inc * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            wt3 = (Math.Pow(Math.Cos(inc * RAD), 2) * Math.Sin(magAzimuth * RAD) * b2) * UncertCalcModel.SdAB / (UncertCalcModel.Gtot * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtABITI2(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = 0;
            wt3 = -b3 * UncertCalcModel.SdAB / (UncertCalcModel.Gtot * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtASITI1(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * UncertCalcModel.SdAS / Math.Sqrt(2);
            wt3 = -b1 * b2 * UncertCalcModel.SdAS / (Math.Sqrt(2) * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }


        private Tuple<double, double, double, double> WtASITI2(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * UncertCalcModel.SdAS / 2;
            wt3 = -b1 * b2 * UncertCalcModel.SdAS / (2 * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtASITI3(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;

            wt1 = 0;
            wt2 = 0;
            wt3 = b3 * UncertCalcModel.SdAS / (2 * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMBITI1(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = 0;
            wt3 = (Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD)) * UncertCalcModel.SdMB / (bn * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMBITI2(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = 0;
            wt3 = (Math.Cos(magAzimuth * RAD)) * UncertCalcModel.SdMB / (bn * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMSITI1(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = 0;
            wt3 = (Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) * b2) * UncertCalcModel.SdMS / (Math.Sqrt(2) * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMSITI2(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(inc * RAD);
            wt3 = wt3 - Math.Pow(Math.Cos(inc * RAD), 2) * Math.Cos(magAzimuth * RAD) - Math.Cos(magAzimuth * RAD);
            wt3 = Math.Sin(magAzimuth * RAD) * wt3 * UncertCalcModel.SdMS / (2 * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMSITI3(double b1, double b2, double b3, double b4, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3;

            if (Math.Abs(b4) < 0.01) b4 = 100;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Cos(inc * RAD) * (Math.Pow(Math.Cos(magAzimuth * RAD), 2) - Math.Pow(Math.Sin(magAzimuth * RAD), 2));
            wt3 = wt3 - Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(magAzimuth * RAD);
            wt3 = wt3 * UncertCalcModel.SdMS / (2 * b4);

            return Tuple.Create(0D, wt1, wt2, wt3);
        }



        private Tuple<double, double, double, double> WtABIx(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0;

            wt1 = 0;
            wt2 = -Math.Cos(inc * RAD) * Math.Sin(tau * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            if (89 < inc && inc < 91)
            {
                if ((80 < magAzimuth && magAzimuth < 100) || (260 < magAzimuth && magAzimuth < 280))
                {
                    ABIamp(magAzimuth, out amp);
                    wt3 = -amp * Math.Cos(tau * RAD) * RAD / UncertCalcModel.Gtot;
                }
                else
                {
                    wt3 = (b1 * b2 * Math.Sin(tau * RAD) - b3 * Math.Cos(tau * RAD)) * UncertCalcModel.SdAB / (UncertCalcModel.Gtot * b4);
                }

            }
            else
            {
                wt3 = (b1 * b2 * Math.Sin(tau * RAD) - b3 * Math.Cos(tau * RAD)) * UncertCalcModel.SdAB / (UncertCalcModel.Gtot * b4);
            }


            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtABIy(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0;

            wt1 = 0;
            wt2 = -Math.Cos(inc * RAD) * Math.Cos(tau * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            if (89 < inc && inc < 91)
            {
                if ((80 < magAzimuth && magAzimuth < 100) || (260 < magAzimuth && magAzimuth < 280))
                {
                    ABIamp(magAzimuth, out amp);
                    wt3 = amp * Math.Sin(tau * RAD) * RAD / UncertCalcModel.Gtot;
                }
                else
                {
                    wt3 = (b1 * b2 * Math.Cos(tau * RAD) + b3 * Math.Sin(tau * RAD)) * UncertCalcModel.SdAB / (UncertCalcModel.Gtot * b4);
                }

            }
            else
            {
                wt3 = (b1 * b2 * Math.Cos(tau * RAD) + b3 * Math.Sin(tau * RAD)) * UncertCalcModel.SdAB / (UncertCalcModel.Gtot * b4);
            }


            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtABIz(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0;

            wt1 = 0;
            wt2 = -Math.Sin(inc * RAD) * UncertCalcModel.SdAB / UncertCalcModel.Gtot;
            if (89 < inc && inc < 91)
            {
                wt3 = 0;

            }
            else
            {
                wt3 = b2 * Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdAB / (UncertCalcModel.Gtot * b4);
            }


            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtASIx(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0;

            wt1 = 0;
            wt2 = Math.Cos(inc * RAD) * Math.Sin(inc * RAD) * Math.Pow(Math.Sin(tau * RAD), 2) * UncertCalcModel.SdAS;
            if (89 < inc && inc < 91)
            {
                if ((80 < magAzimuth && magAzimuth < 100) || (260 < magAzimuth && magAzimuth < 280))
                {
                    ASIamp(magAzimuth, out amp);
                    wt3 = -amp * Math.Sin(2 * tau * RAD) * RAD;
                }
                else
                {
                    wt3 = -Math.Sin(tau * RAD) * (b1 * b2 * Math.Sin(tau * RAD) - b3 * Math.Cos(tau * RAD)) * UncertCalcModel.SdAS / b4;
                }
            }

            else
            {
                wt3 = -Math.Sin(tau * RAD) * (b1 * b2 * Math.Sin(tau * RAD) - b3 * Math.Cos(tau * RAD)) * UncertCalcModel.SdAS / b4;
            }

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtASIy(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0;

            wt1 = 0;
            wt2 = Math.Cos(inc * RAD) * Math.Sin(inc * RAD) * Math.Pow(Math.Cos(tau * RAD), 2) * UncertCalcModel.SdAS;
            if (89 < inc && inc < 91)
            {
                if ((80 < magAzimuth && magAzimuth < 100) || (260 < magAzimuth && magAzimuth < 280))
                {
                    ASIamp(magAzimuth, out amp);
                    wt3 = amp * Math.Sin(2 * tau * RAD) * RAD;
                }
                else
                {
                    wt3 = -Math.Cos(tau * RAD) * (b1 * b2 * Math.Cos(tau * RAD) + b3 * Math.Sin(tau * RAD)) * UncertCalcModel.SdAS / b4;
                }
            }

            else
            {
                wt3 = -Math.Cos(tau * RAD) * (b1 * b2 * Math.Cos(tau * RAD) + b3 * Math.Sin(tau * RAD)) * UncertCalcModel.SdAS / b4;
            }

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtASIz(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0;

            wt1 = 0;
            wt2 = -Math.Sin(inc * RAD) * Math.Cos(inc * RAD) * UncertCalcModel.SdAS;
            if (89 < inc && inc < 91)
            {
                wt3 = 0;

            }
            else
            {
                wt3 = b1 * b2 * UncertCalcModel.SdAS / b4;
            }

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMBIx(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            wt1 = 0;
            wt2 = 0;
            if (89 < inc && inc < 91)
            {
                if ((80 < magAzimuth && magAzimuth < 100) || (260 < magAzimuth && magAzimuth < 280))
                {
                    MBIamp(magAzimuth, out amp);
                    wt3 = -amp * Math.Cos(tau * RAD) * RAD;
                }
                else
                {
                    wt3 = (Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD) - Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD)) * UncertCalcModel.SdMB / (bn * b4);
                }
            }

            else
            {
                wt3 = (Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD) - Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD)) * UncertCalcModel.SdMB / (bn * b4);
            }

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMBIy(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt1, wt2, wt3, amp = 0.0, bn;

            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            wt1 = 0;
            wt2 = 0;
            if (89 < inc && inc < 91)
            {
                if ((80 < magAzimuth && magAzimuth < 100) || (260 < magAzimuth && magAzimuth < 280))
                {
                    MBIamp(magAzimuth, out amp);
                    wt3 = amp * Math.Sin(tau * RAD) * RAD;
                }
                else
                {
                    wt3 = -(Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD) + Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD)) * UncertCalcModel.SdMB / (bn * b4);
                }
            }

            else
            {
                wt3 = -(Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD) + Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD)) * UncertCalcModel.SdMB / (bn * b4);
            }

            return Tuple.Create(0D, wt1, wt2, wt3);
        }
        private Tuple<double, double, double, double> WtMSIx(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt3 = 0.0, amp = 0.0;


            if ((89 < inc) && (inc < 91) && (((80 < magAzimuth) && (magAzimuth < 100)) || ((260 < magAzimuth) && (magAzimuth < 280))))
            {

                MSIamp(magAzimuth, out amp);
                wt3 = -amp * Math.Sin(2 * tau * RAD) * RAD;
            }
            else
            {
                wt3 = Math.Cos(inc * RAD) * Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD) - Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Sin(tau * RAD) + Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD);
                wt3 = wt3 * (Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD) - Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD)) * UncertCalcModel.SdMS / b4;

            }

            return Tuple.Create(0D, 0D, 0D, wt3);
        }
        private Tuple<double, double, double, double> WtMSIy(double b1, double b2, double b3, double b4, double tau, double inc, double magAzimuth)
        {
            double wt3 = 0.0, amp = 0.0;


            if ((89 < inc) && (inc < 91) && (((80 < magAzimuth) && (magAzimuth < 100)) || ((260 < magAzimuth) && (magAzimuth < 280))))
            {

                MSIamp(magAzimuth, out amp);
                wt3 = amp * Math.Sin(2 * tau * RAD) * RAD;
            }
            else
            {
                wt3 = Math.Cos(inc * RAD) * Math.Cos(magAzimuth * RAD) * Math.Cos(tau * RAD) - Math.Tan(UncertCalcModel.Dip * RAD) * Math.Sin(inc * RAD) * Math.Cos(tau * RAD) - Math.Sin(magAzimuth * RAD) * Math.Sin(tau * RAD);
                wt3 = -wt3 * (Math.Cos(magAzimuth * RAD) * Math.Sin(tau * RAD) + Math.Cos(inc * RAD) * Math.Sin(magAzimuth * RAD) * Math.Cos(tau * RAD)) * UncertCalcModel.SdMS / b4;
            }


            return Tuple.Create(0D, 0D, 0D, wt3);
        }
        private Tuple<double, double, double, double> WtMMudI(double b2, double b4, double inc, double az, double sd)
        {
            double wt3 = 0.0, amp = 0.0, hAinc = 0.0, hA70 = 0.0, std70, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;

            if ((80 < inc) && (inc < 100) && (((70 < magAzimuth) && (magAzimuth < 110)) || ((250 < magAzimuth) && (magAzimuth < 290))))
            {
                HAapprox(magAzimuth, sd, out hAinc);
                HAapprox(70, sd, out hA70);
                std70 = Math.Sin(inc * RAD) * Math.Sin(70 * RAD) / (1 - Math.Pow((Math.Sin(inc * RAD) * Math.Sin(70 * RAD)), 2));
                std70 = std70 * (Math.Tan(UncertCalcModel.Dip * RAD) * Math.Cos(inc * RAD) + Math.Sin(inc * RAD) * Math.Cos(70 * RAD)) * sd;
                wt3 = -((hAinc - hA70) * RAD + std70);
            }
            else
            {
                wt3 = -Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) * b2 * sd / b4;
            }


            return Tuple.Create(0D, 0D, 0D, wt3);
        }

        private void ASIamp(double magAzimuth, out double ampErr)
        {
            double kerr, azq = 0.0, minErr;

            kerr = Math.Asin(Math.Abs(Math.Sin(magAzimuth * RAD)) - Math.Abs(Math.Tan(UncertCalcModel.Dip * RAD) * UncertCalcModel.SdAS)) * DEGREE;
            Quad(magAzimuth, out azq);
            minErr = Math.Min(azq - kerr, 90 - azq);
            ampErr = Math.Sqrt(0.5 * (Math.Pow((azq - kerr), 2) + Math.Pow(minErr, 2)) * Math.Sign(Math.Cos(magAzimuth * RAD)));

        }
        private void ABIamp(double magAzimuth, out double ampErr)
        {
            double kerr, azq = 0.0, minErr;

            kerr = Math.Asin(Math.Abs(Math.Sin(magAzimuth * RAD)) - Math.Abs(Math.Tan(UncertCalcModel.Dip * RAD) * UncertCalcModel.SdAB)) * DEGREE;
            Quad(magAzimuth, out azq);
            minErr = Math.Min(azq - kerr, 90 - azq);
            ampErr = Math.Sqrt(0.5 * (Math.Pow((azq - kerr), 2) + Math.Pow(minErr, 2)) * Math.Sign(Math.Cos(magAzimuth * RAD)));

        }
        private void MBIamp(double magAzimuth, out double ampErr)
        {
            double kerr, azq = 0.0, minErr;
            double bn;
            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            kerr = Math.Asin(Math.Abs(Math.Sin(magAzimuth * RAD)) - Math.Abs(UncertCalcModel.SdMB / bn)) * DEGREE;
            Quad(magAzimuth, out azq);
            minErr = Math.Min(azq - kerr, 90 - azq);
            ampErr = Math.Sqrt(0.5 * (Math.Pow((azq - kerr), 2) + Math.Pow(minErr, 2)) * Math.Sign(Math.Cos(magAzimuth * RAD)));
        }
        private void MSIamp(double magAzimuth, out double ampErr)
        {
            double kerr, azq = 0.0, minErr;
            double bn;
            bn = UncertCalcModel.Btot * Math.Cos(UncertCalcModel.Dip * RAD);
            kerr = Math.Asin(Math.Abs(Math.Sin(magAzimuth * RAD)) - Math.Abs(UncertCalcModel.SdMS / bn)) * DEGREE;
            Quad(magAzimuth, out azq);
            minErr = Math.Min(azq - kerr, 90 - azq);
            ampErr = Math.Sqrt(0.5 * (Math.Pow((azq - kerr), 2) + Math.Pow(minErr, 2)) * Math.Sign(Math.Cos(magAzimuth * RAD)));
        }

        private void HAapprox(double azMag, double sd, out double daz)
        {
            double kerr, azq = 0;
            kerr = Math.Asin(Math.Abs(Math.Sin(azMag * RAD)) - Math.Abs(sd)) * DEGREE;

            Quad(azMag, out azq);
            daz = azq - kerr;
        }

        private void Quad(double inAng, out double outQuad)
        {
            if (inAng <= 90)
            {
                outQuad = inAng;
            }
            else if (90 < inAng && inAng <= 180)
            {
                outQuad = 180 - inAng;
            }
            else if (180 < inAng && inAng <= 270)
            {
                outQuad = inAng - 180;
            }
            else
            {
                outQuad = 360 - inAng;
            }
        }

        private Tuple<double, double, double, double> WtDREF(double md, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = UncertCalcModel.SdDREF;
            wt2 = 0;
            wt3 = 0;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMIS4(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = Math.Abs(Math.Cos(inc * RAD)) * Math.Sin(az * RAD) * UncertCalcModel.SdMXY2 * RAD;
            wt3 = Math.Abs(Math.Cos(inc * RAD)) * Math.Cos(az * RAD) * UncertCalcModel.SdMXY2 * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMIS3(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = Math.Abs(Math.Cos(inc * RAD)) * Math.Cos(az * RAD) * UncertCalcModel.SdMXY2 * RAD;
            wt3 = -Math.Abs(Math.Cos(inc * RAD)) * Math.Sin(az * RAD) * UncertCalcModel.SdMXY2 * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMIS2(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = 0;
            wt3 = -UncertCalcModel.SdMXY1 * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtMIS1(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = (Math.Sin(inc * RAD)) * UncertCalcModel.SdMXY1 * RAD;
            wt3 = 0;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtAMID(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, magAzimuth;

            magAzimuth = az - UncertCalcModel.Declination;

            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Sin(inc * RAD) * Math.Sin(magAzimuth * RAD) * UncertCalcModel.SdAMID * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtAMIC(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = 0;
            wt3 = UncertCalcModel.SdAMIC * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> WtSAG(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = 0;
            wt2 = Math.Sin(inc * RAD) * UncertCalcModel.SdSAG * RAD;
            wt3 = 0;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private void SysCovMis(int leg, int ist, Tuple<double, double, double, double> wPrev,
                                Tuple<double, double, double, double> wCurr,
                                double[] prevPE, double[,] covar)
        {
            double[] arrMat = new double[3];
            double[] arrErrorPosition = new double[3];

            if (ist > _lstKS[leg - 1] + 1)
            {
                MVmult(_aDRMis, wPrev, arrMat);
                VecAdd(prevPE, arrMat, 3, prevPE);
            }

            MVmult(_aDRBMis, wCurr, arrErrorPosition);
            VecAdd(arrErrorPosition, prevPE, 3, arrErrorPosition);

            if (ist == UncertCalcModel.Nst)
            {
                VVmult(arrErrorPosition, arrErrorPosition, covar);
            }
            else if (ist == _lstKS[leg] + 1)
            {
                VVmult(prevPE, prevPE, covar);
            }
            else
            {
                VVmult(arrErrorPosition, arrErrorPosition, covar);
            }
        }

        private void SysCov(int leg, int ist, Tuple<double, double, double, double> wPrev,
                            Tuple<double, double, double, double> wCurr,
                            double[] arrPrevPE, double[,] arrCover)
        {
            double[] arrMat = new double[3];
            double[] arrErrorPosition = new double[3];

            if (ist > _lstKS[leg - 1] + 1)
            {
                MVmult(_aDR, wPrev, arrMat);
                VecAdd(arrPrevPE, arrMat, 3, arrPrevPE);
            }

            MVmult(_aDRB, wCurr, arrErrorPosition);
            VecAdd(arrErrorPosition, arrPrevPE, 3, arrErrorPosition);

            if (ist == UncertCalcModel.Nst)
            {
                VVmult(arrErrorPosition, arrErrorPosition, arrCover);
            }
            else if (ist == _lstKS[leg] + 1)
            {
                VVmult(arrPrevPE, arrPrevPE, arrCover);
            }
            else
            {
                VVmult(arrErrorPosition, arrErrorPosition, arrCover);
            }
        }

        private void VVmult(double[] a, double[] b, double[,] c)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    c[i, k] = 0;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    c[i, k] = c[i, k] + a[i] * b[k];
                }
            }
        }

        private void VecAdd(double[] a, double[] b, int rowNumber, double[] c)
        {
            for (int i = 0; i < rowNumber; i++)
            {
                c[i] = a[i] + b[i];
            }
        }

        private void MVmult(double[,] a, Tuple<double, double, double, double> b, double[] c)
        {

            double[] arrB = new double[4];
            arrB[0] = b.Item1;
            arrB[1] = b.Item2;
            arrB[2] = b.Item3;
            arrB[3] = b.Item4;

            for (int i = 0; i < 3; i++)
            {
                c[i] = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    c[i] = c[i] + a[i, j] * arrB[j + 1];
                }
            }
        }

        private Tuple<double, double, double, double> WtDsf(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3;

            wt1 = md * UncertCalcModel.SdDSF;
            wt2 = 0;
            wt3 = 0;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private Tuple<double, double, double, double> BAmid(double md, double tau, double inc, double az)
        {
            double wt1, wt2, wt3, azimuthMag;

            azimuthMag = az - UncertCalcModel.Declination;
            wt1 = 0;
            wt2 = 0;
            wt3 = Math.Sin(inc * RAD) * Math.Sin(azimuthMag * RAD) * UncertCalcModel.MnAMID * RAD;

            return Tuple.Create(0D, wt1, wt2, wt3);
        }

        private void BtDiffernce(double s0, double i0, double a0,
                                 double s1, double i1, double a1,
                                 double s2, double i2, double a2)
        {
            _aDR[0, 0] = 0.5 * (Math.Sin(i0 * RAD) * Math.Cos(a0 * RAD) - Math.Sin(i2 * RAD) * Math.Cos(a2 * RAD));
            _aDR[1, 0] = 0.5 * (Math.Sin(i0 * RAD) * Math.Sin(a0 * RAD) - Math.Sin(i2 * RAD) * Math.Sin(a2 * RAD));
            _aDR[2, 0] = 0.5 * (Math.Cos(i0 * RAD) - Math.Cos(i2 * RAD));
            _aDR[0, 1] = 0.5 * (s2 - s0) * Math.Cos(i1 * RAD) * Math.Cos(a1 * RAD);
            _aDR[1, 1] = 0.5 * (s2 - s0) * Math.Cos(i1 * RAD) * Math.Sin(a1 * RAD);
            _aDR[2, 1] = -0.5 * (s2 - s0) * Math.Sin(i1 * RAD);
            _aDR[0, 2] = -0.5 * (s2 - s0) * Math.Sin(i1 * RAD) * Math.Sin(a1 * RAD);
            _aDR[1, 2] = 0.5 * (s2 - s0) * Math.Sin(i1 * RAD) * Math.Cos(a1 * RAD);
            _aDR[2, 2] = 0;

            _aDRMis[0, 0] = _aDR[0, 0];
            _aDRMis[1, 0] = _aDR[1, 0];
            _aDRMis[2, 0] = _aDR[2, 0];
            _aDRMis[0, 1] = _aDR[0, 1];
            _aDRMis[1, 1] = _aDR[1, 1];
            _aDRMis[2, 1] = _aDR[2, 1];
            _aDRMis[0, 2] = -0.5 * (s2 - s0) * Math.Sin(a1 * RAD);
            _aDRMis[1, 2] = 0.5 * (s2 - s0) * Math.Cos(a1 * RAD);
            _aDRMis[2, 2] = 0;
        }

        private void BtDifferenceB(double s1, double i1, double a1,
                                   double s2, double i2, double a2)
        {
            _aDRB[0, 0] = 0.5 * (Math.Sin(i1 * RAD) * Math.Cos(a1 * RAD) + Math.Sin(i2 * RAD) * Math.Cos(a2 * RAD));
            _aDRB[1, 0] = 0.5 * (Math.Sin(i1 * RAD) * Math.Sin(a1 * RAD) + Math.Sin(i2 * RAD) * Math.Sin(a2 * RAD));
            _aDRB[2, 0] = 0.5 * (Math.Cos(i1 * RAD) + Math.Cos(i2 * RAD));
            _aDRB[0, 1] = 0.5 * (s2 - s1) * Math.Cos(i2 * RAD) * Math.Cos(a2 * RAD);
            _aDRB[1, 1] = 0.5 * (s2 - s1) * Math.Cos(i2 * RAD) * Math.Sin(a2 * RAD);
            _aDRB[2, 1] = -0.5 * (s2 - s1) * Math.Sin(i2 * RAD);
            _aDRB[0, 2] = -0.5 * (s2 - s1) * Math.Sin(i2 * RAD) * Math.Sin(a2 * RAD);
            _aDRB[1, 2] = 0.5 * (s2 - s1) * Math.Sin(i2 * RAD) * Math.Cos(a2 * RAD);
            _aDRB[2, 2] = 0;

            _aDRBMis[0, 0] = _aDRB[0, 0];
            _aDRBMis[1, 0] = _aDRB[1, 0];
            _aDRBMis[2, 0] = _aDRB[2, 0];
            _aDRBMis[0, 1] = _aDRB[0, 1];
            _aDRBMis[1, 1] = _aDRB[1, 1];
            _aDRBMis[2, 1] = _aDRB[2, 1];
            _aDRBMis[0, 2] = -0.5 * (s2 - s1) * Math.Sin(a2 * RAD);
            _aDRBMis[1, 2] = 0.5 * (s2 - s1) * Math.Cos(a2 * RAD);
            _aDRBMis[2, 2] = 0;
        }

        private void BtAssignDepthDiffernce(double s1, double i1, double a1,
                                            double s2, double i2, double a2)
        {
            _aDRB[0, 0] = 0.5 * (Math.Sin(i1 * RAD) * Math.Cos(a1 * RAD) - Math.Sin(i2 * RAD) * Math.Cos(a2 * RAD));
            _aDRB[1, 0] = 0.5 * (Math.Sin(i1 * RAD) * Math.Sin(a1 * RAD) - Math.Sin(i2 * RAD) * Math.Sin(a2 * RAD));
            _aDRB[2, 0] = 0.5 * (Math.Cos(i1 * RAD) - Math.Cos(i2 * RAD));
            _aDRB[0, 1] = 0.5 * (s2 - s1) * Math.Cos(i2 * RAD) * Math.Cos(a2 * RAD);
            _aDRB[1, 1] = 0.5 * (s2 - s1) * Math.Cos(i2 * RAD) * Math.Sin(a2 * RAD);
            _aDRB[2, 1] = -0.5 * (s2 - s1) * Math.Sin(i2 * RAD);
            _aDRB[0, 2] = -0.5 * (s2 - s1) * Math.Sin(i2 * RAD) * Math.Sin(a2 * RAD);
            _aDRB[1, 2] = 0.5 * (s2 - s1) * Math.Sin(i2 * RAD) * Math.Cos(a2 * RAD);
            _aDRB[2, 2] = 0;


            _aDRBMis[0, 0] = _aDRB[0, 0];
            _aDRBMis[1, 0] = _aDRB[1, 0];
            _aDRBMis[2, 0] = _aDRB[2, 0];
            _aDRBMis[0, 1] = _aDRB[0, 1];
            _aDRBMis[1, 1] = _aDRB[1, 1];
            _aDRBMis[2, 1] = _aDRB[2, 1];
            _aDRBMis[0, 2] = -0.5 * (s2 - s1) * Math.Sin(a2 * RAD);
            _aDRBMis[1, 2] = 0.5 * (s2 - s1) * Math.Cos(a2 * RAD);
            _aDRBMis[2, 2] = 0;
        }

        private double AdjustRange(double angle)
        {
            if (angle < 0)
            {
                return angle + 360;
            }
            else if (angle >= 360)
            {
                return angle - 360;
            }
            else
            {
                return angle;
            }
        }

        private void ClearUncertData()
        {
            foreach (Uncert uncert in LstUncert)
            {
                uncert.Depth = 0;
                UncertColumns(uncert);
            }
        }

        private void ResetUncertData(Uncert uncert)
        {
            UncertColumns(uncert);
        }

        private double Atanc(double den, double num)
        {
            if (den == 0)
            {
                if (num > 0)
                    return 0.5 * Math.PI;
                else if (num < 0)
                    return -0.5 * Math.PI;
                else
                    return 0;
            }
            else
            {
                return Math.Atan2(num, den);
            }
        }

        private void UncertColumns(Uncert uncert)
        {

            uncert.SigmaN = 0;
            uncert.SigmaE = 0;
            uncert.SigmaV = 0;
            uncert.BiasN = 0;
            uncert.BiasE = 0;
            uncert.BiasV = 0;
            uncert.SigmaH = 0;
            uncert.SigmaL = 0;
            uncert.SigmaA = 0;
            uncert.BiasH = 0;
            uncert.BiasL = 0;
            uncert.BiasA = 0;
            uncert.CorrHL = 0;
            uncert.CorrHA = 0;
            uncert.CorrLA = 0;
            uncert.HMajSA = 0;
            uncert.HMinSA = 0;
            uncert.RotAng = 0;
            uncert.SemiAx1 = 0;
            uncert.SemiAx2 = 0;
            uncert.SemiAx3 = 0;
            uncert.CovNN = 0;
            uncert.CovNE = 0;
            uncert.CovNV = 0;
            uncert.CovEE = 0;
            uncert.CovEV = 0;
            uncert.CovVV = 0;

        }

        #endregion

        #region InParam Validations

        public bool ValidateInParamGTotal(double gTot)
        {
            if (gTot > GTOTALMIN && gTot < GTOTALMAX)
            {
                return true;
            }

            return false;
        }
        public bool ValidateInParamDip(double dip)
        {
            if (dip > DIPMIN && dip < DIPMAX)
            {
                return true;
            }

            return false;
        }
        public bool ValidateInParamDeclination(double decl)
        {
            if (decl > DECLINATIONMIN && decl < DECLINATIONMAX)
            {
                return true;
            }

            return false;
        }
        public bool ValidateInParamLatitude(double latitude)
        {
            if (latitude > LATITUDEMIN && latitude < LATITUDEMAX)
            {
                return true;
            }

            return false;
        }
        public bool ValidateInParamSigmaValue(double sigmaLevel)
        {
            if (sigmaLevel > SIGMALEVELMIN && sigmaLevel < SIGMALEVELMAX)
            {
                return true;
            }

            return false;
        }
        public bool ValidateInParamNorth(string north, out double tieN)
        {
            return double.TryParse(north, out tieN);
        }
        public bool ValidateInParamEast(string east, out double tieE)
        {
            return double.TryParse(east, out tieE);
        }
        public bool ValidateInParamVertical(string vertical, out double tieV)
        {
            return double.TryParse(vertical, out tieV);
        }
        public bool ValidateInParamGtotal(string gTotal, out double gTot)
        {
            return double.TryParse(gTotal, out gTot);
        }
        public bool ValidateInParamBtotal(string bTotal, out double bTotIn)
        {
            if (double.TryParse(bTotal, out bTotIn))
            {
                if (bTotIn < 0)
                    return false;
                else
                {
                    if (bTotIn < 1000)
                        bTotIn = bTotIn * BTOTAL;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
