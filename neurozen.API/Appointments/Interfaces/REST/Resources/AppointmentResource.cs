using neurozen.API.Appointments.Domain.Model.ValueObjects;

namespace neurozen.API.Appointments.Interfaces.REST.Resources;

public record AppointmentResource(
    int Id, long PatientId, long ProfessionalId, DateTime AppointmentDateTime
    ,EAppointmentType AppointmentType, string notasAdicionales );
