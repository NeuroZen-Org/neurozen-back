namespace neurozen.API.Appointments.Domain.Model.Commands;

public class CreateAppointmentCommand
{
    public long PatientId { get; set; }
    public long ProfessionalId { get; set; }
    public DateTime AppointmentDate { get; set; }
}