using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using neurozen.API.Appointments.Domain.Model.Queries;
using neurozen.API.Appointments.Domain.Services;
using neurozen.API.Appointments.Interfaces.REST.Resources;
using neurozen.API.Appointments.Interfaces.REST.Transform;

namespace neurozen.API.Appointments.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Appointments")]
public class AppointmentsController(
    IAppointmentCommandService appointmentCommandService,
    IAppointmentQueryService appointmentQueryService) : ControllerBase
{
    
    
    
    
    
    
    [HttpGet("appointments/{patientId}")]
    [SwaggerOperation(
        Summary = "Get all appointments by patient ID",
        Description = "Retrieves all appointments associated with a specific patient ID."
    )]
    [SwaggerResponse(200, "Appointments retrieved successfully", typeof(IEnumerable<AppointmentResource>))]
    [SwaggerResponse(400, "Appointments retrieval failed")]
    public async Task<ActionResult<IEnumerable<AppointmentResource>>> GetAllAppointmentsByPatientIdAsync(int patientId)
    {
        var getAllAppointmentsByPatientIdQuery = new GetAllAppointmentsQueryByPatientId(patientId);
        var result = await appointmentQueryService.Handle(getAllAppointmentsByPatientIdQuery);
        var appointments = result.Select(AppointmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(appointments);
    }
}