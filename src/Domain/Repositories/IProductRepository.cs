using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(Guid id);        
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
    }
}
