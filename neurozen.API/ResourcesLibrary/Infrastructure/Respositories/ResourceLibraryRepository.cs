using Microsoft.EntityFrameworkCore;
using neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;
using neurozen.API.ResourcesLibrary.Domain.Repositories;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace neurozen.API.ResourcesLibrary.Infrastructure.Repositories;

public class ResourceLibraryRepository(AppDbContext context)
    : BaseRepository<ResourceLibrary>(context), IResourceLibraryRepository
{
    public async Task<IEnumerable<ResourceLibrary>> GetByResourceTypeAsync(string resourceType)
    {
        return await Context.Set<ResourceLibrary>()
            .Where(r => r.ResourceType == resourceType)
            .ToListAsync();
    }

    public async Task<ResourceLibrary?> GetByIdAsync(int id)
    {
        return await Context.Set<ResourceLibrary>()
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}