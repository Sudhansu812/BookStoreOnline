using BookSharingOnlineApi.Controllers;
using BookSharingOnlineApi.Models.Dto.BookDto;
using BookSharingOnlineApi.Models.Dto.CartDto;
using BookSharingOnlineApi.Models.Dto.CategoryDto;
using BookSharingOnlineApi.Models.Dto.OrderDto;
using BookSharingOnlineApi.Models.Dto.RatedBookDto;
using BookSharingOnlineApi.Models.Dto.Search;
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
    public class TransactionsManagementControllerTest
    {
        [TestMethod]
        public async Task AddBookTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            BookCreateDto bookCreateDto = new BookCreateDto() { 
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookQuantity = 100,
                BookCoverPath = "path\\file.jpg",
                Category = "Fiction"
            };

            mock.Setup(b => b.AddBook(bookCreateDto)).ReturnsAsync(true);

            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            Assert.IsTrue(await controller.AddBook(bookCreateDto));
        }

        [TestMethod]
        public async Task RateBookTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();
            RatedBookCreateDto ratedBookCreateDto = new RatedBookCreateDto()
            {
                BookId = 1,
                UserId = 1,
                Rating = 5
            };

            mock.Setup(b => b.RateBook(ratedBookCreateDto.UserId, ratedBookCreateDto.BookId, ratedBookCreateDto.Rating)).ReturnsAsync(true);
            mock.Setup(b => b.SearchRatedBook(ratedBookCreateDto.UserId, ratedBookCreateDto.BookId)).ReturnsAsync(false); ;
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            Assert.IsTrue(await controller.RateBook(ratedBookCreateDto));
        }

        [TestMethod]
        public async Task SearchBookTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();
            SearchBookDto searchBookDto = new SearchBookDto()
            {
                bookTitle = "abc"
            };

            BookReadDto bookReadDto = new BookReadDto()
            {
                BookAuthorName = "John Doe",
                BookTitle = "abc",
                BookDescription = "xyz",
                BookPrice = 1500,
                BookCoverPath = "path\\file.jpg",
                Category = "Fiction",
                BookId = 1,
                BookNumberOfRatings = 3,
                BookRating = 4
            };

            mock.Setup(b => b.Search(searchBookDto.bookTitle)).ReturnsAsync(bookReadDto);
            
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            BookReadDto output = await controller.Search(searchBookDto);

            Assert.AreEqual(output, bookReadDto);
        }

        [TestMethod]
        public async Task AddToCartTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();
            CartCreateDto cartCreateDto = new CartCreateDto()
            {
                UserId = 1,
                BookId = 2,
                CartQuantity = 10
            };

            mock.Setup(b => b.AddToCart(cartCreateDto)).ReturnsAsync(true);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            Assert.IsTrue(await controller.AddToCart(cartCreateDto));
        }

        [TestMethod]
        public async Task GetCartListTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();
            List<CartReadDto> cartList = new List<CartReadDto>();

            CartReadDto cartReadDto = new CartReadDto() {
                CartId = 1,
                CartQuantity = 12,
                BookId = 2,
                BookPrice = 1500,
                CartSumTotal = 18000
            };

            cartList.Add(cartReadDto);

            mock.Setup(b => b.GetCartList(1)).ReturnsAsync(cartList);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            int output = (await controller.GetCartList(1)).ToList().Count;

            Assert.AreEqual(output, cartList.Count);
        }

        [TestMethod]
        public async Task DeleteFromCartTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            mock.Setup(b => b.DeleteFromCart(1)).ReturnsAsync(true);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            bool output = await controller.DeleteFromCart(1);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task GetCartTableTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            CartDetailsDto cartDetailsDto = new CartDetailsDto() {
                UserId = 1
            };

            List<CartTableDto> cartTables = new List<CartTableDto>();

            CartTableDto cartTableDto = new CartTableDto() { 
                BookCoverPath = "fakepath/image.jpg",
                BookPrice = 1500,
                BookQuantity = 10,
                BookSumTotal = 15000,
                BookTitle = "Test Book",
                CartId = 1
            };

            cartTables.Add(cartTableDto);

            mock.Setup(b => b.GetCartTable(cartDetailsDto)).ReturnsAsync(cartTables);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            List<CartTableDto> output = (await controller.GetCartTable(cartDetailsDto)).ToList();

            Assert.AreEqual(output.Count, cartTables.Count);
        }

        [TestMethod]
        public async Task OrderCartItemsTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            CartOrderDto cartOrderDto = new CartOrderDto()
            {
                UserId = 1
            };

            mock.Setup(b => b.OrderCartItems(cartOrderDto)).ReturnsAsync(true);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            bool output = await controller.OrderCartItems(cartOrderDto);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task GetByCategoryTest()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            CategoryGetDto category = new CategoryGetDto()
            {
                category = "Fiction"
            };

            List<BookReadDto> bookList = new List<BookReadDto>();

            BookReadDto bookReadDto = new BookReadDto() { 
                BookAuthorName = "abc",
                BookCoverPath = "fakepath/image.jpg",
                BookDescription = "desc",
                BookId = 1,
                BookNumberOfRatings = 23,
                BookPrice = 1500,
                BookRating = 3.4,
                BookTitle = "title",
                Category = "Fiction"
            };

            bookList.Add(bookReadDto);

            mock.Setup(b => b.GetByCategory(category.category)).ReturnsAsync(bookList);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            List<BookReadDto> output = (await controller.GetByCategory(category)).ToList();

            Assert.AreEqual(output.Count, bookList.Count);
        }

        [TestMethod]
        public async Task GetByStatus()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            List<BookReadDto> bookList = new List<BookReadDto>();

            BookReadDto bookReadDto = new BookReadDto()
            {
                BookAuthorName = "abc",
                BookCoverPath = "fakepath/image.jpg",
                BookDescription = "desc",
                BookId = 1,
                BookNumberOfRatings = 23,
                BookPrice = 1500,
                BookRating = 3.4,
                BookTitle = "title",
                Category = "Fiction"
            };

            mock.Setup(b => b.GetByStatus(1)).ReturnsAsync(bookList);

            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            List<BookReadDto> output = (await controller.GetByStatus(1)).ToList();

            Assert.AreEqual(output.Count, bookList.Count);
        }

        [TestMethod]
        public async Task Order()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            OrderCreateDto orderCreateDto = new OrderCreateDto()
            {
                BookId = 1,
                OrderQuantity = 10,
                UserId = 1
            };

            mock.Setup(b => b.Order(orderCreateDto)).ReturnsAsync(true);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            bool output = await controller.Order(orderCreateDto);

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task GetBookRemainingQuantity()
        {
            Mock<ITransactionManagementService> mock = new Mock<ITransactionManagementService>();

            mock.Setup(b => b.GetBookRemainingQuantity(1)).ReturnsAsync(10);
            TransactionsManagementController controller = new TransactionsManagementController(mock.Object);

            int output = await controller.GetBookRemainingQuantity(1);

            Assert.AreEqual(output, 10);
        }
    }
}
