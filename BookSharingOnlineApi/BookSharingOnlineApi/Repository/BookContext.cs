using BookSharingOnlineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSharingOnlineApi.Repository
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        { }

        public DbSet<BookModel> Books { get; set; }
    }
}