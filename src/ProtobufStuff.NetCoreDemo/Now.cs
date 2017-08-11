using System;
using System.Runtime.Serialization;

namespace ProtobufStuff.NetCoreDemo
{
    [DataContract]
    public class Now
    {
        [DataMember(Order = 1)]
        public string Value
        {
            get { return DateTimeOffset.Now.ToString(); }
            set { }
        }
    }
}
