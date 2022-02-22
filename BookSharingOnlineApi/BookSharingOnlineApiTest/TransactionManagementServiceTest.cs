using AutoMapper;
using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Models.Dto.BookDto;
using BookSharingOnlineApi.Models.Dto.CartDto;
using BookSharingOnlineApi.Models.Dto.OrderDto;
using BookSharingOnlineApi.Models.Profiles;
using BookSharingOnlineApi.Repository.Interfaces;
using BookSharingOnlineApi.Services;
using BookSharingOnlineApi.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSharingOnlineApiTest
{
    [TestClass]
    public class TransactionManagementServiceTest
    {
        [TestMethod]
        public async Task AddBookTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            BookProfile myProfile = new BookProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            BookCreateDto bookCreateDto = new BookCreateDto()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookQuantity = 100,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction"
            };

            BookModel bookModel = new BookModel() {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookQuantity = 100,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookAddTime = DateTime.UtcNow,
                BookId = 1,
                BookNumberOfRatings = 4,
                BookQuantitySold = 12,
                BookRating = 4.5
            };

            mockBook.Setup(book => book.Create(bookModel));
            mockBook.Setup(book => book.SaveChanges()).ReturnsAsync(true);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            bool output = await services.AddBook(bookCreateDto);
            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task AddToCartTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            CartProfile myProfile = new CartProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            CartCreateDto cartCreateDto = new CartCreateDto()
            {
                BookId = 1,
                CartQuantity = 10,
                UserId = 1
            };

            CartModel cart = new CartModel()
            {
                BookPrice = 1500,
                BookId = 1,
                CartId = 1,
                CartQuantity = 10,
                CartSumTotal = 15000,
                UserId = 1
            };

            mockBook.Setup(book => book.GetPrice(cart.BookId)).ReturnsAsync(1500);
            mockCart.Setup(c => c.Create(cart));
            mockCart.Setup(c => c.SaveChanges()).ReturnsAsync(true);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            bool output = await services.AddToCart(cartCreateDto);

            Console.WriteLine(cart.BookPrice);
            Console.WriteLine(cart.CartSumTotal);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task OrderTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            OrderProfile myProfile = new OrderProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            OrderCreateDto orderCreateDto = new OrderCreateDto() { 
                BookId = 1,
                OrderQuantity = 10,
                UserId = 1
            };

            OrderModel order = new OrderModel() { 
                BookId = 1,
                BookPrice = 1000,
                OrderId = 12,
                OrderQuantity = 10,
                OrderSumTotal = 10000,
                UserId = 1
            };

            mockBook.Setup(book => book.GetPrice(order.BookId)).ReturnsAsync(1000);
            mockBook.Setup(c => c.DecrementQuantity(order.BookId, order.OrderQuantity)).ReturnsAsync(true);
            mockOrder.Setup(c => c.Order(order));
            mockOrder.Setup(c => c.SaveChanges()).ReturnsAsync(true);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            bool output = await services.Order(orderCreateDto);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task GetByCategoryTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            BookProfile myProfile = new BookProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            BookReadDto bookReadDto = new BookReadDto()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookId = 1,
                BookNumberOfRatings = 4,
                BookRating = 4.5
            };

            BookModel bookModel = new BookModel()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookQuantity = 100,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookAddTime = DateTime.UtcNow,
                BookId = 1,
                BookNumberOfRatings = 4,
                BookQuantitySold = 12,
                BookRating = 4.5
            };

            List<BookModel> bookList = new List<BookModel>();
            bookList.Add(bookModel);

            mockBook.Setup(book => book.GetAll()).ReturnsAsync(bookList);
            mockBook.Setup(c => c.GetByCategory("Fiction")).ReturnsAsync(bookList);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            List<BookReadDto> output = (await services.GetByCategory("Fiction")).ToList();

            Assert.AreEqual(output.Count,bookList.Count);
        }

        [TestMethod]
        public async Task GetByStatusTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            BookProfile myProfile = new BookProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            BookReadDto bookReadDto = new BookReadDto()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookId = 1,
                BookNumberOfRatings = 4,
                BookRating = 4.5
            };

            BookModel bookModel = new BookModel()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookQuantity = 100,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookAddTime = DateTime.UtcNow,
                BookId = 1,
                BookNumberOfRatings = 4,
                BookQuantitySold = 12,
                BookRating = 4.5
            };

            List<BookReadDto> bookReadDtos = new List<BookReadDto>();
            bookReadDtos.Add(bookReadDto);
            List<BookModel> bookList = new List<BookModel>();
            bookList.Add(bookModel);

            mockBook.Setup(book => book.GetAll()).ReturnsAsync(bookList);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            List<BookReadDto> output = (await services.GetByStatus(1)).ToList();

            Assert.AreEqual(output.Count, bookList.Count);
        }

        [TestMethod]
        public async Task RateBookTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            BookProfile myProfile = new BookProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            RatedBookModel ratedBook = new RatedBookModel()
            {
                BookId = 1,
                UserId = 1,
                Rating = 4
            };

            mockBook.Setup(book => book.RateBook(1,1)).ReturnsAsync(4.0);
            mockRatedBook.Setup(book => book.Create(ratedBook));

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            bool output = await services.RateBook(1, 1, 1);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task SearchTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            BookProfile myProfile = new BookProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            BookReadDto bookReadDto = new BookReadDto()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookId = 1,
                BookNumberOfRatings = 4,
                BookRating = 4.5
            };

            BookModel bookModel = new BookModel()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookQuantity = 100,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookAddTime = DateTime.UtcNow,
                BookId = 1,
                BookNumberOfRatings = 4,
                BookQuantitySold = 12,
                BookRating = 4.5
            };

            mockBook.Setup(book => book.Search("abc")).ReturnsAsync(bookModel);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            BookReadDto output = await services.Search("abc");
            Console.WriteLine(output.BookTitle);
            Assert.AreEqual(output.BookTitle, bookReadDto.BookTitle);
        }

        [TestMethod]
        public async Task SearchRatedBookTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            BookProfile myProfile = new BookProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            RatedBookModel ratedBook = new RatedBookModel()
            {
                BookId = 1,
                UserId = 1,
                Rating = 4
            };

            mockRatedBook.Setup(book => book.Get(1, 1)).ReturnsAsync(ratedBook);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            bool output = await services.SearchRatedBook(1, 1);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task GetBookRemainingQuantity()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            BookProfile myProfile = new BookProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            BookModel bookModel = new BookModel()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookQuantity = 100,
                BookCoverPath = "fakepath/file.jpg",
                Category = "Fiction",
                BookAddTime = DateTime.UtcNow,
                BookId = 1,
                BookNumberOfRatings = 4,
                BookQuantitySold = 12,
                BookRating = 4.5
            };

            mockBook.Setup(book => book.GetBook(1)).ReturnsAsync(bookModel);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            int output = await services.GetBookRemainingQuantity(1);

            Assert.AreEqual(output, bookModel.BookQuantity);
        }

        [TestMethod]
        public async Task GetCartListTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            CartProfile myProfile = new CartProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            CartModel cart = new CartModel() {
                CartId = 1,
                CartQuantity = 12,
                BookId = 2,
                BookPrice = 1500,
                CartSumTotal = 18000,
                UserId = 1
            };

            List<CartModel> cartList = new List<CartModel>();
            cartList.Add(cart);

            mockCart.Setup(c => c.GetAll()).ReturnsAsync(cartList);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            List<CartReadDto> output = (await services.GetCartList(1)).ToList();

            Assert.AreEqual(output[0].CartId, cart.CartId);
        }

        [TestMethod]
        public async Task DeleteFromCartTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            CartProfile myProfile = new CartProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            CartModel cart = new CartModel()
            {
                CartId = 1,
                CartQuantity = 12,
                BookId = 2,
                BookPrice = 1500,
                CartSumTotal = 18000,
                UserId = 1
            };

            mockCart.Setup(c => c.GetCartOfUser(1)).ReturnsAsync(cart);
            mockCart.Setup(c => c.SaveChanges()).ReturnsAsync(true);

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            bool output = await services.DeleteFromCart(1);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task GetCartTableTest()
        {
            Mock<IBookRepo> mockBook = new Mock<IBookRepo>();
            Mock<IOrderRepo> mockOrder = new Mock<IOrderRepo>();
            Mock<ICartRepo> mockCart = new Mock<ICartRepo>();
            Mock<IRatedBookRepo> mockRatedBook = new Mock<IRatedBookRepo>();
            CartProfile myProfile = new CartProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            CartDetailsDto cartDetailsDto = new CartDetailsDto()
            {
                UserId = 1
            };

            List<CartTableDto> cartTables = new List<CartTableDto>();

            CartTableDto cartTableDto = new CartTableDto()
            {
                BookCoverPath = "fakepath/image.jpg",
                BookPrice = 1500,
                BookQuantity = 10,
                BookSumTotal = 15000,
                BookTitle = "Test Book",
                CartId = 1
            };

            cartTables.Add(cartTableDto);

            CartModel cart = new CartModel()
            {
                CartId = 1,
                CartQuantity = 12,
                BookId = 2,
                BookPrice = 1500,
                CartSumTotal = 18000,
                UserId = 1
            };

            List<CartModel> cartlist = new List<CartModel>();

            cartlist.Add(cart);

            mockCart.Setup(c => c.GetForUser(1)).ReturnsAsync(cartlist);
            mockBook.Setup(c => c.GetBookTitle(2)).ReturnsAsync("Test Book");
            mockBook.Setup(c => c.GetBookCoverPath(2)).ReturnsAsync("fakepath/image.jpg");

            ITransactionManagementService services = new TransactionManagementService(
                mockBook.Object,
                mockOrder.Object,
                mockCart.Object,
                mockRatedBook.Object,
                mapper
            );

            List<CartTableDto> output = (await services.GetCartTable(cartDetailsDto)).ToList();

            Assert.IsTrue(output[0].BookTitle.Equals(cartTableDto.BookTitle));
        }

    }
}
