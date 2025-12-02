using System;

namespace neurozen.API.Sales.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Sku { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public string Metadata { get; set; } = "{}";
    }
}
