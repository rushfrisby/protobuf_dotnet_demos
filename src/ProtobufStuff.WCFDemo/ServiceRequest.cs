using System.Runtime.Serialization;

namespace ProtobufStuff.WCFDemo
{
    [DataContract]
    public class ServiceRequest
    {
        [DataMember(Order = 1)]
        public int Apples { get; set; }

        [DataMember(Order = 2)]
        public int Bananas { get; set; }

        [DataMember(Order = 3)]
        public int Coconuts { get; set; }
    }
}
