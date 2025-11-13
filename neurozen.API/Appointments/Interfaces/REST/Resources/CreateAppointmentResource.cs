using System.ComponentModel.DataAnnotations;
using neurozen.API.Appointments.Domain.Model.ValueObjects;

namespace neurozen.API.Appointments.Interfaces.REST.Resources;

public record CreateAppointmentResource(
    int PatientId, 
    int ProfessionalId, 
    DateTime AppointmentDateTime,
    [Range(1,3, ErrorMessage = "AppointmentType must be between 1 and 3")]
    EAppointmentType AppointmentType,
    string? NotasAdicionales = null);
