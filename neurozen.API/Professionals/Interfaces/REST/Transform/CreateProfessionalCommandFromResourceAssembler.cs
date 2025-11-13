using neurozen.API.Professionals.Domain.Model.Commands;
using neurozen.API.Professionals.Interfaces.REST.Resources;

namespace neurozen.API.Professionals.Interfaces.REST.Transform;

public class CreateProfessionalCommandFromResourceAssembler
{
  public static CreateProfessionalCommand ToCommandFromResource(CreateProfessionalResource resource) => new(resource.Name, resource.Specialty, resource.Experience, resource.Rating, resource.Reviews, resource.Price, resource.Availability, resource.Bio, resource.Image);
}