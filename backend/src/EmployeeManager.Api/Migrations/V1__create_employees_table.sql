-- Simple SQL migration to create Employees table (used if dotnet-ef not available)
CREATE TABLE IF NOT EXISTS "Employees" (
  "Id" uuid PRIMARY KEY,
  "FirstName" text NOT NULL,
  "LastName" text NOT NULL,
  "Email" text NOT NULL UNIQUE,
  "DocumentNumber" text NOT NULL UNIQUE,
  "BirthDate" timestamp without time zone NOT NULL,
  "PasswordHash" text,
  "Role" integer NOT NULL,
  "ManagerId" uuid NULL
);
