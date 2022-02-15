using ApiProject_Nurlan.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.DTOs.AuthorDtos
{
    public class AuthorUpdateDto
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
    {
        public AuthorUpdateDtoValidator()
        {

            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Genre name must be less than 50chars").NotEmpty().WithMessage("This fill is required!");
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile != null)
                {
                    if (!x.ImageFile.IsImage())
                    {
                        context.AddFailure("Please select an image file");
                    }
                    else if (!x.ImageFile.IsSizeOkay(2))
                    {
                        context.AddFailure("Image size must be less than 2mb");
                    }
                }
            });
        }
    }
}
