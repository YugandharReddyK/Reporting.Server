
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Helpers;
using System.IO;
using System.Linq;

namespace Sperry.MxS.Core.Common.Utilities
{
    public class RequestToObjectConverter
    {
        private static byte[] StreamToBytesConverter(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var memoryStream = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }
                return memoryStream.ToArray();
            }
        }

        public static T GetObjectFromRequest<T>(HttpRequest request)
        {
            if (request?.Form?.Files != null && request.Form.Files.Any())
            {
                using (Stream receiveStream = request.Form.Files.First().OpenReadStream())
                {
                    byte[] requestData = StreamToBytesConverter(receiveStream);
                    TextReader textReader = GZipCompression.Unzip(requestData);
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    using (JsonTextReader jsonTextReader = new JsonTextReader(textReader))
                    {
                        return jsonSerializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }
            return default(T);
        }
    }
}
