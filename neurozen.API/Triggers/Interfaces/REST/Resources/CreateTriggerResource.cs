namespace neurozen.API.Triggers.Interfaces.REST.Resources;

public record CreateTriggerResource(int PatientId, int CategoryId, int StressLevel , DateTime TriggerDateTime, string Description);