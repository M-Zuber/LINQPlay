using GenFu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQPLay
{
    class Program
    {
        static int MinCount = 10;
        static int MaxCount = 20;

        static void Main(string[] args)
        {
            var store = SetupStore();

            var totalPossibleProfit =
                store.Products
                    .Select(p => new { Profit = p.Price - p.Cost })
                    .Sum(p => p.Profit);

            var possibleProfitByCategory =
                store.Products
                    .Select(p => new { Category = p.Category, Profit = p.Price - p.Cost })
                    .GroupBy(p => p.Category)
                    .ToDictionary(p => p.Key, p => p.Sum(pf => pf.Profit));
        }

        static Store SetupStore()
        {
            var randomizer = new Random();

            // Setup products
            A.Configure<Product>()
             .Fill(p => p.Price).WithinRange(1, int.MaxValue)
             .Fill(p => p.Cost).WithinRange(1, int.MaxValue)
             .Fill(p => p.Category).WithRandom(Enum.GetValues(typeof(Category)).OfType<Category>());

            var products = A.ListOf<Product>(randomizer.Next(MinCount, MaxCount));

            // Set category
            var categories = Enum.GetValues(typeof(Category)).OfType<Category>().ToList();

            products.ForEach(p => p.Category = categories[A.Random.Next(0, categories.Count - 1)]);

            // Create a list of customers
            var customers = A.ListOf<Customer>(randomizer.Next(MinCount, MaxCount));

            // Create a List<List<OrderDetails>> to turn into Dictionary<Product, OrderDetails>
            A.Configure<OrderDetails>()
             .Fill(od => od.Product).WithRandom(products);

            var orderDetails = new List<List<OrderDetails>>();
            FillListOfList(orderDetails, randomizer.Next(MinCount, MaxCount), randomizer.Next(MinCount, MaxCount));

            var orderDetailsDictionariefied =
                orderDetails
                    .Select(l => l.GroupBy(p => p.Product.ID))
                    .Select(g => g.ToDictionary(k => k.First().Product, v => new OrderDetails { Product = v.First().Product, Count = v.Sum(i => i.Count) }))
                    .ToList();

            // Give each customer orders
            foreach (var customer in customers)
            {
                // Create list of orders
                var orders = A.ListOf<Order>(randomizer.Next(MinCount, MaxCount));

                // Add a random List of order details to each order
                foreach (var order in orders)
                {
                    order.Products = orderDetailsDictionariefied[A.Random.Next(0, orderDetailsDictionariefied.Count - 1)];
                }

                customer.Orders = orders;
            }

            var store = new Store { Customers = customers, Products = products };
            return store;
        }

        static void FillListOfList<T>(List<List<T>> list, int outerCount = 25, int innerCount = 25) where T : new()
        {
            Enumerable.Range(0, outerCount).ToList().ForEach(i => list.Add(A.ListOf<T>(innerCount)));
        }
    }
}
