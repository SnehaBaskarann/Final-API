using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public  Mobile mobile { get; set; }

        public User user { get; set; }


    }
}
