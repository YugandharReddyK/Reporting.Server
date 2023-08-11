using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Admin
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ResultStatus
    {
        public bool WasCheckSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exception { get; set; }
    }
}
