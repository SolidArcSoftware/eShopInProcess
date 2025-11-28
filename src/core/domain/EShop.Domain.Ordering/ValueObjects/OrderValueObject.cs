using EShop.Domain.Ordering.Enums;
using EShop.Domain.SharedKernel.ValueObjects;

namespace EShop.Domain.Ordering.ValueObjects
{
    public sealed class OrderValueObject : ValueObject
    {
        public int? BuyerId { get; }
        public int? PaymentId { get; }
        public AddressValueObject Address { get; }
        public OrderStatus OrderStatus { get; }
        public string Description { get; }
        public DateTime OrderDate { get; }

        public OrderValueObject(
            int? buyerId,
            int? paymentId,
            AddressValueObject address,
            OrderStatus orderStatus,
            string description,
            DateTime orderDate)
        {
            BuyerId = buyerId;
            PaymentId = paymentId;
            Address = address;
            OrderStatus = orderStatus;
            Description = description;
            OrderDate = orderDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BuyerId!;
            yield return PaymentId!;
            yield return Address;
            yield return OrderStatus;
            yield return Description;
            yield return OrderDate;
        }
    }
}