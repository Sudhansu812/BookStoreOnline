using AutoMapper;
using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Models.Dto.UserDto;
using BookSharingOnlineApi.Repository.Interfaces;
using BookSharingOnlineApi.Services.Interfaces;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;

        public AccountManagementService(IUserRepo userRepo, IMapper mapper, IEncryptionService encryptionService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        public async Task<bool> Register(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
            {
                return false;
            }
            UserModel user = _mapper.Map<UserModel>(userRegisterDto);

            // Encryption
            user.Password = _encryptionService.Encrypt(user.Password);

            _userRepo.Register(user);
            return await _userRepo.SaveChanges();
        }

        public async Task<UserReadDto> LogIn(UserLogInDto userLogInDto)
        {
            UserModel user = await _userRepo.LogIn(userLogInDto.Email);

            if(user == null)
            {
                return null;
            }

            // Decrypt
            string password = _encryptionService.Decrypt(user.Password);

            if (password.Equals(userLogInDto.Password))
            {
                return _mapper.Map<UserReadDto>(user);
            }
            return null;
        }
    }
}