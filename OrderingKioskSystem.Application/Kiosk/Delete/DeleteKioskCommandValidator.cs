using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Delete
{
    public class DeleteKioskCommandValidator : AbstractValidator<DeleteKioskCommand>    
    {
        public DeleteKioskCommandValidator() {
            RuleFor(command => command.Id)
              .NotEmpty().WithMessage("ID không để trống.");
        }
    }
}
