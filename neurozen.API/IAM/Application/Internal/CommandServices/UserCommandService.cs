using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.IAM.Application.Internal.OutboundServices;
using neurozen.API.IAM.Domain.Model.Aggregates;
using neurozen.API.IAM.Domain.Model.Commands;
using neurozen.API.IAM.Domain.Repositories;
using neurozen.API.IAM.Domain.Services;

namespace neurozen.API.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This class is used to handle user commands
 * </remarks>
 */
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    /**
     * <summary>
     *     Handle sign in command
     * </summary>
     * <param name="command">The sign in command</param>
     * <returns>The authenticated user and the JWT token</returns>
     */
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    /**
     * <summary>
     *     Handle sign up commands
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        
        // Actualizar perfil con email y fullName si se proporcionaron
        if (!string.IsNullOrWhiteSpace(command.Email) || !string.IsNullOrWhiteSpace(command.FullName))
        {
            user.UpdateProfile(command.Email, command.FullName, null, null, null, null);
        }
        
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }

    /**
     * <summary>
     *     Handle update user profile command
     * </summary>
     * <param name="command">The update user profile command</param>
     * <returns>The updated user</returns>
     */
    public async Task<User?> Handle(UpdateUserProfileCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.UserId);

        if (user == null)
            throw new Exception($"User with id {command.UserId} not found");

        user.UpdateProfile(
            command.Email,
            command.FullName,
            command.PhoneNumber,
            command.Address,
            command.AvatarUrl,
            command.DateOfBirth
        );

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while updating user profile: {e.Message}");
        }
    }
}