using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.SpecificationRepository
{
    public interface IProductSpecificationRepository
    {
        Task AddSpecification(List<ProductSpecification> specifications);

        Task<List<ProductSpecification>> GetAllByProductId(int productId);
    }
}
