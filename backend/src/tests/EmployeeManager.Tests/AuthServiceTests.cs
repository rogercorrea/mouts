// src/tests/EmployeeManager.Tests/AuthServiceTests.cs
using System;
using System.Threading.Tasks;
using EmployeeManager.Api.Repositories;
using EmployeeManager.Api.Services;
using EmployeeManager.Infrastructure.Configuration;
using EmployeeManager.Infrastructure;
using EmployeeManager.Domain;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace EmployeeManager.Tests
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IEmployeeRepository> _employeeRepoMock;

        public AuthServiceTests()
        {
            // Mock do repositório
            _employeeRepoMock = new Mock<IEmployeeRepository>();

            // Configuração JWT
            var jwtSettings = Options.Create(new JwtSettings
            {
                Key = "VerySecretKeyForDevDontUseInProd123!",
                Issuer = "EmployeeManagerApi"
            });

            // Configuração de IConfiguration se necessário
            var configurationMock = new Mock<IConfiguration>();

            // Instancia o serviço com mocks
            _authService = new AuthService(_employeeRepoMock.Object, jwtSettings);
        }

        [Fact]
        public async Task Authenticate_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            var password = "Test@1234";

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john@company.com",
                DocumentNumber = "123456789",
                BirthDate = DateTime.UtcNow,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = 0,
                ManagerId = null
            };

            _employeeRepoMock
                .Setup(r => r.GetByEmailAsync(employee.Email))
                .ReturnsAsync(employee);

            // Act
            var result = await _authService.LoginAsync(new LoginDto
            {
                Email = employee.Email,
                Password = password
            });

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Authenticate_WithInvalidPassword_ReturnsNull()
        {
            // Arrange
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john@company.com",
                DocumentNumber = "123456789",
                BirthDate = DateTime.UtcNow,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword"),
                Role = 0,
                ManagerId = null
            };

            _employeeRepoMock
                .Setup(r => r.GetByEmailAsync(employee.Email))
                .ReturnsAsync(employee);

            // Act
            var result = await _authService.LoginAsync(new LoginDto
            {
                Email = employee.Email,
                Password = "WrongPassword"
            });

            // Assert
            result.Should().BeNull();
        }
    }
}
