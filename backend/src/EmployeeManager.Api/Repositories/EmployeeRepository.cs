using EmployeeManager.Domain;
using EmployeeManager.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult<Employee>> GetByDocumentAsync(string doc)
        {
            var employee = await _db.Employees.Where(x => x.DocumentNumber.ToLower().Trim() == doc.ToLower().Trim()).FirstOrDefaultAsync();
            if (employee == null)
                return new NotFoundResult();
            return employee;
        }

        public async Task<ActionResult<Employee>>  GetByEmailAsync(string email)
        {
            var employee = await _db.Employees.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefaultAsync();
            if (employee == null)
                return new NotFoundResult();
            return employee;
        }

        public async Task<ActionResult<Employee>>  GetByIdAsync(Guid id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
                return new NotFoundResult();
            return employee;
        }

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
