using System.Text.Json.Serialization;

namespace neurozen.API.IAM.Domain.Model.Aggregates;

/**
 * <summary>
 *     The user aggregate
 * </summary>
 * <remarks>
 *     This class is used to represent a user
 * </remarks>
 */
public class User(string username, string passwordHash)
{
    public User() : this(string.Empty, string.Empty)
    {
    }

    public int Id { get; }
    public string Username { get; private set; } = username;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    public string? Email { get; private set; }
    public string? FullName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Address { get; private set; }
    public string? AvatarUrl { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; } = DateTime.UtcNow;

    /**
     * <summary>
     *     Update the username
     * </summary>
     * <param name="username">The new username</param>
     * <returns>The updated user</returns>
     */
    public User UpdateUsername(string username)
    {
        Username = username;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    /**
     * <summary>
     *     Update the password hash
     * </summary>
     * <param name="passwordHash">The new password hash</param>
     * <returns>The updated user</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    /**
     * <summary>
     *     Update user profile information
     * </summary>
     * <param name="email">The email</param>
     * <param name="fullName">The full name</param>
     * <param name="phoneNumber">The phone number</param>
     * <param name="address">The address</param>
     * <param name="avatarUrl">The avatar URL</param>
     * <param name="dateOfBirth">The date of birth</param>
     * <returns>The updated user</returns>
     */
    public User UpdateProfile(string? email, string? fullName, string? phoneNumber, string? address, string? avatarUrl, DateTime? dateOfBirth)
    {
        Email = email;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Address = address;
        AvatarUrl = avatarUrl;
        DateOfBirth = dateOfBirth;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }
}