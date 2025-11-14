using Cortex.Mediator.Notifications;
using neurozen.API.LearningCenterPlatform.API.Shared.Domain.Model.Events;

namespace neurozen.API.LearningCenterPlatform.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}