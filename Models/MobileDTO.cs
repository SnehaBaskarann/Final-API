using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class MobileDTO
    {
        public int MobileId { get; set; }

        [Required]
        public string? MobileName { get; set; }
        [Required]
        public string? MobileModel { get; set; }
        [Required]
        public string? MobilePrice { get; set; }

        [NotMapped]
        public IFormFile? MobileImage { get; set; }

        public string? UniqueFileName { get; set; }
        
        public int StockId { get; set; }

    }
}
