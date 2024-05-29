using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.CreateManager
{
    public class CreateManagerUserCommandValidator : AbstractValidator<CreateManagerUserCommand>
    {
        public CreateManagerUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Role).NotEmpty().Equal("Manager");
            RuleFor(x => x.ManagerName).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+?\d+$").WithMessage("Invalid phone number format");
        }
    }
}
