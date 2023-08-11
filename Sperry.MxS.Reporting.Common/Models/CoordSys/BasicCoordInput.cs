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
    public class BasicCoordInput
    {
        public BasicCoordInput()
        {
            CoordSys = null;
            Lat = 0;
            Lng = 0;
        }

        public BasicCoordInput(string coordSys)
        {
            CoordSys = coordSys;
            Lat = 0;
            Lng = 0;
        }

        public BasicCoordInput(string coordSys, double latitude, double longitude)
        {
            CoordSys = coordSys;
            Lat = latitude;
            Lng = longitude;
        }

        [JsonProperty]
        public string CoordSys { get; set; }

        [JsonProperty]
        public double Lat { get; set; }

        [JsonProperty] 
        public double Lng { get;set; }

    }
}
