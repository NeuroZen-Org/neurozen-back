using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Appointments.Domain.Model.Queries;
using neurozen.API.Appointments.Domain.Repositories;
using neurozen.API.Appointments.Domain.Services;

namespace neurozen.API.Appointments.Application.Internal.QueryServices;

public class AppointmentQueryService(IAppointmentRepository appointmentRepository) : IAppointmentQueryService
{
    public async Task<IEnumerable<Appointment>> Handle(GetAllAppointmentsQueryByPatientId query)
    {
        return await appointmentRepository.GetAllAppointmentsQueryByPatientId(query.PatientId);
    }
}