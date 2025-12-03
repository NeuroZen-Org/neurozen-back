using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using neurozen.API.Resources;
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
    ISubscriptionRepository subscriptionRepository,
    IStringLocalizer<SharedResource> _localizer) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Creates a subscription",
        Description = "Creates a subscription with given parameters")]
    [SwaggerResponse(201, "The subscription was created", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "The subscription failed to be created")]
    public async Task<ActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource)
    {
        String msgNumberCard = _localizer.GetString("NumberCardError");
        // 🔹 Validaciones
        if (string.IsNullOrWhiteSpace(resource.NumberCard) || resource.NumberCard.Length != 16 || !resource.NumberCard.All(char.IsDigit))
            return BadRequest(new { message = msgNumberCard });
        
        String msgPlanId = _localizer.GetString("PlanIdError");
        if (resource.PlanId is < 1 or > 3)
            return BadRequest(new { message = msgPlanId });
        
        String msgCvv = _localizer.GetString("CvvError");
        if (string.IsNullOrWhiteSpace(resource.Cvv) || resource.Cvv.Length != 3 || !resource.Cvv.All(char.IsDigit))
            return BadRequest(new { message = msgCvv });

        String msgExpirationDate = _localizer.GetString("ExpirationDateError");
        if (string.IsNullOrWhiteSpace(resource.ExpirationDate) || resource.ExpirationDate.Length != 5)
            return BadRequest(new { message = msgExpirationDate });
        
        String msgEmailUser = _localizer.GetString("EmailUserError");
        if (string.IsNullOrWhiteSpace(resource.EmailUser) || !resource.EmailUser.EndsWith("@gmail.com"))
            return BadRequest(new { message = msgEmailUser });
        
        String msgSubscriptionActive = _localizer.GetString("SubscriptionActiveError");
        // 🔹 Validación explícita de IsActive
        if (resource.IsActive == true)
            return BadRequest(new { message = msgSubscriptionActive });
        
        String msgUserId = _localizer.GetString("UserIdError");
        // 🔹 Validación de UserId
        if (resource.UserId <= 0)
            return BadRequest(new { message = msgUserId });

        // 🔹 Crear comando y procesar
        var createSubscriptionCommand = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await subscriptionCommandService.Handle(createSubscriptionCommand);

        if (result is null)
        {
            String msg = _localizer.GetString("CreateSubscriptionError");
            return BadRequest(new { message = msg });
        }

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
        String msg = _localizer.GetString("GetSubscriptionsByPlanIdError");
        var subscriptions = await subscriptionRepository.FindByPlanIdAsync(planId);
        if (!subscriptions.Any())
            return NotFound(new { message = msg});

        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    // 🔹 GET por IsActive
    [HttpGet("active/{isActive}")]
    [SwaggerOperation(Summary = "Get subscriptions by IsActive")]
    [SwaggerResponse(200, "Subscriptions found", typeof(IEnumerable<SubscriptionResource>))]
    [SwaggerResponse(404, "No subscriptions found with given IsActive")]
    public async Task<ActionResult<IEnumerable<SubscriptionResource>>> GetByIsActive(bool isActive)
    {
        String msg = _localizer.GetString("GetSubscriptionsByIsActiveError");
        var subscriptions = await subscriptionRepository.FindByIsActiveAsync(isActive);
        if (!subscriptions.Any())
            return NotFound(new { message = msg});

        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    // 🔹 GET por UserId
    [HttpGet("user/{userId}")]
    [SwaggerOperation(Summary = "Get subscriptions by UserId")]
    [SwaggerResponse(200, "Subscriptions found", typeof(IEnumerable<SubscriptionResource>))]
    [SwaggerResponse(404, "No subscriptions found for given UserId")]
    public async Task<ActionResult<IEnumerable<SubscriptionResource>>> GetByUserId(int userId)
    {
        String msg = _localizer.GetString("GetSubscriptionsByUserIdError");
        var subscriptions = await subscriptionRepository.FindByUserIdAsync(userId);
        if (!subscriptions.Any())
            return NotFound(new { message = msg });

        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
