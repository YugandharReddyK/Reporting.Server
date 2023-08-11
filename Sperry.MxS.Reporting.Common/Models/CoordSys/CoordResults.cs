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
    public class CoordResults
    {
        //public Dictionary<string, string> ParsedInformation { get; set; }
        [JsonProperty]
        public int Type { get; set; }

        [JsonProperty]
        public int SystemId { get; set; }

        [JsonProperty]
        public int LocationId { get; set; }

        [JsonProperty]
        public int ZoneId { get; set; }

        [JsonProperty]
        public int UTMHemisphere { get; set; }

        [JsonProperty]
        public double MslTvd { get; set; }

        [JsonProperty]
        public double MapLat { get; set; }

        [JsonProperty]
        public double MapDep { get; set; }

        [JsonProperty]
        public double WgsLat { get; set; }

        [JsonProperty]
        public double WgsLng { get; set; }

        [JsonProperty]
        public double GeoLat { get; set; }

        [JsonProperty]
        public double GeoLng { get; set; }

        // Calculated results
        [JsonProperty]
        public string GridSource { get; set; }

        [JsonProperty]
        public string GridEllipsoidName { get; set; }

        [JsonProperty]
        public string GridDescription { get; set; }

        [JsonProperty]
        public string GridDatumName { get; set; }

        [JsonProperty]
        public string GridProjectionName { get; set; }

        [JsonProperty]
        public double GridConvergence { get; set; }

        [JsonProperty]
        public double GridScaleFactor { get; set; }

        [JsonProperty]
        public double MagDeclination { get; set; }

        [JsonProperty]
        public double MagDip { get; set; }

        [JsonProperty]
        public double MagFieldStrength { get; set; }

        [JsonProperty]
        public double MagHorizontalComponent { get; set; }

        [JsonProperty]
        public double MagVerticalComponent { get; set; }

        [JsonProperty]
        public double MagNorthComponent { get; set; }

        [JsonProperty]
        public double MagEastComponent { get; set; }

        [JsonProperty]
        public double CentralMeridian { get; set; }

        [JsonProperty]
        public double LatitudeOrigin { get; set; }

        [JsonProperty]
        public double LongitudeOrigin { get; set; }

        [JsonProperty]
        public double FalseNorthing { get; set; }

        [JsonProperty]
        public double FalseEasting { get; set; }

        [JsonProperty]
        public double ScaleReduction { get; set; }

        [JsonProperty]
        public double EquatorialRadius { get; set; }

        [JsonProperty]
        public double PolarRadius { get; set; }

        [JsonProperty]
        public double InverseFlattening { get; set; }

        [JsonProperty]
        public double EccentricitySquared { get; set; }

        [JsonProperty]
        public int MagChecksumInsite5 { get; set; }

        [JsonProperty]
        public int MagChecksumInsite6 { get; set; }

        [JsonProperty]
        public short Hemisphere { get; set; }

    }
}
