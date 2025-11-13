using System.ComponentModel.DataAnnotations.Schema;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Appointments.Domain.Model.Aggregates;

public partial class AppointmentAudit : IEntityWithCreatedUpdatedDated
{
    [Column ("CreatedAt")] public DateTimeOffset CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset UpdatedDate { get; set; }
}