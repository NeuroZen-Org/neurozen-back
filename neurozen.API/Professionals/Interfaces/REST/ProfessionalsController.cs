using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using neurozen.API.Professionals.Domain.Model.Queries;
using neurozen.API.Professionals.Domain.Services;
using neurozen.API.Professionals.Interfaces.REST.Resources;
using neurozen.API.Professionals.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace neurozen.API.Professionals.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Professionals")]
public class ProfessionalsController(
  IProfessionalCommandService professionalCommandService,
  IProfessionalQueryService professionalQueryService) : ControllerBase
{
  [HttpGet]
  [SwaggerOperation(
      Summary = "Get all professionals",
      Description = "Retrieves all professionals"
    )]
  [SwaggerResponse(200, "Professionals retrieved", typeof(IEnumerable<ProfessionalResource>))]
  [SwaggerResponse(500, "Internal server error")]
  public async Task<ActionResult<IEnumerable<ProfessionalResource>>> GetAllProfessionals()
  {
    var getAllProfessionalsQuery = new GetAllProfessionalsQuery();
    var result = await professionalQueryService.Handle(getAllProfessionalsQuery);
    var professionals = result.Select(ProfessionalResourceFromEntityAssembler.ToResourceFromEntity);
    return Ok(professionals);
  }

  [HttpPost]
  [SwaggerOperation(
      Summary = "Create a new professional",
      Description = "Creates a new professional with the provided details."
    )]
  [SwaggerResponse(201, "Professional created successfully", typeof(ProfessionalResource))]
  [SwaggerResponse(400, "Professional creation failed")]
  public async Task<ActionResult> CreateProfessional([FromBody] CreateProfessionalResource resource)
  {
    var createProfessionalCommand = CreateProfessionalCommandFromResourceAssembler.ToCommandFromResource(resource);

    var result = await professionalCommandService.Handle(createProfessionalCommand);

    if (result is null) return BadRequest(new { message = "Failed to create professional." });
    return CreatedAtAction(nameof(GetProfessionalById), new { id = result.Id }, ProfessionalResourceFromEntityAssembler.ToResourceFromEntity(result));
  }

  [HttpGet("{id}")]
  [SwaggerOperation(
        Summary = "Get professional by ID",
        Description = "Retrieves a professional by its unique ID.")]
  [SwaggerResponse(200, "Professional retrieved successfully", typeof(ProfessionalResource))]
  [SwaggerResponse(404, "Professional not found")]
  public async Task<ActionResult> GetProfessionalById(int id)
  {
    var getAppointmentByIdQuery = new GetProfessionalByIdQuery(id);
    var result = await professionalQueryService.Handle(getAppointmentByIdQuery);
    if (result is null) return NotFound();
    return Ok(ProfessionalResourceFromEntityAssembler.ToResourceFromEntity(result));
  }
}