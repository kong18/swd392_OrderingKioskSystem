using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Update
{
    public class UpdateMenuCommandValidator : AbstractValidator<UpdateMenuCommand>
    {
        private readonly string[] validTypes = { "Morning", "Afternoon", "Evening" };
        public UpdateMenuCommandValidator() 
        {
            RuleFor(command => command.ID)
                    .NotEmpty().WithMessage("MenuID can't be empty or null");

            RuleFor(command => command.Type)
                .Must(type => Array.Exists(validTypes, s => s.Equals(type, StringComparison.OrdinalIgnoreCase)))
                .WithMessage("Type must be one of the predefined values: Morning, Afternoon, Evening");
        }
    }
}
