using EmployeeManager.Domain;
using EmployeeManager.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly AppDbContext _db;
        public EmployeeRepository(AppDbContext db) { _db = db; }

        public async Task<Employee> CreateAsync(Employee e)
        {
            _db.Employees.Add(e);
            await _db.SaveChangesAsync();
            return e;
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Employees.FindAsync(id);
            if (e == null) return;
            _db.Employees.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<Employee> GetByDocumentAsync(string doc) =>
            await _db.Employees.FirstOrDefaultAsync(x => x.DocumentNumber == doc);

        public async Task<Employee> GetByEmailAsync(string email) =>
            await _db.Employees.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<Employee> GetByIdAsync(Guid id) =>
            await _db.Employees.FindAsync(id);

        public async Task<List<Employee>> ListAsync() =>
            await _db.Employees.ToListAsync();

        public async Task<Employee> UpdateAsync(Employee e)
        {
            _db.Employees.Update(e);
            await _db.SaveChangesAsync();
            return e;
        }
    }
}
