using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(command => command.name)
                .NotEmpty().WithMessage("Name can't be empty or null")
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

            RuleFor(command => command.url)
                .NotEmpty().WithMessage("Url can't be empty or null");
        }
    }
}
