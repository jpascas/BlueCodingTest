using Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class CreateProductValidation : AbstractValidator<CreateProductCommand>
    {
   

        public CreateProductValidation()
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
