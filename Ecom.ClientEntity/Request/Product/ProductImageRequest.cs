using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Request.Product
{
    public class ProductImageRequest
    {
        public IFormFileCollection Images { get; set; }

        public int ProductId { get; set; }

        public string CreatedBy { get; set; }
    }
}
