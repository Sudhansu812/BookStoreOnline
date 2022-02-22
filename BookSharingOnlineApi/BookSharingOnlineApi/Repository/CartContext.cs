using BookSharingOnlineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSharingOnlineApi.Repository
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        { }

        public DbSet<CartModel> Carts { get; set; }
    }
}