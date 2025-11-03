using System;

namespace neurozen.API.Sales.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid? ProductId { get; set; }
        public neurozen.API.Catalog.Domain.Entities.Product? Product { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public string Metadata { get; set; } = "{}";
    }
}
