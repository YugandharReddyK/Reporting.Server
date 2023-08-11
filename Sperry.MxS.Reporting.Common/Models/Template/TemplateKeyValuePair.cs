using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    public struct TemplateKeyValuePair<K, V>
    {
        public K Key { get; set; }

        public V Value { get; set; }
    }
}
