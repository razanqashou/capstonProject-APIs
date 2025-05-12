using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace capAPI.DTOs.Request.Item
{
    public class CreatItemInputDTO
    {
      
        public string NameEn { get; set; } = null!;

    
        public string NameAr { get; set; } = null!;

  
        public string? DescriptionEn { get; set; }

  
        public string? DescriptionAr { get; set; }


        public IFormFile Image { get; set; } = null!;

    
        public decimal Price { get; set; }

   
        public int CategoryId { get; set; }

      
        public string? ItemBadge { get; set; }



    }
}
