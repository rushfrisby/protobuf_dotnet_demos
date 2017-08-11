using System;
using System.Runtime.Serialization;

namespace ProtobufStuff.Common.Entities
{
    [DataContract]
    public class Order
    {
        public Guid Id { get; set; }

        [DataMember(Order = 1, Name = nameof(Id))]
        private string IdValue
        {
            get => Id.ToString();
            set => Id = Guid.Parse(value);
        }

        [DataMember(Order = 2)]
        public decimal TotalAmount { get; set; }
    }
}
