using Ecom.ClientEntity.Request.Product;
using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        Task<bool> Add(ProductRequest product,string token);
        Task<bool> Update(ProductRequest product , int productId,string token);
        Task<bool> Delete(int id,string token);
        Task<bool> Active(int id,string token);
        Task<bool> InActive(int id,string token);

        
    }
}
