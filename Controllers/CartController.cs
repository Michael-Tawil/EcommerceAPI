using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    { 
        private readonly StoreContext _Context;
        public CartController(StoreContext context) { 
            _Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCart()
        {
            var cart = await RetrieveCart();
            if (cart == null) {return NotFound($"Cart not found.");}
            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task <ActionResult> AddItemToCart(int ProductId, int quantity)
        {
            var cart = await RetrieveCart() ?? CreateCart();

            var product = await _Context.Products.FindAsync(ProductId);

            if (product == null) { return NotFound("Product not found."); }

            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == ProductId);

            if (existingItem != null) {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem { Product = product,Quantity = quantity});
            }

            var result = await _Context.SaveChangesAsync() > 0;
            if (result) return CreatedAtAction(nameof(GetCart), cart);
            return BadRequest(new { Message = "Problem saving item to cart" });
        }

        [HttpDelete("items")]
        public async Task <ActionResult> RemoveCartItem(int productId)
        {
            var cart = await RetrieveCart();
            if (cart == null) return NotFound("Cart not found.");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return NotFound("Item not found in cart.");

            cart.Items.Remove(item);
            _Context.CartItems.Remove(item);

            var result = await _Context.SaveChangesAsync() > 0;
            if (result) return Ok();

            return BadRequest(new { Message = "Problem removing item from cart" });

        }

        private async Task<Cart> RetrieveCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return null;

            return await _Context.Cart
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        private Cart CreateCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = new Cart { UserId = userId };
            _Context.Cart.Add(cart);
            return cart;
        }

    }   
}
