using Azure;
using Dapper;
using Ecom.ClientEntity.Request.Product;
using Ecom.ClientEntity.Response;

using Ecom.Entity.Domain;
using Ecom.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        private readonly Context _dbcontext;

        public ProductRepository(SqlConnectionFactory connectionFactory, Context dbcontext)
        {
            _connectionFactory = connectionFactory;
            _dbcontext = dbcontext;
        }

        public async Task Add(Product product)
        {
            await _dbcontext.Products.AddAsync(product);
        }

        public async Task<Product?> Find(int id)
        {
            return await _dbcontext.Products.FindAsync(id);
        }

        public async Task<List<ProductResponse>> GetAllProduct(int page, int pageSize)
        {
            using var connection = _connectionFactory.Create();

            var productRespnose = new List<ProductResponse>();

            var products = await connection.QueryAsync<ProductDetails, ProductSpecificationResponse, ProductImageResponse, ProductResponse>(
                                                      $"sp_GetAllProduct @page={page}, @pageSize={pageSize}",

                   (product, specification, image) =>
                   {
                       var existingProduct = productRespnose.FirstOrDefault(p => p.Product.Id == product.Id);

                       if (existingProduct == null)
                       {
                           existingProduct = new ProductResponse
                           {
                               Product = product,
                               Specifications = new List<ProductSpecificationResponse>(),
                               Images = new List<string>()
                           };
                           productRespnose.Add(existingProduct);
                       }
                       else
                       {
                           if (specification.Key is not null && !existingProduct.Specifications.Where(x => x.Key == specification.Key).Any())
                           {
                               existingProduct.Specifications.Add(specification);
                           }
                           if (image.ImagePath is not null && !existingProduct.Images.Where(images => images == image.ImagePath).Any())
                           {
                               existingProduct.Images.Add(image.ImagePath);
                           }

                       }


                       return existingProduct;
                   },
                  splitOn: "SpecificationId,ImageId"
            );


            return productRespnose;
        }




        public async Task<ProductResponse?> GetById(int id)
        {
            using var connection = _connectionFactory.Create();

            ProductResponse productResponse = null;

            var products = await connection.QueryAsync<ProductDetails, ProductSpecificationResponse, ProductImageResponse, ProductResponse>($"sp_GetProductById @id={id}",

                (product, specification, image) =>
                {


                    if (productResponse is null)
                    {
                        productResponse = new ProductResponse
                        {
                            Product = product,
                            Specifications = new List<ProductSpecificationResponse>(),
                            Images = new List<string>()
                        };

                    }
                    else
                    {
                        if (specification.Key is not null && !productResponse.Specifications.Where(x => x.Key == specification.Key).Any())
                        {
                            productResponse.Specifications.Add(specification);
                        }
                        if (image.ImagePath is not null && !productResponse.Images.Where(images => images == image.ImagePath).Any())
                        {
                            productResponse.Images.Add(image.ImagePath);
                        }

                    }

                    return productResponse;
                },splitOn: "SpecificationId,ImageId"

                );

            return productResponse;
        }


    }
}

