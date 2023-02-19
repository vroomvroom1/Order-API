using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Models
{
    public class Order
    {
        // TODO: Make GUID
        public int Id {get; set;}
        [Required]
        public string? OrderType {get; set;}
        [Required]
        public string? CustomerName {get; set;}
        public DateTime CreatedDate {get; set;}
        [Required]
        public string? CreatedByUsername {get; set;}
    }
}