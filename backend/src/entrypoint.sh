#!/bin/bash
set -e

echo "Waiting for Postgres to be ready..."
until pg_isready -h "$POSTGRES_HOST" -p "$POSTGRES_PORT" -U "$POSTGRES_USER"; do
  sleep 2
done

echo "Running database migrations..."
# Usa o SDK container separado para rodar ef migrations
dotnet tool restore
dotnet ef database update

echo "Starting API..."
exec dotnet EmployeeManager.Api.dll
