namespace neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;

public record ResourceLibraryResource(
    int Id,
    string Title,
    string Description,
    string ResourceType,
    string ContentUrl,
    long Duration,
    string Author
);