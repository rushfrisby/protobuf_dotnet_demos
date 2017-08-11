using System.Runtime.Serialization;

namespace ProtobufStuff.WCFDemo
{
    [DataContract]
    public class ServiceResponse
    {
        [DataMember(Order = 1)]
        public int Total { get; set; }
    }
}
