using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using neurozen.API.Subscriptions.Domain.Services;
using neurozen.API.Subscriptions.Domain.Repositories;
using neurozen.API.Subscriptions.Interfaces.REST.Resources;
using neurozen.API.Subscriptions.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace neurozen.API.Subscriptions.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Subscriptions")]
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionRepository subscriptionRepository) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a subscription",
        Description = "Creates a subscription with given parameters")]
    [SwaggerResponse(201, "The subscription was created", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "The subscription failed to be created")]
    public async Task<ActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource)
    {
        // 🔹 Validaciones
        if (string.IsNullOrWhiteSpace(resource.NumberCard) || resource.NumberCard.Length != 16 || !resource.NumberCard.All(char.IsDigit))
            return BadRequest(new { message = "Card number must be exactly 16 digits and numeric." });

        if (resource.PlanId is < 1 or > 3)
            return BadRequest(new { message = "PlanId must be 1, 2, or 3." });

        if (string.IsNullOrWhiteSpace(resource.Cvv) || resource.Cvv.Length != 3 || !resource.Cvv.All(char.IsDigit))
            return BadRequest(new { message = "CVV must be exactly 3 digits and numeric." });

        if (string.IsNullOrWhiteSpace(resource.ExpirationDate) || resource.ExpirationDate.Length != 5)
            return BadRequest(new { message = "ExpirationDate must be exactly 5 characters (MM/YY)." });

        if (string.IsNullOrWhiteSpace(resource.EmailUser) || !resource.EmailUser.EndsWith("@gmail.com"))
            return BadRequest(new { message = "Email must end with @gmail.com." });
        
        // 🔹 Validación explícita de IsActive
        if (!(resource.IsActive == true || resource.IsActive == false))
            return BadRequest(new { message = "IsActive must be either true or false." });

        // 🔹 Validación de UserId
        if (resource.UserId <= 0)
            return BadRequest(new { message = "UserId must be a positive integer." });

        // 🔹 Crear comando y procesar
        var createSubscriptionCommand = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await subscriptionCommandService.Handle(createSubscriptionCommand);

        if (result is null) 
            return BadRequest(new { message = "Failed to create subscription" });

        return CreatedAtAction(
            nameof(CreateSubscription), 
            new { id = result.Id }, 
            SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(result)
        );
    }

    // 🔹 GET por PlanId
    [HttpGet("plan/{planId}")]
    [SwaggerOperation(Summary = "Get subscriptions by PlanId")]
    [SwaggerResponse(200, "Subscriptions found", typeof(IEnumerable<SubscriptionResource>))]
    [SwaggerResponse(404, "No subscriptions found for given PlanId")]
    public async Task<ActionResult<IEnumerable<SubscriptionResource>>> GetByPlanId(int planId)
    {
        var subscriptions = await subscriptionRepository.FindByPlanIdAsync(planId);
        if (!subscriptions.Any())
            return NotFound(new { message = $"No subscriptions found for PlanId {planId}" });

        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    // 🔹 GET por IsActive
    [HttpGet("active/{isActive}")]
    [SwaggerOperation(Summary = "Get subscriptions by IsActive")]
    [SwaggerResponse(200, "Subscriptions found", typeof(IEnumerable<SubscriptionResource>))]
    [SwaggerResponse(404, "No subscriptions found with given IsActive")]
    public async Task<ActionResult<IEnumerable<SubscriptionResource>>> GetByIsActive(bool isActive)
    {
        var subscriptions = await subscriptionRepository.FindByIsActiveAsync(isActive);
        if (!subscriptions.Any())
            return NotFound(new { message = $"No subscriptions found with IsActive = {isActive}" });

        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    // 🔹 GET por UserId
    [HttpGet("user/{userId}")]
    [SwaggerOperation(Summary = "Get subscriptions by UserId")]
    [SwaggerResponse(200, "Subscriptions found", typeof(IEnumerable<SubscriptionResource>))]
    [SwaggerResponse(404, "No subscriptions found for given UserId")]
    public async Task<ActionResult<IEnumerable<SubscriptionResource>>> GetByUserId(int userId)
    {
        var subscriptions = await subscriptionRepository.FindByUserIdAsync(userId);
        if (!subscriptions.Any())
            return NotFound(new { message = $"No subscriptions found for UserId {userId}" });

        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
