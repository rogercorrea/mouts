# EmployeeManager Project

## Overview
EmployeeManager is a full-stack application for managing employees, built with:
- **Backend:** .NET 8, Entity Framework Core, PostgreSQL, JWT Authentication
- **Frontend:** React + Vite
- **Testing:** xUnit, Moq, FluentAssertions, EF Core InMemory

The backend exposes REST APIs for employee management and authentication. The frontend provides a responsive interface with password validation.

## Backend Setup

### Prerequisites
- .NET 8 SDK
- PostgreSQL
- Docker & Docker Compose (optional for containerized setup)

### Configuration
Create a `.env` file with the following variables:

```env
POSTGRES_HOST=postgres
POSTGRES_PORT=5432
POSTGRES_USER=emuser
POSTGRES_PASSWORD=EmP@ssw0rd!
POSTGRES_DB=employee_manager_db

CONNECTION_STRING=Host=postgres;Port=5432;Database=employee_manager_db;Username=emuser;Password=EmP@ssw0rd!

JWT_ISSUER=EmployeeManagerApi
JWT_KEY=YourSuperSecretJwtKey

REACT_APP_API_URL=http://localhost:8080
```

### Running Locally

1. Restore packages and build the project:
```bash
dotnet restore
dotnet build
```
2. Apply database migrations:
```bash
dotnet ef database update
```
3. Run the API:
```bash
dotnet run --project EmployeeManager.Api
```

### Running with Docker Compose

1. Build and start the containers:
```bash
docker-compose up --build
```
2. The API will be available at `http://localhost:8080`

## Frontend Setup

### Prerequisites
- Node.js 20+
- npm or yarn

### Running Locally

1. Install dependencies:
```bash
npm install
```
2. Run the development server:
```bash
npm run dev
```
3. Open `http://localhost:3000` in your browser.

### Production Build
```bash
npm run build
npm run preview
```

## Password Validation
The frontend validates password strength with the following rules:
- Minimum 8 characters
- At least one lowercase letter
- At least one uppercase letter
- At least one number
- At least one special character (!@#$%)
- No spaces

Weak passwords will trigger an alert: `Weak password — please fix the requirements before continuing.`

## Running Tests

### Backend
```bash
dotnet test EmployeeManager.Tests
```

### Frontend
```bash
npm test
```

## Security Notes
- JWT keys **should not** be hardcoded in the source code. Use environment variables or a secure secrets manager.
- Passwords are hashed using BCrypt.

## CORS Configuration
The API allows requests from `http://localhost:3000` during development.

## License
MIT

