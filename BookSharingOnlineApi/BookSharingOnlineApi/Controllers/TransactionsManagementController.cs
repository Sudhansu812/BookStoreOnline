using BookSharingOnlineApi.Models.Dto.BookDto;
using BookSharingOnlineApi.Models.Dto.CartDto;
using BookSharingOnlineApi.Models.Dto.CategoryDto;
using BookSharingOnlineApi.Models.Dto.OrderDto;
using BookSharingOnlineApi.Models.Dto.RatedBookDto;
using BookSharingOnlineApi.Models.Dto.Search;
using BookSharingOnlineApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsManagementController : ControllerBase
    {
        private readonly ITransactionManagementService _service;

        public TransactionsManagementController(ITransactionManagementService service)
        {
            _service = service;
        }

        // Books
        [HttpPost("addbook")]
        [Authorize]
        public async Task<bool> AddBook(BookCreateDto bookCreateDto)
        {
            return await _service.AddBook(bookCreateDto);
        }

        [HttpPut("ratebook")]
        [Authorize]
        public async Task<bool> RateBook(RatedBookCreateDto ratedBookCreateDto)
        {
            if (await _service.SearchRatedBook(ratedBookCreateDto.UserId, ratedBookCreateDto.BookId))
            {
                return false;
            }
            return await _service.RateBook(ratedBookCreateDto.UserId, ratedBookCreateDto.BookId, ratedBookCreateDto.Rating);
        }

        [HttpPost("search")]
        public async Task<BookReadDto> Search(SearchBookDto book)
        {
            return await _service.Search(book.bookTitle);
        }

        // Carts
        [HttpPost("addtocart")]
        [Authorize]
        public async Task<bool> AddToCart(CartCreateDto cartCreateDto)
        {
            return await _service.AddToCart(cartCreateDto);
        }

        [HttpGet("getcartlist/{id}")]
        public async Task<IEnumerable<CartReadDto>> GetCartList(int id)
        {
            return await _service.GetCartList(id);
        }

        [HttpDelete("deletefromcart/{id}")]
        public async Task<bool> DeleteFromCart(int id)
        {
            bool deleteResult = await _service.DeleteFromCart(id);
            return deleteResult;
        }

        [HttpPost("getcarttable")]
        public async Task<IEnumerable<CartTableDto>> GetCartTable(CartDetailsDto cartDetails)
        {
            return await _service.GetCartTable(cartDetails);
        }

        [HttpPost("ordercartitems")]
        public async Task<bool> OrderCartItems(CartOrderDto cart)
        {
            return await _service.OrderCartItems(cart);
        }

        // categories
        [HttpPost("categories")]
        public async Task<IEnumerable<BookReadDto>> GetByCategory(CategoryGetDto category)
        {
            return await _service.GetByCategory(category.category);
        }

        [HttpGet("categories/{id}")]
        public async Task<IEnumerable<BookReadDto>> GetByStatus(int id)
        {
            // 1- Best Sellers - Most Sold Books
            // 2- Trending - Higest Rated
            // 3- Latest Uploads - Date Uploaded
            return await _service.GetByStatus(id);
        }

        // orders
        [HttpPost("order")]
        [Authorize]
        public async Task<bool> Order(OrderCreateDto orderCreateDto)
        {
            return await _service.Order(orderCreateDto);
        }

        [HttpGet("orders/{id}")]
        public async Task<int> GetBookRemainingQuantity(int id)
        {
            return await _service.GetBookRemainingQuantity(id);
        }
    }
}