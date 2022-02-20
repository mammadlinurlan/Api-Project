using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.DTOs
{
    public class BookPostDto
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public decimal CostPrice { get; set; }
        [Required]

        public decimal SalePrice { get; set; }
        [Required]

        public int PageCount { get; set; }
        [Required]

        public string Language { get; set; }
        [Required]

        public IFormFile ImageFile { get; set; }
        [Required]

        public int AuthorId { get; set; }
        [Required]

        public int GenreId { get; set; }

    }
}
