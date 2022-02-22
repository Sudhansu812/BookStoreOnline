using BookSharingOnlineApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository.Interfaces
{
    public interface IBookRepo
    {
        Task<bool> SaveChanges();

        Task<BookModel> Search(string bookTitle);

        Task<double> RateBook(int bookId, double ratings);

        Task<IEnumerable<BookModel>> GetAll();

        void Create(BookModel book);

        Task<IEnumerable<BookModel>> GetByCategory(string category);

        Task<bool> DecrementQuantity(int id, int n);

        Task<double> GetPrice(int id);

        Task<BookModel> GetBook(int id);

        Task<string> GetBookCoverPath(int bookId);

        Task<string> GetBookTitle(int bookId);
    }
}