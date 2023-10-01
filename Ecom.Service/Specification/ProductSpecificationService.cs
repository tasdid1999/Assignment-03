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

        public async Task UpdateSpecification(ProductSpecificationRequest request)
        {
            var listOfSpecification = await _unitOfWork.ProductSpecificationRepository.GetAllByProductId(request.ProductId);

            var newSpecification = new List<ProductSpecification>();

            foreach (var specification in request.Specification)
            {
                var item =  listOfSpecification.Where(x=>x.Key == specification.Key).FirstOrDefault();

                if(item != null)
                {
                    item.Value = specification.Value; 
                }
                else
                {
                    newSpecification.Add(new ProductSpecification { ProductId = request.ProductId, Key = specification.Key, Value = specification.Value, CreatedAt = DateTime.Now, CreatedBy = request.UserId, StatusId = 1 });
                }
            }

            if(newSpecification.Count > 0)
            {
                await _unitOfWork.ProductSpecificationRepository.AddSpecification(newSpecification);
            }
        }
    }
}
