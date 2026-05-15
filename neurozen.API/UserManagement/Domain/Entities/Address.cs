using System;

namespace neurozen.API.UserManagement.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public neurozen.API.IAM.Domain.Model.Aggregates.User? User { get; set; }
        public string? Label { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
