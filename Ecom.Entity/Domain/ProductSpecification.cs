using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Entity.Domain
{
    public class ProductSpecification : BaseEntity
    {

        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
