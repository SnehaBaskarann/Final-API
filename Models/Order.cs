using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string OrderDate { get; set;}

        public User? users { get; set; }
       
       
    }
}
