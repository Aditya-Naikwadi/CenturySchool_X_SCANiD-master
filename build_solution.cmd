@echo off
setlocal

echo ========================================
echo Building Century Rayon School Application
echo ========================================
echo.

set MSBUILD_PATH=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe
set PROJECT_PATH=CenturyRayonSchool\CenturyRayonSchool.csproj

if not exist "%MSBUILD_PATH%" (
    echo ERROR: MSBuild not found at: %MSBUILD_PATH%
    exit /b 1
)

if not exist "%PROJECT_PATH%" (
    echo ERROR: Project file not found at: %PROJECT_PATH%
    exit /b 1
)

echo MSBuild: %MSBUILD_PATH%
echo Project: %PROJECT_PATH%
echo.
echo Building project (this may take a minute)...
echo.

"%MSBUILD_PATH%" "%PROJECT_PATH%" /p:Configuration=Debug /t:Rebuild /v:minimal /nologo /m

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo BUILD SUCCESSFUL!
    echo ========================================
    echo.
    echo Checking generated DLLs...
    dir CenturyRayonSchool\bin\*.dll 2>nul
    echo.
    echo Build completed successfully!
) else (
    echo.
    echo ========================================
    echo BUILD FAILED - Exit Code: %ERRORLEVEL%
    echo ========================================
    echo.
    exit /b %ERRORLEVEL%
)

endlocal
