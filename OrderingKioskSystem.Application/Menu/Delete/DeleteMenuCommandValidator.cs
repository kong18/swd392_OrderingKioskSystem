using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Delete
{
    public class DeleteMenuCommandValidator : AbstractValidator<DeleteMenuCommand>
    {
        public DeleteMenuCommandValidator() 
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("MenuID can't be empty or null");
        }
    }
}
