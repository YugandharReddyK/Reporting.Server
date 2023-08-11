namespace Sperry.MxS.Core.Common.Models
{ 
    public static class ObservatoryReadingExtension
    {
        public static void SetObservatoryStation(this ObservatoryReading observatoryReading, ObservatoryStation obsStation)
        {
            observatoryReading.ObservatoryStation= obsStation;
            observatoryReading.ObservatoryStationId = obsStation.Id; 

        }
    }
}
