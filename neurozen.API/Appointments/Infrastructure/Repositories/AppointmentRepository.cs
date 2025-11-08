using Microsoft.EntityFrameworkCore;
using neurozen.API.Appointments.Domain.Model.Aggregates;
using neurozen.API.Appointments.Domain.Model.Queries;
using neurozen.API.Appointments.Domain.Repositories;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace neurozen.API.Appointments.Infrastructure.Repositories;

public class AppointmentRepository(AppDbContext context) : BaseRepository<Appointment>(context), IAppointmentRepository
{
    public async Task<IEnumerable<Appointment>> GetAllAppointmentsQueryByPatientId(int patientId)
    {
        return await context.Set<Appointment>()
            .Where(a => a.PatientId == patientId)
            .ToListAsync();
    }
}