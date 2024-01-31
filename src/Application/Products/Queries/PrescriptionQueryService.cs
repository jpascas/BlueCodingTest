using Application.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductRepository repository;
        private readonly IContextProvider contextProvider;

        public ProductQueryService(IProductRepository repository, IContextProvider contextProvider)
        {
            this.repository = repository;
            this.contextProvider = contextProvider;
        }


        public async Task<Product> FindById(Guid id)
        {
            var entity = await this.repository.GetById(id);
            return entity;
        }
    }
}
