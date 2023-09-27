using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Response
{
    public class ProductResponse
    {
        public ProductDetails Product {  get; set; }

        public List<ProductSpecificationResponse> Specifications { get; set; }

        public List<string> Images { get; set; }
    }
}
