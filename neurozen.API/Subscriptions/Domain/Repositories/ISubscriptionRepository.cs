﻿using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.Subscriptions.Domain.Model.Aggregates;

namespace neurozen.API.Subscriptions.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    // Obtener una suscripción por su Id
    Task<Subscription?> FindByIdAsync(int id);

    // Obtener todas las suscripciones de un Plan específico
    Task<IEnumerable<Subscription>> FindByPlanIdAsync(int planId);

    // Obtener todas las suscripciones según si están activas o no
    Task<IEnumerable<Subscription>> FindByIsActiveAsync(bool isActive);

    // Obtener todas las suscripciones de un usuario específico
    Task<IEnumerable<Subscription>> FindByUserIdAsync(int userId); // 👈 nuevo
    
    // Verificar si un usuario tiene una suscripción activa
    Task<Subscription?> FindActiveSubscriptionByUserIdAsync(int userId);
}