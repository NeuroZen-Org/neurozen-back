using neurozen.API.IAM.Domain.Model.Commands;
using neurozen.API.IAM.Interfaces.REST.Resources;

namespace neurozen.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        // Combinar firstName y lastName en fullName
        string? fullName = null;
        if (!string.IsNullOrWhiteSpace(resource.FirstName) || !string.IsNullOrWhiteSpace(resource.LastName))
        {
            fullName = $"{resource.FirstName?.Trim()} {resource.LastName?.Trim()}".Trim();
        }
        
        return new SignUpCommand(
            resource.Username, 
            resource.Password, 
            resource.Email, 
            fullName);
    }
}