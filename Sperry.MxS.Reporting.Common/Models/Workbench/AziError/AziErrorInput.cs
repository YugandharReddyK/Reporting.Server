using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.AziError
{
    [Serializable]
    public class AziErrorInput
    {
        public AziErrorInput()
        { }

        public AziErrorInput(CorrectedSurvey correctedSurvey)
        {
            DateTime = correctedSurvey.DateTime;
            Depth = correctedSurvey.Depth;
            AziErrorAzimuth = correctedSurvey.AziErrorAzimuth;
            AziErrorInclination = correctedSurvey.AziErrorInclination;
            AziErrorDipe = correctedSurvey.AziErrorDipe;
            AziErrorBe = correctedSurvey.AziErrorBe;
            AziErrorAzimuthMagnetic = correctedSurvey.AziErrorAzimuthMagnetic;
            AziErrordDecle = correctedSurvey.AziErrordDecle;
        }

        public DateTime DateTime { get; set; }

        public double? Depth { get; set; }

        public double? AziErrorAzimuth { get; set; }

        public double? AziErrorInclination { get; set; }

        public double AziErrorDipe { get; set; }

        public double AziErrorBe { get; set; }

        public double AziErrorAzimuthMagnetic { get; set; }

        public double AziErrordDecle { get; set; }
    }
}
