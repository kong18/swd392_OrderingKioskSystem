using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.GetShipperById
{
    public class GetShipperByIdValidator : AbstractValidator<GetShipperByIdQuery>
    {
        public GetShipperByIdValidator() {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID must not be empty or null");

        }
    }
}
