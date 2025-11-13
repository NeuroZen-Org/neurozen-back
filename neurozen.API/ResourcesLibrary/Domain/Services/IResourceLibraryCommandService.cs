using neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;
using neurozen.API.ResourcesLibrary.Domain.Model.Commands;

namespace neurozen.API.ResourcesLibrary.Domain.Services;

public interface IResourceLibraryCommandService
{
    Task<ResourceLibrary?> Handle(CreateResourceLibraryCommand command);
}