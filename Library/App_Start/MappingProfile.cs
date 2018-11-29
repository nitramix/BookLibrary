using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Library.Models;
using Library.Dtos;
using Library.App_Start;

namespace Library.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Client, ClientDto>();
            Mapper.CreateMap<Book, BookDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<Author, AuthorDto>();
            Mapper.CreateMap<Rental, RentalDto>();


            Mapper.CreateMap<BookDto, Book>().ForMember(x => x.Id, c => c.Ignore());
            Mapper.CreateMap<ClientDto, Client>().ForMember(x => x.Id, c => c.Ignore());
            Mapper.CreateMap<GenreDto, Genre>();
            Mapper.CreateMap<AuthorDto, Author>();
            Mapper.CreateMap<RentalDto, Rental>();
        }
       
    }
}