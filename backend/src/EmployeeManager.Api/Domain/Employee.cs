using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Domain
{
    public enum Role
    {
        User = 0,
        Leader = 1,
        Director = 2,
        Admin = 3
    }

    public class Employee
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string DocumentNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string? PasswordHash { get; set; }

        public Role Role { get; set; } = Role.User;

        public Guid? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee? Manager { get; set; }
    }
}
