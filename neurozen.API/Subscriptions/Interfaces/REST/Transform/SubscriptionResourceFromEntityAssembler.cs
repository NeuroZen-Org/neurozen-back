using neurozen.API.Subscriptions.Domain.Model.Aggregates;
using neurozen.API.Subscriptions.Interfaces.REST.Resources;

namespace neurozen.API.Subscriptions.Interfaces.REST.Transform;

public class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity) =>
        new SubscriptionResource(
            entity.UserId,     // 👈 agregado
            entity.PlanId,
            entity.NameUser,
            entity.LastNameUser,
            entity.EmailUser,
            entity.NumberCard,
            entity.ExpirationDate,
            entity.Cvv,
            entity.IsActive ?? false
        );
}