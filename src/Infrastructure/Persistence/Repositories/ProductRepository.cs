using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistency.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : PostgresqlRepositoryBase<BlueCodingConnectionConfig>, IProductRepository
    {
        public ProductRepository(BlueCodingConnectionConfig config) : base(config)
        {
        }

        public Task<Product> Create(Product product)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>()
            {                        
                        GetNamedParameter("p_name", product.Name, NpgsqlTypes.NpgsqlDbType.Text),
                        GetNamedParameter("p_stock", product.Stock, NpgsqlTypes.NpgsqlDbType.Bigint),
                        GetNamedParameter("p_description", product.Description, NpgsqlTypes.NpgsqlDbType.Text),
                        GetNamedParameter("p_created_by", product.CreatedBy, NpgsqlTypes.NpgsqlDbType.Bigint),                        
            };                       
            return Task.FromResult(this.ExecuteAsSingleOrDefault<Product>("sp_insert_product", parameters.ToArray(), Mapper));            
        }
        public Task<Product> GetById(Guid id)
        {
            NpgsqlParameter[] parameters =
            {
                GetNamedParameter("p_id", id, NpgsqlTypes.NpgsqlDbType.Uuid)
            };
            return Task.FromResult(this.ExecuteAsSingleOrDefault<Product>("sp_getbyid_product", parameters, Mapper));
        }

        public static Product Mapper(NpgsqlDataReader reader)
        {
            Product product = new Product();
            product.ProductId = reader.Get<Guid>("product_id");
            product.Name = reader.Get<string>("Name");
            product.Stock = reader.Get<long>("stock");
            product.Description = reader.Get<string>("description");
            product.Price = reader.Get<decimal>("price");
            product.CreatedBy = reader.Get<long>("created_by");
            product.CreatedAt = reader.Get<DateTime>("created_at");
            product.ModifiedBy = reader.Get<long>("modified_by");
            product.ModifiedAt = reader.Get<DateTime>("modified_at");
            return product;
        }

        public Task<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
