using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Application.Interfaces;

public interface ISaleRepository
{
    Task AddAsync(Sale sale);
    Task<Sale?> GetByIdAsync(Guid id);
    Task UpdateAsync(Sale sale);
    Task<List<Sale>> GetAllAsync();
}