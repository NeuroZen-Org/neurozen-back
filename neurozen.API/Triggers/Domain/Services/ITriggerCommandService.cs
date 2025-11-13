using neurozen.API.Triggers.Domain.Model.Aggregates;
using neurozen.API.Triggers.Domain.Model.Commands;

namespace neurozen.API.Triggers.Domain.Services;

public interface ITriggerCommandService
{
    Task<Trigger?> Handle(CreateTriggerCommand command);
}