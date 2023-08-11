using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Security
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ResourceGroup : DataModelBase
    {
        public ResourceGroup()
        {
            //Commented By Sandeep in core also

            //_resources = new StateList<Resource>();
            //_resourceUsers = new StateList<ResourceUser>();
            //_permissions = new StateList<Permission>();
        }

        // Commented By Sandeep in core Also

        //private ResourceStatus _status;
        //private StateList<Resource> _resources;
        //private StateList<Permission> _permissions;
        //private StateList<ResourceUser> _resourceUsers;

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public Guid AppUserId { get; set; }

        [JsonProperty]
        public Guid WellId { get; set; }

        [JsonProperty]
        public MxSSecurityPermission Permission { get; set; }

        [JsonProperty]
        public AppUser AppUser { get; set; }

        //Commentd By Sandeep in core also

        //[DataMember]
        //public ResourceStatus Status
        //{
        //    get { return _status; }
        //    set
        //    {
        //        _status = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //[DataMember]
        //public StateList<Resource> Resources
        //{
        //    get { return _resources; }
        //    set { _resources = value; }
        //}

        //[DataMember]
        //public StateList<Permission> Permissions
        //{
        //    get { return _permissions; }
        //    set { _permissions = value; }
        //}

        //[DataMember]
        //public StateList<ResourceUser> ResourceUsers
        //{
        //    get { return _resourceUsers; }
        //    set { _resourceUsers = value; }
        //}

        //public void AddResource(Resource resource)
        //{
        //    if (resource != null)
        //    {
        //        _resources.Add(resource);
        //    }
        //}

        //public void AddPermission(Permission permission)
        //{
        //    if (permission != null)
        //    {
        //        _permissions.Add(permission);
        //    }
        //}

        //public void AddResourceUser(ResourceUser user)
        //{
        //    if (user != null)
        //    {
        //        _resourceUsers.Add(user);
        //    }
        //}

    }
}
