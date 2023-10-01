using Azure.Core;
using Ecom.ClientEntity.Request.Product;
using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.UnitOfWork;
using Ecom.Service.Image;
using Ecom.Service.Price;
using Ecom.Service.Specification;
using Ecom.Service.Token;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;


namespace Ecom.Service.productService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageServices _imageService;
        private readonly IProductPriceService _priceService;
        private readonly IProductSpecificationService _specificationService;
       

        public ProductService(IUnitOfWork unitOfWork,
                              IImageServices imageService,
                              IProductPriceService priceService,
                              IProductSpecificationService specificationService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _priceService = priceService;
            _specificationService = specificationService;
        }

        public async Task<bool> Active(int id,string token)
        {
            var product = await _unitOfWork.ProductRepository.Find(id);
            var updatedBy = JwtTokenFactory.GetUserIdFromToken(token);

            if (product is not null)
            {
                if (product.StatusId == 0)
                {
                    product.StatusId = 1;
                    product.UpdatedBy = updatedBy;
                    product.UpdatedAt = DateTime.UtcNow;

                    return await _unitOfWork.SaveChangesAsync() ? true : false;

                }

                return false;
            }

            return false;
        }
        public async Task<bool> InActive(int id,string token)
        {
            var product = await _unitOfWork.ProductRepository.Find(id);
            var updatedBy = JwtTokenFactory.GetUserIdFromToken(token);

            if (product is not null)
            {
                if (product.StatusId == 1)
                {
                    product.StatusId = 0;
                    product.UpdatedBy = updatedBy;
                    product.UpdatedAt = DateTime.UtcNow;

                    return await _unitOfWork.SaveChangesAsync() ? true : false;

                }

                return false;
            }

            return false;
        }

        public async Task<bool> Add(ProductRequest request,string token)
        {
            var CreatedBy = JwtTokenFactory.GetUserIdFromToken(token);

            var product = new Product
            {
                Id = 0,
                Name = request.ProductName,
                Brand = request.Brand,
                Model = request.Model,
                StatusId = 1,
                CreatedAt = DateTime.Now,
                CreatedBy = CreatedBy


            };
            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();

            var LastInsertedProductById =  product.Id;
            
            //product price add section
            var price = new ProductPriceRequest
            {
                Price = request.Price,
                ProductId = LastInsertedProductById,
                UserId = CreatedBy
            };
            await _priceService.AddPrice(price);
           


            //product specification add section
            var specificationData = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(request.Specification);

            var specificationRequest = new ProductSpecificationRequest
            {
                UserId = CreatedBy,
                ProductId = LastInsertedProductById,
                Specification = specificationData
            };
            await _specificationService.AddSpecification(specificationRequest);


            //image section
            var productImage = new ProductImageRequest
            {
                Images = request.Images,
                ProductId = LastInsertedProductById,
                CreatedBy = CreatedBy,
            };
            await _imageService.AddImage(productImage);

            //final commit
            return await _unitOfWork.SaveChangesAsync();
  
        }

        public async Task<bool> Delete(int id, string token)
        {
            var product = await _unitOfWork.ProductRepository.Find(id);

            var updatedBy = JwtTokenFactory.GetUserIdFromToken(token);

            if (product is not null && product.StatusId != 2)
            {
               product.StatusId = 2;
               product.UpdatedBy = updatedBy;
               product.UpdatedAt = DateTime.UtcNow;

               await _unitOfWork.SaveChangesAsync();
               return true;
            }

            return false;
        }

        public async Task<List<ProductResponse>> GetAllProduct(int page , int pageSize)
        {
            var products = await _unitOfWork.ProductRepository.GetAllProduct(page, pageSize);

            return products;

        }

        public async Task<ProductResponse?> GetProductById(int id)
        {
           return await _unitOfWork.ProductRepository.GetById(id);
        }

  
        public async Task<bool> Update(ProductRequest product , int productId, string token)
        {
            var updatedBy = JwtTokenFactory.GetUserIdFromToken(token);

            //product update
            var existingProduct = await _unitOfWork.ProductRepository.Find(productId);

            if (existingProduct is not null)
            {
                existingProduct.Brand = product.Brand;
                existingProduct.Model = product.Model;
                existingProduct.Name = product.ProductName;
                existingProduct.UpdatedAt = DateTime.UtcNow;
                existingProduct.UpdatedBy = updatedBy;
            }
            // price update

            var updatedPrice = new ProductPrice
            {
                Price = product.Price,
                ProductId = productId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = updatedBy,

            };
            await _priceService.UpdatePrice(updatedPrice);

            // specification

            var specificationData = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(product.Specification);

            var specificationRequest = new ProductSpecificationRequest
            {
                UserId = updatedBy,
                ProductId = productId,
                Specification = specificationData
            };

            await _specificationService.UpdateSpecification(specificationRequest);

            return await _unitOfWork.SaveChangesAsync();

        }
    }
}
