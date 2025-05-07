namespace capAPI.DTOs.Request.Offers
{
    public class CreateOfferCategoryDto
    {
        public int OfferID { get; set; }
        public int CategoryId { get; set; }


        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
