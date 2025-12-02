using System.ComponentModel.DataAnnotations.Schema;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Triggers.Domain.Model.Aggregates;

public partial class TriggerAudit : IEntityWithCreatedUpdatedDated
{
    [Column ("CreatedAt")] public DateTimeOffset CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset UpdatedDate { get; set; }
}