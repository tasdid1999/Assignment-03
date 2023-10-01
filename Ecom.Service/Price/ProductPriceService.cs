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

        public async Task UpdatePrice(ProductPrice price)
        {
           var existingPrice =  await _unitOfWork.ProductPriceRepository.FindByProductId(price.ProductId);

            if (price is not null)
            {
                if(existingPrice.Price != price.Price)
                {
                    var newPrice = new ProductPrice {

                        Price = price.Price,
                        ProductId = price.ProductId,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = price.UpdatedBy,
                        StatusId = 1,
                    };
                    await _unitOfWork.ProductPriceRepository.AddPrice(newPrice);
                }
            }


        }
    }
}
