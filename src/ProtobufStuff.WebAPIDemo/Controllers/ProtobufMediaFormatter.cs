using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using ProtobufStuff.Common.Serializers;

namespace ProtobufStuff.WebAPIDemo.Controllers
{
    public class ProtobufMediaFormatter : BufferedMediaTypeFormatter
    {
        public ProtobufMediaFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentTypes.Protobuf));
            MediaTypeMappings.Add(new QueryStringMapping("type", "pb", new MediaTypeHeaderValue(ContentTypes.Protobuf)));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            ISerializer serializer = new ProtobufSerializer();
            serializer.Serialize(value, writeStream);
        }
    }
}
