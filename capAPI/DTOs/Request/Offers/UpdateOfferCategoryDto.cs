namespace capAPI.DTOs.Request.Offers
{
    public class UpdateOfferCategoryDto
    {
        public int OfferCategoryId { get; set; }
        public int? OfferID { get; set; }
        public int? CategoryId { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

