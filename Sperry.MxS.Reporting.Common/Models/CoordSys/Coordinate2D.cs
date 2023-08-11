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
    public class Coordinate2D
    {
        public Coordinate2D()
        {
            Lng = 0.0;
            Lat = 0.0;
        }

        public Coordinate2D(double longitude, double latitude)
        {
            Lng = longitude;
            Lat = latitude;
        }

        [JsonProperty]
        public double Lat { get; set; }

        [JsonProperty] 
        public double Lng { get;set; }

    }
}
