using System;
using Core.Dal.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Concrete;
using Entity.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class BookDal : EntityRepositoryBase<Book, ExampleContext>, IBookDal
    {
        public List<BookDetailDto> GetBookDetails()
        {
            using (ExampleContext context = new ExampleContext())
            {
                var result = from b in context.Books
                             join a in context.Authors
                             on b.AuthorId equals a.Id
                             select new BookDetailDto
                             {
                                 AuthorName = a.Name,
                                 BookId = b.Id,
                                 BookName = b.BookName,
                                 UnitPrice = b.UnitPrice
                             };
                return result.ToList();
            }

        }

        public List<BookListDto> GetAll2()
        {
            using (ExampleContext context = new ExampleContext())
            {
                var result = from b in context.Books
                             join a in context.Authors
                             on b.AuthorId equals a.Id
                             select new BookListDto
                             {
                                 Author = new AuthorDto { Id =a.Id, Name = a.Name, Surname = a.Surname },
                                 Id = b.Id,
                                 BookName = b.BookName,
                                 PageNumber = b.PageNumber,
                                 UnitPrice = b.UnitPrice,
                                 Year = b.Year,
                                 
                             };
                return result.ToList();
            }
        }

        public BookListDto GetById2(int id)
        {
            using (ExampleContext context = new ExampleContext())
            {
                var result = from b in context.Books
                             join a in context.Authors
                             on b.AuthorId equals a.Id
                             select new BookListDto
                             {
                                 Author = new AuthorDto { Id = a.Id, Name = a.Name, Surname = a.Surname },
                                 Id = b.Id,
                                 BookName = b.BookName,
                                 PageNumber = b.PageNumber,
                                 UnitPrice = b.UnitPrice,
                                 Year = b.Year,
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
