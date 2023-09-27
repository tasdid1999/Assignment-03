using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Response
{
    public class ProductDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public Decimal Price { get; set; }
       
    }
}
