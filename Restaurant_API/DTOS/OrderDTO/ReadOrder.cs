using Restaurant_API.Models;

namespace Restaurant_API.DTOS.OrderDTO
{
    public class ReadOrder
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Preparing, Delivered

        public decimal TotalPrice { get; set; }

    }
}

