using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.GetByPagnition
{
    public class GetOrderByPagnitionQueryValidator : AbstractValidator<GetOrderByPagnitionQuery>
    {
        public GetOrderByPagnitionQueryValidator()
        {
            Configure();
        }

        public void Configure()
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
