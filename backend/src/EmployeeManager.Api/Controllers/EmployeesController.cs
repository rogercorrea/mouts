using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Api.Repositories;
using EmployeeManager.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repo;

    public EmployeesController(IEmployeeRepository repo) { _repo = repo; }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var list = await _repo.ListAsync();
        var dto = list.Select(e => new EmployeeDto {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            DocumentNumber = e.DocumentNumber,
            BirthDate = e.BirthDate,
            Role = e.Role.ToString(),
            ManagerId = e.ManagerId
        }).ToList();
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var ret = await _repo.GetByIdAsync(id);
        if (ret == null) return NotFound();
        
        var employee = ret.Value;
        if (employee == null) return NotFound();
        var dto = new EmployeeDto {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            DocumentNumber = employee.DocumentNumber,
            BirthDate = employee.BirthDate,
            Role = employee.Role.ToString(),
            ManagerId = employee.ManagerId
        };
        
        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Director,Leader")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        try
        {
            // creator role from token
            var creatorRole = Role.User;
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim != null) Enum.TryParse<Role>(roleClaim.Value, out creatorRole);

            // Age validation (already in service) - but check here too
            var age = DateTime.UtcNow.Year - dto.BirthDate.Year;
            if (dto.BirthDate > DateTime.UtcNow.AddYears(-age)) age--;
            if (age < 18) return BadRequest(new { message = "Employee must be adult (>=18)" });

            var created = await _repo.CreateAsync(new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DocumentNumber = dto.DocumentNumber,
                BirthDate = dto.BirthDate.ToUniversalTime(),
                Role = dto.Role,
                ManagerId = dto.ManagerId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            });

            return CreatedAtAction(nameof(Get), new { id = created.Id }, new { created.Id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Director,Leader")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeDto dto)
    {
        var ret = await _repo.GetByIdAsync(id);
        if (ret == null) return NotFound();

        var employee = ret.Value;
        if (employee == null) return NotFound();

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Email = dto.Email;
        employee.ManagerId = dto.ManagerId;
        employee.Role = dto.Role;
        await _repo.UpdateAsync(employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}
