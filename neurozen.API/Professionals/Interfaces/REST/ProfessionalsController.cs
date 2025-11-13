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
}