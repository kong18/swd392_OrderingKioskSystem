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
        private readonly string[] validTypes = { "Morning", "Aternoon", "Evening" };
        public UpdateMenuCommandValidator() 
        {
            RuleFor(command => command.ID)
                    .NotEmpty().WithMessage("MenuID can't be empty or null");

            RuleFor(command => command.Name)
                    .NotEmpty().WithMessage("Name can't be empty or null");

            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("Title can't be empty or null");

            RuleFor(command => command.Type)
                .Must(type => Array.Exists(validTypes, s => s.Equals(type, StringComparison.OrdinalIgnoreCase)))
                .WithMessage("Type must be one of the predefined values: Morning, Afternoon, Evening");

            RuleFor(command => command.Status)
                .NotEmpty().WithMessage("Status can't be empty or null");
        }
    }
}
