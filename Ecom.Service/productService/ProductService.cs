using AutoMapper;
using Azure.Core;
using Ecom.ClientEntity.Request.Product;
using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.UnitOfWork;
using Ecom.Service.Image;
using Ecom.Service.Price;
using Ecom.Service.Specification;
using Ecom.Service.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.productService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageServices _imageService;
        private readonly IProductPriceService _priceService;
        private readonly IProductSpecificationService _specificationService;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IImageServices imageService, IProductPriceService priceService, IProductSpecificationService specificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
            _priceService = priceService;
            _specificationService = specificationService;
        }

        public async Task<bool> Active(int id)
        {
            var product = await _unitOfWork.ProductRepository.Find(id);

            if (product is not null)
            {
                if (product.StatusId == 0)
                {
                    product.StatusId = 1;
                    return await _unitOfWork.SaveChangesAsync() ? true : false;

                }

                return false;
            }

            return false;
        }
        public async Task<bool> InActive(int id)
        {
            var product = await _unitOfWork.ProductRepository.Find(id);

            if (product is not null)
            {
                if (product.StatusId == 1)
                {
                    product.StatusId = 0;
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

            return await _unitOfWork.SaveChangesAsync();
  
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.Find(id);

            if(product is not null && product.StatusId != 2)
            {
               product.StatusId = 2;
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

            var dbProduct = await _unitOfWork.ProductRepository.GetById(productId);

            if(dbProduct is  null)
            {
                return false;
            }

            var newProduct = new Product
            {
                Id = productId,
                Name = product.ProductName,
                Brand = product.Brand,
                Model = product.Model,
                UpdatedBy = updatedBy,
            };

           // var isProductUpdated = await _unitOfWork.ProductRepository.Update(newProduct);

            var newProductPrice = new ProductPrice
            {
                Price = product.Price,
                ProductId = productId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = updatedBy
            };

            var isPriceUpdated = await _unitOfWork.ProductPriceRepository.UpdatePrice(newProductPrice);

            

            
            return true;
        }
    }
}
