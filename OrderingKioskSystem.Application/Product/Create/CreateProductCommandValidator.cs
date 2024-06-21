﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name can't be empty or null")
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

            RuleFor(command => command.Code)
                .NotEmpty().WithMessage("Code can't be empty or null");

          
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Description can't be empty or null")
                .MaximumLength(150).WithMessage("Description can't be over 150 words");

            RuleFor(command => command.Price)
                .NotEmpty().WithMessage("Price can't be empty or null")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(command => command.CategoryID)
                .NotEmpty().WithMessage("CategoryID can't be empty or null");

            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("URL can't be empty");
        }
    }
}
