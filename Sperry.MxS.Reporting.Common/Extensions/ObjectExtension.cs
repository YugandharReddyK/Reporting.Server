using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Utilities.MaxSurvey.Core.Infrastructure.Utilities;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class ObjectExtension
    {
        public static T ConvertToObject<T>(this byte[] buffer)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Binder = new TypeSerializationBinder();
            memoryStream.Write(buffer, 0, buffer.Length);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            return (T)binaryFormatter.Deserialize(memoryStream);
        }
        
        public static T DeepClone<T>(this T item)
        {
            string json = JsonConvert.SerializeObject(item);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static bool IsOfType<T>(this object obj)
        {
            return obj.IsOfType(typeof(T));
        }

        public static bool IsOfType(this object obj, Type type)
        {
            return (obj.GetType().Equals(type));
        }
    }
}

