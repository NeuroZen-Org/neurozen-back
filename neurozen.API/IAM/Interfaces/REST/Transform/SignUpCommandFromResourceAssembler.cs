using neurozen.API.IAM.Domain.Model.Commands;
using neurozen.API.IAM.Interfaces.REST.Resources;

namespace neurozen.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}