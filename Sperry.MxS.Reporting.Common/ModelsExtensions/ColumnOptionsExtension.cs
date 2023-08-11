using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sperry.MxS.Core.Common.Models
{
    public static class ColumnOptionsExtension
    {
        public static object Clone(this ColumnOptions columnOptions)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, columnOptions);
                ms.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(ms);
            }
        }

    }
}
