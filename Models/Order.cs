using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string? OrderDate { get; set;}

        public string? OrderStatus { get; set; }



        public User? users { get; set; }

        public Mobile mobile { get; set; }
       
       
    }
}
