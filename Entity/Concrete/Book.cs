using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string BookName { get; set; }

        public int PageNumber { get; set; }
        public double UnitPrice { get; set; }
        public DateTime Year { get; set; }

        public virtual Author Author { get; set; }
    }
}
