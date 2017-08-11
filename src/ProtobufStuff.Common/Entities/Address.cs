using ProtoBuf;

namespace ProtobufStuff.Common.Entities
{
    [ProtoContract]
    public class Address
    {
        [ProtoMember(1)]
        public string Address1 { get; set; }

        [ProtoMember(2)]
        public string Address2 { get; set; }

        [ProtoMember(3)]
        public string City { get; set; }

        [ProtoMember(4)]
        public string State { get; set; }

        [ProtoMember(5)]
        public string Zip { get; set; }

    }
}
