using System.ComponentModel.DataAnnotations;
using neurozen.API.Appointments.Domain.Model.ValueObjects;

namespace neurozen.API.Appointments.Interfaces.REST.Resources;

public record CreateAppointmentResource(
    
    [Range(1, int.MaxValue, ErrorMessage = "PatientId must be greater than 0")]
    int PatientId, 
    [Range(1, int.MaxValue, ErrorMessage = "ProfessionalId must be greater than 0")]
    int ProfessionalId, 
    DateTime AppointmentDateTime,
    [Range(1,3, ErrorMessage = "AppointmentType must be between 1 and 3")]
    EAppointmentType AppointmentType,
    string? NotasAdicionales = null);
