public class EmployeeDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string DocumentNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public required string Role { get; set; }
    public Guid? ManagerId { get; set; }
}
