namespace neurozen.API.Triggers.Interfaces.REST.Resources;

public record TriggerResource(int Id, long PatientId, long CategoryId, int StressLevel , DateTime TriggerDateTime, string Description);