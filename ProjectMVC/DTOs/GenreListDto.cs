using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.DTOs
{
    public class GenreListDto
    {
        public int TotalCount { get; set; }
        public List<GenreListItemDto> Items { get; set; }
    }
}
