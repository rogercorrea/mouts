using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Api.Services;
using EmployeeManager.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly IEmployeeRepository _repo;

    public AuthController(IAuthService auth, IEmployeeRepository repo)
    {
        _auth = auth;
        _repo = repo;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin,Director,Leader,User")]
    public async Task<IActionResult> Register([FromBody] CreateEmployeeDto dto)
    {
        try
        {
            // Determine creator role from token
            var creatorRole = Role.User;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                if (roleClaim != null) Enum.TryParse<Role>(roleClaim.Value, out creatorRole);
            }

            var created = await _auth.RegisterAsync(dto, creatorRole);
            return CreatedAtAction(nameof(Register), new { id = created.Id }, new { created.Id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = await _auth.LoginAsync(dto);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
