using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string? EmailId { get; set; }

        [Required]
        public string? Password { get; set;}

        
       

      
    }
}
