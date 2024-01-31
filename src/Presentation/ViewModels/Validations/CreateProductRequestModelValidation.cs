using Application.Validations;
using FluentValidation;
using Presentation.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Presentation.ViewModels.Validations
{
    public class CreateProductRequestModelValidation : AbstractValidator<CreateProductRequestModel>
    {

        public CreateProductRequestModelValidation()
        {
            ValidateName();
            ValidateStatus();
            ValidateStock();
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name Cannot be Empty");
        }

        protected void ValidateStatus()
        {
            RuleFor(p => p.Status).NotEmpty().WithMessage("Status Cannot be Empty");
        }
        protected void ValidateStock()
        {
            RuleFor(p => p.Stock).NotEmpty().WithMessage("Stock Cannot be Empty");
        }
    }
}
