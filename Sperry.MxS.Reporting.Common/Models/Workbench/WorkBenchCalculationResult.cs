using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.Workbench.Icarus;
using Sperry.MxS.Core.Common.Models.Workbench.AziError;
using Sperry.MxS.Core.Common.Models.Workbench.Cazandra;
using Sperry.MxS.Core.Common.Models.Workbench.Icarus;
using System.Collections.Generic;
using System;

namespace Sperry.MxS.Core.Common.Models.Workbench
{
    [Serializable]
  //  [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class WorkBenchCalculationResult
    {
        public IList<CazandraInputData> CazandraInputData { get; set; }

        public IList<CazandraCentreData> CazandraCentreData { get; set; }

        public IList<CazandraDiyData> CazandraDiyData { get; set; }

        public IList<CazandraRawData> CazandraRawData { get; set; }

        public IList<CazandraSfeData> CazandraSfeData { get; set; }

        public IList<CazandraTfcData> CazandraTfcData { get; set; }

        public IList<CazandraTxyData> CazandraTxyData { get; set; }

        public int CazandraDiyFlag { get; set; }

        public CazandraDiyAveData CazandraDiyAveData { get; set; }

        public CazandraDiyStdData CazandraDiyStdData { get; set; }

        public int CazandraRawFlag { get; set; }

        public CazandraRawAveData CazandraRawAveData { get; set; }

        public CazandraRawStdData CazandraRawStdData { get; set; }

        public int CazandraSfeFlag { get; set; }

        public CazandraSfeAveData CazandraSfeAveData { get; set; }

        public CazandraSfeStdData CazandraSfeStdData { get; set; }

        public CazandraSfeAvexData CazandraSfeAvexData { get; set; }

        public CazandraSfeStdxData CazandraSfeStdxData { get; set; }

        public int CazandraTfcFlag { get; set; }

        public CazandraTfcAveData CazandraTfcAveData { get; set; }

        public CazandraTfcStdData CazandraTfcStdData { get; set; }

        public CazandraTfcAvexData CazandraTfcAvexData { get; set; }

        public CazandraTfcStdxData CazandraTfcStdxData { get; set; }

        public int CazandraTxyFlag { get; set; }

        public CazandraTxyAveData CazandraTxyAveData { get; set; }

        public CazandraTxyStdData CazandraTxyStdData { get; set; }

        public CazandraTxyAvexData CazandraTxyAvexData { get; set; }

        public CazandraTxyStdxData CazandraTxyStdxData { get; set; }

        public IList<IcarusCentreData> IcarusCentreData { get; set; }

        public IList<IcarusDiyData> IcarusDiyData { get; set; }

        public IList<IcarusRawData> IcarusRawData { get; set; }

        public IList<IcarusSfeData> IcarusSfeData { get; set; }

        public IList<IcarusTfcData> IcarusTfcData { get; set; }

        public IList<IcarusTxyData> IcarusTxyData { get; set; }

        public int IcarusDiyFlag { get; set; }

        public IcarusDiyAveData IcarusDiyAveData { get; set; }

        public IcarusDiyStdData IcarusDiyStdData { get; set; }

        public int IcarusRawFlag { get; set; }

        public IcarusRawAveData IcarusRawAveData { get; set; }

        public IcarusRawStdData IcarusRawStdData { get; set; }

        public int IcarusSfeFlag { get; set; }

        public IcarusSfeAveData IcarusSfeAveData { get; set; }

        public IcarusSfeStdData IcarusSfeStdData { get; set; }

        public IcarusSfeAvexData IcarusSfeAvexData { get; set; }

        public IcarusSfeStdxData IcarusSfeStdxData { get; set; }

        public int IcarusTfcFlag { get; set; }

        public IcarusTfcAveData IcarusTfcAveData { get; set; }

        public IcarusTfcStdData IcarusTfcStdData { get; set; }

        public IcarusTfcAvexData IcarusTfcAvexData { get; set; }

        public IcarusTfcStdxData IcarusTfcStdxData { get; set; }

        public int IcarusTxyFlag { get; set; }

        public IcarusTxyAveData IcarusTxyAveData { get; set; }

        public IcarusTxyStdData IcarusTxyStdData { get; set; }

        public IcarusTxyAvexData IcarusTxyAvexData { get; set; }

        public IcarusTxyStdxData IcarusTxyStdxData { get; set; }

        public MxSIcarusSolution IcarusSolution { get; set; }

        public IList<AziErrorLCData> AziErrorLCData { get; set; }
        
        public IList<AziErrorSCData> AziErrorSCData { get; set; }
        
        public IList<AziErrorPositionData> AziErrorPositionData { get; set; }
        
        public IList<AziErrorLCPositionData> AziErrorLCPositionData { get; set; }
        
        public IList<AziErrorSCPositionData> AziErrorSCPositionData { get; set; }

        public string ErrorMessage { get; set; }
    }

}
