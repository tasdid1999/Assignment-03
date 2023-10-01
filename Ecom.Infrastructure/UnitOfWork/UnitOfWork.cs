using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repository.authRepository;
using Ecom.Infrastructure.Repository.ImageRepository;
using Ecom.Infrastructure.Repository.ProductPriceRepository;
using Ecom.Infrastructure.Repository.ProductRepository;
using Ecom.Infrastructure.Repository.SpecificationRepository;
using Ecom.Infrastructure.Repository.UserRepository;
using Microsoft.AspNetCore.Identity;
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
        private readonly SqlConnectionFactory _connectionFactory;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UnitOfWork(Context dbcontext,
                          SqlConnectionFactory connectionFactory,
                          UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager,
                          RoleManager<IdentityRole> roleManager)
        {
            _dbcontext = dbcontext;
            _connectionFactory = connectionFactory;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            AuthRepository = new AuthRepository(_userManager, _signInManager, _roleManager);
            UserRepository =new UserRepository(_userManager);
            ProductRepository = new ProductRepository(_connectionFactory, _dbcontext);
            ProductImageRepository = new ProductImageRepository(_connectionFactory, _dbcontext);
            ProductSpecificationRepository = new ProductSpecificationRepository(_connectionFactory, _dbcontext);
            ProductPriceRepository  = new ProductPriceRepository(_connectionFactory, _dbcontext);

        }
        public IAuthRepository AuthRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }
        public IProductImageRepository ProductImageRepository { get; private set; }

        public IProductSpecificationRepository ProductSpecificationRepository { get; private set; }

        public IProductPriceRepository ProductPriceRepository { get; private set; }

        public async Task<bool>  SaveChangesAsync()
        {
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        
    }
}
