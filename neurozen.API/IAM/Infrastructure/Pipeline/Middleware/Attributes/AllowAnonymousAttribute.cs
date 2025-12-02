namespace neurozen.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

using Microsoft.AspNetCore.Authorization;

/**
 * This attribute is used to decorate controllers and actions that do not require authorization.
 * It reuses ASP.NET Core's built-in AllowAnonymousAttribute so authorization filters recognize it.
 */
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AllowAnonymousAttribute : Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute
{
}