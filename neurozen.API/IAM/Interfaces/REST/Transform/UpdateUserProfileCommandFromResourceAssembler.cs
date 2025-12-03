using neurozen.API.IAM.Domain.Model.Commands;
using neurozen.API.IAM.Interfaces.REST.Resources;

namespace neurozen.API.IAM.Interfaces.REST.Transform;

public static class UpdateUserProfileCommandFromResourceAssembler
{
    public static UpdateUserProfileCommand ToCommandFromResource(int userId, UpdateUserProfileResource resource)
    {
        return new UpdateUserProfileCommand(
            userId,
            resource.Email,
            resource.FullName,
            resource.PhoneNumber,
            resource.Address,
            resource.AvatarUrl,
            resource.DateOfBirth
        );
    }
}

