using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using neurozen.API.Triggers.Domain.Model.Aggregates;
using neurozen.API.Triggers.Domain.Repositories;

namespace neurozen.API.Triggers.Infraestructure.Respositories;

public class TriggerRepository(AppDbContext context) : BaseRepository<Trigger>(context), ITriggerRepository
{
    
}