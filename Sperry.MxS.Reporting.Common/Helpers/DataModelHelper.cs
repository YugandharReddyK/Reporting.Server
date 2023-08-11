using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
      public static class DataModelHelper
      {
            public static T DeepClone<T>(T item) where T : class
            {
                  using (MemoryStream ms = new MemoryStream())
                  {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ms, item);
                        ms.Seek(0, SeekOrigin.Begin);
                        return bf.Deserialize(ms) as T;
                  }
            }
      }
}
