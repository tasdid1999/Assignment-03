using Ecom.ClientEntity.Request.Product;
using Ecom.ClientEntity.Response;
using Ecom.Entity.DataBase;
using Ecom.Entity.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<ProductResponse?> GetById(int id);
        Task<Product?> Find(int id);
        Task<List<ProductResponse>> GetAllProduct(int page , int pageSize);
      
    }
}
