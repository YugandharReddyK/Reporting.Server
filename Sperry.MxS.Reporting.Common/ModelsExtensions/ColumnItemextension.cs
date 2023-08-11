using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sperry.MxS.Core.Common.Models
{
    public static class ColumnItemextension
    {
        public static object Clone(this ColumnItem columnItem)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, columnItem);
                ms.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(ms);
            }
        }
    }
}
