using neurozen.API.IAM.Domain.Model.Commands;
using neurozen.API.IAM.Interfaces.REST.Resources;

namespace neurozen.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}