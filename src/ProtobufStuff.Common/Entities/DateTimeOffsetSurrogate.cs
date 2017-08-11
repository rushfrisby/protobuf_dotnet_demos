using System;
using System.Runtime.Serialization;

namespace ProtobufStuff.Common.Entities
{
    [DataContract(Name = nameof(DateTimeOffset))]
    public class DateTimeOffsetSurrogate
    {
        [DataMember(Order = 1)]
        public long Value { get; set; }

        public static implicit operator DateTimeOffset(DateTimeOffsetSurrogate surrogate)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(surrogate.Value);
        }

        public static implicit operator DateTimeOffsetSurrogate(DateTimeOffset source)
        {
            return new DateTimeOffsetSurrogate { Value = source.ToUnixTimeMilliseconds() };
        }
    }
}
