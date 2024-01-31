using Application.Abstractions;
using Application.Commands;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, Product>
    {

        private readonly IProductRepository repository;
        private readonly IContextProvider contextProvider;

        public CreateProductHandler
            (IProductRepository repository, IContextProvider contextProvider)
        {
            this.repository = repository;            
            this.contextProvider = contextProvider;
        }

        public async Task<OperationResult<Product>> Handle(CreateProductCommand command)
        {
            if (!command.IsValid())
                return OperationResult<Product>.FailureResult("Input is Invalid");

            var currentUserId = this.contextProvider.GetCurrentUserId();
            var newProduct = new Product() {
                Name = command.Name, Status = command.Status, Stock = command.Stock,
                Description = command.Description, CreatedBy = currentUserId
            };
            var createdProduct = await this.repository.Create(newProduct);
            return OperationResult<Product>.SuccessResult(createdProduct);
        }
    }
}
