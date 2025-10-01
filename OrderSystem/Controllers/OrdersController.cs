using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;

namespace OrderSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MainDbContext _db;
        public OrdersController(MainDbContext db) 
        {
            _db = db;
        }
        private void CalculateOrderInfo(List<Models.Order> orderList)
        {
            var sumAmount = orderList.Sum(o => o.Amount);
            var avgAmount = orderList.Any() ? orderList.Average(o => o.Amount) : 0m;
            var topCustomer = orderList
                        .GroupBy(o => o.Customer.Name)
                        .Select(g => new { Customer = g.Key, Total = g.Sum(o => o.Amount) })
                        .OrderByDescending(g => g.Total)
                        .FirstOrDefault()?.Customer ?? "";

            ViewBag.TotalAmount = sumAmount;
            ViewBag.AvgAmount = avgAmount;
            ViewBag.TopCustomer = topCustomer;
        }
        public IActionResult Index(char? statusCode, string customerName)
        {
            var orders = _db.Orders
                            .Include(o => o.Customer)
                            .Include(o => o.Status)
                            .AsQueryable();

            if (statusCode.HasValue)
                orders = orders.Where(o => o.Status.StatusCode == statusCode.Value);
            if (!string.IsNullOrWhiteSpace(customerName))
                orders = orders.Where(o => o.Customer.Name.Contains(customerName.ToLower()));


            var orderList = orders.ToList();
            CalculateOrderInfo(orderList);
            return View(orderList);
        }
        public IActionResult AddOrder()
        {
            ViewBag.Customers = _db.Customers.ToList();
            ViewBag.Statuses = _db.Statuses.ToList();

            return View();
        }

        public IActionResult FilterRows(char? statusCode, string customerName)
        {
            var orders = _db.Orders
                            .Include(o => o.Customer)
                            .Include(o => o.Status)
                            .AsQueryable();

            if (statusCode.HasValue)
                orders = orders.Where(o => o.Status.StatusCode == statusCode.Value);

            if (!string.IsNullOrWhiteSpace(customerName))
                orders = orders.Where(o => o.Customer.Name.ToLower().Contains(customerName.ToLower()));


            var orderList = orders.ToList();
            CalculateOrderInfo(orderList);

            return PartialView("OrdersTable", orderList);
        }
        public IActionResult FilterTotals(char? statusCode, string customerName)
        {
            var orders = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .AsQueryable();

            if (statusCode.HasValue)
                orders = orders.Where(o => o.Status.StatusCode == statusCode.Value);

            if (!string.IsNullOrWhiteSpace(customerName))
                orders = orders.Where(o => o.Customer.Name.ToLower().Contains(customerName.ToLower()));

            var orderList = orders.ToList();
            return PartialView("OrdersTotals", orderList);
        }
    }
}
