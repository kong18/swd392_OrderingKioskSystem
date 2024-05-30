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

            

            RuleFor(x => x.ShipperName)
                .NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address can't be empty").MaximumLength(100).WithMessage("Address can't be longer than 100 characters");


            RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+?\d+$").WithMessage("Invalid phone number format");

        }

       
    }
}
