using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class UpdatePayload
    {
        public Guid Id { get; set; }

        public MxSUpdateType UpdateType { get; set; }

        public MxSUpdateActionType UpdateTypeAction { get; set; }

        public UpdatePayload(Guid id, MxSUpdateType updateType, MxSUpdateActionType updateTypeAction)
        {
            Id = id;
            UpdateType = updateType;
            UpdateTypeAction = updateTypeAction;
        }
    }
}
