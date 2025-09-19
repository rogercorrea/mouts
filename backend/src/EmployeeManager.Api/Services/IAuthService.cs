using EmployeeManager.Domain;

namespace EmployeeManager.Api.Services
{
    public interface IAuthService
    {
        Task<Employee> RegisterAsync(CreateEmployeeDto dto, Role creatorRole);
        Task<string> LoginAsync(LoginDto dto);
    }
}
