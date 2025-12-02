using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Appointments.Domain.Repositories;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    /// <summary>
    ///     Get all appointments by patient id and professional id
    /// </summary>
    /// <returns>An enumerable with all appointments</returns>
    Task<IEnumerable<Appointment>> GetAllAppointmentsQueryByPatientId(int patientId);
}