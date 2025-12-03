namespace neurozen.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    string Username, 
    string Password, 
    string? Email = null, 
    string? FirstName = null, 
    string? LastName = null);
