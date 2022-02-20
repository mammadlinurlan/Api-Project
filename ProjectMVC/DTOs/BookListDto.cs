using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.DTOs
{
    public class BookListDto
    {
        public int TotalCount { get; set; }
        public List<BookListItemDto> Items { get; set; }
    }
}
