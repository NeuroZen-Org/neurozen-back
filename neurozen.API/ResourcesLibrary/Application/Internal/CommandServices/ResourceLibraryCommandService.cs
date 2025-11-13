using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;
using neurozen.API.ResourcesLibrary.Domain.Model.Commands;
using neurozen.API.ResourcesLibrary.Domain.Repositories;
using neurozen.API.ResourcesLibrary.Domain.Services;

namespace neurozen.API.ResourcesLibrary.Application.Internal.CommandServices;

public class ResourceLibraryCommandService(
    IResourceLibraryRepository resourceLibraryRepository,
    IUnitOfWork unitOfWork,
    ILogger<ResourceLibraryCommandService> logger) : IResourceLibraryCommandService
{
    public async Task<ResourceLibrary?> Handle(CreateResourceLibraryCommand command)
    {
        var resourceLibrary = new ResourceLibrary(command);
        try
        {
            await resourceLibraryRepository.AddAsync(resourceLibrary);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating resource library. Title: {Title}, ResourceType: {ResourceType}, Author: {Author}, ContentUrl: {ContentUrl}",
                command.Title, command.ResourceType, command.Author, command.ContentUrl);
            return null;
        }

        return resourceLibrary;
    }
}