using neurozen.API.Triggers.Domain.Model.Commands;

namespace neurozen.API.Triggers.Domain.Model.Aggregates;

public partial class Trigger 
{
    protected Trigger()
    {
        PatientId = 0;
        CategoryId = 0;
        StressLevel = 0;
        TriggerDateTime = DateTime.MinValue;
        Description = string.Empty;
    }

    public Trigger(CreateTriggerCommand command)
    {
        PatientId = command.PatientId;
        CategoryId = command.CategoryId;
        StressLevel = command.StressLevel;
        TriggerDateTime = command.TriggerDateTime;
        Description = command.Description;
    }
    
    public int Id { get; private set; }
    public int PatientId { get; private set; }
    public int CategoryId { get; private set; }
    public int StressLevel { get; private set; } 
    public DateTime TriggerDateTime { get; private set; }
    public string Description { get; private set; }
}