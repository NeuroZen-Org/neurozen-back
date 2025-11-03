using System;
using System.Collections.Generic;

namespace neurozen.API.Catalog.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Sku { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public int Stock { get; set; }
        public bool Active { get; set; } = true;
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Metadata { get; set; } = "{}";
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
