using System;

namespace neurozen.API.UserManagement.Domain.Entities
{
    public class Session
    {
        public Guid Id { get; set; }
        public int? UserId { get; set; }
        public neurozen.API.IAM.Domain.Model.Aggregates.User? User { get; set; }
        public string Token { get; set; } = null!;
        public DateTimeOffset? ExpiresAt { get; set; }
        public string? Ip { get; set; }
        public string? UserAgent { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
