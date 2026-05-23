using AsyncDataLibrary.Interfaces;
using Newtonsoft.Json; 
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace AsyncDataLibrary.Infrastructure
{
    public class JsonDataSerializer : IDataSerializer
    {
        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            if (stream.Length == 0) return default;

            using (StreamReader reader = new StreamReader(stream))
            {
                string json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task SerializeAsync<T>(Stream stream, T data)
        {
            string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(json);
            }
        }
    }
}