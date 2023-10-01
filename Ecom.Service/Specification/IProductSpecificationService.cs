using Ecom.ClientEntity.Request.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.Specification
{
    public interface IProductSpecificationService
    {
        Task AddSpecification(ProductSpecificationRequest request);

        Task UpdateSpecification(ProductSpecificationRequest request);


    }
}
