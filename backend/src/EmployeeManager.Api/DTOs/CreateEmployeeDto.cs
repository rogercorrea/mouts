using EmployeeManager.Domain;
using System.ComponentModel.DataAnnotations;

public class CreateEmployeeDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }
    [Required] public string DocumentNumber { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    [Required] public string Password { get; set; }
    public Guid? ManagerId { get; set; }
    public Role Role { get; set; } = Role.User;
}
