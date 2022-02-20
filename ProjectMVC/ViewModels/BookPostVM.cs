using ProjectMVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.ViewModels
{
    public class BookPostVM
    {
        public  GenreListDto Genres { get; set; }
        public AuthorListDto Authors { get; set; }
        public BookPostDto Books { get; set; }
    }
}
