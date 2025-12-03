namespace neurozen.API.IAM.Interfaces.REST.Resources;

public record UserResource(
    int Id, 
    string Username,
    string? Email,
    string? FullName,
    string? PhoneNumber,
    string? Address,
    string? AvatarUrl,
    DateTime? DateOfBirth,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
);
