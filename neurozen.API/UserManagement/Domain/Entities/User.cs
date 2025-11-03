using System;
using System.Collections.Generic;

namespace neurozen.API.UserManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string? PasswordHash { get; set; }
        public string? FullName { get; set; }
        public string Role { get; set; } = "user";
        public string? AvatarUrl { get; set; }
        public string Meta { get; set; } = "{}";
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<neurozen.API.Sales.Domain.Entities.Cart> Carts { get; set; } = new List<neurozen.API.Sales.Domain.Entities.Cart>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<neurozen.API.Sales.Domain.Entities.Order> Orders { get; set; } = new List<neurozen.API.Sales.Domain.Entities.Order>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
