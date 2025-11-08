namespace neurozen.API.Shared.Domain.Repositories;

public interface IEntityWithCreatedUpdatedDated
{
    DateTimeOffset CreatedDate { get; set; }
    DateTimeOffset UpdatedDate { get; set; }
}

