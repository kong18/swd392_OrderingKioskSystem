using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.GetById
{
    public class GetMenuByIdQueryValidator : AbstractValidator<GetMenuByIdQuery>
    {
        public GetMenuByIdQueryValidator() 
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("MenuID can't be empty or null");
        }
    }
}
