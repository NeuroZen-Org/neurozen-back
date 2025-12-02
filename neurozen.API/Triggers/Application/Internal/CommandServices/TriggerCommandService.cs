using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.Triggers.Domain.Model.Aggregates;
using neurozen.API.Triggers.Domain.Model.Commands;
using neurozen.API.Triggers.Domain.Repositories;
using neurozen.API.Triggers.Domain.Services;

namespace neurozen.API.Triggers.Application.Internal.CommandServices;

public class TriggerCommandService(
    ITriggerRepository triggerRepository,
    IUnitOfWork unitOfWork,
    ILogger<TriggerCommandService> logger) : ITriggerCommandService
{
    public async Task<Trigger?> Handle(CreateTriggerCommand command)
    {
        var trigger = new Trigger(command);
        try
        {
            await triggerRepository.AddAsync(trigger); //guarda en la DB
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error in create trigger. PatientId: {PatientId}, CategoryId: {CategoryId}, StressLevel: {StressLevel}, TriggerDateTime:{TriggerDateTime}, Description: {Description}",
                command.PatientId, command.CategoryId, command.StressLevel, command.TriggerDateTime, command.Description);
            return null;
        }

        return trigger;
    }
}