using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Update
{
    public class UpdateKioskValidator : AbstractValidator<UpdateKioskCommand>
    {
        public UpdateKioskValidator() {
            RuleFor(cmd => cmd.Id)
         .NotEmpty().WithMessage("ID not null");

            RuleFor(cmd => cmd.location)
         .NotEmpty().WithMessage("location not null");
        }
    }
}
