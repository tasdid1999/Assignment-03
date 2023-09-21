using Azure;
using Dapper;
using Ecom.ClientEntity.Response;
using Ecom.Entity.DataBase;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        private readonly Context _dbcontext;

        public ProductRepository(SqlConnectionFactory connectionFactory, Context dbcontext)
        {
            _connectionFactory = connectionFactory;
            _dbcontext = dbcontext;
        }
        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var product = await _dbcontext.Products.Where(product => product.Id == id).FirstOrDefaultAsync();

           if (product is not null)
           {
                product.StatusId = 2;

                return true;
           }

            return false;
        }

        public async Task<List<DbProduct>> GetAllProduct(int page , int pageSize)
        {
            using var connection = _connectionFactory.Create();
            var products = await connection.QueryAsync<DbProduct>($"sp_GetAllProduct @page={page} , @pageSize={pageSize}", CommandType.StoredProcedure);
            return products.ToList();
            
        }

        public async Task<List<DbProduct>> GetById(int id)
        {
            using var connection = _connectionFactory.Create();
            var products = await connection.QueryAsync<DbProduct>($"sp_GetProductById @id={id}", CommandType.StoredProcedure);

            return products.ToList();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
