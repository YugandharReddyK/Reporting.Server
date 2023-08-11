using Sperry.MxS.Core.Common.Models.ASA;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models
{
    public static class ObservatoryStationExtension
    {
        public static void AddNewObsReadings(this ObservatoryStation observatoryStation, IList<ObservatoryReading> obsReadings)
        {
            foreach (var observatoryReading in obsReadings)
            {
                if (observatoryReading.DateTime > observatoryStation.LastUpdateTime)
                {
                    var obsReading = new ObservatoryReading(observatoryReading);
                    obsReading.SetObservatoryStation(observatoryStation);
                    observatoryStation.Data.Add(obsReading);
                }
            }
        }

        public static ObservatoryStation UpdateFrom(ObservatoryStation observatoryStation)
        {
            observatoryStation.Date = observatoryStation.Date;
            observatoryStation.Description = observatoryStation.Description;
            observatoryStation.Latitude = observatoryStation.Latitude;
            observatoryStation.Longitude = observatoryStation.Longitude;
            observatoryStation.Name = observatoryStation.Name;
            observatoryStation.OffsetTime = observatoryStation.OffsetTime;
            observatoryStation.Type = observatoryStation.Type;
            observatoryStation.AddNewObsReadings(observatoryStation.Data);
            observatoryStation.LastUpdateTime = observatoryStation.LastUpdateTime;
            // SetState(State.Modified);//To do
            return observatoryStation;
        }
        public static void AddForeignKey(this ObservatoryStation observatoryStation, Solution solution)
        {
            observatoryStation.Id = solution.Id;
        }
    }
}
