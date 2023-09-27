using AutoMapper;
using Ecom.ClientEntity.Response;
using Ecom.Entity.DataBase;
using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.Mapper
{
    public class MapperHandler : Profile
    {
        public MapperHandler()
        {
            CreateMap<Product, ProductDetails>();
            CreateMap<DbProduct , ProductDetails>();
        }
    }
}
