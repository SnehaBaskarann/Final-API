using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileStoreAPI.Models
{
    public class Mobile
    {
        [Key]
        public int MobileId { get; set; }

       
        public string? MobileName { get; set; }
      
        public string? MobileModel { get; set;}
     
        public string? MobilePrice { get; set;}

        [NotMapped]
        public IFormFile? MobileImage { get; set; }

        public string? UniqueFileName { get; set; }

         //public List<Stock> stocks { get; set; }
         
        public Stock stock { get; set; }
    }
}
