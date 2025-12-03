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
        // ✅ PASO 1: Validar que el usuario NO tenga una suscripción activa
        var activeSubscription = await subscriptionRepository.FindActiveSubscriptionByUserIdAsync(command.UserId);
        
        if (activeSubscription != null)
        {
            logger.LogWarning("User {UserId} already has an active subscription (ID: {SubscriptionId}). Cannot create a new one.", 
                command.UserId, activeSubscription.Id);
            throw new InvalidOperationException($"El usuario con ID {command.UserId} ya tiene una suscripción activa. No puede crear otra hasta que la actual esté inactiva.");
        }
        
        // ✅ PASO 2: Crear la suscripción con IsActive = false (valor por defecto)
        var subscription = new Subscription(command);
        
        try
        {
            // ✅ PASO 3: Guardar la suscripción en la base de datos
            await subscriptionRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();
            
            // ✅ PASO 4: Activar la suscripción después de guardarla exitosamente
            subscription.Activate();
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync();
            
            logger.LogInformation("Subscription created and activated successfully for user {UserId}. Subscription ID: {SubscriptionId}", 
                command.UserId, subscription.Id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating subscription. UserId: {UserId}, PlanId: {PlanId}, NameUser: {NameUser}, LastName: {LastName}, EmailUser: {EmailUser}",
                command.UserId, command.PlanId, command.NameUser, command.LastNameUser, command.EmailUser);
            return null;
        }
        
        return subscription;
    }
}