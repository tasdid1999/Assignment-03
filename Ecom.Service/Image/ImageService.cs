using Ecom.ClientEntity.Request.Product;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.Image
{
    public class ImageService : IImageServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        public ImageService(IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        public async Task AddImage(ProductImageRequest request)
        { 
            foreach (var image in request.Images)
            {
                string folder = "product/gallery/";

                var path = await UploadImage(folder, image);
              
                await _unitOfWork.ProductImageRepository.AddImage(new ProductImage { ProductId = request.ProductId, ImagePath = path , CreatedAt = DateTime.Now,CreatedBy = request.CreatedBy,StatusId = 1 });
                
                 
            }
            
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
