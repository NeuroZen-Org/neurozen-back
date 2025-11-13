using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Model.Commands;

namespace neurozen.API.Professionals.Domain.Services;

public interface IProfessionalCommandService
{
  Task<Professional?> Handle(CreateProfessionalCommand command);
}