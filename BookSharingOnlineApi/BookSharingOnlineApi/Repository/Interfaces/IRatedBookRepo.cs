using BookSharingOnlineApi.Models;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository.Interfaces
{
    public interface IRatedBookRepo
    {
        Task<bool> SaveChanges();

        void Create(RatedBookModel ratedBook);

        Task<RatedBookModel> Get(int userId, int bookId);
    }
}