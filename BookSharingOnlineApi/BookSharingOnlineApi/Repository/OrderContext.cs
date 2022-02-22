using BookSharingOnlineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSharingOnlineApi.Repository
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { }

        public DbSet<OrderModel> Orders { get; set; }
    }
}