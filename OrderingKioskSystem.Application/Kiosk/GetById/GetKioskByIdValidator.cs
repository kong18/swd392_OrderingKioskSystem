using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.GetById
{
    public class GetKioskByIdValidator : AbstractValidator<GetKioskByIdQuery>
    {
        public GetKioskByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id not null");  
        }
    }
}
