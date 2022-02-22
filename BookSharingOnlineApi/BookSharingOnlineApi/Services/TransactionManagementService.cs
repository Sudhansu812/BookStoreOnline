using AutoMapper;
using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Models.Dto.BookDto;
using BookSharingOnlineApi.Models.Dto.CartDto;
using BookSharingOnlineApi.Models.Dto.OrderDto;
using BookSharingOnlineApi.Repository.Interfaces;
using BookSharingOnlineApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Services
{
    public class TransactionManagementService : ITransactionManagementService
    {
        private readonly IBookRepo _bookRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly ICartRepo _cartRepo;
        private readonly IRatedBookRepo _ratedBookRepo;
        private readonly IMapper _mapper;

        public TransactionManagementService(IBookRepo bookRepo, IOrderRepo orderRepo, ICartRepo cartRepo, IRatedBookRepo ratedBookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _ratedBookRepo = ratedBookRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddBook(BookCreateDto bookCreateDto)
        {
            BookModel book = _mapper.Map<BookModel>(bookCreateDto);
            book.BookAddTime = DateTime.UtcNow;
            _bookRepo.Create(book);
            return await _bookRepo.SaveChanges();
        }

        public async Task<bool> AddToCart(CartCreateDto cartCreateDto)
        {
            CartModel cart = _mapper.Map<CartModel>(cartCreateDto);
            cart.BookPrice = await _bookRepo.GetPrice(cart.BookId);
            cart.CartSumTotal = cart.BookPrice * cart.CartQuantity;
            _cartRepo.Create(cart);
            return await _cartRepo.SaveChanges();
        }

        public async Task<bool> Order(OrderCreateDto orderCreateDto)
        {
            OrderModel order;
            try
            {
                order = _mapper.Map<OrderModel>(orderCreateDto);
                order.BookPrice = await _bookRepo.GetPrice(order.BookId);
                order.OrderSumTotal = order.BookPrice * order.OrderQuantity;
                bool result = await _bookRepo.DecrementQuantity(order.BookId, order.OrderQuantity);
                if (result == false)
                {
                    // TODO throw exception
                    return false;
                }
                _orderRepo.Order(order);
            }
            catch
            {
                return false;
            }
            return await _orderRepo.SaveChanges();
        }

        public async Task<IEnumerable<BookReadDto>> GetByCategory(string category)
        {
            List<BookModel> books;
            if (category=="All")
            {
                books = (await _bookRepo.GetAll()).ToList();
            }
            else
            {
                books = (await _bookRepo.GetByCategory(category)).ToList();
            }
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<IEnumerable<BookReadDto>> GetByStatus(int id)
        {
            // 1- Best Sellers
            // 2- Trending
            // 3- Latest Uploads
            int n = 3;
            List<BookModel> books = (await _bookRepo.GetAll()).ToList();
            List<BookModel> reqBooks;
            if (id == 1)
            {
                books = (from book in books orderby book.BookQuantitySold descending select book).ToList();
            }
            else if (id == 2)
            {
                books = (from book in books orderby book.BookRating descending select book).ToList();
            }
            else if (id == 3)
            {
                books = (from book in books orderby book.BookAddTime descending select book).ToList();
            }

            if (books.Count < 3)
            {
                reqBooks = books;
                return _mapper.Map<IEnumerable<BookReadDto>>(reqBooks);
            }
            else
            {
                reqBooks = new List<BookModel>();
                for (int i = 0; i < n; i++)
                {
                    reqBooks.Add(books[i]);
                }
            }
            return _mapper.Map<IEnumerable<BookReadDto>>(reqBooks);
        }

        public async Task<bool> RateBook(int userId, int bookId, double rating)
        {
            double bookRatings = await _bookRepo.RateBook(bookId, rating);
            if (bookRatings == -1)
            {
                // TODO NoSuchBookFoundExcpetion
                return false;
            }
            RatedBookModel ratedBook = new RatedBookModel()
            {
                BookId = bookId,
                UserId = userId,
                Rating = (int)rating
            };
            _ratedBookRepo.Create(ratedBook);
            await _ratedBookRepo.SaveChanges();
            return true;
        }

        public async Task<BookReadDto> Search(string bookTitle)
        {
            BookModel book;
            try
            {
                book = await _bookRepo.Search(bookTitle);
                if (book == null)
                {
                    // TODO throw custom excpetion for no such book
                }
            }
            catch
            {
                return null;
            }
            return _mapper.Map<BookReadDto>(book);
        }

        public async Task<bool> SearchRatedBook(int userId, int bookId)
        {
            RatedBookModel ratedBook = await _ratedBookRepo.Get(userId, bookId);
            if (ratedBook == null)
            {
                return false;
            }
            return true;
        }

        public async Task<int> GetBookRemainingQuantity(int bookId)
        {
            BookModel book = await _bookRepo.GetBook(bookId);
            return (book.BookQuantity);
        }

        public async Task<IEnumerable<CartReadDto>> GetCartList(int id)
        {
            List<CartModel> cart = (await _cartRepo.GetAll()).Where(c => c.UserId == id).ToList();
            return _mapper.Map<IEnumerable<CartReadDto>>(cart);
        }

        public async Task<bool> DeleteFromCart(int id)
        {
            CartModel cart = await _cartRepo.GetCartOfUser(id);
            if(cart==null)
            {
                // TODO throw exception
                return false;
            }
            try
            {
                _cartRepo.Delete(cart);
            }
            catch
            {
                return false;
            }
            return await _cartRepo.SaveChanges();
        }

        public async Task<IEnumerable<CartTableDto>> GetCartTable(CartDetailsDto cartDetails)
        {
            List<CartModel> cartList = (await _cartRepo.GetForUser(cartDetails.UserId)).ToList();
            List<CartTableDto> cartTableItems = new List<CartTableDto>();

            for(int i=0;i<cartList.Count;i++)
            {
                CartTableDto cart = new CartTableDto
                {
                    CartId = cartList[i].CartId,
                    BookCoverPath = await _bookRepo.GetBookCoverPath(cartList[i].BookId),
                    BookTitle = await _bookRepo.GetBookTitle(cartList[i].BookId),
                    BookQuantity = cartList[i].CartQuantity,
                    BookPrice = cartList[i].BookPrice,
                    BookSumTotal = cartList[i].CartSumTotal
                };
                cartTableItems.Add(cart);
            }

            return cartTableItems;
        }

        public async Task<bool> OrderCartItems(CartOrderDto cart)
        {
            List<CartModel> cartList = (await _cartRepo.GetForUser(cart.UserId)).ToList();
            for(int i=0;i<cartList.Count;i++)
            {
                OrderCreateDto order = new OrderCreateDto
                {
                    UserId = cartList[i].UserId,
                    BookId = cartList[i].BookId,
                    OrderQuantity = cartList[i].CartQuantity
                };
                bool result = await Order(order);
                if(result == false)
                {
                    // TODO Exception and log
                    return false;
                }
                _cartRepo.Delete(cartList[i]);
                bool saveResult = await _cartRepo.SaveChanges();
                if(saveResult == false)
                {
                    return false;
                }
            }
            return true;
        }

    }
}