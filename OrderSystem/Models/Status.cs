namespace OrderSystem.Models
{
    public class Status
    {
        public int Id { get; set; }
        public char StatusCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
