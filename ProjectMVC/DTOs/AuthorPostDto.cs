using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.DTOs
{
    public class AuthorPostDto
    {
        public int Id { get; set; }
       
        [StringLength(maximumLength:50)]
        [Required]
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
