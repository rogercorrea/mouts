using EmployeeManager.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee e);
        Task<ActionResult<Employee>> GetByIdAsync(Guid id);
        Task<ActionResult<Employee>> GetByEmailAsync(string email);
        Task<ActionResult<Employee>> GetByDocumentAsync(string doc);
        Task<List<Employee>> ListAsync();
        Task<Employee> UpdateAsync(Employee e);
        Task DeleteAsync(Guid id);
    }
}
