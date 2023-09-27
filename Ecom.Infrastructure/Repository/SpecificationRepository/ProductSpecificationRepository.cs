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
        private readonly Context _dbContext;
        public ProductSpecificationRepository(SqlConnectionFactory connectionFactory, Context dbContext)
        {
            _connectionFactory = connectionFactory;
            _dbContext = dbContext;
        }

        public async Task AddSpecification(List<ProductSpecification> specifications)
        {
            await _dbContext.Specifications.AddRangeAsync(specifications);
        }
    }
    
}
