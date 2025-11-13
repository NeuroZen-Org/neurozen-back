using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Model.Queries;
using neurozen.API.Professionals.Domain.Repositories;
using neurozen.API.Professionals.Domain.Services;

namespace neurozen.API.Professionals.Application.Internal.QueryServices;

public class ProfessionalQueryService(IProfessionalRepository professionalRepository) : IProfessionalQueryService
{
    public async Task<IEnumerable<Professional>> Handle(GetAllProfessionalsQuery query)
    {
        return await professionalRepository.GetAllProfessionals();
    }

    public async Task<Professional?> Handle(GetProfessionalByIdQuery query)
    {
        return await professionalRepository.FindByIdAsync(query.Id);
    }
}