using EmployeeManager.Domain;
using System.ComponentModel.DataAnnotations;

public class CreateEmployeeDto
{
    [Required] public required string FirstName { get; set; }
    [Required] public required string LastName { get; set; }
    [Required] [EmailAddress] public required string Email { get; set; }
    [Required] public required string DocumentNumber { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    [Required] public required string Password { get; set; }
    public Guid? ManagerId { get; set; }
    public Role Role { get; set; } = Role.User;
}
