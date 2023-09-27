using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ProductPriceRepository
{
    public interface IProductPriceRepository
    {
        Task AddPrice(ProductPrice price);
        Task<ProductPrice> Find(int productId);
        Task<bool> UpdatePrice(ProductPrice price);
    }
}
