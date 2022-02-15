using ApiProject_Nurlan.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.DTOs.AuthorDtos
{
    public class AuthorPostDto
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class AuthorPostDtoValidator : AbstractValidator<AuthorPostDto>
    {
        public AuthorPostDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Genre name must be less than 50chars").NotEmpty().WithMessage("This fill is required!");
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile == null)
                {
                    context.AddFailure("ImageFile","Image cannot be null!");
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
