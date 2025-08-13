using Microsoft.AspNetCore.Routing.Constraints;

namespace EcommerceAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public Products Product { get; set; }
    }
}
