namespace neurozen.API.Appointments.Interfaces.REST.Resources;

public record CreateAppointmentResource(int PatientId, int ProfessionalId, DateTime AppointmentDateTime);