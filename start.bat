@echo off
echo Starting Chat Application...
echo.

echo Checking if .NET is installed...
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET 9.0 SDK is not installed!
    echo Please install .NET 9.0 SDK from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo .NET is installed. Version:
dotnet --version
echo.

echo Restoring packages...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: Failed to restore packages!
    pause
    exit /b 1
)

echo Packages restored successfully!
echo.

echo Building the application...
dotnet build
if %errorlevel% neq 0 (
    echo ERROR: Build failed!
    pause
    exit /b 1
)

echo Build successful!
echo.

echo Starting the application...
echo The application will be available at:
echo - API: https://localhost:7000
echo - Swagger UI: https://localhost:7000/swagger
echo - SignalR Hub: https://localhost:7000/chathub
echo.
echo Press Ctrl+C to stop the application
echo.

dotnet run 