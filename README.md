# Employee Manager - Complete Scaffold

Includes:
- Backend (.NET 8) with automatic migration on startup (entrypoint.sh)
- Frontend (React + Vite) with Create Employee form
- PostgreSQL in Docker
- Basic unit test (xUnit) and GitHub Actions CI


Run:
1. cp .env.sample .env
2. docker-compose up --build

Notes:
- entrypoint will attempt to run `dotnet ef database update`. If dotnet-ef isn't available in runtime stage, the SQL migration in /backend/src/EmployeeManager.Api/Migrations can be used manually.
- In CI, tests run via `dotnet test`.
