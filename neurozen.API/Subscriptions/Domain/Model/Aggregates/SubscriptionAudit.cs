using System.ComponentModel.DataAnnotations.Schema;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Subscriptions.Domain.Model.Aggregates;

public class SubscriptionAudit : IEntityWithCreatedUpdatedDated
{
    [Column ("CreatedAt")] public DateTimeOffset CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset UpdatedDate { get; set; }
}