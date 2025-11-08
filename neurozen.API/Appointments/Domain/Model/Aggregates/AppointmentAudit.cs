using System.ComponentModel.DataAnnotations.Schema;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Appointments.Domain.Model.Aggregates;

public partial class AppointmentAudit : IEntityWithCreatedUpdatedDated
{
    [Column ("CreatedAt")] public DateTimeOffset CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset UpdatedDate { get; set; }
    [Column ("PatientId")] public long PatientId { get; private set; }
    [Column ("ProfessionalId")] public long ProfessionalId { get; private set; }
    [Column ("AppointmentDate")] public DateTime AppointmentDate { get; private set; }
}