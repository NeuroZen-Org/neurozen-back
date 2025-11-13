using Microsoft.Extensions.Logging;
using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Appointments.Domain.Model.Commands;
using neurozen.API.Appointments.Domain.Repositories;
using neurozen.API.Appointments.Domain.Services;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Appointments.Application.Internal.CommandServices;

public class AppointmentCommandService(
    IAppointmentRepository appointmentRepository, 
    IUnitOfWork unitOfWork,
    ILogger<AppointmentCommandService> logger) : IAppointmentCommandService
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
            logger.LogError(e, "Error creating appointment. PatientId: {PatientId}, ProfessionalId: {ProfessionalId}, AppointmentDate: {AppointmentDate}", 
                command.PatientId, command.ProfessionalId, command.AppointmentDate);
            return null;
        }
        return appointment;
    }
}