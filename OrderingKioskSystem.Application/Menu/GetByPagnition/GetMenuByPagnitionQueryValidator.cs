using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.GetByPagnition
{
    public class GetMenuByPagnitionQueryValidator : AbstractValidator<GetMenuByPagnitionQuery>
    {
        public GetMenuByPagnitionQueryValidator()
        {
            RuleFor(x => x.PageSize).NotEmpty()
                .NotNull()
                .WithMessage("PageSize not null or empty");

            RuleFor(x => x.PageNumber).NotEmpty()
                .NotNull()
                .WithMessage("PageNumber not null or empty");
        }
    }
}
