using System.ComponentModel.DataAnnotations;

namespace MobileStoreAPI.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set;}
        [Required]
       
        public string? AvailableStock { get; set;}

        //public Mobile? mobiles { get; set; }
        //public int MobileId { get; set; }
    }
}
