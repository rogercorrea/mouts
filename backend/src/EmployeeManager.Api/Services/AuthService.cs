using EmployeeManager.Api.Repositories;
using EmployeeManager.Domain;
using Microsoft.Extensions.Options; // Add this for IOptions<>
using EmployeeManager.Infrastructure.Configuration; // Add this for JwtSettings (adjust namespace if needed)

namespace EmployeeManager.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthService(IEmployeeRepository repo, IOptions<JwtSettings> jwtSettings)
        {
            _repo = repo;
            _jwtSettings = jwtSettings;
        }

        public async Task<Employee> RegisterAsync(CreateEmployeeDto dto, Role creatorRole)
        {
            // Validate age
            var age = DateTime.UtcNow.Year - dto.BirthDate.Year;
            if (dto.BirthDate > DateTime.UtcNow.AddYears(-age)) age--;
            if (age < 18) throw new Exception("Employee must be adult (>=18)");

            // Unique checks
            var byEmail = await _repo.GetByEmailAsync(dto.Email);
            if (byEmail != null) throw new Exception("Email already used");
            var byDoc = await _repo.GetByDocumentAsync(dto.DocumentNumber);
            if (byDoc != null) throw new Exception("Document number already used");

            // Role rule: cannot create higher role than creator
            if ((int)dto.Role > (int)creatorRole) throw new Exception("Cannot create user with higher role than creator");

            var e = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DocumentNumber = dto.DocumentNumber,
                BirthDate = dto.BirthDate,
                Role = dto.Role,
                ManagerId = dto.ManagerId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _repo.CreateAsync(e);
            return e;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var ret = await _repo.GetByEmailAsync(dto.Email);
            if (ret == null) throw new InvalidDataException("Invalid user");

            var user = ret?.Value;
            if (user == null)
            {
                throw new InvalidDataException("Invalid user");
            }

            if (string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");
            
            return new LoginService(_jwtSettings).GenerateToken(user);
        }
    }
}
