using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.DTOs
{
    public class AuthorListDto
    {
        public int TotalCount { get; set; }
        public List<AuthorListItemDto> Items { get; set; }
    }
}
