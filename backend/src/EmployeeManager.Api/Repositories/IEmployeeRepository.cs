using EmployeeManager.Domain;

namespace EmployeeManager.Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee e);
        Task<Employee> GetByIdAsync(Guid id);
        Task<Employee> GetByEmailAsync(string email);
        Task<Employee> GetByDocumentAsync(string doc);
        Task<List<Employee>> ListAsync();
        Task<Employee> UpdateAsync(Employee e);
        Task DeleteAsync(Guid id);
    }
}
