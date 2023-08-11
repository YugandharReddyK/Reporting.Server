using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSInsiteSurvey
    {
        DateTime DateTime { get; set; }

        double Depth { get; set; }

        bool Enabled { get; set; }

        double? Gx { get; set; }

        double? Gy { get; set; }

        double? Gz { get; set; }
        
        double? Bx { get; set; }
        
        double? By { get; set; }
        
        double? Bz { get; set; }
    }
}
