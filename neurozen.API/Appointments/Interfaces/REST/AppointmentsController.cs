using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using neurozen.API.Appointments.Domain.Model.Commands;
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
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new appointment",
        Description = "Creates a new appointment with the provided details."
    )]
    [SwaggerResponse(201, "Appointment created successfully", typeof(AppointmentResource))]
    [SwaggerResponse(400, "Appointment creation failed")]
    public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentResource resource)
    {
        var createAppointmentCommand = CreateAppointmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result =  await appointmentCommandService.Handle(createAppointmentCommand);
        if (result is null) return BadRequest(new { message = "Failed to create appointment. Please verify that the Patient ID and Professional ID exist in the database." });
        return CreatedAtAction(nameof(GetAppointmentById), new{id = result.Id}, AppointmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
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
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get appointment by ID",
        Description = "Retrieves an appointment by its unique ID.")]
    [SwaggerResponse(200, "Appointment retrieved successfully", typeof(AppointmentResource))]
    [SwaggerResponse(404, "Appointment not found")]
    public async Task<ActionResult> GetAppointmentById(int id)
    {
        var getAppointmentByIdQuery = new GetAppointmentByIdQuery(id);
        var result = await appointmentQueryService.Handle(getAppointmentByIdQuery);
        if (result is null) return NotFound();
        return Ok(AppointmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}