using EShop.Domain.Ordering.ValueObjects;
using EShop.Domain.SharedKernel.Events;

namespace EShop.Domain.Ordering.Events;

public class OrderCancelledDomainEvent : IDomainEvent
{
    public OrderValueObject Order { get; }

    public OrderCancelledDomainEvent(OrderValueObject order)
    {
        Order = order;
    }
}
