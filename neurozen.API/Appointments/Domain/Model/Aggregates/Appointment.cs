using neurozen.API.Appointments.Domain.Model.Commands;

namespace neurozen.API.Appointments.Domain.Model.Aggregates;

public partial class Appointment
{
    protected Appointment()
    {
        PatientId = 0;
        ProfessionalId = 0;
        AppointmentDate = DateTime.MinValue;
    }

    public Appointment(CreateAppointmentCommand command)
    {
        PatientId = command.PatientId;
        ProfessionalId = command.ProfessionalId;
        AppointmentDate = command.AppointmentDate;
    }
    
    public int Id { get; private set; }
    public long PatientId { get; private set; }
    public long ProfessionalId { get; private set; }
    public DateTime AppointmentDate { get; private set; }
}