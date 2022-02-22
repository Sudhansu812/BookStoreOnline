using BookSharingOnlineApi.Controllers;
using BookSharingOnlineApi.Models.Dto.UserDto;
using BookSharingOnlineApi.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace BookSharingOnlineApiTest
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public async Task RegisterTestPass()
        {
            Mock<IAccountManagementService> mock = new Mock<IAccountManagementService>();
            UserRegisterDto userRegisterDto = new UserRegisterDto() { 
                Email = "abc@example.com",
                Password = "password123",
                UserFirstName = "Joe",
                UserLastName = "Smith",
                UserName = "smithjoe"
            };

            mock.Setup(acc => acc.Register(userRegisterDto)).ReturnsAsync(true);

            AccountsController controller = new AccountsController(mock.Object);

            bool output = (await controller.Register(userRegisterDto));

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task RegisterTestFail()
        {
            Mock<IAccountManagementService> mock = new Mock<IAccountManagementService>();
            UserRegisterDto userRegisterDto = new UserRegisterDto()
            {
                Email = "abc",
                Password = "password123",
                UserFirstName = "Joe",
                UserLastName = "Smith",
                UserName = "smithjoe"
            };

            mock.Setup(acc => acc.Register(userRegisterDto)).ReturnsAsync(false);

            AccountsController controller = new AccountsController(mock.Object);

            bool output = (await controller.Register(userRegisterDto));

            Assert.IsFalse(output);
        }
    }
}
