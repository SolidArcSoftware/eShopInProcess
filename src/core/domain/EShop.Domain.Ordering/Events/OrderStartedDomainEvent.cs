using EShop.Domain.Ordering.ValueObjects;
using EShop.Domain.SharedKernel.Events;

namespace EShop.Domain.Ordering.Events
{
    public record class OrderStartedDomainEvent(
            OrderValueObject Order,
            AddressValueObject Address
            ) : IDomainEvent;
}