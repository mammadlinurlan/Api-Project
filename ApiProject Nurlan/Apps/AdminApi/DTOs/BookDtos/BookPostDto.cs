using ApiProject_Nurlan.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.DTOs.BookDtos
{
    public class BookPostDto
    {
        public string Name { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
       
        public int PageCount { get; set; }
        public string Language { get; set; }
        public IFormFile ImageFile { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        
    }

    public class BookPostDtoValidator : AbstractValidator<BookPostDto>
    {
        public BookPostDtoValidator()
        {
            RuleFor(x => x.AuthorId).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.GenreId).NotNull().GreaterThanOrEqualTo(1);


            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Uzunluq max 50 ola biler!")
                .NotEmpty().WithMessage("Name mecburidir!");

            RuleFor(x => x.Language)
                .MaximumLength(50).WithMessage("Uzunluq max 50 ola biler , qaqa!")
                .NotEmpty().WithMessage("Language mecburidir!");


            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("CostPrice 0-dan asagi ola bilmez!")
                .NotEmpty().WithMessage("CostPrice mecburidir!");

            RuleFor(x => x.PageCount)
                .GreaterThanOrEqualTo(0).WithMessage("PageCount 0-dan asagi ola bilmez!")
                .NotEmpty().WithMessage("PageCount mecburidir!");

            RuleFor(x => x.SalePrice)
                .GreaterThanOrEqualTo(0).WithMessage("SalePrice 0-dan asagi ola bilmez!")
                .NotEmpty().WithMessage("SalePrice mecburidir!");

            //RuleFor(x => x.DisplayStatus).NotNull().WithMessage("DisplayStatus mecburidir!");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.CostPrice > x.SalePrice)
                    context.AddFailure("CostPrice", "CostPrice SalePrice-dan boyuk ola bilmez");
            });

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile == null)
                {
                    context.AddFailure("ImageFile", "Image cannot be null!");
                }
                else if (!x.ImageFile.IsImage())
                {
                    context.AddFailure("Please select an image file");
                }
                else if (!x.ImageFile.IsSizeOkay(2))
                {
                    context.AddFailure("Image size must be less than 2mb");
                }

            });
        }
    }
}
