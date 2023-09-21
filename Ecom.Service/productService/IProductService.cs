using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.productService
{
    public interface IProductService
    {
        Task<ProductResponse?> GetProductById(int id);
        Task<List<ProductResponse>> GetAllProduct(int page, int pageSize);
        void Add(Product product);
        void Update(Product product);
        Task<bool> Delete(int id);
    }
}
