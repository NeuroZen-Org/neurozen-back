using System;
using System.Collections.Generic;

namespace neurozen.API.Sales.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public neurozen.API.UserManagement.Domain.Entities.User? User { get; set; }
        public string Status { get; set; } = "pending";
        public decimal Total { get; set; }
        public string Currency { get; set; } = "USD";
        public Guid? ShippingAddressId { get; set; }
        public neurozen.API.UserManagement.Domain.Entities.Address? ShippingAddress { get; set; }
        public Guid? BillingAddressId { get; set; }
        public neurozen.API.UserManagement.Domain.Entities.Address? BillingAddress { get; set; }
        public string? PaymentInfo { get; set; }
        public string Metadata { get; set; } = "{}";
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<neurozen.API.Payments.Domain.Entities.Payment> Payments { get; set; } = new List<neurozen.API.Payments.Domain.Entities.Payment>();
    }
}
