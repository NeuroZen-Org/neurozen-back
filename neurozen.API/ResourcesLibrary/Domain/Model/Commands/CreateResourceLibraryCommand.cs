namespace neurozen.API.ResourcesLibrary.Domain.Model.Commands;

public record CreateResourceLibraryCommand(
    string Title,
    string Description,
    string ResourceType,
    string ContentUrl,
    long Duration,
    string Author);