using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Infrastructure.Interface
{
    public interface IMxSConverter<T>
    {
        T Value { get; }
    }
}
