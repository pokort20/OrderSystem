namespace OrderSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
