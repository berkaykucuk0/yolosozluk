using FluentValidation;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Validators
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} cannot be null!");
        }
    }
}
