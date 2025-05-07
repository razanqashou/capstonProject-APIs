namespace capAPI.DTOs.Request.Order
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }

      
    }
}
