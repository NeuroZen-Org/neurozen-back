using Microsoft.AspNetCore.Mvc;
using neurozen.API.ResourcesLibrary.Domain.Model.Commands;
using neurozen.API.ResourcesLibrary.Domain.Repositories;
using neurozen.API.ResourcesLibrary.Domain.Services;
using neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;
using neurozen.API.ResourcesLibrary.Interfaces.REST.Transform;

namespace neurozen.API.ResourcesLibrary.Interfaces.REST;

[ApiController]
[Route("api/v1/resource-libraries")]  // ← Ruta explícita consistente
[Produces("application/json")]
public class ResourceLibrariesController : ControllerBase
{
    private readonly IResourceLibraryCommandService _commandService;
    private readonly IResourceLibraryRepository _repository;

    public ResourceLibrariesController(
        IResourceLibraryCommandService commandService,
        IResourceLibraryRepository repository)
    {
        _commandService = commandService;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateResourceLibrary([FromBody] CreateResourceLibraryResource resource)
    {
        var command = CreateResourceLibraryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await _commandService.Handle(command);

        if (result is null)
            return BadRequest();

        var resourceLibraryResource = ResourceLibraryResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetResourceLibraryById), new { id = result.Id }, resourceLibraryResource);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetResourceLibraryById(int id)
    {
        var result = await _repository.FindByIdAsync(id);

        if (result is null)
            return NotFound();

        var resource = ResourceLibraryResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllResourceLibraries()
    {
        var results = await _repository.ListAsync();
        var resources = results.Select(ResourceLibraryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
