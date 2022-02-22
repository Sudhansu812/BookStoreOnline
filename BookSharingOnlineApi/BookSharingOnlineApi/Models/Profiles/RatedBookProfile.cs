using AutoMapper;
using BookSharingOnlineApi.Models.Dto.RatedBookDto;

namespace BookSharingOnlineApi.Models.Profiles
{
    public class RatedBookProfile : Profile
    {
        public RatedBookProfile()
        {
            CreateMap<RatedBookModel, RatedBookReadDto>();
            CreateMap<RatedBookReadDto, RatedBookModel>();
            CreateMap<RatedBookCreateDto, RatedBookReadDto>();
            CreateMap<RatedBookReadDto, RatedBookCreateDto>();
        }
    }
}