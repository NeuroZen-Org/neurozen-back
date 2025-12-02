using neurozen.API.Triggers.Domain.Model.Commands;
using neurozen.API.Triggers.Interfaces.REST.Resources;

namespace neurozen.API.Triggers.Interfaces.REST.Transform;

public class CreateTriggerCommandFromResourceAssembler
{
    public static CreateTriggerCommand ToCommandFromResource(CreateTriggerResource resource) =>
        new CreateTriggerCommand(
            resource.PatientId,
            resource.CategoryId,
            resource.StressLevel,
            resource.TriggerDateTime,
            resource.Description);
}