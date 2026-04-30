using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Infrastructure.Repositories;

public class FakeSaleRepository : ISaleRepository
{
    private readonly List<Sale> _storage = new();

    public Task AddAsync(Sale sale)
    {
        _storage.Add(sale);
        return Task.CompletedTask;
    }

    public Task<Sale?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_storage.FirstOrDefault(x => x.Id == id));
    }

    public Task UpdateAsync(Sale sale)
    {
        return Task.CompletedTask;
    }
}