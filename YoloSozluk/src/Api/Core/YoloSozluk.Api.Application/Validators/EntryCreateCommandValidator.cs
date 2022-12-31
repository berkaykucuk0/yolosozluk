using FluentValidation;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Validators
{
    public class EntryCreateCommandValidator : AbstractValidator<EntryCreateCommand>
    {
        public EntryCreateCommandValidator()
        {
            RuleFor(x => x.Content).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x=>x.Subject).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x => x.CreatedUserId).NotNull().WithMessage("Created User cannot be null!");
        }
    }
}
