using System;
using capAPI.DTOs.Request.Order;
using capAPI.DTOs.Responce;
using capAPI.DTOs.Responce.Order;
using capAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly DBCapstoneContext _context;

        public OrderController(DBCapstoneContext context)
        {
            _context = context;

        }






        //orderItem

        [HttpPost("AddOrderItem")]
        public IActionResult AddOrderItem(OrderItemDto dto)
        {
            
            var item = _context.Items.FirstOrDefault(x => x.ItemId == dto.ItemId);
            if (item == null)
            {
                return BadRequest("Item not found.");
            }

           
            var itemOffer = _context.OfferItems
                .Include(x => x.Offer)
                .FirstOrDefault(x => x.ItemId == dto.ItemId &&
                                     x.Offer.IsActive == true &&
                                     x.Offer.StartDate <= DateTime.Now &&
                                     x.Offer.EndDate >= DateTime.Now);

            var categoryOffer = _context.OfferCategories
                .Include(x => x.Offer)
                .FirstOrDefault(x => x.CategoryId == item.CategoryId &&
                                     x.Offer.IsActive == true &&
                                     x.Offer.StartDate <= DateTime.Now &&
                                     x.Offer.EndDate >= DateTime.Now);

            decimal discountPercent = 0;
            if (itemOffer != null && categoryOffer != null)
            {
                discountPercent = Math.Max(
                    itemOffer.Offer.DiscountPercent ?? 0,
                    categoryOffer.Offer.DiscountPercent ?? 0
                );
            }
            else if (itemOffer != null)
            {
                discountPercent = itemOffer.Offer.DiscountPercent ?? 0;
            }
            else if (categoryOffer != null)
            {
                discountPercent = categoryOffer.Offer.DiscountPercent ?? 0;
            }

            decimal originalPrice = item.Price;
            decimal discountValue = originalPrice * (discountPercent / 100);
            decimal unitPrice = originalPrice - discountValue;
            decimal totalPrice = unitPrice * dto.Quantity;

            var orderItem = new OrderItem
            {
                OrderId = dto.OrderId,
                ItemId = dto.ItemId,
                Quantity = dto.Quantity,
                UnitPrice = totalPrice,
                DiscountValue = discountValue * dto.Quantity, 
                CreatedBy = dto.CreatedBy,

            };

            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();

            var output = new CreateOrderItemOutput
            {
                OrderItemId = orderItem.OrderItemId,
                Quantity = orderItem.Quantity,
                UnitPrice = Math.Round(orderItem.UnitPrice, 2),
                DiscountValue = orderItem.DiscountValue.HasValue
        ? Math.Round(orderItem.DiscountValue.Value, 2)
        : (decimal?)null
            };

            return Ok(output);
        }







        [HttpGet("GetOrderItem/{id}")]
        public IActionResult GetOrderItem(int id)
        {
            var orderItem = _context.OrderItems
                .FirstOrDefault(x => x.OrderItemId == id);

            if (orderItem == null)
            {
                return NotFound("Order item not found.");
            }



            var orderItemDetails = new OrderItemDetalies
            {
                OrderId = orderItem.OrderId,
                ItemId = orderItem.ItemId,
                Quantity = orderItem.Quantity,
                UnitPrice = Math.Round(orderItem.UnitPrice, 2),
                DiscountValue = orderItem.DiscountValue.HasValue
                    ? Math.Round(orderItem.DiscountValue.Value, 2)
                    : (decimal?)null
            };

            return Ok(orderItemDetails);
        }
        [HttpPut("UpdateOrderItem")]
        public IActionResult UpdateOrderItem(UpdateOrderItem dto)
        {
            try
            {
                
                if (dto.OrderItemId <= 0)
                    return BadRequest("OrderItemId is required and must be greater than 0.");

                if (dto.ItemId.HasValue && dto.ItemId.Value <= 0)
                    return BadRequest("ItemId is required and must be greater than 0.");

              
                var orderItem = _context.OrderItems
                    .FirstOrDefault(x => x.OrderItemId == dto.OrderItemId);

                if (orderItem == null)
                    return NotFound("Order item not found.");

                
                if (dto.ItemId.HasValue && dto.ItemId != orderItem.ItemId)
                {
                   
                    var item = _context.Items.FirstOrDefault(x => x.ItemId == dto.ItemId.Value);
                    if (item == null)
                        return BadRequest("Item not found.");

                   
                    orderItem.ItemId = dto.ItemId.Value;

                 
                    decimal discountPercent = 0;
                    var itemOffer = _context.OfferItems
                        .Include(x => x.Offer)
                        .FirstOrDefault(x => x.ItemId == dto.ItemId.Value &&
                                             x.Offer.IsActive == true &&
                                             x.Offer.StartDate <= DateTime.Now &&
                                             x.Offer.EndDate >= DateTime.Now);

                    var categoryOffer = _context.OfferCategories
                        .Include(x => x.Offer)
                        .FirstOrDefault(x => x.CategoryId == item.CategoryId &&
                                             x.Offer.IsActive == true &&
                                             x.Offer.StartDate <= DateTime.Now &&
                                             x.Offer.EndDate >= DateTime.Now);

                    if (itemOffer != null && categoryOffer != null)
                    {
                        discountPercent = Math.Max(
                            itemOffer.Offer.DiscountPercent ?? 0,
                            categoryOffer.Offer.DiscountPercent ?? 0
                        );
                    }
                    else if (itemOffer != null)
                    {
                        discountPercent = itemOffer.Offer.DiscountPercent ?? 0;
                    }
                    else if (categoryOffer != null)
                    {
                        discountPercent = categoryOffer.Offer.DiscountPercent ?? 0;
                    }

                   
                    decimal originalPrice = item.Price;
                    decimal discountValue = originalPrice * (discountPercent / 100);
                    decimal unitPrice = originalPrice - discountValue;
                    orderItem.UnitPrice = unitPrice* orderItem.Quantity;

                
                     orderItem.DiscountValue = discountValue * orderItem.Quantity;

                  //  Console.WriteLine($"Received DiscountValue: {dto.DiscountValue}");
                }

               
               
                
                if (!dto.ItemId.HasValue)
                {
                   
                    if (dto.Quantity.HasValue && dto.Quantity.Value > 0)
                    {
                        orderItem.Quantity = dto.Quantity.Value;
                    }
                }

              
                _context.OrderItems.Update(orderItem);
                _context.SaveChanges();

                return Ok("Order item updated successfully.");
            }
            catch (Exception ex)
            {
               
                return StatusCode(500,  ex.Message);
            }
        }

        [HttpDelete("DeleteOrderItem/{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            try
            {
                var orderItem = _context.OrderItems.FirstOrDefault(x => x.OrderItemId == id);

                if (orderItem == null)
                    return NotFound("Order item not found.");

                _context.OrderItems.Remove(orderItem);

                _context.SaveChanges();

                return Ok("Order item deleted successfully.");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

    }
}