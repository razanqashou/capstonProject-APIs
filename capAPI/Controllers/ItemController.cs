using capAPI.DTOs.Request;
using capAPI.DTOs.Request.Item;
using capAPI.DTOs.Request.ItemOption;
using capAPI.DTOs.Responce;
using capAPI.Helpers;
using capAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {


        private readonly DBCapstoneContext _context;

        public ItemController(DBCapstoneContext context)
        {
            _context = context;

        }



        // **Create Item**
        [HttpPost("create-item ")]
        public async Task<IActionResult> CreateItem([FromForm] CreatItemInputDTO dto, IFormFile imageFile)
        {
            var errors = Validation.Validate(dto, imageFile, _context.Categories.ToList());

            if (errors.Any())
            {
                return BadRequest(new { message = "Validation failed", errors });
            }
            var item = new Item
            {
                NameEn = dto.NameEn,
                NameAr = dto.NameAr,
                DescriptionEn = dto.DescriptionEn,
                DescriptionAr = dto.DescriptionAr,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                IsActive = true, 
                CreatedBy = "Admin", 
                CreatedAt = DateTime.UtcNow 
            };

            
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "New Item Has Been Created" });
        }

        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] CreatItemInputDTO dto, IFormFile imageFile)
        {
            var errors = Validation.Validate(dto, imageFile, _context.Categories.ToList());

            if (errors.Any())
            {
                return BadRequest(new { message = "Validation failed", errors });
            }

            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            item.NameEn = dto.NameEn;
            item.NameAr = dto.NameAr;
            item.DescriptionEn = dto.DescriptionEn;
            item.DescriptionAr = dto.DescriptionAr;
            item.Price = dto.Price;
            item.CategoryId = dto.CategoryId;

            if (imageFile != null && imageFile.Length > 0)
            {
                item.Image = await SaveImage(imageFile);
            }

            item.UpdatedBy = "Admin"; 
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Item Has Been Updated" });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _context.Items.ToListAsync();
            if (items == null || !items.Any())
            {
                return NotFound(new { message = "No items found." });
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            return Ok(item);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Item has been deleted successfully." });
        }

        
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var filePath = Path.Combine("wwwroot/images", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return filePath;
        }
    






        //ItemOption

        [HttpPost]
        [Route("Create-Item-Option")]
        public async Task<IActionResult> CreateItemOption(CreateItemOption input)
        {
            try
            {
                var validationError = Validation.ValidateRequiredFieldsItemOption(input);
                if (validationError != null)
                {
                    return BadRequest(validationError);
                }


                ItemOption itemOption = new ItemOption
                {
                    ItemId = input.ItemId,
                    NameAr = input.NameAr,
                    NameEn = input.NameEn,
                    PriceAfterDiscount = input.PriceAfterDiscount,
                    OptionCategoryId = input.OptionCategoryId,
                    IsRequired = input.IsRequired,
                    Quantity = input.Quantity,
                    CreatedBy = input.CreatedBy,
                    CreatedAt = DateTime.Now

                };


                await _context.AddAsync(itemOption);
                await _context.SaveChangesAsync();


                ItemOptionDto itemOptionDTO = new ItemOptionDto
                {
                    OptionId=itemOption.OptionId,
                    NameAr = itemOption.NameAr,
                    NameEn = itemOption.NameEn,
                    PriceAfterDiscount = input.PriceAfterDiscount ?? 0,
                    
                   
                };
                return Ok(itemOptionDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }




        [HttpPost]
        [Route("UpdateItemOption")]
        public async Task<IActionResult> UpdateItemOption(UpdateItemOption input)
        {
            try
            {
                var itemOption=_context.ItemOptions.Where(o=>o.OptionId==input.OptionId).SingleOrDefault();
                if (itemOption == null)
                {
                    return NotFound("Item option not found.");
                }
                if (input.ItemId <= 0)
                {
                    return BadRequest("ItemId is required and must be greater than 0.");
                }

                if (input.OptionCategoryId <= 0)
                {
                    return BadRequest("OptionCategoryId is required and must be greater than 0.");
                }

                itemOption.OptionId = input.OptionId;
                itemOption.ItemId = input.ItemId;
                itemOption.NameAr = !string.IsNullOrEmpty(input.NameAr) ? input.NameAr : itemOption.NameAr;
                itemOption.NameEn = !string.IsNullOrEmpty(input.NameEn) ? input.NameEn : itemOption.NameEn;
                itemOption.OptionCategoryId = input.OptionCategoryId ?? itemOption.OptionCategoryId;
                itemOption.IsRequired = input.IsRequired ?? itemOption.IsRequired;
                itemOption.PriceAfterDiscount = input.PriceAfterDiscount ?? itemOption.PriceAfterDiscount;
                itemOption.Quantity = input.Quantity ?? itemOption.Quantity;
                itemOption.UpdatedBy = input.UpdatedBy ?? itemOption.UpdatedBy; 
                itemOption.UpdatedAt = input.UpdatedAt ?? DateTime.Now;


                await _context.SaveChangesAsync();
               

                return Ok("Updated Successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }


        [HttpGet]
        [Route("GetItemOption/{id}")]
        public async Task<IActionResult> GetItemOption(int id)
        {
            try
            {
                var itemOption = await _context.ItemOptions.FindAsync(id);
                if (itemOption == null)
                {
                    return NotFound("Item option not found.");
                }

                SpesificItemOptionOutputDTO itemOptionDTO = new SpesificItemOptionOutputDTO
                {
                  
                    ItemId = itemOption.ItemId,
                    NameAr = itemOption.NameAr,
                    NameEn = itemOption.NameEn,
                    OptionCategoryId = itemOption.OptionCategoryId,
                    IsRequired = itemOption.IsRequired ?? true,
                    PriceAfterDiscount = itemOption.PriceAfterDiscount ??0,
                    Quantity = itemOption.Quantity ?? 0
                };

                return Ok(itemOptionDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }


        [HttpDelete]
        [Route("DeleteItemOption")]
        public async Task<IActionResult> DeleteItemOption(int id)
        {
            try
            {
                var existing = await _context.ItemOptions.FindAsync(id);
                if (existing == null)
                {
                    return NotFound("Item option not found.");
                }

                _context.ItemOptions.Remove(existing);
                await _context.SaveChangesAsync();
                

                return Ok("Deleted Succesfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }


        [HttpGet]
        [Route("GetAllItemOptions")]
        public async Task<IActionResult> GetAllItemOptions()
        {
            try
            {
                var itemOptions = await _context.ItemOptions
                    .Select(itemOption => new ItemOptionDto
                    {
                        OptionId = itemOption.OptionId,
                 
                        NameAr = itemOption.NameAr,
                        NameEn = itemOption.NameEn,
                        PriceAfterDiscount = itemOption.PriceAfterDiscount ?? 0,
                       
                    })
                    .ToListAsync();

                if (itemOptions == null || !itemOptions.Any())
                {
                    return NotFound("No item options found.");
                }

                return Ok(itemOptions);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}
