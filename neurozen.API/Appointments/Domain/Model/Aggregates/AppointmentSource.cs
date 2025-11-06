namespace DefaultNamespace;

public partial class AppointmentSource
{
    protected AppointmentSource()
    {
        ProfessionalId = long.empty;
        PatientId = long.empty;
        AppointmentDateTime = string.empty;
    }

    public AppointmentSource(CreateAppointmentCommand command)
    {
        ProfessionalId = command.ProfessionalId;
        PatientId = command.PatientId;
        AppointmentDateTime = command.AppointmentDateTime;
    }
    
    public int Id { get; }
    public long ProfessionalId { get; private set; }
    public long  PatientId { get; private set; } 
    public string AppointmentDateTime { get; private set; }
}