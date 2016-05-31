using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQPLay
{
    public class Store
    {
        public string Name { get; set; }

        public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
