namespace Restaurant_API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Preparing, Delivered

        public decimal TotalPrice { get; set; }

        // العلاقة العكسية: كل أوردر فيه أصناف متعددة
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
