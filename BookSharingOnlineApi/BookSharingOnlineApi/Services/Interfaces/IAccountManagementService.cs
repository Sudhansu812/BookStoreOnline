using BookSharingOnlineApi.Models.Dto.UserDto;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Services.Interfaces
{
    public interface IAccountManagementService
    {
        Task<bool> Register(UserRegisterDto userRegisterDto);

        Task<UserReadDto> LogIn(UserLogInDto userLogInDto);
    }
}