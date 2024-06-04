using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Update
{
    public class UpdateProductMenuCommandValidator : AbstractValidator<UpdateProductMenuCommand>
    {
        public UpdateProductMenuCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("ProductMenuID can't be null or empty");
        }
    }
}
