using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DependencyResolvers.AutoMapper
{
    public class BookProfile: BaseProfile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDetailDto>().ReverseMap();

            CreateMap<Book, BookAddDto>().ReverseMap();

            CreateMap<BookUpdateDto, Book>().ReverseMap();

            CreateMap<BookListDto, Book>().ReverseMap();

        }
    }
}
