using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class Adminview
    {
        [Required]
        public string? EmailId { get; set; }

        [Required]
        public string? Password { get; set; }

      
    }
}
