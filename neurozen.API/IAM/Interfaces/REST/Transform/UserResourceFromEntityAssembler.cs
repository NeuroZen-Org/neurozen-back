using neurozen.API.IAM.Domain.Model.Aggregates;
using neurozen.API.IAM.Interfaces.REST.Resources;

namespace neurozen.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(
            user.Id, 
            user.Username,
            user.Email,
            user.FullName,
            user.PhoneNumber,
            user.Address,
            user.AvatarUrl,
            user.DateOfBirth,
            user.CreatedAt,
            user.UpdatedAt
        );
    }
}