using ApiProject_Nurlan.Apps.AdminApi.DTOs;
using ApiProject_Nurlan.Apps.AdminApi.DTOs.AuthorDtos;
using ApiProject_Nurlan.Data.DAL;
using ApiProject_Nurlan.Data.Entities;
using ApiProject_Nurlan.Extensions;
using Microsoft.AspNetCore.Authorization;
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
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("admin/api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthorsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] AuthorPostDto postDto)
        {
            if (_context.Authors.Any(x => x.Name.ToLower().Trim() == postDto.Name.ToLower().Trim()))
            {
                return StatusCode(409);
            }

            Author author = new Author
            {
                Name = postDto.Name,
                Image = postDto.ImageFile.SaveImg(_env.WebRootPath, "assets/author/img")

            };

            _context.Authors.Add(author);
            _context.SaveChanges();
            return StatusCode(201, author);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Author author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (author == null)
            {
                return NotFound();
            }
            AuthorGetDto getDto = new AuthorGetDto
            {
                Id = author.Id,
                BooksCount = author.Books.Count(),
                CreatedAt = author.CreatedAt,
                Image = author.Image,
                ModifiedAt = author.ModifiedAt,
                Name = author.Name
            };
            return Ok(getDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Authors.Include(x => x.Books).Where(x => !x.IsDeleted);
            ListDto<AuthorListItemDto> listDto = new ListDto<AuthorListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * 8).Take(8).Select(x => new AuthorListItemDto
                {
                    Id = x.Id,
                    Image = x.Image,
                    Name = x.Name,
                    BooksCount=x.Books.Count()
                }).ToList()

            };

            return Ok(listDto);

        }


        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromForm]AuthorUpdateDto postDto)
        {
            Author author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (author == null)
            {
                return NotFound();
            }
            if (_context.Authors.Any(x => x.Id != id && x.Name.ToLower().Trim() == postDto.Name.ToLower().Trim()))
            {
                return StatusCode(409);
            }

            if (postDto.ImageFile!=null)
            {
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/author/img", author.Image);
                author.Image = postDto.ImageFile.SaveImg(_env.WebRootPath, "assets/author/img");
            }

            author.ModifiedAt = DateTime.UtcNow;
            author.Name = postDto.Name;
            _context.SaveChanges();
            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Author author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (author == null)
            {
                return StatusCode(404);

            }

            author.ModifiedAt = DateTime.UtcNow;
            author.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }

    }
}
