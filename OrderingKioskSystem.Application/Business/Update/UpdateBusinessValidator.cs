﻿using FluentValidation;

namespace OrderingKioskSystem.Application.Business.Update
{
    public class UpdateBusinessValidator : AbstractValidator<UpdateBusinessCommand>
    {
        public UpdateBusinessValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id can't be empty");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL can't be empty")
                .Must(BeAValidUrl).WithMessage("Invalid URL format");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");

            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("Bank Account Number can't be empty")
                .Matches(@"^\d+$").WithMessage("Bank Account Number must be numeric");

            RuleFor(x => x.BankAccountName)
                .NotEmpty().WithMessage("Bank Account Name can't be empty")
                .MaximumLength(100).WithMessage("Bank Account Name can't be longer than 100 characters");

            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank Name can't be empty")
                .MaximumLength(100).WithMessage("Bank Name can't be longer than 100 characters");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
