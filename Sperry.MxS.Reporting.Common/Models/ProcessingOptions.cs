using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MxSProcessingOptions : IMxSProcessingOptions
    {
        public MxSProcessingOptions()
        { }

        public MxSProcessingOptions(IMxSProcessingOptions processingOptions)
        {
            IsProcessMSA = processingOptions.IsProcessMSA;
            IsProcessPositionOnly = processingOptions.IsProcessPositionOnly;
            IsProcessUncertainityOnly = processingOptions.IsProcessUncertainityOnly;
            RunASA = processingOptions.RunASA;
            UseTieIn = processingOptions.UseTieIn;
            ProcessingCaller = processingOptions.ProcessingCaller;
        }

        [JsonProperty]
        public bool IsProcessMSA { get; set; }

        [JsonProperty]
        public bool IsProcessPositionOnly { get; set; } = false;

        [JsonProperty]
        public bool IsProcessUncertainityOnly { get; set; } = false;

        [JsonProperty]
        public bool RunASA { get; set; } = true;

        [JsonProperty]
        public bool UseTieIn { get; set; } = false;

        [JsonProperty]
        public MxSSurveyProcessCaller ProcessingCaller { get; set; }
    }
}
