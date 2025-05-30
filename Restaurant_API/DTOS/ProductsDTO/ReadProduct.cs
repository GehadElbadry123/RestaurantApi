﻿namespace Restaurant_API.DTOS.ProductsDTO
{
    public class ReadProduct
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
    }
}
