using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class DoubleExtensions
    {
        private const double AccumulatedErrorFactor = 0.005;

        private const double DoubleFactor = 5E-07;

        
        public static double Limit360(this double value)
        {
            double num = value % 360.0;
            if (num < 0.0)
            {
                num += 360.0;
            }

            return num;
        }

        public static double? Limit360(this double? value)
        {
            if (value.HasValue)
            {
                return value.Value.Limit360();
            }

            return value;
        }

        public static bool IsNaN(this double? value)
        {
            if (value.HasValue)
            {
                return double.IsNaN(value.Value);
            }

            return false;
        }

        public static bool CompareDouble(this double inputA, double inputB, double degree = DoubleFactor)
        {
            return Math.Abs(inputA - inputB) < degree;
        }

        public static bool CompareDouble(this double? inputA, double? inputB, double degree = DoubleFactor)
        {
            if (!inputA.HasValue && !inputB.HasValue)
            {
                return true;
            }

            if (!inputA.HasValue || !inputB.HasValue)
            {
                return false;
            }

            return inputA.Value.CompareDouble(inputB.Value, degree);
        }

        public static bool CompareDoubleByFormat(this double? inputA, double? inputB, MxSFormatType formatType, MxSUnitSystemEnum unitSystem)
        {
            if (!inputA.HasValue && !inputB.HasValue)
            {
                return true;
            }

            if (!inputA.HasValue || !inputB.HasValue)
            {
                return false;
            }

            return inputA.Value.CompareDoubleByFormat(inputB.Value, formatType, unitSystem);
        }

        public static bool CompareDoubleByFormat(this double inputA, double inputB, MxSFormatType formatType, MxSUnitSystemEnum unitSystem)
        {
            Format precision = FormatHelper.GetPrecision(formatType, unitSystem);
            if (precision == null)
            {
                return inputA.CompareDouble(inputB);
            }

            return FormatHelper.GetRoundValueByPrecision(inputA, precision.Precision).CompareDouble(FormatHelper.GetRoundValueByPrecision(inputB, precision.Precision));
        }

        public static double GetValueOrNaN(this double? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }

            return double.NaN;
        }

        public static double Limit180(this double value)
        {
            return value.Limit360() - 360.0;
        }

        public static double? GetValueOrNull(this double value)
        {
            if (double.IsNaN(value))
            {
                return null;
            }

            return value;
        }

        public static bool EqualWithZero(this double one)
        {
            return one.CompareDouble(0.0);
        }

        public static bool ExistNotNullAndNotZero(this IEnumerable<double?> doubles)
        {
            return doubles.Any((double? item) => item.HasValue && !item.Value.EqualWithZero());
        }

        public static bool CompareDoubleWithAccumulatedError(this double inputA, double inputB)
        {
            return inputA.CompareDouble(inputB, AccumulatedErrorFactor);
        }

        public static bool CompareDoubleWithAccumulatedError(this double? inputA, double? inputB)
        {
            return inputA.CompareDouble(inputB, AccumulatedErrorFactor);
        }
    }

}
