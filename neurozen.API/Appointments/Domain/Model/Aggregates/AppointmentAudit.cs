using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Appointments.Domain.Model.Aggregates;

public partial class AppointmentAudit : IEntityWithCreatedUpdatedDated
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public string Action { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}