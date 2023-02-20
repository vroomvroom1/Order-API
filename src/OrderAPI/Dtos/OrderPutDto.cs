namespace OrderAPI.Dtos
{
    public class OrderPutDto
    {
        public string? OrderType {get; set;}
        public string? CustomerName {get; set;}
        public DateTime CreatedDate {get; set;}
        public string? CreatedByUsername {get; set;}
    }
}