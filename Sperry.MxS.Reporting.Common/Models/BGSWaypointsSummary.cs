using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class BGSWaypointsSummary : DataModelBase
    {
        [JsonProperty]
        public string Field { get; set; }

        [JsonProperty]
        public string PadRigInstallation { get; set; }

        [JsonProperty]
        public string Well { get; set; }

        //metres,kilometres,feet
        [JsonProperty]
        public string Depths { get; set; }
       
        //E.g. MSL, RKB, RT
        [JsonProperty]
        public string TvdReferenceTo { get; set; }

        [JsonProperty]
        public string ReferenceToMsl { get; set; }

        //E.g. WGS84, ED50
        [JsonProperty]
        public string Datum { get; set; }

        //decdegree or degminsec
        [JsonProperty]
        public string Position { get; set; }

        //format dd mm yyyy
        [JsonProperty]
        public string Date { get; set; }

        [JsonProperty]
        public string MagneticFieldModel { get; set; }

        [JsonProperty]
        public string FileName { get; set; }

        [JsonProperty]
        public double RefLatitude { get; set; }

        [JsonProperty]
        public double RefLongitude { get; set; }

        [JsonProperty]
        public virtual List<BGSWaypoint> Waypoints { get; set; }

    }
}
