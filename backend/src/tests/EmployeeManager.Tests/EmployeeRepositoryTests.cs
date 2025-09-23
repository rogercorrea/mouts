using EmployeeManager.Api.Repositories;
using EmployeeManager.Api.Services;
using EmployeeManager.Infrastructure;
using EmployeeManager.Infrastructure.Configuration;
using EmployeeManager.Domain;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

public class EmployeeRepositoryTests
{
    private readonly AppDbContext _contextMock;
    private readonly Mock<IEmployeeRepository> _employeeRepoMock;

    public EmployeeRepositoryTests()
    {
        _contextMock = TestHelpers.CreateInMemoryDbContext();
        _employeeRepoMock = new Mock<IEmployeeRepository>();
    }

    [Fact]
    public void AddEmployee_ShouldAddEmployee()
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "test@company.com",
            DocumentNumber = "123456789",
            BirthDate = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("teste@1234"),
            Role = Role.User,
            ManagerId = null
        };

        _employeeRepoMock.Object.CreateAsync(employee);

        _contextMock.Employees.Should().ContainSingle(e => e.Email == "test@company.com");
    }

    [Fact]
    public void GetByEmail_ShouldReturnEmployee()
    {
        var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "test@company.com",
                DocumentNumber = "123456789",
                BirthDate = DateTime.UtcNow,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword"),
                Role = Role.User,
                ManagerId = null
            };

        _contextMock.Employees.Add(employee);
        _contextMock.SaveChanges();

        var result = _employeeRepoMock.Object.GetByEmailAsync("test@company.com").Result.Value;
        result.Should().NotBeNull();
        result.Email.Should().Be("test@company.com");
    }
}
