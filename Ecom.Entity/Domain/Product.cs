using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Entity.Domain
{
    public class Product : BaseEntity
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Brand { get; set; }

        


    }
}
