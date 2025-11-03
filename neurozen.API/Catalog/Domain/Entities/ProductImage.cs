using System;

namespace neurozen.API.Catalog.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public string Url { get; set; } = null!;
        public string? Alt { get; set; }
        public int Position { get; set; }
    }
}
