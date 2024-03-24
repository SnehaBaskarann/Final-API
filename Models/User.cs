using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string? FirstName { get; set;}

        [Required]
        public string? LastName { get; set;}

        [Required]
      public string? EmailId { get; set;}

        [Required]
        public string? Password { get; set;}

        [Required]
        public long PhoneNumber { get; set;}
        
        //public List<Mobile> Mobiles { get; set; }
    }
}
