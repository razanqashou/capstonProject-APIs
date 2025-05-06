namespace capAPI.DTOs.Request
{
    public class UpdateItemOption
    {
        public int OptionId { get; set; }


        public int ItemId { get; set; }


        public string? NameAr { get; set; } 

        public string? NameEn { get; set; } 


        public int? OptionCategoryId { get; set; }

        public bool? IsRequired { get; set; }


        public decimal? PriceAfterDiscount { get; set; }

        public int? Quantity { get; set; }





        public string? UpdatedBy { get; set; }

   



        public DateTime? UpdatedAt { get; set; }
    }
}
