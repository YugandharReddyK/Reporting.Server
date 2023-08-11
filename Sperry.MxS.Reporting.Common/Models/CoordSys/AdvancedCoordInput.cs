using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CoordSys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AdvancedCoordInput
    {
        public AdvancedCoordInput()
        {
            CoordSys = null;
            OldCoordSys = null;
            MagModel = null;
            Date = new DateTime();
            Tvd = 0;
            Lat = 0;
            Lng = 0;
        }

        public AdvancedCoordInput(string coordSys, double latitude, double longitude)
        {
            CoordSys = coordSys;
            OldCoordSys = null;
            MagModel = null;
            Date = new DateTime();
            Tvd = 0;
            Lat = latitude;
            Lng = longitude;
        }

        public AdvancedCoordInput(string coordSys, string oldCoordSys, double latitude, double longitude)
        {
            CoordSys = coordSys;
            OldCoordSys = oldCoordSys;
            MagModel = null;
            Date = new DateTime();
            Tvd = 0;
            Lat = latitude;
            Lng = longitude;
        }

        public AdvancedCoordInput(string coordSys, string magModel, DateTime date, double tvd, double latitude, double longitude)
        {
            CoordSys = coordSys;
            OldCoordSys = null;
            MagModel = magModel;
            Date = date;
            Tvd = tvd;
            Lat = latitude;
            Lng = longitude;
        }

        public AdvancedCoordInput(string coordSys, string oldCoordSys, string magModel, DateTime date, double tvd, double latitude, double longitude)
        {
            CoordSys = coordSys;
            OldCoordSys = oldCoordSys;
            MagModel = magModel;
            Date = date;
            Tvd = tvd;
            Lat = latitude;
            Lng = longitude;
        }

        public AdvancedCoordInput(string coordSys, string magModel, DateTime date, double tvd, double latitude, double longitude, double north, double east)
        {
            CoordSys = coordSys;
            OldCoordSys = null;
            MagModel = magModel;
            Date = date;
            Tvd = tvd;
            Lat = latitude;
            Lng = longitude;
            North = north;
            East = east;
        }

        [JsonProperty]
        public string CoordSys { get; set; }

        [JsonProperty]
        public DateTime Date { get; set; }

        [JsonProperty]
        public double East { get; set; }

        //Y; //Lat;  //Northing
        [JsonProperty]
        public double Lat { get; set; }

        //X; //Lng;  //Easting
        [JsonProperty]
        public double Lng { get; set; }

        //public string CoordSysPath;

        [JsonProperty]
        public string MagModel { get; set; }

        [JsonProperty]
        public double North { get; set; }

        [JsonProperty]
        public string OldCoordSys { get; set; }

        //Z; //Tvd;  //Tvd
        [JsonProperty]
        public double Tvd { get; set; }
    }
}
