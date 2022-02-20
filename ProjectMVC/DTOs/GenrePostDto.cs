using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.DTOs
{
    public class GenrePostDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }

    }
}
