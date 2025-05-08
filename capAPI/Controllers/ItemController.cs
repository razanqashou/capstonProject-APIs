using capAPI.DTOs.Request;
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
