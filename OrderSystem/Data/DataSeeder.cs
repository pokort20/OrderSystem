using OrderSystem.Models;

namespace OrderSystem.Data
{
    public static class DataSeeder
    {
        public static void Seed(MainDbContext db, int numberOfCustomers, int numberOfOrders)
        {
            //If there already is data, don't create more
            if (db.Statuses.Any()) return;

            Random rnd = new Random();
            //Create statuses
            var statuses = new[]
            {
                new Status{ StatusCode = 'P', Name = "Processing"},
                new Status{ StatusCode = 'S', Name = "Shipped"},
                new Status{ StatusCode = 'C', Name = "Cancelled"}
            };
            //Add statuses
            db.Statuses.AddRange(statuses);

            //Create customers
            var customers = Enumerable.Range(1, numberOfCustomers)
                .Select(i => new Customer
                {
                    Name = "Customer_" + i.ToString()
                }).ToArray();
            //Add customers
            db.Customers.AddRange(customers);

            //save db changes so that the FKs are ready for orders
            db.SaveChanges();

            //Create orders
            var orders = Enumerable.Range(1, numberOfOrders)
                .Select(i => new Order
                {
                    Amount = Math.Round(rnd.Next(1, int.MaxValue) + (decimal)rnd.NextDouble(), 2),
                    Date = new DateTime(rnd.NextInt64(630822816000000000, DateTime.Now.Ticks)), // 1.1.2000 - now
                    CustomerId = customers[rnd.Next(customers.Length)].Id,
                    StatusId = statuses[rnd.Next(statuses.Length)].Id,
                }).ToArray();

            //Add orders
            db.Orders.AddRange(orders);

            //save changes
            db.SaveChanges();
        }
    }
}
