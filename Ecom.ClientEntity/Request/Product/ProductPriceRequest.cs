using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Request.Product
{
    public class ProductPriceRequest
    {
        public Decimal Price { get; set; }

        public int ProductId { get; set; }
        public string UserId { get; set; }


    }
}
