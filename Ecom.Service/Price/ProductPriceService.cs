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
    public class ProductPriceService : IProductPriceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductPriceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddPrice(ProductPriceRequest request)
        {
            var price = new ProductPrice
            {
                Price = request.Price,
                ProductId = request.ProductId,
                CreatedBy = request.UserId,
                CreatedAt = DateTime.UtcNow,
                StatusId = 1,
            };
            await _unitOfWork.ProductPriceRepository.AddPrice(price);
            
        }
    }
}
