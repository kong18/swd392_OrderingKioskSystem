using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.CreateShipper
{
    public class CreateShipperCommandValidator : AbstractValidator<CreateShipperCommand>
    {
        public CreateShipperCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can't be empty");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number format. The phone number must contain exactly 10 digits.");
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
