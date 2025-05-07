namespace capAPI.DTOs.Request.Order
{
    public class UpdateOrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }


        public int? ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? DiscountValue { get; set; }
    }
}
