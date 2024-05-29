using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.CreateShipper
{
    public class CreateShipperUserCommandValidator : AbstractValidator<CreateShipperUserCommand>
    {
        public CreateShipperUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Role).NotEmpty().Equal("Shipper");
            RuleFor(x => x.ShipperName).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+?\d+$").WithMessage("Invalid phone number format");
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
