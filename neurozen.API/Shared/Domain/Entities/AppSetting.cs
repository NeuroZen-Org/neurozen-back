using System;

namespace neurozen.API.Shared.Domain.Entities
{
    public class AppSetting
    {
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
