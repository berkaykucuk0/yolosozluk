using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Validators
{
    public class UserLoginCommandValidator: AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(i => i.EmailAddress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");

            RuleFor(i => i.Password)
            .NotNull()
            .MinimumLength(6)
            .WithMessage("{PropertyName} should at least be {MinLength} characters");
        }
    }
}
