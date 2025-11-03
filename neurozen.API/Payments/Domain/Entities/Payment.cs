using System;
using neurozen.API.Sales.Domain.Entities;

namespace neurozen.API.Payments.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public string? Provider { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? Status { get; set; }
        public string? ProviderResponse { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
