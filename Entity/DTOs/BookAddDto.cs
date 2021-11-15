using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class BookAddDto: IDto
    {
        public int AuthorId { get; set; }
        public string BookName { get; set; }
        public int PageNumber { get; set; }
        public double UnitPrice { get; set; }
        public DateTime Year { get; set; }
        public AuthorDto Author { get; set; }
    }
}
