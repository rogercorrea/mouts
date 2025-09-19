using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using EmployeeManager.Api.Repositories;
using EmployeeManager.Domain;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManager.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IEmployeeRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
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
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null) throw new Exception("Invalid credentials");
            if (user.PasswordHash == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            // create token
            var key = Encoding.ASCII.GetBytes(_config["JWT:Key"] ?? throw new Exception("JWT key missing"));
            var issuer = _config["JWT:Issuer"] ?? "EmployeeManagerApi";
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
