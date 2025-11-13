using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Professionals.Domain.Repositories;

public interface IProfessionalRepository : IBaseRepository<Professional>
{
    /// <summary>
    ///     Get all professionals
    /// </summary>
    /// <returns>An enumerable with all professionals</returns>
    Task<IEnumerable<Professional>> GetAllProfessionals();
}