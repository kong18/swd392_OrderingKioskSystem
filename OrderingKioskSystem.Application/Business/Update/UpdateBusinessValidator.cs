using FluentValidation;

namespace OrderingKioskSystem.Application.Business.Update
{
    public class UpdateBusinessValidator : AbstractValidator<UpdateBusinessCommand>
    {
        public UpdateBusinessValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id can't be empty");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");

            RuleFor(x => x.BankAccountNumber)
                .GreaterThan(0).WithMessage("Bank Account Number must be numeric");

            RuleFor(x => x.BankAccountName)
                .MaximumLength(100).WithMessage("Bank Account Name can't be longer than 100 characters");

            RuleFor(x => x.BankName)
                .MaximumLength(100).WithMessage("Bank Name can't be longer than 100 characters");
        }
    }
}
