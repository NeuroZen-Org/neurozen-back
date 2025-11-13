using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Interfaces.REST.Resources;

namespace neurozen.API.Professionals.Interfaces.REST.Transform;

public class ProfessionalResourceFromEntityAssembler
{
  public static ProfessionalResource ToResourceFromEntity(Professional entity) => new(
    entity.Id,
    entity.Name,
    entity.Specialty,
    entity.Experience,
    entity.Rating,
    entity.Reviews,
    entity.Price,
    entity.Availability,
    entity.Bio,
    entity.Image
  );
}