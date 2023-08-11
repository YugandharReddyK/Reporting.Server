using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CoordSys
{
    /// <summary>
    /// MS3 : clsMagModel
    /// </summary>

    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MagneticModel
    {
        public MagneticModel()
        {
            PrimaryKey = 0;
            ID = null;
            Description = null;
        }

        public MagneticModel(string id, string description)
        {
            PrimaryKey = 0;
            ID = id;
            Description = description;
        }

        public MagneticModel(int key, string id, string description)
        {
            PrimaryKey = key;
            ID = id;
            Description = description;
        }
        public int PrimaryKey { get; set; }

        [JsonProperty]
        public string ID { get; set; }

        [JsonProperty]
        public string Description { get; set; }

    }
}
