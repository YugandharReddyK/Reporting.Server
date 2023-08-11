using Sperry.MxS.Core.Common.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.DTOModel
{
    public class AssociationFor3MWDUser
    {
        public List<Guid> userIds { get; set; }
        public MxSUser loggedInUser { get; set; }
    }
}
