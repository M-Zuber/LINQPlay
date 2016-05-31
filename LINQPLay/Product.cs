using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQPLay
{
    public enum Category
    {
        Food,
        Clothing,
        Toys,
        Recreation
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
    }
}
