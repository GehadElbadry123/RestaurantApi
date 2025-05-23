namespace Restaurant_API.Models
{
    public class User
    {
        
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "Customer"; // Admin / Customer

        // العلاقة العكسية: المستخدم ممكن يعمل أكثر من أوردر
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        

    }
}

