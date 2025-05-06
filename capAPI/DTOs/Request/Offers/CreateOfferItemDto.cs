namespace capAPI.DTOs.Request.Offers
{
    public class CreateOfferItemDto
    {
        public int OfferID { get; set; }
        public int  ItemID { get; set; }

       
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
