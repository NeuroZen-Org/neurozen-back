namespace neurozen.API.Appointments.Domain.Model.Commands;

public record CreateAppointmentCommand(int PatientId, int ProfessionalId, DateTime AppointmentDate);