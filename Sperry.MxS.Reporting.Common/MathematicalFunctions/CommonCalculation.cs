using Microsoft.VisualBasic;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.MathematicalFunctions
{
    public class CommonCalculation
    {
        // All input values and output values should keep consistent unit system
        // Angle in Radian

        public static double CalcLcAzm(double gx, double gy, double gz, double bx, double by, double bz)
        {
            var dHsd = AtanC(-gx, gy);
            var dInc = AtanC(gz, Math.Sqrt(Math.Pow(gx, 2) + Math.Pow(gy, 2)));
            var dBxp = bx * Math.Cos(dHsd) - by * Math.Sin(dHsd);
            var dByp = bx * Math.Sin(dHsd) + by * Math.Cos(dHsd);
            var dBx2p = dBxp * Math.Cos(dInc) + bz * Math.Sin(dInc);
            return Conversions.ModTwoPi(AtanC(dBx2p, -dByp));
        }

        public static double CalcBTotal(double bx, double by, double bz)
        {
            return Math.Sqrt(Math.Pow(bx, 2) + Math.Pow(by, 2) + Math.Pow(bz, 2));
        }

        public static double CalcBoxy(double bx, double by)
        {
            return Math.Sqrt(Math.Pow(bx, 2) + Math.Pow(by, 2));
        }

        public static double CalcInc(double gx, double gy, double gz)
        {
            return Math.Atan2(Math.Sqrt(Math.Pow(gx, 2) + Math.Pow(gy, 2)), gz);
        }

        public static double CalcXYInc(double rawGx, double rawGy)
        {
            var gs = Math.Sqrt(Math.Pow(rawGx, 2) + Math.Pow(rawGy, 2));
            return gs > 1 ? Math.PI / 2 : Math.Asin(gs);
        }

        public static double CalcZInc(double rawGz)
        {
            return rawGz > 1 ? 0 : Math.Acos(rawGz);
        }

        public static double CalcGoxy(double rawGx, double rawGy)
        {
            return Math.Sqrt(Math.Pow(rawGx, 2) + Math.Pow(rawGy, 2));
        }

        public static double CalcGTotal(double rawGx, double rawGy, double rawGz)
        {
            return Math.Sqrt(Math.Pow(rawGx, 2) + Math.Pow(rawGy, 2) + Math.Pow(rawGz, 2));
        }

        public static double CalcDip(double gx, double gy, double gz, double bx, double by, double bz)
        {
            var inc = Math.Atan2(Math.Sqrt(Math.Pow(gx, 2) + Math.Pow(gy, 2)), gz);
            var hsd = Math.Atan2(gy, -gx);
            var bc = Math.Sqrt(Math.Pow(bx, 2) + Math.Pow(by, 2) + Math.Pow(bz, 2));
            if (hsd < 0)
            {
                hsd += 2 * Math.PI;
            }
            var bxp = bx * Math.Cos(hsd) - by * Math.Sin(hsd);
            var bvc = -bxp * Math.Sin(inc) + bz * Math.Cos(inc);
            var bnc = Math.Sqrt(Math.Pow(bc, 2) - Math.Pow(bvc, 2));
            return Math.Atan2(bvc, bnc);
        }

        public static double CalcScAzm(double dGx, double dGy, double dGz, double dBx, double dBy, double dBz, double dBe, double dDipe)
        {
            const double cConvLimit = 0.000001;
            bool bConverged;
            int nCount;
            int nIterations;

            double dBxp;
            double dByp;
            double dBen;
            double dBev;
            double dAzm1;
            double dAzm2;
            double dAzmLast;
            double dAzmNext = 0;

            double dSin;
            double dCos;
            double dHsd;
            double dInc;
            double dSeed;

            double dDenom;
            double dBzi;
            double dAzmOut;
            double deltapsi;
            double dGrad;

            dHsd = AtanC(-dGx, dGy);
            dInc = AtanC(dGz, Math.Sqrt(Math.Pow(dGx, 2) + Math.Pow(dGy, 2)));

            dSin = Math.Sin(dInc);
            dCos = Math.Cos(dInc);

            dBxp = dBx * Math.Cos(dHsd) - dBy * Math.Sin(dHsd);
            dByp = dBx * Math.Sin(dHsd) + dBy * Math.Cos(dHsd);

            dBen = dBe * Math.Cos(dDipe);
            dBev = dBe * Math.Sin(dDipe);

            if (dInc > 1.66 || dInc < 1.5)
            {
                dSeed = AtanC((dBxp + dBev * dSin) / dCos, -dByp);
            }
            else
            {
                dSeed = AtanC(dBxp * dCos + dBz * dSin, -dByp);
            }

            if (dSeed < -90 * Math.PI / 180)
            {
                dAzm1 = -135 * Math.PI / 180;
                dAzm2 = -45 * Math.PI / 180;
            }
            else if (dSeed < 0)
            {
                dAzm1 = -45 * Math.PI / 180;
                dAzm2 = -135 * Math.PI / 180;
            }
            else if (dSeed >= 90 * Math.PI / 180)
            {
                dAzm1 = 135 * Math.PI / 180;
                dAzm2 = 45 * Math.PI / 180;
            }
            else
            {
                dAzm1 = 45 * Math.PI / 180;
                dAzm2 = 135 * Math.PI / 180;
            }

            nCount = 1;
            bConverged = false;
            while (nCount <= 2 && !bConverged)
            {
                if (nCount == 1)
                {
                    dAzmLast = dAzm1;
                }
                else
                {
                    dAzmLast = dAzm2;
                }

                nIterations = 0;
                do
                {
                    dBzi = dBen * Math.Cos(dAzmLast) * dSin + dBev * dCos;
                    dDenom = dBxp * dCos + dBzi * dSin;
                    //dAzmOut = Atan2(-dByp, dDenom)
                    dAzmOut = AtanC(dDenom, -dByp);
                    deltapsi = dAzmOut - dAzmLast;
                    //dGradTop = dBen * Sin(dInc) * Sin(dInc) * Sin(dAzmLast) * Sin(dAzmOut) * Cos(dAzmOut)
                    if (dDenom == 0)
                    {
                        dAzmNext = dAzmOut;
                    }
                    else
                    {
                        //dGrad = (dGradTop / dDenom) - 1
                        dGrad = dBen * Math.Sin(dInc) * Math.Sin(dInc) * Math.Sin(dAzmLast) * Math.Sin(dAzmOut) * Math.Cos(dAzmOut) / dDenom - 1;
                        dAzmNext = dAzmLast - deltapsi / dGrad;
                    }

                    if (Math.Abs(dAzmNext - dAzmLast) > cConvLimit)
                    {
                        // bConverged = false;
                        dAzmLast = dAzmNext;
                        nIterations++;
                    }
                    else
                    {
                        bConverged = true;
                    }
                } while (!bConverged && nIterations < 20);

                if (!bConverged)
                {
                    nCount++;
                }
            }

            if (bConverged)
            {
                return Conversions.ModTwoPi(dAzmNext);
            }
            return 0;
        }

        public static double CalcBz(double dBe, double dDipe, double dInc, double dAzm)
        {
            var dBen = dBe * Math.Cos(dDipe);
            var dBev = dBe * Math.Sin(dDipe);
            var dBz = dBen * Math.Sin(dInc) * Math.Cos(dAzm) + dBev * Math.Cos(dInc);
            return dBz;
        }

        public static double CalcBxy(double dBx, double dBy)
        {
            return Math.Sqrt(Math.Pow(dBx, 2) + Math.Pow(dBy, 2));
        }

        public static double CalcHsd(double dGx, double dGy)
        {
            return Math.Atan2(dGy, -dGx);
        }

        public static double CalcBtotalBz(double dBx, double dBy, double dBzCalc)
        {
            return Math.Sqrt(dBx * dBx + dBy * dBy + dBzCalc * dBzCalc);
        }

        public static double CalcDipBz(double dBx, double dBy, double dGx, double dGy, double dGz, double dBzCalc, double dBtotalBzCalc)
        {
            var hsg = AtanC(-dGx, dGy);
            var inc = CalcInc(dGx, dGy, dGz);
            var bxp = dBx * Math.Cos(hsg) - dBy * Math.Sin(hsg);
            var byp = dBx * Math.Sin(hsg) + dBy * Math.Cos(hsg);
            var bx2p = bxp * Math.Cos(inc) + dBzCalc * Math.Sin(inc);
            var bvc = -bxp * Math.Sin(inc) + dBzCalc * Math.Cos(inc);
            var bnc = Math.Sqrt(dBtotalBzCalc * dBtotalBzCalc - bvc * bvc);
            return AtanC(bnc, bvc);
        }

        public static double CalcBtDip(double dBtotal, double dBtotalReferenced, double dDip, double dDipReferenced)
        {
            return Math.Sqrt(Math.Pow(dBtotalReferenced - dBtotal, 2) + Math.Pow((dDipReferenced - dDip) * dBtotal, 2));
        }

        public static double CalcBv(double dBtotal, double dDip)
        {
            return dBtotal * Math.Sin(dDip);
        }

        public static double CalcBh(double dBtotal, double dDip)
        {
            return dBtotal * Math.Cos(dDip);
        }

        public static PositionValues CalcPositionValues(double Inc, double RefInc, double Az, double RefAz, double MD, double RefMD)
        {
            double F;
            var inc = Inc - RefInc;
            var daz = Az - RefAz;
            var cosDL = Math.Cos(inc) - Math.Sin(Inc) * Math.Sin(RefInc) * (1 - Math.Cos(daz));
            var DL = Math.Acos(cosDL);
            F = DL > 0.001 ? Math.Tan(0.5 * DL) / DL : 0.5;

            var pos = new PositionValues();
            pos.NSDeparture = (MD - RefMD) * F * (Math.Sin(Inc) * Math.Cos(Az) + Math.Sin(RefInc) * Math.Cos(RefAz));
            pos.EWDeparture = (MD - RefMD) * F * (Math.Sin(Inc) * Math.Sin(Az) + Math.Sin(RefInc) * Math.Sin(RefAz));
            pos.TVD = (MD - RefMD) * F * (Math.Cos(Inc) + Math.Cos(RefInc));
            return pos;
        }

        public static double AtanC(double x, double y)
        {
            return Math.Atan2(y, x);
        }

        public static double ModTwoPi(double angleInradian)
        {
            while (angleInradian < 0)
            {
                angleInradian += 2 * Math.PI;
            }
            while (angleInradian >= 2 * Math.PI)
            {
                angleInradian -= 2 * Math.PI;
            }
            return angleInradian;
        }

        public static double GetTotalCorrection(MxSNorthReference northRef, double declination, double convergence)
        {
            double totalCorrection = 0;
            switch (northRef)
            {
                case MxSNorthReference.True:
                    totalCorrection = declination;
                    break;

                case MxSNorthReference.Grid:
                    totalCorrection = declination - convergence;
                    break;

                default:
                    totalCorrection = 0;
                    break;
            }
            return totalCorrection;
        }

        public static double AzmConvert(MxSNorthReference northRef, double dAzimuth, double dDeclination, double dConvergence)
        {
            return dAzimuth + GetTotalCorrection(northRef, dDeclination, dConvergence);
        }

        //Accord with method in vb for calculate Toolface while doing SA.
        //Reference modSupport.vb line 42
        private const double gcPi = 3.14159265358979;
        private const double gcPiOver2 = gcPi / 2;
        public static double CalculateHighSideToolface(double gy, double gx, double gz)
        {
            gy = -gy;
            if (gy == 0)
                return Conversions.RadiansToDiameter(gcPiOver2 * Math.Sign(gx));
            else if (gx == 0)
            {
                var numberSignY = Math.Sign(gy) < 0 ? 1 : 0;
                return Conversions.RadiansToDiameter((-gcPi) * (short)(numberSignY));
            }
            else
            {
                var numberY = gy < 0 ? 1 : 0;
                return Conversions.RadiansToDiameter(Math.Atan(gx / gy) - numberY * Math.Sign(gx) * gcPi);
            }
        }

        //Reference $/Sperry/INSITE/Development/LibSrc/FE/Processing/SperrySurveyCalcs/SperrySurvey.cs
        //Need to make sure is correct.
        public static double CalculateGravityToolface(double highSideToolface)
        {
            var gravityToolface = Limit180(highSideToolface);
            return gravityToolface;
        }

        public static double Limit180(double angle)
        {
            if (angle != double.MinValue && angle != double.MaxValue)
            {
                while (angle <= -180.0)
                {
                    angle += 360.0;
                }
                while (angle > 180.0)
                {
                    angle -= 360.0;
                }
            }
            return angle;
        }

        public static double Limit360(double angle)
        {
            if (angle != double.MinValue && angle != double.MaxValue)
            {
                while (angle < 0.0)
                {
                    angle += 360.0;
                }
                while (angle >= 360.0)
                {
                    angle -= 360.0;
                }
            }
            return angle;
        }

        public static double CalculationAve(double[] inputs)
        {
            if (inputs.Length <= 0)
                return 0;
            var sum = 0d;
            foreach (var item in inputs)
            {
                sum = sum + item;
            }
            return sum / inputs.Length;
        }

        public static double CalculationStd(double[] inputs)
        {
            if (inputs.Length <= 1)
                return 0;
            var sum1 = 0d;
            var sum2 = 0d;
            foreach (var item in inputs)
            {
                sum1 = sum1 + item;
                sum2 = sum2 + Math.Pow(item, 2);
            }
            return Math.Sqrt((sum2 - sum1 * sum1 / inputs.Length) / (inputs.Length - 1));
        }

        public static double CalculateTotalCorrection(double declination, double gridConvergence, MxSNorthReference referenceTo)
        {
            var totalCorrection = declination;
            switch (referenceTo)
            {
                case MxSNorthReference.Magnetic:
                    totalCorrection = 0;
                    break;
                case MxSNorthReference.Grid:
                    totalCorrection = declination - gridConvergence;
                    break;
                case MxSNorthReference.True:
                    totalCorrection = declination;
                    break;
                default:
                    break;
            }
            return totalCorrection;
        }

        public static void CalculateTotalCorrectionForWaypoint(ref Waypoint waypoint, MxSNorthReference MxSNorthReference)
        {
            waypoint.BGGMTotalCorrection = CalculateTotalCorrection(waypoint.BGGMDeclination, waypoint.GridConvergence, MxSNorthReference);
            waypoint.IFRTotalCorrection = CalculateTotalCorrection(waypoint.IFRDeclination, waypoint.GridConvergence, MxSNorthReference);
        }
    }
}
