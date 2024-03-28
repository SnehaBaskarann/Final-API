using Microsoft.AspNetCore.Mvc;
using MobileStoreAPI.Data;
using MobileStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MobileStoreAPI.Controllers
{
    
        [Route("api/Post/[controller]/[action]")]
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
                Mobile Mobile = _context.mobiles.Find(DTO.MobileId);
                Order order = new Order()
                {
                    OrderId = DTO.OrderId,
                    OrderDate= DTO.OrderDate,
                    OrderStatus=DTO.OrderStatus,
                    users = User,
                    mobile= Mobile
                };
                _context.orders.Add(order);

                await _context.SaveChangesAsync();

                return Ok();
            }



        [HttpPost]
        public async Task<IActionResult> Put(UpdateOrderStatus DTO)
        {
            // Find the FinalAppointment by id
            var fappointment = await _context.orders.FindAsync(DTO.Id);



            //User User = _context.User.Find(DTO.Id)!;
            fappointment.OrderStatus = DTO.Status;   
            _context.orders.Update(fappointment);
            await _context.SaveChangesAsync();
            return Ok();
        }




        [Route("api/Get/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return _context.orders.Include(a =>a.users).Include(s => s.mobile).ToList();
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
