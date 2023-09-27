using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Request.Product
{
    public class ProductSpecificationRequest
    {
        public List<KeyValuePair<string, string>> Specification;

        public string UserId { get; set; }

        public int ProductId { get; set; }
    }
}
