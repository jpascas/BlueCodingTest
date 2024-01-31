using Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateProductCommand : BaseCommand
    {        
        public string Name { get; set; }
        public int Status { get; set; }
        public long Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }


        public CreateProductCommand(string name, int status, long stock, string description, decimal price, string currency)
        {
            this.Name = name;
            this.Status = status;
            this.Stock = stock;
            this.Description = description;
            this.Price = price;
            this.Currency = currency;
        }

        public override bool IsValid()
        {
            var validator = new CreateProductValidation();
            this.ValidationResult = validator.Validate(this);
            return this.ValidationResult.IsValid;
        }
    }
}
