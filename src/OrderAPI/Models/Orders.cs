namespace OrderAPI.Models
{
    public class Order
    {
        public int Id {get; set;}
        public int OrderType {get; set;}
        public string? CustomerName {get; set;}
        public DateTime CreatedDate {get; set;}
        public string? CreatedByUsername {get; set;}
    }
}