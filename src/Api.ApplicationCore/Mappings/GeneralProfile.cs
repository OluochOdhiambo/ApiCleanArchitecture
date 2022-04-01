using AutoMapper;
using Api.ApplicationCore.DTOs;
using Api.ApplicationCore.Entities;

namespace Api.ApplicationCore.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateBookRequest, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.Pages, opt => opt.Ignore())
                .ForMember(dest => dest.InStock, opt => opt.Ignore())
                .ForMember(dest => dest.Summary, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<Book, BookResponse>();
        }
    }
}
