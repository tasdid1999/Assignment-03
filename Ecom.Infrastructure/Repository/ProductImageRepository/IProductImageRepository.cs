using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ImageRepository
{
    public interface IProductImageRepository
    {
        Task<List<string>> GetAllImageByProductId(int productId);

        Task<bool> DeleteImageByProductId(int productId);

        Task AddImage(ProductImage image);
    }
}
