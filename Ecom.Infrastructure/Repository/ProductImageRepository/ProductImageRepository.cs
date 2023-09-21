using Dapper;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ImageRepository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        private readonly Context _context;
        public ProductImageRepository(SqlConnectionFactory connectionFactory, Context context)
        {
            _connectionFactory = connectionFactory;
            _context = context;
        }

        public async Task<bool> DeleteImageByProductId(int productId)
        {
            var images = await _context.Images.Where(image => image.ProductId == productId).ToListAsync();

            if(images is not null)
            {
                foreach (var image in images)
                {
                    image.StatusId = 2;
                }
                return true;
            }

            return false;
        }

        public async Task<List<string>> GetAllImageByProductId(int productId)
        {
            using var connection = _connectionFactory.Create();

            var imageList = await connection.QueryAsync<string>($"sp_getImages @productId={productId}", CommandType.StoredProcedure);

            return imageList.ToList();
        }
    }
}
