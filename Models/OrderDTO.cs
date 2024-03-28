using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class OrderDTO
    {

        [Key]
        public int OrderId { get; set; }
        [Required]
        public string OrderDate { get; set; }

        public string? OrderStatus { get; set; }
        public int UserId { get; set; }

        public int MobileId { get; set; }


    }
}
