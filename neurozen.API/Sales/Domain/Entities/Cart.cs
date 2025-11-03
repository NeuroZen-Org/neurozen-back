using System;
using System.Collections.Generic;

namespace neurozen.API.Sales.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public neurozen.API.UserManagement.Domain.Entities.User? User { get; set; }
        public string? SessionId { get; set; }
        public string Metadata { get; set; } = "{}";
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
