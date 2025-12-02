using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Model.Commands;
using neurozen.API.Professionals.Domain.Repositories;
using neurozen.API.Professionals.Domain.Services;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Professionals.Application.Internal.CommandServices;

public class ProfessionalCommandService(
  IProfessionalRepository professionalRepository,
  IUnitOfWork unitOfWork,
  ILogger<ProfessionalCommandService> logger
) : IProfessionalCommandService
{
  public async Task<Professional?> Handle(CreateProfessionalCommand command)
  {
    var professional = new Professional(command);

    try
    {
      await professionalRepository.AddAsync(professional);
      await unitOfWork.CompleteAsync();
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error creating professional");
      return null;
    }
    return professional;
  }
}