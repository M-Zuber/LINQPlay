using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQPLay
{
    public class Order
    {
        public IDictionary<Product, OrderDetails> Products { get; set; } = new Dictionary<Product, OrderDetails>();
        public decimal TotalCost => Products.Sum(kev => kev.Value.TotalCost);
    }

    public class OrderDetails
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal TotalCost => Product.Price * Count;
    }
}
