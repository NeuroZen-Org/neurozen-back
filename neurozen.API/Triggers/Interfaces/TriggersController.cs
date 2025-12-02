using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using neurozen.API.Resources;
using neurozen.API.Triggers.Domain.Services;
using neurozen.API.Triggers.Interfaces.REST.Resources;
using neurozen.API.Triggers.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace neurozen.API.Triggers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Triggers")]
public class TriggersController(
    ITriggerCommandService triggercommandService,
    IStringLocalizer<SharedResource> _localizer) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a trigger",
        Description = "Creates a trigger with given parameters")]
    [SwaggerResponse(201, "The trigger was created", typeof(TriggerResource))]
    [SwaggerResponse(400, "The trigger was failed to be created")]
    public async Task<ActionResult> CreateTrigger([FromBody] CreateTriggerResource resource)
    {
        String msg = _localizer.GetString("CreateTriggerError");
        if(resource.StressLevel>10 || resource.StressLevel<1) 
            return BadRequest(new {message = "Stress Level must be between 1 and 10"});
        var createTriggerCommand = CreateTriggerCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await triggercommandService.Handle(createTriggerCommand);
        if (result is null) return BadRequest(new {message = msg});
        return CreatedAtAction(nameof(CreateTrigger), new { id = result.Id }, TriggerResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
        
}