using AutoMapper;
using Ecom.API.Validator;
using Ecom.Entity.Helper;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repository.authRepository;
using Ecom.Infrastructure.Repository.ImageRepository;
using Ecom.Infrastructure.Repository.ProductPriceRepository;
using Ecom.Infrastructure.Repository.ProductRepository;
using Ecom.Infrastructure.Repository.SpecificationRepository;
using Ecom.Infrastructure.Repository.UserRepository;
using Ecom.Infrastructure.UnitOfWork;
using Ecom.Service.authService;
using Ecom.Service.Image;
using Ecom.Service.Mapper;
using Ecom.Service.Price;
using Ecom.Service.productService;
using Ecom.Service.Specification;
using Ecom.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

//repository
builder.Services.AddScoped<IAuthRepository , AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPriceRepository,ProductPriceRepository>();
builder.Services.AddScoped<IProductImageRepository,ProductImageRepository>();
builder.Services.AddScoped<IProductSpecificationRepository, ProductSpecificationRepository>();

//services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductPriceService, ProductPriceService>();
builder.Services.AddScoped<IProductSpecificationService,ProductSpecificationService>();
builder.Services.AddScoped<IImageServices, ImageService>();
builder.Services.AddScoped<ProductValidator>();


//unitofwork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//automapper configuration
var automapper = new MapperConfiguration(item => item.AddProfile(new MapperHandler()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);

//jwt config
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey"))),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});
//database configuration for Dapper
builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("conn") ?? throw new Exception("connection string not found");

    return new SqlConnectionFactory(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
