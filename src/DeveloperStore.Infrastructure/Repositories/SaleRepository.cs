using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infrastructure.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _context.Sales
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Sale sale)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(x => x.Items)
            .ToListAsync();
    }
}