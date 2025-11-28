using EShop.Domain.Ordering.Enums;
using EShop.Domain.Ordering.ValueObjects;

namespace EShop.Domain.Ordering.Policies.Context
{
    public class CreateOrderPolicyContext
    {
        public OrderValueObject Order { get; }
        public AddressValueObject Address { get; }

        public CreateOrderPolicyContext(
            OrderValueObject order,
            AddressValueObject address)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        // --------------------------------------------
        //  High-level evaluation used by policy
        // --------------------------------------------
        public bool CanCreateOrder()
        {
            return
                HasValidAddress() &&
                HasValidOrderStatus() &&
                HasValidUser() &&
                HasValidPayment() &&
                HasValidOrderTimestamp();
        }

        // --------------------------------------------
        //  Rule Components
        // --------------------------------------------
        public bool HasValidAddress() =>
            !string.IsNullOrWhiteSpace(Address.Street) &&
            !string.IsNullOrWhiteSpace(Address.City) &&
            !string.IsNullOrWhiteSpace(Address.Country) &&
            !string.IsNullOrWhiteSpace(Address.ZipCode);

        public bool HasValidOrderStatus() =>
            Order.OrderStatus == OrderStatus.Submitted; // only valid creation state

        public bool HasValidUser() =>
            Order.BuyerId is null || Order.BuyerId > 0;
        // or map inputs if buyerId is required

        public bool HasValidPayment() =>
            Order.PaymentId is null || Order.PaymentId > 0;

        public bool HasValidOrderTimestamp() =>
            Order.OrderDate <= DateTime.UtcNow;
    }
}