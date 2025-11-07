namespace neurozen.API.Shared.Domain.Repositories;

public interface IEntityWithCreatedUpdatedDated
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}

