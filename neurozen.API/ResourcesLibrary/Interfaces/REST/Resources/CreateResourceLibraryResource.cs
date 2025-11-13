namespace neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;

public record CreateResourceLibraryResource(
    string Title,
    string Description,
    string ResourceType,
    string ContentUrl,
    long Duration,
    string Author
);
