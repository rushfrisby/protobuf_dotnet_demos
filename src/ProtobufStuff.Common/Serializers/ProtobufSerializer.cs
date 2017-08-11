using System.IO;
using System.Threading.Tasks;
using ProtoBuf;

namespace ProtobufStuff.Common.Serializers
{
    public class ProtobufSerializer : ISerializer
    {
        public void Serialize<T>(T instance, Stream destination)
        {
            Serializer.Serialize(destination, instance);
        }

        public async Task SerializeAsync<T>(T instance, Stream destination)
        {
            await Task.Factory.StartNew(() => Serializer.Serialize(destination, instance));
        }

        public byte[] Serialize<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                Serialize(instance, ms);
                return ms.ToArray();
            }
        }

        public async Task<byte[]> SerializeAsync<T>(T instance)
        {
            return await Task.Factory.StartNew(() => Serialize(instance));
        }

        public T Deserialize<T>(Stream source)
        {
            return Serializer.Deserialize<T>(source);
        }

        public async Task<T> DeserializeAsync<T>(Stream source)
        {
            return await Task.Factory.StartNew(() => Serializer.Deserialize<T>(source));
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
