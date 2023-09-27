using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Entity.DataBase
{
    public class DbProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public Decimal Price { get; set; }

        public int StatusId { get; set; }
    }
}
