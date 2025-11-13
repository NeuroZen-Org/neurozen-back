using neurozen.API.ResourcesLibrary.Domain.Model.Commands;

namespace neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;

public partial class ResourceLibrary
{
    protected  ResourceLibrary()
    {
        Title = string.Empty;
        Description = string.Empty;
        ResourceType = string.Empty;
        ContentUrl = string.Empty;
        Duration = 0;
        Author = string.Empty;
    }
    public ResourceLibrary(CreateResourceLibraryCommand command)
    {
        Title = command.Title;
        Description = command.Description;
        ResourceType = command.ResourceType;
        ContentUrl = command.ContentUrl;
        Duration = command.Duration;
        Author = command.Author;
    }
    
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ResourceType { get; private set; } // "Book", "Video", "Audio", "Exercise"
    public string ContentUrl { get; private set; }
    public long Duration { get; private set; } // En minutos, 0 para libros/ejercicios
    public string Author { get; private set; }
}