using EmployeeManager.Domain;
using System.ComponentModel.DataAnnotations;

public class UpdateEmployeeDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }
    public Guid? ManagerId { get; set; }
    public Role Role { get; set; }
}
