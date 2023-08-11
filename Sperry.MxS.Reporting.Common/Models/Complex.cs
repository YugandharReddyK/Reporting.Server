using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class Complex
    {
        public double Real { get; set; }

        public double Imaginary { get; set; }

        public Complex() : this(0, 0) { }

        public Complex(double real) : this(real, 0) { }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public override string ToString()
        {
            if (Imaginary > 0)
                return string.Format("{0} + {1}i", Real, Imaginary);
            else if (Imaginary == 0)
                return string.Format("{0}", Real);
            else
                return string.Format("{0} {1}i", Real, Imaginary);
        }

        // Commented by sandeep Kiran sir tell to remove

        //public static Complex operator +(Complex c1, Complex c2)
        //{
        //    return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        //}

        //public static Complex operator -(Complex c1, Complex c2)
        //{
        //    return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        //}

        //public static Complex operator *(Complex c1, Complex c2)
        //{
        //    return new Complex(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
        //                       c1.Imaginary * c2.Real + c1.Real * c2.Imaginary);
        //}

        //public static Complex operator *(Complex c, double multipiler)
        //{
        //    return new Complex(c.Real * multipiler, c.Imaginary * multipiler);
        //}

        //public static Complex operator /(Complex c, double divider)
        //{
        //    return new Complex(c.Real / divider, c.Imaginary / divider);
        //}
    }
}
