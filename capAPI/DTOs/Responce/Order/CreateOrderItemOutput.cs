namespace capAPI.DTOs.Responce.Order
{
    public class CreateOrderItemOutput
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? DiscountValue { get; set; }
    }
}
