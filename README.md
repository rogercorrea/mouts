# EmployeeManager

EmployeeManager is a web application for managing employees, built with .NET 8 for the backend and React for the frontend. It uses JWT for authentication, PostgreSQL as the database, and includes password validation, role-based access, and API endpoints for managing employees.

---

## Tech Stack

- **Backend**: .NET 8, C#
- **Frontend**: React (Vite)
- **Database**: PostgreSQL
- **Authentication**: JWT
- **ORM**: Entity Framework Core
- **Testing**: xUnit, Moq, FluentAssertions, Microsoft.EntityFrameworkCore.InMemory

---

## Environment Variables

Create a `.env` file in the project root with the following variables:

### Backend
```env
POSTGRES_HOST=postgres
POSTGRES_PORT=5432
POSTGRES_USER=emuser
POSTGRES_PASSWORD=EmP@ssw0rd!
POSTGRES_DB=employee_manager_db

CONNECTION_STRING=Host=postgres;Port=5432;Database=employee_manager_db;Username=emuser;Password=EmP@ssw0rd!

JWT_ISSUER=EmployeeManagerApi
JWT_KEY=VerySecretKeyForDevDontUseInProd123!
```

> **Note:** Do not hardcode the JWT key in production. Use environment variables or a secure secret manager.

### Frontend
```env
REACT_APP_API_URL=http://localhost:8080
```

---

## Backend Setup

1. Navigate to the backend folder:
```bash
cd backend
```

2. Restore packages:
```bash
dotnet restore
```

3. Apply database migrations:
```bash
dotnet ef database update
```

4. Run the backend:
```bash
dotnet run
```

---

## Frontend Setup

1. Navigate to the frontend folder:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

3. Run the frontend:
```bash
npm run dev
```

4. Build for production:
```bash
npm run build
```

---

## Password Validation

The frontend validates passwords according to these rules:

- Minimum 8 characters
- At least one lowercase letter
- At least one uppercase letter
- At least one number
- At least one special character (e.g., !@#$%)
- No spaces

Weak passwords will trigger the message:

```
Weak password — please fix the requirements before continuing.
```

---

## Testing

### Backend
- Unit tests are in `src/tests/EmployeeManager.Tests`.
- Run tests using:
```bash
dotnet test
```

- Coverage is collected via `coverlet.collector`.

---

## Important Notes

- Always use UTC for `DateTime` values in the backend to avoid PostgreSQL `timestamp with time zone` errors.
- Use environment variables for sensitive data such as JWT keys and database passwords.
- Ensure package versions are compatible to avoid JWT or EF Core issues.

---

## License

This project is for educational purposes. No license specified.

