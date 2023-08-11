using Newtonsoft.Json;
using System.Reflection;
using Sperry.MxS.Core.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Ex.Helpers;
using System;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public abstract class DataModelBase : IDisposable
    {
        private static readonly Dictionary<string, HashSet<string>> IgnoreProperties = new Dictionary<string, HashSet<string>>();

        private static readonly Dictionary<string, List<PropertyInfo>> CollectionPropertiesWithState = new Dictionary<string, List<PropertyInfo>>();

        private string _createdBy;

        private string _lastEditedBy;

        private bool _isDeserializing = false;

        private MxSState _state;

        [JsonProperty]
        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (value != null && !value.ToLower().Equals(_createdBy))
                {
                    _createdBy = string.Intern(value.ToLower());
                }
            }
        }

        [JsonProperty]
        public DateTime CreatedTime { get; set; }

        //[IgnoreImportProperty]
        [JsonProperty]
        public string LastEditedBy
        {
            get { return _lastEditedBy; }
            set
            {
                if (value != null && !value.ToLower().Equals(_lastEditedBy))
                {

                    _lastEditedBy = string.Intern(value.ToLower());
                }
            }
        }

        [IgnoreImportProperty]
        [JsonProperty]
        public DateTime LastEditedTime { get; set; }

        [JsonProperty]
        public Guid Id { get; set; }

        [NotMapped]
        [JsonProperty]
        public MxSState State
        {
            get { return _state; }
            set
            {
                //if we are in the middle of deserializing, then just set the state otherwise, run though the setstate method.
                if (_isDeserializing)
                {
                    _state = value;
                }
                else
                {
                    SetState(value);
                }
            }
        }

        protected DataModelBase()
        {
            Id = Guid.NewGuid();
            //the client code had extension methods to assign the CreatedTime time to UTC 
            CreatedTime = DateTime.UtcNow;
            LastEditedTime = DateTime.UtcNow;
            State = MxSState.Added;
        }

        //[field: NonSerialized]
        //public event PropertyChangedEventHandler PropertyChanged;

        public Well ResetState(Well well)
        {
            string data = JsonConvert.SerializeObject(well, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.All });
            if (data != null)
            {
                data = data.Replace("State\":1", "State\":0");
                data = data.Replace("State\":3", "State\":0");

                well = (Well)JsonConvert.DeserializeObject(data, typeof(Well));
            }
            return well;
        }

        public void SetState(MxSState newState)
        {
            if (newState == MxSState.Modified)
            {
                LastEditedBy = MxSConstants.UserName;
                LastEditedTime = DateTime.UtcNow;
            }
            if (newState == MxSState.Added)
            {
                if (string.IsNullOrEmpty(CreatedBy))
                {
                    CreatedBy = MxSConstants.UserName;
                    CreatedTime = DateTime.UtcNow;
                }
                //LastEditedBy = MxSConstants.UserName;
                //LastEditedTime = DateTime.UtcNow;
            }
            if (_state == MxSState.Added)
            {
                switch (newState)
                {
                    case MxSState.Unchanged:
                    case MxSState.Deleted:
                        _state = newState;
                        break;
                }
            }
            else
            {
                _state = newState;
            }
        }

        public void Dispose()
        {
            //Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
