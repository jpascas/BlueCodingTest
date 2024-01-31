using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IProductQueryService
    {
        Task<Product> FindById(Guid id);        
    }
}
