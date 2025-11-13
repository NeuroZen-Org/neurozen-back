namespace neurozen.API.Professionals.Domain.Model.Commands;

public record CreateProfessionalCommand(
    string Name,
    string Specialty,
    string Experience,
    int Rating,
    int Reviews,
    int Price,
    string Availability,
    string Bio,
    string Image
);