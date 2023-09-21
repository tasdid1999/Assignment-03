using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Entity.Domain
{
    public class ProductPrice : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
