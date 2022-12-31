using FluentValidation;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Validators
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x => x.LastName).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x => x.Email).NotNull()
                                 .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                                 .WithMessage("{PropertyName} cannot be null!");
        }
    }
}
