using Ecom.ClientEntity.Request.Product;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.Price
{
    public interface IProductPriceService
    {
        Task AddPrice(ProductPriceRequest request);
       
        Task UpdatePrice(ProductPrice price);
    }
}
