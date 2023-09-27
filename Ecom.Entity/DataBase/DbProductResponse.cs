using Ecom.ClientEntity.Request.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Entity.DataBase
{
    public class DbProductResponse
    {
      
        public DbProduct Products { get; set; }
        public List<Specification> Specification { get; set; }

        public  List<Images> ImagePath { get; set; }

       
    }
}
