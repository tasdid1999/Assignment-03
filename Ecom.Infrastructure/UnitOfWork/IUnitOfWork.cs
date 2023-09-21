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
    public interface IUnitOfWork
    {
      
        IAuthRepository AuthRepository { get; }

        IUserRepository UserRepository { get; }

        IProductRepository ProductRepository { get; }

        IProductImageRepository ProductImageRepository { get; }

        IProductSpecificationRepository ProductSpecificationRepository { get; }

        Task<bool> SaveChangeAsync();
    }
}
