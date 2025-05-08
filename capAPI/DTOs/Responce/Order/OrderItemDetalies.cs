using System.ComponentModel.DataAnnotations.Schema;

namespace capAPI.DTOs.Responce.Order
{
    public class OrderItemDetalies
    {
        public int OrderId { get; set; }

     
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? DiscountValue { get; set; }
    }
}
