using System.Text;
using Newtonsoft.Json;
using System.IO.Compression;
using System.IO;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class GZipCompression
    {
        public static void CopyTo(Stream source, Stream destination)
        {
            source.CopyToAsync(destination).Wait();
        }

        public static byte[] SerializeAndCompress(this object obj)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress, true))
                {
                    CopyTo(msi, gs);
                }
                return mso.ToArray();
            }
        }

        public static T DecompressAndDeserialize<T>(this byte[] data)
        {
            using (var msi = new MemoryStream(data))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress, true))
                {
                    CopyTo(gs, mso);
                }

                return (T)JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(mso.ToArray()));
            }
        }

        public static byte[] Zip(StringBuilder input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input.ToString());
            using (MemoryStream source = new MemoryStream(bytes))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (GZipStream destination = new GZipStream(memoryStream, CompressionMode.Compress))
                    {
                        CopyTo(source, destination);
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        public static TextReader Unzip(byte[] content)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (GZipStream source = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        CopyTo(source, memoryStream);
                    }
                    return new StreamReader(new MemoryStream(memoryStream.ToArray()), Encoding.UTF8);
                }
            }
        }
    }
}
