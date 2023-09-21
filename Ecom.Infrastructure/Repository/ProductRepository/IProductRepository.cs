using Ecom.ClientEntity.Response;
using Ecom.Entity.DataBase;
using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<DbProduct>> GetById(int id);

        Task<List<DbProduct>> GetAllProduct(int page , int pageSize);
        void Add(Product product);
        void Update(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
