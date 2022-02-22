using AutoMapper;
using BookSharingOnlineApi.Models.Dto.UserDto;

namespace BookSharingOnlineApi.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserLogInDto>();
            CreateMap<UserLogInDto, UserModel>();
            CreateMap<UserRegisterDto, UserModel>();
            CreateMap<UserUpdateDto, UserModel>();
            CreateMap<UserModel, UserUpdateDto>();
            CreateMap<UserModel, UserReadDto>();
            CreateMap<UserReadDto, UserModel>();
        }
    }
}