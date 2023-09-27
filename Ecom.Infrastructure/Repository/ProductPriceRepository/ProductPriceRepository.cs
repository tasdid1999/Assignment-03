using Ecom.Entity.Domain;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ProductPriceRepository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly Context _dbcontext;
        private readonly SqlConnectionFactory _connectionFactory;

        public ProductPriceRepository(SqlConnectionFactory connectionFactory,Context dbcontext)
        {
            _dbcontext = dbcontext;
            _connectionFactory = connectionFactory;
        }

        public async Task AddPrice(ProductPrice price)
        {
            await _dbcontext.Prices.AddAsync(price);
        }

        public async Task<ProductPrice?> Find(int productId)
        {
            return await _dbcontext.Prices.Where(x => x.ProductId == productId).FirstOrDefaultAsync();  
        }

        public async Task<bool> UpdatePrice(ProductPrice price)
        {
           var dbPrice = await _dbcontext.Prices.Where(x => x.ProductId == price.ProductId).LastOrDefaultAsync();

            if(dbPrice is not null)
            {
                if(price.Price != dbPrice.Price)
                {
                    var newPrice = new ProductPrice
                    {

                        Price = price.Price,
                        CreatedAt = DateTime.Now,
                        CreatedBy = dbPrice.UpdatedBy,
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = dbPrice.UpdatedBy,
                        StatusId = 1,

                    };

                    await _dbcontext.Prices.AddAsync(newPrice);
                    await _dbcontext.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }
    }
}
