using neurozen.API.Appointments.Domain.Model.Commands;

namespace neurozen.API.Appointments.Domain.Model.Aggregates;

public partial class Appointment
{
    protected Appointment()
    {
        PatientId = 0;
        ProfessionalId = 0;
        AppointmentDateTime = DateTime.MinValue;
    }

    public Appointment(CreateAppointmentCommand command)
    {
        PatientId = command.PatientId;
        ProfessionalId = command.ProfessionalId;
        AppointmentDateTime = command.AppointmentDate;
    }
    
    public int Id { get; private set; }
    public long PatientId { get; private set; }
    public long ProfessionalId { get; private set; }
    public DateTime AppointmentDateTime { get; private set; }
}