using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class BGSWaypoint : DataModelBase
    {
        private string _name;

        [JsonProperty]
        public BGSWaypointsSummary Summary { get; set; }

        [JsonProperty]
        public Guid Summary_Id { get; set; }
      
        [JsonProperty]
        public string Name
        {
            get { return _name; }
            set
            {
                if (!value.Equals(_name, StringComparison.InvariantCultureIgnoreCase))
                {
                    _name = string.Intern(value);
                }
            }
        }

        [JsonProperty]
        public double StartDepth { get; set; }

        [JsonProperty]
        public double EndDepth { get; set; }

        [JsonProperty]
        public double Elevation { get; set; }

        [JsonProperty]
        public double MeasuredDepth { get; set; }

        [JsonProperty]
        public double VerticalDepth { get; set; }

        [JsonProperty]
        public double Latitude { get; set; }

        [JsonProperty]
        public double Longitude { get; set; }

        [JsonProperty]
        public double Gravity { get; set; }

        [JsonProperty]
        public DateTime CalculatedDate { get; set; }

        [JsonProperty]
        public string MagneticModel { get; set; }

        [JsonProperty]
        public double BGGMDeclination { get; set; }

        [JsonProperty]
        public double BGGMDip { get; set; }

        [JsonProperty]
        public double BGGMBTotal { get; set; }

        [JsonProperty]
        public double IFRDeclination { get; set; }

        [JsonProperty]
        public double IFRDip { get; set; }

        [JsonProperty]
        public double IFRBTotal { get; set; }

        [JsonProperty]
        public double CADecOffset { get; set; }

        [JsonProperty]
        public double CADipOffset { get; set; }

        [JsonProperty]
        public double CABtOffset { get; set; }

        [JsonProperty]
        [NotMapped]
        public double RefDeclination { get; set; }

        [JsonProperty]
        [NotMapped]
        public double RefDip { get; set; }

        [JsonProperty]
        [NotMapped]
        public double RefBTotal { get; set; }
    }
}
