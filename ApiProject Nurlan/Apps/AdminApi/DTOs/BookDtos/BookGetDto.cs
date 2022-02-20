using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.DTOs.BookDtos
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Image { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }

        public decimal Profit { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public bool DisplayStatus { get; set; }

        public GenreInBookGetDto Genre { get; set; }
        public AuthorInBookGetDto Author { get; set; }
    }

    public class GenreInBookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
    }
    public class AuthorInBookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
    }

}
