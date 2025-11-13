using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Model.Queries;

namespace neurozen.API.Professionals.Domain.Services;

public interface IProfessionalQueryService
{
    Task<IEnumerable<Professional>> Handle(GetAllProfessionalsQuery query);
}