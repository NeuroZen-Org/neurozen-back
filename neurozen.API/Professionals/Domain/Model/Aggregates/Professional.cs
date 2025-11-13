using neurozen.API.Professionals.Domain.Model.Commands;

namespace neurozen.API.Professionals.Domain.Model.Aggregates;

public partial class Professional
{
    protected Professional() { }

    public Professional(CreateProfessionalCommand command)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new ArgumentException("Name cannot be empty", nameof(command.Name));

        if (string.IsNullOrWhiteSpace(command.Specialty))
            throw new ArgumentException("Specialty cannot be empty", nameof(command.Specialty));

        if (command.Rating < 0 || command.Rating > 5)
            throw new ArgumentException("Rating must be between 0 and 5", nameof(command.Rating));

        if (command.Reviews < 0)
            throw new ArgumentException("Reviews cannot be negative", nameof(command.Reviews));

        if (command.Price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(command.Price));

        if (string.IsNullOrWhiteSpace(command.Bio))
            throw new ArgumentException("Bio cannot be empty", nameof(command.Bio));

        Name = command.Name;
        Specialty = command.Specialty;
        Experience = command.Experience;
        Rating = command.Rating;
        Reviews = command.Reviews;
        Price = command.Price;
        Availability = command.Availability;
        Bio = command.Bio;
        Image = command.Image;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Specialty { get; private set; } = string.Empty;
    public string Experience { get; private set; } = string.Empty;
    public int Rating { get; private set; }
    public int Reviews { get; private set; }
    public int Price { get; private set; }
    public string Availability { get; private set; } = string.Empty;
    public string Bio { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
}