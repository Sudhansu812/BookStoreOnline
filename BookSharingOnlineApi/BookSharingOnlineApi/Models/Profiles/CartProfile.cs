using AutoMapper;
using BookSharingOnlineApi.Models.Dto.CartDto;

namespace BookSharingOnlineApi.Models.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartModel, CartReadDto>();
            CreateMap<CartCreateDto, CartModel>();
        }
    }
}