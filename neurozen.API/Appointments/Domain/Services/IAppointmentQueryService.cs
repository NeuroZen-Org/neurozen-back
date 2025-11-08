using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Appointments.Domain.Model.Queries;

namespace neurozen.API.Appointments.Domain.Services;

public interface IAppointmentQueryService
{
    
    Task<IEnumerable<Appointment>> Handle(GetAllAppointmentsQueryByPatientId query);
}