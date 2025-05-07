namespace capAPI.DTOs.Request.Offers
{
    public class UpdateOfferItemDto
    {
        public int OfferItemId { get; set; }
        public int? OfferID { get; set; }
        public int? ItemID { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
