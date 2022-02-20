using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.DTOs
{
    public class AuthorListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BooksCount { get; set; }
        public string Image { get; set; }
    }
}
