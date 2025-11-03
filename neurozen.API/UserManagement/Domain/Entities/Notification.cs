using System;

namespace neurozen.API.UserManagement.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public bool Read { get; set; }
        public string Meta { get; set; } = "{}";
        public DateTimeOffset CreatedAt { get; set; }
    }
}
