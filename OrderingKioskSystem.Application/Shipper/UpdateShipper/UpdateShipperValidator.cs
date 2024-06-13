using FluentValidation;
using OrderingKioskSystem.Application.Business.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.UpdateShipper
{
    public class UpdateShipperValidator : AbstractValidator<UpdateShipperCommand>
    {
        public UpdateShipperValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id can't be empty");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");

            RuleFor(x => x.Address).MaximumLength(100).WithMessage("Address can't be longer than 100 characters");

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number format. The phone number must contain exactly 10 digits.");

        }

       
    }
}
