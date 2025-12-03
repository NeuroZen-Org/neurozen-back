using Microsoft.EntityFrameworkCore;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using neurozen.API.Subscriptions.Domain.Model.Aggregates;
using neurozen.API.Subscriptions.Domain.Repositories;

namespace neurozen.API.Subscriptions.Infraestructure.Respositories;

public class SubcriptionRepository(AppDbContext context) 
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    // Obtener una suscripción por Id
    public async Task<Subscription?> FindByIdAsync(int id)
    {
        return await context.Set<Subscription>()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    // Obtener todas las suscripciones de un Plan específico
    public async Task<IEnumerable<Subscription>> FindByPlanIdAsync(int planId)
    {
        return await context.Set<Subscription>()
            .Where(s => s.PlanId == planId)
            .ToListAsync();
    }

    // Obtener todas las suscripciones según si están activas o no
    public async Task<IEnumerable<Subscription>> FindByIsActiveAsync(bool isActive)
    {
        return await context.Set<Subscription>()
            .Where(s => s.IsActive.HasValue && s.IsActive.Value == isActive)
            .ToListAsync();
    }

    // Obtener todas las suscripciones de un usuario específico
    public async Task<IEnumerable<Subscription>> FindByUserIdAsync(int userId)
    {
        return await context.Set<Subscription>()
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
    
    // Verificar si un usuario tiene una suscripción activa
    public async Task<Subscription?> FindActiveSubscriptionByUserIdAsync(int userId)
    {
        return await context.Set<Subscription>()
            .FirstOrDefaultAsync(s => s.UserId == userId && s.IsActive == true);
    }
}