using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Icarus.Base
{
    public abstract class IcarusDataBase : WorkBenchDataModel
    {
        public double? Gx { get; set; }

        public double? Gy { get; set; }

        public double? Gz { get; set; }
    }
}
