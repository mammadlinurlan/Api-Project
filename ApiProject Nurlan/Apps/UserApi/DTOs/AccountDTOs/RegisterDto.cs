using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.UserApi.DTOs.AccountDTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(20).NotEmpty();

            RuleFor(x => x.FullName).NotEmpty().MaximumLength(25).MinimumLength(4);

            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(20).NotEmpty();
            RuleFor(x => x.ConfirmPassword).MinimumLength(8).MaximumLength(20).NotEmpty();


            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                    context.AddFailure("ConfirmPassword", "Password ConfirmPassword-e beraber olmalidir!");
            });
        }
    }
}
