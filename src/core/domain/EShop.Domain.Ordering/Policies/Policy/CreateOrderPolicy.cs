using EShop.Domain.Ordering.Events;
using EShop.Domain.Ordering.Policies.Context;
using EShop.Domain.Ordering.Policies.Contracts;
using EShop.Domain.SharedKernel.Policies;

namespace EShop.Domain.Ordering.Policies.Policy
{
    public class CreateOrderPolicy : ICreateOrderPolicy
    {
        public PolicyResult Apply(CreateOrderPolicyContext context)
        {
            // Safety: context cannot be null because ctor enforces it
            if (!context.CanCreateOrder())
            {
                //Return failure
            }

            // If the order is valid, we emit a domain event indicating the order process has started
            var startedEvent = new OrderStartedDomainEvent(
                context.Order,
                context.Address
            );

            return PolicyResult.Success(startedEvent);
        }
    }
}