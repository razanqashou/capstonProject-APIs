using capAPI.DTOs.Request.Offers;
using capAPI.DTOs.Responce.offer;
using System;
using capAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly DBCapstoneContext _context;

        public OfferController(DBCapstoneContext context)
        {
            _context = context;

        }





        //itemOffer




        [HttpPost]
        [Route("Create-Offer-Item")]
        public async Task<IActionResult> CreateOfferItem(CreateOfferItemDto input)
        {
            try
            {

                if (input.OfferID <= 0 || input.ItemID <= 0)
                    return BadRequest("OfferID and ItemID are required.");

                var offerItem = new OfferItem
                {
                    OfferId = input.OfferID,
                    ItemId = input.ItemID,
                    CreatedBy = input.CreatedBy,
                    CreatedAt = DateTime.Now
                };

                await _context.OfferItems.AddAsync(offerItem);
                await _context.SaveChangesAsync();

                var offerItemDto = new OfferItemOutputDTO
                {
                    OfferItemId = offerItem.OfferItemId,
                    OfferID = offerItem.OfferId,
                    ItemID = offerItem.ItemId
                };

                return Ok(offerItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost]
        [Route("Update-Offer-Item")]
        public async Task<IActionResult> UpdateOfferItem(UpdateOfferItemDto input)
        {
            try
            {
                var offerItem = await _context.OfferItems
                    .FirstOrDefaultAsync(o => o.OfferItemId == input.OfferItemId);

                if (offerItem == null)
                    return NotFound("Offer item not found.");

                offerItem.OfferId = input.OfferID ?? offerItem.OfferId;
                offerItem.ItemId = input.ItemID ?? offerItem.ItemId;
                offerItem.UpdatedBy = input.UpdatedBy ?? offerItem.UpdatedBy;
                offerItem.UpdatedAt = input.UpdatedAt ?? DateTime.Now;

                await _context.SaveChangesAsync();
                return Ok("Updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOfferItem/{id}")]
        public async Task<IActionResult> GetOfferItem(int id)
        {
            try
            {
                var offerItem = await _context.OfferItems.FindAsync(id);
                if (offerItem == null)
                    return NotFound("Offer item not found.");

                var dto = new OfferItemOutputDTO
                {
                    OfferItemId = offerItem.OfferItemId,
                    OfferID = offerItem.OfferId,
                    ItemID = offerItem.ItemId
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteOfferItem/{id}")]
        public async Task<IActionResult> DeleteOfferItem(int id)
        {
            try
            {
                var existing = await _context.OfferItems.FindAsync(id);
                if (existing == null)
                    return NotFound("Offer item not found.");

                _context.OfferItems.Remove(existing);
                await _context.SaveChangesAsync();

                return Ok("Deleted successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }



        //categoryOffer



        [HttpPost]
        [Route("Create-Offer-Category")]
        public async Task<IActionResult> CreateOfferCategory(CreateOfferCategoryDto input)
        {
            try
            {

                if (input.OfferID <= 0 || input.CategoryId <= 0)
                    return BadRequest("OfferID and CategoryId are required.");

                var offercategory = new OfferCategory
                {
                    OfferId = input.OfferID,
                    CategoryId = input.CategoryId,
                    CreatedBy = input.CreatedBy,
                    CreatedAt = DateTime.Now
                };

                await _context.OfferCategories.AddAsync(offercategory);
                await _context.SaveChangesAsync();

                var dto = new OfferCategoryOutputDTO
                {
                    OfferCategoryId = offercategory.OfferCategoryId,
                    OfferID = offercategory.OfferId,
                    CategoryId = offercategory.CategoryId
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost]
        [Route("Update-Offer-Category")]
        public async Task<IActionResult> UpdateOfferCategory(UpdateOfferCategoryDto input)
        {
            try
            {
                var offerItem = await _context.OfferCategories
                    .FirstOrDefaultAsync(o => o.OfferCategoryId == input.OfferCategoryId);

                if (offerItem == null)
                    return NotFound("Offer Category not found.");
                offerItem.OfferCategoryId = input.OfferCategoryId;

                offerItem.OfferId = input.OfferID ?? offerItem.OfferId;
                offerItem.CategoryId= input.CategoryId ?? offerItem.CategoryId;
                offerItem.UpdatedBy = input.UpdatedBy ?? offerItem.UpdatedBy;
                offerItem.UpdatedAt = input.UpdatedAt ?? DateTime.Now;

                await _context.SaveChangesAsync();
                return Ok("Updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOfferCategory/{id}")]
        public async Task<IActionResult> GetOfferCategory(int id)
        {
            try
            {
                var offerItem = await _context.OfferCategories.FindAsync(id);
                if (offerItem == null)
                    return NotFound("Offer Category not found.");

                var dto = new OfferCategoryOutputDTO
                {
                    OfferCategoryId = offerItem.OfferCategoryId,
                    OfferID = offerItem.OfferId,
                    CategoryId = offerItem.CategoryId
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }



        [HttpDelete]
        [Route("DeleteOfferCategory/{id}")]
        public async Task<IActionResult> DeleteOfferCategory(int id)
        {
            try
            {
                var existing = await _context.OfferCategories.FindAsync(id);
                if (existing == null)
                    return NotFound("Offer Category not found.");

                _context.OfferCategories.Remove(existing);
                await _context.SaveChangesAsync();

                return Ok("Deleted successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }






    }
}
