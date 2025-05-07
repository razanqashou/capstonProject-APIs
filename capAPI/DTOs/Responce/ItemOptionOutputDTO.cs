namespace capAPI.DTOs.Responce
{
    public class ItemOptionOutputDTO
    {
        public int OptionId { get; set; }
        public int ItemId { get; set; }

        public string NameAr { get; set; } 

        public string NameEn { get; set; } 

        public decimal? PriceAfterDiscount { get; set; }
        public int OptionCategoryId { get; set; }

        public bool? IsRequired { get; set; }

        public int? Quantity { get; set; }




       
    }
}
