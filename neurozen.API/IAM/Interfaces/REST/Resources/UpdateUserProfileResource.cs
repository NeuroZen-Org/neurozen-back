namespace neurozen.API.IAM.Interfaces.REST.Resources;

/**
 * <summary>
 *     The update user profile resource
 * </summary>
 * <remarks>
 *     This resource is used to update user profile information
 * </remarks>
 */
public record UpdateUserProfileResource(
    string? Email,
    string? FullName,
    string? PhoneNumber,
    string? Address,
    string? AvatarUrl,
    DateTime? DateOfBirth
);

