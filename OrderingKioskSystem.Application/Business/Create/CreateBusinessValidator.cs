using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace OrderingKioskSystem.Application.Business.CreateBusinessCommand
{
    public class CreateBusinessValidator : AbstractValidator<CreateBusinessCommand>
    {
        public CreateBusinessValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be empty or null")
                .EmailAddress().WithMessage("Not type of Email");

            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("URL can't be empty");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");

            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("Bank Account Number can't be empty")
                .GreaterThan(0).WithMessage("Bank Account Number must be a numberic");

            RuleFor(x => x.BankAccountName)
                .NotEmpty().WithMessage("Bank Account Name can't be empty")
                .MaximumLength(100).WithMessage("Bank Account Name can't be longer than 100 characters");

            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank Name can't be empty")
                .MaximumLength(100).WithMessage("Bank Name can't be longer than 100 characters");
        }
    }
}
