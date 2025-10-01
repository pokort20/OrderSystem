using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Models;

namespace OrderSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersApiController : ControllerBase
    {
        private readonly MainDbContext _db;
        public OrdersApiController(MainDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Orders.Add(order);
            _db.SaveChanges();

            var savedOrder = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .Select(o => new {
                    o.Id,
                    o.Amount,
                    o.Date,
                    CustomerName = o.Customer.Name,
                    StatusName = o.Status.Name
                })
                .First(o => o.Id == order.Id);

            return Ok(savedOrder);
        }
    }
}
