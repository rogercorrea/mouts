# Employee Manager

A complete application to manage employees with authentication, roles, and security best practices.  
This project is divided into two main parts: **Backend (ASP.NET Core + PostgreSQL)** and **Frontend (React + Vite)**.  
It also includes **Docker support** for local development and production.

---

## 🚀 Features

- User registration and authentication with **JWT**  
- Secure password handling with **BCrypt**  
- Role-based authorization (Admin, Manager, Employee)  
- Employee CRUD (Create, Read, Update, Delete)  
- PostgreSQL integration with **Entity Framework Core**  
- Password strength validation on frontend  
- API documentation with **Swagger**  
- Unit and integration tests with **xUnit** and **Moq**

---

## 🏗 SOLID Principles

This project was developed following **SOLID principles** to ensure clean architecture and maintainability:

- **S (Single Responsibility Principle):** Each service, controller, and repository has one clear responsibility.  
- **O (Open/Closed Principle):** The system is open for extension (new services, roles, validation rules) but closed for modification.  
- **L (Liskov Substitution Principle):** Interfaces and abstractions (e.g., `IEmployeeRepository`) allow substituting implementations without breaking functionality.  
- **I (Interface Segregation Principle):** Services use focused interfaces (e.g., repositories, authentication service) instead of large ones.  
- **D (Dependency Inversion Principle):** High-level modules depend on abstractions, and dependencies are injected using **.NET Core Dependency Injection**.

✅ Result: Easier testing, modular design, and scalability.

---

## 🔒 Security Benefits

This project implements several security best practices:

- **JWT Authentication:** Tokens are generated with strong cryptography and configurable expiration times.  
- **BCrypt Password Hashing:** Passwords are never stored in plain text.  
- **Environment Variables:** Secrets like `JWT_KEY` and database credentials are stored in `.env` files (not in source code).  
- **Docker Isolation:** Backend, frontend, and database run in **separate containers**, reducing risks of cross-contamination.  
- **Role-based Access Control:** APIs are protected according to user role (Admin, Manager, Employee).  
- **Validation:** Inputs are validated with **FluentValidation** to prevent malformed or malicious data.

---

## 🐳 Running with Docker

This project supports **Docker Compose** for easy setup.

1. Make sure you have **Docker** and **Docker Compose** installed.  
2. Copy `.env.example` to `.env` and configure your environment variables (JWT key, DB connection string, etc.).  
3. Run the following command:

```bash
docker compose up --build
```

4. The services will be available at:
   - **Backend API:** http://localhost:5000  
   - **Frontend App:** http://localhost:3000  
   - **PostgreSQL Database:** localhost:5432  

To stop containers:

```bash
docker compose down
```

---

## 🖥 Backend Setup (Manual)

```bash
cd backend/EmployeeManager.Api
dotnet restore
dotnet ef database update
dotnet run
```

---

## 🌐 Frontend Setup (Manual)

```bash
cd frontend
npm install
npm run dev
```

---

## ✅ Running Tests

```bash
cd backend/src/tests/EmployeeManager.Tests
dotnet test
```

---

## 📂 Project Structure

```
backend/
  EmployeeManager.Api/         # ASP.NET Core API
  EmployeeManager.Infrastructure/ # Repositories, DbContext, Configurations
  src/tests/EmployeeManager.Tests # Unit & integration tests
frontend/
  src/components/              # React components
  src/utils/                   # Password validation logic
docker-compose.yml             # Docker configuration
README.md
```

---

## 📌 Notes

- Do **not** hardcode secrets (like JWT keys) in the codebase. Always use **environment variables**.  
- Make sure to run database migrations when changing models.  
- Passwords must follow strong validation rules (length, uppercase, lowercase, numbers, symbols, no spaces).

---
