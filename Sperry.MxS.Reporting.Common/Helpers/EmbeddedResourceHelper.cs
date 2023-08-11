using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class EmbeddedResourceHelper
    {
        public static T ReadResource<T>(string fileName)
        {
            try
            {
                Assembly assebmly = Assembly.GetExecutingAssembly();
                using (var stream = assebmly.GetManifestResourceStream(assebmly.GetName().Name + ".Resources." + fileName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex, MethodNameHelper.GetMethodContextName());
            }
            return default(T);
        }

        public static string ReadResource(string fileName)
        {
            try
            {
                Assembly assebmly = Assembly.GetExecutingAssembly();
                using (var stream = assebmly.GetManifestResourceStream(assebmly.GetName().Name + ".Resources." + fileName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex, MethodNameHelper.GetMethodContextName());
            }
            return "";
        }

        public static byte[] ReadDllResource(string fileName)
        {
            try
            {
                byte[] buffer = { 0 };
                Assembly assebmly = Assembly.GetExecutingAssembly();
                using (var stream = assebmly.GetManifestResourceStream(assebmly.GetName().Name + ".Resources." + fileName))
                {
                    if (stream != null)
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        return buffer;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex, MethodNameHelper.GetMethodContextName());
            }
            return default(byte[]);
        }
    }
}
