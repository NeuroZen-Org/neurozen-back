using neurozen.API.ResourcesLibrary.Domain.Model.Commands;
using neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;

namespace neurozen.API.ResourcesLibrary.Interfaces.REST.Transform;

public class CreateResourceLibraryCommandFromResourceAssembler
{
    public static CreateResourceLibraryCommand ToCommandFromResource(CreateResourceLibraryResource resource) =>
        new CreateResourceLibraryCommand(
            resource.Title,
            resource.Description,
            resource.ResourceType,
            resource.ContentUrl,
            resource.Duration,
            resource.Author);
}