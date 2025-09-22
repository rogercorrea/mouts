using EmployeeManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Pega a connection string das variáveis de ambiente
        var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? throw new Exception("POSTGRES_HOST not set");
        var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? throw new Exception("POSTGRES_PORT not set");
        var db = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? throw new Exception("POSTGRES_DB not set");
        var user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? throw new Exception("POSTGRES_USER not set");
        var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? throw new Exception("POSTGRES_PASSWORD not set");

        var conn = $"Host={host};Database={db};Username={user};Password={pass}";

        optionsBuilder.UseNpgsql(conn);
        
        return new AppDbContext(optionsBuilder.Options);
    }
}
