﻿using Restaurant_API.Models;

namespace Restaurant_API.DTOS.OrderItemDTO
{
    public class ReadOrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
