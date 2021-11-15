using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class BookUpdateDto : IDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string BookName { get; set; }
        public int PageNumber { get; set; }
        public double UnitPrice { get; set; }
        public DateTime Year { get; set; }
    }
}
