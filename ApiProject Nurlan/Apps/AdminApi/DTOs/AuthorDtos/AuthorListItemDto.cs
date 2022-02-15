using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.DTOs.AuthorDtos
{
    public class AuthorListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BooksCount { get; set; }
        public string Image { get; set; }
    }
}
