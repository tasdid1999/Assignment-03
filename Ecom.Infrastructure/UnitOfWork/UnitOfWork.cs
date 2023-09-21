using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repository.authRepository;
using Ecom.Infrastructure.Repository.ImageRepository;
using Ecom.Infrastructure.Repository.ProductRepository;
using Ecom.Infrastructure.Repository.SpecificationRepository;
using Ecom.Infrastructure.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _dbcontext;
        public UnitOfWork(IAuthRepository authRepository,
                          IUserRepository userRepository,
                          IProductRepository productRepository,
                          IProductImageRepository ProductImageRepository,
                          IProductSpecificationRepository ProductSpecificationRepository,
                          Context dbcontext)
        {
            this.AuthRepository = authRepository;
            this.UserRepository = userRepository;
            this.ProductRepository = productRepository;
            this.ProductImageRepository = ProductImageRepository;
            this.ProductSpecificationRepository = ProductSpecificationRepository;
            _dbcontext = dbcontext;
        }
        public IAuthRepository AuthRepository { get; set; } 

        public IUserRepository UserRepository {  get; set; }

        public IProductRepository ProductRepository { get; set; }

        public IProductImageRepository ProductImageRepository { get; set; }

        public IProductSpecificationRepository ProductSpecificationRepository { get; set; }

        public async Task<bool> SaveChangeAsync()
        {
            return await _dbcontext.SaveChangesAsync() > 0;
        }
    }
}
