using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace capAPI.DTOs.Request.ItemOption
{
    public class CreateItemOption
    {



        public int ItemId { get; set; }


        public string NameAr { get; set; }

        public string NameEn { get; set; }


        public int OptionCategoryId { get; set; }

        public bool? IsRequired { get; set; }

        public decimal? PriceAfterDiscount { get; set; }
        public int? Quantity { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }








    }
}
