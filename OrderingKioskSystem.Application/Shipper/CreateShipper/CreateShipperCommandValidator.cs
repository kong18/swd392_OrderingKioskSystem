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
            RuleFor(x => x.Role).NotEmpty().Equal("Shipper");
            RuleFor(x => x.ShipperName).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+?\d+$").WithMessage("Invalid phone number format");
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
