public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string Role { get; set; }
    public Guid? ManagerId { get; set; }
}
