namespace neurozen.API.Subscriptions.Domain.Model.Commands;

public record CreateSubscriptionCommand(int UserId,int PlanId, string NameUser, string LastNameUser, string EmailUser, string NumberCard, string ExpirationDate, string Cvv, bool IsActive);