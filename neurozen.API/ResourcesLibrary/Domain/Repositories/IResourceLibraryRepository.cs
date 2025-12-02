using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;

namespace neurozen.API.ResourcesLibrary.Domain.Repositories;

public interface IResourceLibraryRepository : IBaseRepository<ResourceLibrary>
{
    // Métodos específicos si los necesitas
    Task<IEnumerable<ResourceLibrary>> GetByResourceTypeAsync(string resourceType);
    Task<ResourceLibrary?> GetByIdAsync(int id);
}