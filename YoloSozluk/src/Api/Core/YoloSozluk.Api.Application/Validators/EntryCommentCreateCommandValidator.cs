using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Validators
{
    public class EntryCommentCreateCommandValidator : AbstractValidator<EntryCommentCreateCommand>
    {
        public EntryCommentCreateCommandValidator()
        {
            RuleFor(x => x.Content).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x => x.EntryId).NotNull().WithMessage("{PropertyName} cannot be null!");
            RuleFor(x => x.UserId).NotNull().WithMessage("{PropertyName} cannot be null!");
        }
    }
}
