using AutoMapper;
using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using Ecom.Infrastructure.UnitOfWork;
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
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            var isDeleted = await _unitOfWork.ProductRepository.DeleteAsync(id);


            if (isDeleted)
            {
                await _unitOfWork.ProductImageRepository.DeleteImageByProductId(id);
                return await _unitOfWork.SaveChangeAsync();
            }

            return false;
        }

        public async Task<List<ProductResponse>> GetAllProduct(int page , int pageSize)
        {
            var products = await _unitOfWork.ProductRepository.GetAllProduct(page, pageSize);

            var productId = new List<int>();
            var ProductResponse = new List<ProductResponse>();

            foreach(var product in products)
            {
                if (productId.Exists(x => x == product.Id))
                {
                    var ExistingProduct = ProductResponse.Where(x => x.Id == product.Id).FirstOrDefault();

                    //image inserting
                    if(ExistingProduct.images.Count == 0 || !ExistingProduct.images.Exists(x => x == product.ImagePath))
                    {
                        ExistingProduct.images.Add(product.ImagePath);
                    }
                   
                    //specification inserting

                    if(ExistingProduct.Specification.Count == 0 || !ExistingProduct.Specification.ContainsKey(product.Key))
                    {
                        ExistingProduct.Specification.Add(product.Key,product.Value);
                    }

                }
                else
                {
                    productId.Add(product.Id);
                    ProductResponse.Add(new ProductResponse { Id = product.Id, Brand = product.Brand, Model = product.Model, Name = product.Name }) ;
                }
            }

            return ProductResponse;

        }

        public async Task<ProductResponse?> GetProductById(int id)
        {
            var productInfo = await _unitOfWork.ProductRepository.GetById(id);

            ProductResponse? productResponse = null;

            if(productInfo is null)
            {
                return productResponse;
            }
            foreach(var info in productInfo)
            {
                if (productResponse is null)
                {
                    productResponse = new ProductResponse { Id = info.Id, Brand = info.Brand, Name = info.Name, Model = info.Model };
                }
                else
                {
                    if (productResponse.images.Count == 0 || !productResponse.images.Exists(x => x == info.ImagePath))
                    {
                        productResponse.images.Add(info.ImagePath);
                    }
                    if (productResponse.Specification.Count == 0 || !productResponse.Specification.ContainsKey(info.Key))
                    {
                        productResponse.Specification.Add(info.Key, info.Value);
                    }
                }
            
                   
            }

            return productResponse;

        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
