using Ecom.ClientEntity.Request.Product;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.Specification
{
    public class ProductSpecificationService : IProductSpecificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductSpecificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddSpecification(ProductSpecificationRequest request)
        {
            var specification = new List<ProductSpecification>();

            foreach(var spec in request.Specification)
            {
                specification.Add(new ProductSpecification { ProductId = request.ProductId, Key = spec.Key, Value = spec.Value, CreatedAt = DateTime.Now, CreatedBy = request.UserId,StatusId = 1 });
            }

            await _unitOfWork.ProductSpecificationRepository.AddSpecification(specification);
            

        }
    }
}
