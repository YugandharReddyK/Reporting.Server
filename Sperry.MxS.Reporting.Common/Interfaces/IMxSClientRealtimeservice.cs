using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSClientRealtimeservice
    {
        bool IsConnected();

        void BroadcastWellChange(Guid wellId, MxSSaveCallerEnum saveCallerEnum);

        void BroadcastWellOnlyUpdate(Well well, MxSSaveCallerEnum saveCallerEnum);

        void BroadcastWellState(Guid wellId, Well well, MxSUpdateActionType wellAction);
        
        void BroadcastAdminUpdate(Guid id, MxSUpdateType updateType, MxSUpdateActionType updateTypeAction);

        void BroadcastWell3MWDResourecUpdate(Guid id, MxS3MWDActionType updateActionType);
    }
}
