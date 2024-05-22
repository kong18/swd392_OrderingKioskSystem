using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetBusinessById
{
    public class GetBusinessValidator : AbstractValidator<GetBusinessByIdQuery> 
    {
        public GetBusinessValidator() {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id must be filled");
        }
    }
}
