using neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;
using neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;

namespace neurozen.API.ResourcesLibrary.Interfaces.REST.Transform;

public class ResourceLibraryResourceFromEntityAssembler
{
    public static ResourceLibraryResource ToResourceFromEntity(ResourceLibrary entity) =>
        new ResourceLibraryResource(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.ResourceType,
            entity.ContentUrl,
            entity.Duration,
            entity.Author
        );
}