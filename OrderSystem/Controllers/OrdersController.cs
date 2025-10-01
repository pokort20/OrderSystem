using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var orders = _db.Orders;
            return View(orders);
        }
    }
}
