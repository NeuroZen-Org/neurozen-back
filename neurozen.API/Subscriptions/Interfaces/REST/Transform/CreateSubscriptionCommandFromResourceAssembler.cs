using neurozen.API.Subscriptions.Domain.Model.Commands;
using neurozen.API.Subscriptions.Interfaces.REST.Resources;

namespace neurozen.API.Subscriptions.Interfaces.REST.Transform;

public class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource) =>
        new CreateSubscriptionCommand(
            resource.UserId,   // 👈 agregado
            resource.PlanId,
            resource.NameUser,
            resource.LastNameUser,
            resource.EmailUser,
            resource.NumberCard,
            resource.ExpirationDate,
            resource.Cvv,
            resource.IsActive
        );
}