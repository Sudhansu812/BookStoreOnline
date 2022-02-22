using AutoMapper;
using BookSharingOnlineApi.Models.Dto.BookDto;

namespace BookSharingOnlineApi.Models.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookModel, BookCreateDto>();
            CreateMap<BookCreateDto, BookModel>();
            CreateMap<BookReadDto, BookModel>();
            CreateMap<BookModel, BookReadDto>();
        }
    }
}