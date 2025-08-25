@echo off
echo Checking available ports for Chat Application...
echo.

echo Checking port 7000 (recommended)...
netstat -ano | findstr :7000 >nul
if %errorlevel% equ 0 (
    echo ❌ Port 7000 is already in use
    netstat -ano | findstr :7000
) else (
    echo ✅ Port 7000 is available
)

echo.
echo Checking port 5010 (previous default)...
netstat -ano | findstr :5010 >nul
if %errorlevel% equ 0 (
    echo ❌ Port 5010 is already in use
    netstat -ano | findstr :5010
) else (
    echo ✅ Port 5010 is available
)

echo.
echo Checking port 7027 (old backend port)...
netstat -ano | findstr :7027 >nul
if %errorlevel% equ 0 (
    echo ❌ Port 7027 is already in use
    netstat -ano | findstr :7027
) else (
    echo ✅ Port 7027 is available
)

echo.
echo Available ports for backend:
echo - 7000 (recommended - matches frontend config)
echo - 5010 (if you prefer this port)
echo - 7027 (if you prefer this port)
echo.
echo To kill a process using a specific port:
echo netstat -ano | findstr :PORT_NUMBER
echo taskkill /PID PROCESS_ID /F
echo.
pause 