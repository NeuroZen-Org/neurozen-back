namespace neurozen.API.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The sign up command
 * </summary>
 * <remarks>
 *     This command object includes the username, password, email and full name to sign up
 * </remarks>
 */
public record SignUpCommand(
    string Username, 
    string Password, 
    string? Email = null, 
    string? FullName = null);
