#!/bin/bash
set -e

# Run EF migrations (this requires dotnet-ef installed in build stage and tools available)
echo 'Running database migrations...'
dotnet ef database update --no-build --project ./

echo 'Starting app...'
dotnet EmployeeManager.Api.dll
