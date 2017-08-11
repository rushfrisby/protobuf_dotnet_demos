using System.Collections.Generic;

namespace ProtobufStuff.Common.Serializers
{
    public static class ContentTypes
    {
        public const string Json = "application/json";
        public const string Protobuf = "application/x-protobuf";
        public const string PlainText = "text/plain";
        public const string Xml = "application/xml";

        private static readonly IDictionary<string, ISerializer> Serializers = new Dictionary<string, ISerializer>
        {
            { Json, new JsonSerializer() },
            { Protobuf, new ProtobufSerializer() }
        };

        public static ISerializer GetSerializer(string contentType)
        {
            return Serializers[Protobuf];
            //return Serializers[string.IsNullOrWhiteSpace(contentType) ? Json : contentType];
        }
    }
}
