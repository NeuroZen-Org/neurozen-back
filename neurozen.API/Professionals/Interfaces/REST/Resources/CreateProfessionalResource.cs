namespace neurozen.API.Professionals.Interfaces.REST.Resources;

public record CreateProfessionalResource(string Name, string Specialty, string Experience, int Rating, int Reviews, int Price, string Availability, string Bio, string Image);