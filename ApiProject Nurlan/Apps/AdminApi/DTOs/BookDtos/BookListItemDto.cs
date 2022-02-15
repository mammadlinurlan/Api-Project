using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.DTOs.BookDtos
{
    public class BookListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Image { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }
        public decimal Profit { get; set; }

        public bool DisplayStatus { get; set; }

        public GenreInBookListItemDto Genre { get; set; }
        public AuthorInBookListItemDto Author { get; set; }
    }

    public class GenreInBookListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AuthorInBookListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
