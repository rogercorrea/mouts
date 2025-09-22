using Xunit;
using Moq;
using EmployeeManager.Api.Repositories;
using EmployeeManager.Api.Services;
using EmployeeManager.Domain;
using System.Threading.Tasks;
using System;

public class AuthServiceTests
{
    [Fact]
    public async Task Register_Should_Reject_Underage()
    {
        var repoMock = new Mock<IEmployeeRepository>();
        var configMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        var svc = new AuthService(repoMock.Object, configMock.Object);

        var dto = new CreateEmployeeDto
        {
            FirstName = "Kid",
            LastName = "Young",
            Email = "kid@example.com",
            DocumentNumber = "12345",
            BirthDate = DateTime.UtcNow.AddYears(-17),
            Password = "Passw0rd!",
        };

        await Assert.ThrowsAsync<Exception>(() => svc.RegisterAsync(dto, Role.Admin));
    }
}
