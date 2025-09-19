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
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return NotFound();
        var dto = new EmployeeDto {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            DocumentNumber = e.DocumentNumber,
            BirthDate = e.BirthDate,
            Role = e.Role.ToString(),
            ManagerId = e.ManagerId
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
                BirthDate = dto.BirthDate,
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
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return NotFound();
        e.FirstName = dto.FirstName;
        e.LastName = dto.LastName;
        e.Email = dto.Email;
        e.ManagerId = dto.ManagerId;
        e.Role = dto.Role;
        await _repo.UpdateAsync(e);
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
