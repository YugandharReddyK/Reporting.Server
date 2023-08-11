using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ObservatoryReading :DataModelBase //INotifyPropertyChanged
    {
        public ObservatoryReading() 
        { }

        public ObservatoryReading(ObservatoryReading obsReading)
        {
            BTotal = obsReading.BTotal;
            DateTime = obsReading.DateTime;
            Declination = obsReading.Declination;
            Dip = obsReading.Dip;
            BgsWebSiteName = obsReading.BgsWebSiteName;
        }

        [JsonProperty]
        public double BTotal { get; set; }

        [JsonProperty]
        public DateTime DateTime { get; set; }

        [JsonProperty]
        public double Declination { get; set; }

        [JsonProperty]
        public double Dip { get; set; }

        [JsonProperty]
        [NotMapped]
        public string BgsWebSiteName { get; set; }

        [JsonProperty]
        [NotMapped]
        public ObservatoryStation ObservatoryStation { get; set; }

        [JsonProperty]
        public Guid ObservatoryStationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string Source { get { return MxSObservatoryDataSource.BGS.ToString(); } }

        public void SetObservatoryStation(ObservatoryStation obsStation)
        {
            ObservatoryStation = obsStation;
            ObservatoryStationId = obsStation.Id;
        }
    }
}
