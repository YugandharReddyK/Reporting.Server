using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSProcessingOptions
    {
        bool IsProcessMSA { get; set; }

        bool IsProcessPositionOnly { get; set; }

        bool IsProcessUncertainityOnly { get; set; }

        bool RunASA { get; set; }

        bool UseTieIn { get; set; }

        MxSSurveyProcessCaller ProcessingCaller { get; set; }
    }
}
