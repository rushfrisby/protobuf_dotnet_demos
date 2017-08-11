using System;
using System.Runtime.Serialization;

namespace ProtobufStuff.Common.Entities
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember(Order = 1)]
        public DateTimeOffset RequestedOn { get; set; }

        [DataMember(Order = 2)]
        public string UserName { get; set; }

        [DataMember(Order = 3)]
        public string Password { get; set; }
    }
}
