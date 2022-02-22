using BookSharingOnlineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSharingOnlineApi.Repository
{
    public class RatedBookContext : DbContext
    {
        public RatedBookContext(DbContextOptions<RatedBookContext> options) : base(options)
        { }

        public DbSet<RatedBookModel> RatedBooks { get; set; }
    }
}