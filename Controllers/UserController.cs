using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileStoreAPI.Data;
using MobileStoreAPI.Models;
using System.Data.Entity;

namespace MobileStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return _context.users.ToList();
        }

        [HttpGet]
        public ActionResult<User> Getindividual(int id)
        {
            var service = _context.users.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpPost]

        public async Task<ActionResult<User>> Create(User User)
        {

            if (_context.users.Any(user => user.EmailId == User.EmailId))
            {

                return Ok("{\"accountexist\":true}");
            }

            else
            {


                _context.Add(User);

                await _context.SaveChangesAsync();

                return Ok("{\"accountexist\":false}");



            }


        }

        [Route("api/Signin")]
        [HttpPost]

        public async Task<IActionResult> Signin(Login user)
        {
            try
            {
                User olduser = _context.users.Where(user1 => user1.EmailId == user.EmailId).FirstOrDefault()!;

                if (_context.users.Any(user1 => user1.EmailId == user.EmailId) || olduser.EmailId == user.EmailId && olduser != null)
                {
                    if (olduser.Password == user.Password)
                    {
                        return Ok("{\"emailstatus\":true,\"passwordstatus\":true}");
                    }
                    else
                    {
                        return Ok("{\"emailstatus\":true,\"passwordstatus\":false}");
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok("{\"emailstatus\":false,\"passwordstatus\":false}");

            }
            return Ok("{\"emailstatus\":false,\"passwordstatus\":false}");


        }





        //[Route("api/Signin")]
        //[HttpPost]

        //public async Task<IActionResult> Signin(Login user)
        //{
        //    try
        //    {
        //        User olduser = _context.users.Where(user1 => user1.EmailId == user.EmailId).FirstOrDefault()!;

        //        if (olduser != null&&olduser.EmailId == user.EmailId )
        //        {
        //            if (olduser.Password == user.Password)
        //            {
        //                return Ok("{\"emailstatus\":true,\"passwordstatus\":true}");
        //            }
        //            else
        //            {
        //                return Ok("{\"emailstatus\":true,\"passwordstatus\":false}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok("{\"emailstatus\":false,\"passwordstatus\":false}");

        //    }
        //    return Ok("{\"emailstatus\":false,\"passwordstatus\":false}");


        //}




        [Route("api/FindEmail")]
        [HttpPost]
        public ActionResult FindEmail([FromBody] EmailDto email)
        {
            var Email = _context.users.Where(s => s.EmailId == email.EmailId).FirstOrDefault();
            if (Email == null)
            {
                return Ok("Not the data");
            }
            else
            {
                return Ok(Email);
            }
        }
    }
}
    
    


