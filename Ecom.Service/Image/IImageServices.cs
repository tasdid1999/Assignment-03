using Ecom.ClientEntity.Request.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.Image
{
    public interface IImageServices
    {
        Task AddImage(ProductImageRequest request);
    }
}
