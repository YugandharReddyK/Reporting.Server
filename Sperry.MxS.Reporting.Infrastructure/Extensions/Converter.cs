using Sperry.MxS.Reporting.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Infrastructure.Extensions
{
    public class Converter<T> : IMxSConverter<T>
    {
        public Converter(T value)
        {
            Value = value;
        }
       
        public T Value { get; private set; }
    }
}
