using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.Triggers.Domain.Model.Aggregates;

namespace neurozen.API.Triggers.Domain.Repositories;

public interface ITriggerRepository : IBaseRepository<Trigger>
{
    //Obtener Trigger por Id
    //Task<Trigger?> GetTriggerByIdAsync(int id);
}