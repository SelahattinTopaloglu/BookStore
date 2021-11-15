using Core;
using System;

namespace Entity.DTOs
{
    public class BookListDto : IDto
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public int PageNumber { get; set; }
        public double UnitPrice { get; set; }
        public DateTime Year { get; set; }
        public AuthorDto Author { get; set; }
    }
}
