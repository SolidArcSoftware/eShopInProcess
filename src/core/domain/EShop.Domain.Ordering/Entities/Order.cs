using EShop.Domain.Ordering.Enums;
using EShop.Domain.Ordering.ValueObjects;
using EShop.Domain.SharedKernel.Entities;

namespace EShop.Domain.Ordering.Entities
{
    public class Order : Entity
    {
        public OrderStatus OrderStatus { get; private set; }
        public string Description { get; private set; }
        public DateTime OrderDate { get; set; }
    }
}