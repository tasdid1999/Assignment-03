using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Request.Product
{
    public class ProductRequest
    {
       
        public string ProductName { get; set; }
        
        public string Brand { get; set; }
        
        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Specification { get; set; }

        public IFormFileCollection Images { get; set; }


    }
}
