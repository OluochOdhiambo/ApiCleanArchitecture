using AutoMapper;
using Api.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Api.ApplicationCore.Entities;

namespace Api.ApplicationCore.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateBookRequest, Book>();
            CreateMap<Book, BookResponse>();
        }
    }
}
