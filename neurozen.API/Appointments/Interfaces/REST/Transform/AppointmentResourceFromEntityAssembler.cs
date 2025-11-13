using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Appointments.Interfaces.REST.Resources;

namespace neurozen.API.Appointments.Interfaces.REST.Transform;

public class AppointmentResourceFromEntityAssembler
{
    public static AppointmentResource ToResourceFromEntity(Appointment entity)=>
        new AppointmentResource(
            entity.Id,
            entity.PatientId,
            entity.ProfessionalId,
            entity.AppointmentDateTime,
            entity.AppointmentType,
            entity.Notas_Adicionales
        );
}