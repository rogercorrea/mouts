// Arquivo: Infrastructure/Configuration/JwtSettings.cs
namespace EmployeeManager.Infrastructure.Configuration
{
    public class JwtSettings
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
    }
}
