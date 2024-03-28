using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileStoreAPI.Data;
using MobileStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MobileStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [Route("/viewbyuserid/{id}")]
        [HttpGet]
        public async Task<IEnumerable<Cart>> GetById(int id)
        {
            var user = _context.users.Find(id);
            return _context.Cart.Include(a => a.user).Include(x => x.mobile).Where(x=>x.user==user).ToList();

        }

        [HttpGet]
        public IActionResult GetAllCart()
        {
            var carts = _context.Cart.Include(a => a.user).Include(x => x.mobile).ToList();

            var cartList = new List<object>();

            foreach (var cart in carts)
            {


                // Create an object containing cart details and image URL
                var cartData = new
                {
                    CartId = cart.CartId,
                    Mobile = cart.mobile.MobileId,
                    MobileName= cart.mobile.MobileName,
                    MobileModel= cart.mobile.MobileModel,
                    MobilePrice= cart.mobile.MobilePrice,

                   user = cart.user.UserId,
                    FirstName = cart.user.FirstName,
                    LastName = cart.user.LastName,
                    EmailId=cart.user.EmailId,
                    PhoneNumber=cart.user.PhoneNumber,
                   
                    imageUrl = String.Format("{0}://{1}{2}/wwwroot/images/{3}", Request.Scheme, Request.Host, Request.PathBase, cart.mobile.UniqueFileName)
                };

                cartList.Add(cartData);
            }

            return Ok(cartList);
        }


        [HttpGet]
        public ActionResult<Cart> Getindividual(int id)
        {
            var service = _context.Cart.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }



        [HttpPost]
        public async Task<ActionResult<Cart>> Create(CartDto Dto)
        {
            User User = _context.users.Find(Dto.UserId)!;
            Mobile Mobile = _context.mobiles.Find(Dto.MobileId)!;
            //Services Services= _context.Services.Find(DTO.ServicesId)!;

            Cart cart = new Cart()
            {
                CartId = Dto.CartId,
                user = User,
                mobile = Mobile,
                //services= Services
            };
            _context.Cart.Add(cart);

            await _context.SaveChangesAsync();

            return Ok();
        }

        //[HttpPut]

        //public async Task<ActionResult<Cart>> Put(AppointmentDTO DTO)
        //{

        //    User User = _context.User.Find(DTO.UserId);
        //    Mobile Mobile = _context.Mobile.Find(DTO.MobileId);
        //    RepairShop RepairShop = _context.RepairShop.Find(DTO.RepairShopId);

        //    Appointment appointment = new Appointment()
        //    {
        //        AppointmentId = DTO.AppointmentId,
        //        AppointmentStatus = DTO.AppointmentStatus,
        //        Date = DTO.Date,
        //        user = User,
        //        repairShop = RepairShop,
        //        mobile = Mobile
        //    };

        //    _context.Appointment.Update(appointment);
        //    await _context.SaveChangesAsync();
        //    return Ok();

        //}

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]CartDto cartDto)
        {
            var mobile= _context.mobiles.Find(cartDto.MobileId);
            var user = _context.users.Find(cartDto.UserId);
            var carts = _context.Cart.Where(x=>x.mobile==mobile).Where(x=>x.user==user).First();

            if (carts == null)
            {
                return NotFound();
            }
            _context.Cart.Remove(carts);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

