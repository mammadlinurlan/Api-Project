﻿using ApiProject_Nurlan.Apps.AdminApi.DTOs;
using ApiProject_Nurlan.Apps.AdminApi.DTOs.BookDtos;
using ApiProject_Nurlan.Data.DAL;
using ApiProject_Nurlan.Data.Entities;
using ApiProject_Nurlan.Extensions;
using AutoMapper;
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
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BooksController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        [HttpPost("")]
        public IActionResult Create([FromForm] BookPostDto postDto)
        {
            if (_context.Books.Any(x => x.Name.ToLower().Trim() == postDto.Name.ToLower().Trim()))
            {
                return StatusCode(409);
            }
            //if (_context.Authors.FirstOrDefault(x => x.Id == postDto.AuthorId)==null)
            //{
            //    return StatusCode(404);
            //}

            Book book = new Book
            {

                AuthorId = postDto.AuthorId,
                CostPrice = postDto.CostPrice,

                GenreId = postDto.GenreId,
                Name = postDto.Name,
                SalePrice = postDto.SalePrice,
                PageCount = postDto.PageCount,
                Language = postDto.Language,
                Image = postDto.ImageFile.SaveImg(_env.WebRootPath, "assets/book/img")
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return StatusCode(201, book);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = _context.Books.Include(x => x.Genre).Include(x => x.Author).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (book == null)
            {
                return StatusCode(404);
            }
            BookGetDto getDto = _mapper.Map<BookGetDto>(book);
            return Ok(getDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Books.Include(x => x.Genre).Include(x => x.Author).Where(x => !x.IsDeleted);
            ListDto<BookListItemDto> listDto = new ListDto<BookListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * 8).Take(8).Select(x => new BookListItemDto
                {

                    Author = new AuthorInBookListItemDto
                    {
                        Id = x.AuthorId,
                        Name = x.Author.Name
                    },
                    Genre = new GenreInBookListItemDto
                    {
                        Id = x.GenreId,
                        Name = x.Genre.Name
                    },
                    CostPrice = x.CostPrice,
                    DisplayStatus = x.DisplayStatus,
                    Id = x.Id,
                    Image = x.Image,
                    SalePrice = x.SalePrice,
                    Language = x.Language,
                    Name = x.Name,
                    PageCount = x.PageCount,
                    Profit = x.SalePrice - x.CostPrice
                }).ToList()

            };
            return Ok(listDto);

        }
    }
}