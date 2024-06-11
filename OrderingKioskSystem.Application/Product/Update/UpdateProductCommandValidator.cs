using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("ID can't be empty or null");

            RuleFor(command => command.Name)
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

            RuleFor(command => command.Description)
                .MaximumLength(150).WithMessage("Description can't be over 150 words");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

        }
    }
}
