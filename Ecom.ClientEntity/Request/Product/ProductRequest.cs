using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Request.Product
{
    public class ProductRequest
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public List<KeyValuePair<string, string>> Specification { get; set; }

        public IFormFileCollection Images { get; set; }


    }
}
