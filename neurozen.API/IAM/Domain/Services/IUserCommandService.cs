using neurozen.API.IAM.Domain.Model.Aggregates;
using neurozen.API.IAM.Domain.Model.Commands;

namespace neurozen.API.IAM.Domain.Services;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This interface is used to handle user commands
 * </remarks>
 */
public interface IUserCommandService
{
    /**
        * <summary>
        *     Handle sign in command
        * </summary>
        * <param name="command">The sign in command</param>
        * <returns>The authenticated user and the JWT token</returns>
        */
    Task<(User user, string token)> Handle(SignInCommand command);

    /**
        * <summary>
        *     Handle sign up command
        * </summary>
        * <param name="command">The sign up command</param>
        * <returns>A confirmation message on successful creation.</returns>
        */
    Task Handle(SignUpCommand command);

    /**
        * <summary>
        *     Handle update user profile command
        * </summary>
        * <param name="command">The update user profile command</param>
        * <returns>The updated user</returns>
        */
    Task<User?> Handle(UpdateUserProfileCommand command);
}