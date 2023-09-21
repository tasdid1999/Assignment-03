using Dapper;
using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.SpecificationRepository
{
    public class ProductSpecificationRepository : IProductSpecificationRepository
    {
       
        private readonly SqlConnectionFactory _connectionFactory;
        public ProductSpecificationRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Dictionary<string, string>> GetProductSpecificationsById(int productId)
        {
            using var connection = _connectionFactory.Create();

            var res = await connection.QueryAsync<KeyValuePair<string,string>>($"sp_GetSpecification @productId={productId}");

            return res.ToDictionary(kvp => kvp.Key , kvp => kvp.Value);
            
        }
    }
    
}
