using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Data.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public bool DisplayStatus { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Image { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }


    }
}
