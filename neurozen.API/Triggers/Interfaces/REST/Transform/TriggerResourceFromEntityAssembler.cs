using neurozen.API.Triggers.Domain.Model.Aggregates;
using neurozen.API.Triggers.Interfaces.REST.Resources;

namespace neurozen.API.Triggers.Interfaces.REST.Transform;

public class TriggerResourceFromEntityAssembler
{
    public static TriggerResource ToResourceFromEntity(Trigger entity) =>
        new TriggerResource(
            entity.Id,
            entity.PatientId,
            entity.CategoryId,
            entity.StressLevel,
            entity.TriggerDateTime,
            entity.Description
        );
}