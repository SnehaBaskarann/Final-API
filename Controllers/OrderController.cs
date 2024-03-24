using Microsoft.AspNetCore.Mvc;
using MobileStoreAPI.Data;
using MobileStoreAPI.Models;

namespace MobileStoreAPI.Controllers
{
    
        [Route("api/Post/[controller]")]
        [ApiController]
        public class OrderController : Controller
        {
            private readonly AppDbContext _context;
            public OrderController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<ActionResult<Mobile>> Create(OrderDTO DTO)
            {
                User User = _context.users.Find(DTO.UserId);
                Order order = new Order()
                {
                    OrderId = DTO.OrderId,
                    OrderDate= DTO.OrderDate,
                    users = User
                };
                _context.orders.Add(order);

                await _context.SaveChangesAsync();

                return Ok();
            }
        [Route("api/Get/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return _context.orders.ToList<Order>();
        }

        [Route("api/Get/[controller]/{id}")]
        [HttpGet]
        public ActionResult<Mobile> Getindividual(int id)
        {
            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
