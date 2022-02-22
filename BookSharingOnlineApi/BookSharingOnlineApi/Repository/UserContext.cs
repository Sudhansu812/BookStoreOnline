using BookSharingOnlineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSharingOnlineApi.Repository
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
    }
}