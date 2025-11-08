using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Appointments.Domain.Model.Commands;
using neurozen.API.Appointments.Domain.Repositories;
using neurozen.API.Appointments.Domain.Services;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Appointments.Application.Internal.CommandServices;

public class AppointmentCommandService(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork) : IAppointmentCommandService
{
    public async Task<Appointment?> Handle(CreateAppointmentCommand command)
    {
        var appointment = new Appointment(command);
        try
        {
            await appointmentRepository.AddAsync(appointment);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        return appointment;
    }
}