namespace EShop.UseCases.Ordering.Dtos
{
    public class OrderDto
    {
        public int OrderStatus { get; set; }
        public string Description { get; set; }
        public int? PaymentId { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}