using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.DTOs
{
    public class GenrePostDto
    {
        public string Name { get; set; }

    }

    public class GenrePostDtoValidator : AbstractValidator<GenrePostDto>
    {
        public GenrePostDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Genre name must be less than 50chars").NotEmpty().WithMessage("This fill is required!");
        }
    }
}
