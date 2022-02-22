using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository
{
    public class RatedBookRepo : IRatedBookRepo
    {
        private readonly RatedBookContext _context;

        public RatedBookRepo(RatedBookContext context)
        {
            _context = context;
        }

        public void Create(RatedBookModel ratedBook)
        {
            _context.RatedBooks.AddAsync(ratedBook);
        }

        public async Task<RatedBookModel> Get(int userId, int bookId)
        {
            return await _context.RatedBooks.FirstOrDefaultAsync(rate => (rate.BookId == bookId) && (rate.UserId == userId));
        }

        public async Task<bool> SaveChanges()
        {
            int n = await _context.SaveChangesAsync();
            return (n >= 0);
        }
    }
}