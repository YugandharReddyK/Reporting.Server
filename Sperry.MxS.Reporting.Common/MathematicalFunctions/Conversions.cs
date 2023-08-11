using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.MathematicalFunctions
{
    public class Conversions
    {

        #region From TT3 mod_Public
        public static double MetersToFeet(double meters)
        {
            return meters / 0.3048;
        }

        public static double FeetToMeters(double feet)
        {
            return feet * 0.3048;
        }

        public static double FahrenheitToCelcius(double degreesFahrenheit)
        {
            return (degreesFahrenheit - 32) * 5 / 9;
        }

        public static double CelciusToFahrenheit(double degreesCelcius)
        {
            return degreesCelcius * 9 / 5 + 32;
        }

        //180/Pi
        public static double RadiansToDiameter(double radians)
        {
            return radians * 180 / Math.PI;
        }

        //pi/180
        public static double DiameterToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public static double ModTwoPi(double dValue)
        {
            while (dValue < 0)
            {
                dValue += 2 * Math.PI;
            }
            while (dValue >= 2 * Math.PI)
            {
                dValue -= 2 * Math.PI;
            }
            return dValue;
        }
        #endregion

    }
}
