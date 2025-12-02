namespace neurozen.API.Triggers.Domain.Model.Commands;

public record CreateTriggerCommand(
    int PatientId,
    int CategoryId,
    int StressLevel,
    DateTime TriggerDateTime,
    string Description)
{
    
}