namespace neurozen.API.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The update user profile command
 * </summary>
 * <remarks>
 *     This command object includes the user id and profile information to update
 * </remarks>
 */
public record UpdateUserProfileCommand(
    int UserId,
    string? Email,
    string? FullName,
    string? PhoneNumber,
    string? Address,
    string? AvatarUrl,
    DateTime? DateOfBirth
);

