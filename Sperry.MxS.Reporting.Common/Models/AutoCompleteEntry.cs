using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AutoCompleteEntry
    {
        private List<string> _keywords;
        private string _displayName;

        public List<string> Keywords
        {
            get
            {
                if (_keywords == null)
                {
                    _keywords = new List<string>() { _displayName };
                }
                return _keywords;
            }
        }

        public string DisplayName { get; set; }

        public AutoCompleteEntry(string displayName, params string[] keywords)
        {
            _displayName = displayName;
            _keywords = new List<string>(keywords);
        }

        public override string ToString()
        {
            return _displayName;
        }
    }
}
