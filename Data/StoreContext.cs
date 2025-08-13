using EcommerceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EcommerceAPI.Data
{
    public class StoreContext : IdentityDbContext
    {
        public StoreContext (DbContextOptions<StoreContext> options) : base(options){}
        public DbSet<Products> Products { get; set; }

        public DbSet<Cart> Cart { get; set; }


        public DbSet<CartItem> CartItems { get; set; }
    }
}
