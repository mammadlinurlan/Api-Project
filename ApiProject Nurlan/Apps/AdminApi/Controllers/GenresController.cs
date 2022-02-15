using ApiProject_Nurlan.Apps.AdminApi.DTOs;
using ApiProject_Nurlan.Data.DAL;
using ApiProject_Nurlan.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public GenresController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("")]
        public IActionResult Create(GenrePostDto postDto)
        {
            if (_context.Genres.Any(x=>x.Name.ToLower().Trim() == postDto.Name.ToLower().Trim()))
            {
                return StatusCode(409);
            }

            Genre genre = new Genre
            {
                Name = postDto.Name,

            };

            _context.Genres.Add(genre);
            _context.SaveChanges();
            return StatusCode(201, genre);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Genre genre = _context.Genres.Include(x=>x.Books).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (genre==null)
            {
                return NotFound();
            }
            GenreGetDto getDto = new GenreGetDto
            {
            
                BooksCount = genre.Books.Count(),
                CreatedAt = genre.CreatedAt,
                Id = genre.Id,
                ModifiedAt = genre.ModifiedAt,
                Name = genre.Name
            };
            return Ok(getDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page=1)
        {
            var query = _context.Genres.Where(x => !x.IsDeleted);
            ListDto<GenreListItemDto> listDto = new ListDto<GenreListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * 8).Take(8).Select(x => new GenreListItemDto { Id = x.Id, Name = x.Name }).ToList()
            };

            return Ok(listDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id , GenrePostDto postDto)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (genre==null)
            {
                return NotFound();
            }
            if (_context.Genres.Any(x=> x.Id!=id && x.Name.ToLower().Trim()== postDto.Name.ToLower().Trim()))
            {
                return StatusCode(409);
            }
            genre.Name = postDto.Name;
            genre.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (genre == null)
            {
                return StatusCode(404);
            }
            genre.IsDeleted = true;
            genre.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return NoContent();

        }




    }
}
