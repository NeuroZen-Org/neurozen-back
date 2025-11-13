

namespace neurozen.API.Subscriptions.Interfaces.REST.Resources;

public record CreateSubscriptionResource(
    int UserId,              // 👈 nuevo campo
    int PlanId,
    string NameUser,
    string LastNameUser,
    string EmailUser,
    string NumberCard,
    string ExpirationDate,
    string Cvv,
    bool IsActive
);