namespace neurozen.API.Appointments.Interfaces.REST.Resources;

public record AppointmentResource(int Id, long PatientId, long ProfessionalId, DateTime AppointmentDateTime);