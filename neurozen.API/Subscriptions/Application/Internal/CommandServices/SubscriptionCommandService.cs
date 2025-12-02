using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.Subscriptions.Domain.Model.Aggregates;
using neurozen.API.Subscriptions.Domain.Model.Commands;
using neurozen.API.Subscriptions.Domain.Repositories;
using neurozen.API.Subscriptions.Domain.Services;

namespace neurozen.API.Subscriptions.Application.Internal.CommandServices;

public class SubscriptionCommandService (ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork, ILogger<SubscriptionCommandService> logger) : ISubscriptionCommandService
{
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {
        var subscription = new Subscription(command);
        try
        {
            await subscriptionRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating subscription. PlanId: {PlanId}, NameUser: {NameUser}, LastName: {LastName}, EmailUser: {EmailUser}, NumberCard: {NumberCard}, ExpirationDate: {ExpirationDate}, Cvv: {Cvv}",
                command.PlanId, command.NameUser, command.LastNameUser, command.EmailUser, command.NumberCard, command.ExpirationDate, command.Cvv);
            return null;
        }
        return subscription;
    }
}