using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.ClientEntity.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public List<string> images { get; set; } = new List<string>();

        public Dictionary<string, string> Specification { get; set; } = new Dictionary<string, string>();
    }
}
