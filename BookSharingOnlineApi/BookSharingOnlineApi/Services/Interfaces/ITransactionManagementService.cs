using BookSharingOnlineApi.Models.Dto.BookDto;
using BookSharingOnlineApi.Models.Dto.CartDto;
using BookSharingOnlineApi.Models.Dto.OrderDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Services.Interfaces
{
    public interface ITransactionManagementService
    {
        Task<bool> AddBook(BookCreateDto bookCreateDto);

        Task<bool> AddToCart(CartCreateDto cartCreateDto);

        Task<bool> Order(OrderCreateDto orderCreateDto);

        Task<bool> RateBook(int userId, int bookId, double rating);

        Task<BookReadDto> Search(string bookTitle);

        Task<IEnumerable<BookReadDto>> GetByCategory(string category);

        Task<IEnumerable<BookReadDto>> GetByStatus(int id);

        Task<bool> SearchRatedBook(int userId, int bookId);

        Task<int> GetBookReamainingQuantity(int bookId);

        Task<IEnumerable<CartReadDto>> GetCartList(int id);

        Task<bool> DeleteFromCart(int id);

        Task<IEnumerable<CartTableDto>> GetCartTable(CartDetailsDto cartDetails);

        Task<bool> OrderCartItems(CartOrderDto cart);
    }
}