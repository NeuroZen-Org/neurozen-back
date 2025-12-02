using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Appointments.Domain.Model.Commands;

namespace neurozen.API.Appointments.Domain.Services;

public interface IAppointmentCommandService
{
    Task<Appointment?> Handle(CreateAppointmentCommand command);
}