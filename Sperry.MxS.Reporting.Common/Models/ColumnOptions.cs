using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    //[JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ColumnOptions : List<ColumnItem>
    {
        public ColumnItem this[string key]
        {
            get { return Find(x => x.FriendlyName == key); }
        }

        public new ColumnItem this[int order]
        {
            get { return Find(x => x.Order == order); }
        }

        public ColumnOptions() : base()
        { }

        public ColumnOptions(IEnumerable<ColumnItem> src) : base(src)
        { }

        public object Clone()
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(ms);
            }
        }
    }
}
