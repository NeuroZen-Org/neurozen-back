using neurozen.API.Appointments.Domain.Model.ValueObjects;

namespace neurozen.API.Appointments.Domain.Model.Commands;

public record CreateAppointmentCommand(
    int PatientId, 
    int ProfessionalId, 
    DateTime AppointmentDate,
    EAppointmentType AppointmentType,
    string? NotasAdicionales = null);
