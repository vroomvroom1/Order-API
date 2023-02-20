using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Dtos
{
    public class OrderPostDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get; set;}
        [Required]
        public string? OrderType {get; set;}
        [Required]
        public string? CustomerName {get; set;}
        public DateTime CreatedDate {get; set;}
        [Required]
        public string? CreatedByUsername {get; set;}
    }
}