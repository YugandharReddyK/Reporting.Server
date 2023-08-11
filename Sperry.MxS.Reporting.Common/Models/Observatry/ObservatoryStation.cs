using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ObservatoryStation : DataModelBase
    {
        public ObservatoryStation()
        {
            ReadingsToUpdate = new List<ObservatoryReading>();
            Data = new List<ObservatoryReading>();
            Name = string.Empty;
        }

        // ToDo - Added by Naveen Kumar
        [NotMapped]
        [JsonIgnore]
        public IList<ObservatoryReading> ReadingsToUpdate { get; set; }

        [NotMapped]
        [JsonProperty]
        public IList<ObservatoryReading> Data { get; set; }

        [JsonProperty]
        public string Date { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public DateTime LastUpdateTime { get; set; }

        [JsonProperty]
        public double Latitude { get; set; }

        [JsonProperty]
        public double Longitude { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public long OffsetTime { get; set; }

        [JsonProperty]
        public MxSObservatoryType Type { get; set; }

        public ObservatoryStation UpdateFrom(ObservatoryStation observatoryStation)
        {
            Date = observatoryStation.Date;
            Description = observatoryStation.Description;
            Latitude = observatoryStation.Latitude;
            Longitude = observatoryStation.Longitude;
            Name = observatoryStation.Name;
            OffsetTime = observatoryStation.OffsetTime;
            Type = observatoryStation.Type;
            AddNewObsReadings(observatoryStation.Data);
            LastUpdateTime = observatoryStation.LastUpdateTime;
            return this;
        }
        private void AddNewObsReadings(IList<ObservatoryReading> obsReadings)
        {
            foreach (var observatoryReading in obsReadings)
            {
                if (observatoryReading.DateTime > LastUpdateTime)
                {
                    var obsReading = new ObservatoryReading(observatoryReading);
                    obsReading.SetObservatoryStation(this);
                    Data.Add(obsReading);
                }
            }
        }
    }
}
