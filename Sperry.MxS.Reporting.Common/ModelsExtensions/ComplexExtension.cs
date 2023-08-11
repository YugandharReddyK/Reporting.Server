using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models
{
    public static class ComplexExtension
    {
        public static double ToModulus(this Complex complex)
        {
            return Math.Sqrt(complex.Real * complex.Real + complex.Imaginary * complex.Imaginary);
        }

        public static double[] ToArray(this Complex[] complexes)
        {
            var outputs = new List<double>();
            foreach (var complex in complexes)
            {
                outputs.Add(complex.Real);
                outputs.Add(complex.Imaginary);
            }
            return outputs.ToArray();
        }

        public static Complex[] FromArray(this double[] inputs, bool justRealPart = true)
        {
            var outputs = new List<Complex>();
            if (justRealPart)
            {
                outputs = (from item in inputs select new Complex(item)).ToList();
            }
            else
            {
                if (inputs.Length % 2 != 0)
                    throw new ArgumentException("The number of inputs must be divided by 2!");

                var half = inputs.Length / 2;
                for (var i = 0; i < half; i++)
                {
                    outputs.Add(new Complex(inputs[2 * i], inputs[2 * i + 1]));
                }
            }
            return outputs.ToArray();
        }

        public static double[] GetReals(Complex[] complexes)
        {
            return (from complex in complexes select complex.Real).ToArray();
        }

        public static double[] GetImaginaries(Complex[] complexes)
        {
            return (from complex in complexes select complex.Imaginary).ToArray();
        }
    }
}
