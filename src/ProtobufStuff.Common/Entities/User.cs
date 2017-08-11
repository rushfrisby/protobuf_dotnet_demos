using System;
using System.Runtime.Serialization;

namespace ProtobufStuff.Common.Entities
{
    [DataContract]
    public class User
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 4)]
        public string EmailAddress { get; set; }

        [DataMember(Order = 5)]
        public bool IsActive { get; set; }

        [DataMember(Order = 6)]
        public int? Age { get; set; }

        //[DataMember(Order = 7)]
        //public object Something { get; set; }
    }
}
