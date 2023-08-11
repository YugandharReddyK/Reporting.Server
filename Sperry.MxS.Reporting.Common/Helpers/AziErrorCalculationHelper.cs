using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public class AziErrorCalculationHelper
    {
        public const double Radians = Math.PI / 180.0;
        public static double CalculateATanC(double den, double num)
        {
            if (den == 0.0)
            {
                if (num > 0.0)
                {
                    return 0.5 * Math.PI;
                }
                if (num < 0.0)
                {
                    return -1 * 0.5 * Math.PI;
                }
                return 0.0;
            }
            return Math.Atan2(num, den);
        }

        public static double AdjustAngle(double angle)
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

        public static double CalculateInclination(double Gx, double Gy, double Gz)
        {
            return CalculateATanC(Gz, Math.Sqrt(Gx * Gx + Gy * Gy)) * 180.0 / Math.PI;
        }

        public static double CalculateBxp(double Gx, double Gy, double Bx, double By)
        {
            var hsg = CalculateATanC(-1 * Gx, Gy);
            return Bx * Math.Cos(hsg) - By * Math.Sin(hsg);
        }

        public static double CalculateByp(double Gx, double Gy, double Bx, double By)
        {
            var hsg = CalculateATanC(-1 * Gx, Gy);
            return Bx * Math.Sin(hsg) + By * Math.Cos(hsg);
        }

        public static double CalculateBtc(double Bx, double By, double Bz)
        {
            return Math.Sqrt(Bx * Bx + By * By + Bz * Bz);
        }

        public static double CalculateBvc(double Gx, double Gy, double Gz, double Bx, double By, double Bz)
        {
            var incRad = CalculateInclination(Gx, Gy, Gz) * Math.PI / 180;
            return -1 * CalculateBxp(Gx, Gy, Bx, By) * Math.Sin(incRad) + Bz * Math.Cos(incRad);
        }

        public static double CalculateBnc(double Gx, double Gy, double Gz, double Bx, double By, double Bz)
        {
            return Math.Sqrt(Math.Pow(CalculateBtc(Bx, By, Bz), 2) - Math.Pow(CalculateBvc(Gx, Gy, Gz, Bx, By, Bz), 2));
        }

        public static double CalculateDipc(double Gx, double Gy, double Gz, double Bx, double By, double Bz)
        {
            var num = CalculateBvc(Gx, Gy, Gz, Bx, By, Bz);
            var den = CalculateBnc(Gx, Gy, Gz, Bx, By, Bz);
            return CalculateATanC(den, num) * 180.0 / Math.PI;
        }

        public static double CalculateAzimuthLongCollar(double Gx, double Gy, double Gz, double Bx, double By, double Bz)
        {
            var hsg = CalculateATanC(-1 * Gx, Gy);
            var inc = CalculateATanC(Gz, Math.Sqrt(Gx * Gx + Gy * Gy));
            var bxp = Bx * Math.Cos(hsg) - By * Math.Sin(hsg);
            var byp = Bx * Math.Sin(hsg) + By * Math.Cos(hsg);
            var bx2p = bxp * Math.Cos(inc) + Bz * Math.Sin(inc);
            var AzLC = CalculateATanC(bx2p, -byp) * 180.0 / Math.PI;
            while (AzLC < 0)
            {
                AzLC += 360.0;
            }
            return AzLC;
        }

        public static void CalculateAzimuthShortCollar(double Gx, double Gy, double Gz, double Bx, double By, double Bz, double Be, double diped, ref double scaz, ref double bzc)
        {
            var phi = CalculateATanC(-1 * Gx, Gy);
            var theta = CalculateATanC(Gz, Math.Sqrt(Gx * Gx + Gy * Gy));
            var bxp = Bx * Math.Cos(phi) - By * Math.Sin(phi);
            var byp = Bx * Math.Sin(phi) + By * Math.Cos(phi);
            var dipe = diped * Radians;
            var ben = Be * Math.Cos(dipe);
            var bev = Be * Math.Sin(dipe);
            var st = Math.Sin(theta);
            var ct = Math.Cos(theta);

            var azseed = 0.0;
            azseed = GetAzSeedValue(Bz, theta, bxp, byp, bev, st, ct, azseed);


            double psin = 0.0, psin2 = 0.0, psin3 = 0.0;
            GetPSinValues(Radians, azseed, ref psin, ref psin2, ref psin3);

            var start = 1;
            var noit = 0;
            var converge = false;
            var psiprev = 0.0;
            var psinew = 0.0;

            while (start <= 3 && !converge)
            {
                switch (start)
                {
                    case 1:
                        psiprev = psin;
                        break;
                    case 2:
                        psiprev = psin2;
                        break;
                    case 3:
                        psiprev = psin3;
                        break;
                }
                noit = 0;

                var denom = 0.0;
                var psout = 0.0;
                var delp = 0.0;
                var grad = 0.0;
                var limit = 0.000001;
                do
                {
                    bzc = ben * Math.Cos(psiprev) * st + bev * ct;
                    denom = bxp * ct + bzc * st;
                    psout = CalculateATanC(denom, -byp);
                    delp = psout - psiprev;
                    if (denom == 0.0)
                    {
                        psinew = psout;
                    }
                    else
                    {
                        grad = ben * st * st * Math.Sin(psiprev) * Math.Sin(psout) * Math.Cos(psout) / denom - 1.0;
                        psinew = psiprev - delp / grad;
                    }

                    if (Math.Abs(psinew - psiprev) > limit)
                    {
                        converge = false;
                        psiprev = psinew;
                        noit = noit + 1;
                    }
                    else
                    {
                        converge = true;
                    }
                } while (!converge && noit < 20);

                if (!converge)
                {
                    start = start + 1;
                }
            }

            if (converge)
            {
                scaz = psinew * 180.0 / Math.PI;
            }

            while (scaz < 0.0)
            {
                scaz += 360.0;
            }
        }

        private static double GetAzSeedValue(double Bz, double theta, double bxp, double byp, double bev, double st, double ct, double azseed)
        {
            if (theta > 1.66 || theta < 1.50)
            {
                azseed = CalculateATanC((bxp + bev * st) / ct, -1 * byp);
            }
            else
            {
                azseed = CalculateATanC(bxp * ct + Bz * st, -byp);
            }
            return azseed;
        }

        private static void GetPSinValues(double rad, double azseed, ref double psin, ref double psin2, ref double psin3)
        {
            if (azseed < -90 * rad)
            {
                psin = -135 * rad;
                psin2 = -45 * rad;
                psin3 = -90 * rad;
            }
            else if (azseed < 0 * rad)
            {
                psin = -45 * rad;
                psin2 = -135 * rad;
                psin3 = -90 * rad;
            }
            else if (azseed >= 90 * rad)
            {
                psin = 135 * rad;
                psin2 = 45 * rad;
                psin3 = 90 * rad;
            }
            else
            {
                psin = 45 * rad;
                psin2 = 135 * rad;
                psin3 = 90 * rad;
            }
        }
    }
}
