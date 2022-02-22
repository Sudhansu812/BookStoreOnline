using AutoMapper;
using BookSharingOnlineApi.Models.Dto.OrderDto;

namespace BookSharingOnlineApi.Models.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderModel, OrderCreateDto>();
            CreateMap<OrderCreateDto, OrderModel>();
        }
    }
}