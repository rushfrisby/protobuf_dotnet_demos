using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProtobufStuff.Common.Serializers
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializer()
        {
            _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
        }

        public void Serialize<T>(T instance, Stream destination)
        {
            var data = Serialize(instance);
            destination.Write(data, 0, data.Length);
        }

        public async Task SerializeAsync<T>(T instance, Stream destination)
        {
            await Task.Factory.StartNew(() => Serialize(instance, destination));
        }

        public byte[] Serialize<T>(T instance)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(instance));
        }

        public async Task<byte[]> SerializeAsync<T>(T instance)
        {
            return await Task.Factory.StartNew(() => Serialize(instance));
        }

        public T Deserialize<T>(Stream source)
        {
            using (var r = new StreamReader(source, Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<T>(r.ReadToEnd(), _settings);
            }
        }

        public async Task<T> DeserializeAsync<T>(Stream source)
        {
            return await Task.Factory.StartNew(() => Deserialize<T>(source));
        }

        public T Deserialize<T>(byte[] source)
        {
            using (var ms = new MemoryStream(source))
            {
                return Deserialize<T>(ms);
            }
        }

        public async Task<T> DeserializeAsync<T>(byte[] source)
        {
            return await Task.Factory.StartNew(() => Deserialize<T>(source));
        }
    }
}
