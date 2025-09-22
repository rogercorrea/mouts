using EmployeeManager.Domain;
using System.ComponentModel.DataAnnotations;

public class UpdateEmployeeDto
{
    [Required] public required string FirstName { get; set; }
    [Required] public required string LastName { get; set; }
    [Required] [EmailAddress] public required string Email { get; set; }
    public Guid? ManagerId { get; set; }
    public Role Role { get; set; }
}
