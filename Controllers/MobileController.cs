using Microsoft.AspNetCore.Mvc;
using MobileStoreAPI.Data;
using MobileStoreAPI.Models;

namespace MobileStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly AppDbContext _cartdetails;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;


        public MobileController(AppDbContext cartdetails, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _cartdetails = cartdetails;
            _environment = environment;
            _configuration = configuration;
        }
        [HttpGet("{Mobileid}")]
        public async Task<Mobile> GetById(int id)
        {
            return await _cartdetails.mobiles.FindAsync(id);
        }
        [HttpGet("{id}/Image")]
        public IActionResult GetImage(int id)
        {
            var cart = _cartdetails.mobiles.Find(id);
            if (cart == null)
            {
                return NotFound(); // User not found
            }

            // Construct the full path to the image file
            var imagePath = Path.Combine(_environment.WebRootPath, "images", cart.UniqueFileName);

            // Check if the image file exists
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound(); // Image file not found
            }

            // Serve the image file
            return PhysicalFile(imagePath, "image/jpeg");
        }


        [HttpPost]
        public async Task<ActionResult<Mobile>> CreateUser([FromForm] MobileDTO cart)
        {

            // Generate a unique file name
            var uniqueFileName = $"{Guid.NewGuid()}_{cart.MobileImage.FileName}";

            // Save the image to a designated folder (e.g., wwwroot/images)
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cart.MobileImage.CopyToAsync(stream);
            }

            // Store the file path in the database
            cart.UniqueFileName = uniqueFileName;

            Stock Stock = _cartdetails.stocks.Find(cart.StockId);
            Mobile mobile = new Mobile()
            {
                MobileId = cart.MobileId,
                MobileName = cart.MobileName,
                MobileModel = cart.MobileModel,
                MobilePrice = cart.MobilePrice,
                MobileImage = cart.MobileImage,
                UniqueFileName = cart.UniqueFileName,
                stock = Stock


            };

            _cartdetails.mobiles.Add(mobile);
            await _cartdetails.SaveChangesAsync();
            // var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{cart.UniqueFileName}";

            // Return the image URL or any other relevant response
            return Ok();



            
        }





        [HttpGet]
        public IActionResult GetAllCart()
        {
            var carts = _cartdetails.mobiles.ToList();

            var cartList = new List<object>();

            foreach (var cart in carts)
            {


                // Create an object containing cart details and image URL
                var cartData = new
                {
                    id = cart.MobileId,
                    name = cart.MobileName,
                    model = cart.MobileModel,
                    price = cart.MobilePrice,
                    imageUrl = String.Format("{0}://{1}{2}/wwwroot/images/{3}", Request.Scheme, Request.Host, Request.PathBase, cart.UniqueFileName)
                };

                cartList.Add(cartData);
            }

            return Ok(cartList);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateMobile(int id, [FromForm] MobileDTO mobile)
        {
            // Retrieve the existing mobile from the database
            var mobileToUpdate = await _cartdetails.mobiles.FindAsync(id);
            if (mobileToUpdate == null)
            {
                return NotFound();
            }

            // Check if a new image is provided
            if (mobile.MobileImage != null)
            {
                // Generate a unique file name for the new image
                var uniqueFileName = $"{Guid.NewGuid()}_{mobile.MobileImage.FileName}";

                // Save the new image to the designated folder
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "Images");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await mobile.MobileImage.CopyToAsync(stream);
                }

                // Delete the old image if necessary
                var oldFilePath = Path.Combine(uploadsFolder, mobileToUpdate.UniqueFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                // Update the file path in the database
                mobileToUpdate.UniqueFileName = uniqueFileName;
            }

            // Update individual properties if provided
            if (!string.IsNullOrEmpty(mobile.MobileName))
            {
                mobileToUpdate.MobileName = mobile.MobileName;
            }

            if (!string.IsNullOrEmpty(mobile.MobileModel))
            {
                mobileToUpdate.MobileModel = mobile.MobileModel;
            }

            if (!string.IsNullOrEmpty(mobile.MobilePrice))
            {
                mobileToUpdate.MobilePrice = mobile.MobilePrice;
            }
           

          




            // Save the changes to the database
            _cartdetails.SaveChanges();

            
            return Ok(mobileToUpdate);
        }






        //public async Task<IActionResult> UpdateMobile(int id, [FromForm] Mobile updatedMobile)
        //{
        //    var existingMobile = await _cartdetails.mobiles.FindAsync(id);

        //    if (existingMobile == null)
        //    {
        //        return NotFound(); // Mobile not found
        //    }

        //    // Update the properties of the existing mobile with the new values
        //    existingMobile.MobileName = updatedMobile.MobileName;
        //    existingMobile.MobileModel = updatedMobile.MobileModel;
        //    existingMobile.MobilePrice = updatedMobile.MobilePrice;

        //    // If a new image is uploaded, update the image
        //    if (updatedMobile.MobileImage != null)
        //    {
        //        var uniqueFileName = $"{Guid.NewGuid()}_{updatedMobile.MobileImage.FileName}";
        //        var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
        //        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await updatedMobile.MobileImage.CopyToAsync(stream);
        //        }

        //        // Remove the old image file
        //        var oldImagePath = Path.Combine(_environment.WebRootPath, "images", existingMobile.UniqueFileName);
        //        if (System.IO.File.Exists(oldImagePath))
        //        {
        //            System.IO.File.Delete(oldImagePath);
        //        }

        //        // Update the unique file name and save it to the database
        //        existingMobile.UniqueFileName = uniqueFileName;
        //    }

        //    // Save changes to the database
        //    _cartdetails.mobiles.Update(existingMobile);
        //    await _cartdetails.SaveChangesAsync();

        //    return Ok(existingMobile);
        //}



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemobileDetails(int id)
        {
            var mobdetails = _cartdetails.mobiles.Find(id);
            if (mobdetails == null)
            {
                return NotFound(); 
            }
 
            _cartdetails.mobiles.Remove(mobdetails);
            await _cartdetails.SaveChangesAsync();
 
            return NoContent();
        }



    }
}
