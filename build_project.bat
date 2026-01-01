@echo off
echo Building Century Rayon School Application...
echo.

REM Set paths
set MSBUILD="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
set PROJECT="CenturyRayonSchool\CenturyRayonSchool.csproj"

REM Check if MSBuild exists
if not exist %MSBUILD% (
    echo ERROR: MSBuild not found at %MSBUILD%
    pause
    exit /b 1
)

REM Check if project exists
if not exist %PROJECT% (
    echo ERROR: Project file not found at %PROJECT%
    pause
    exit /b 1
)

echo Found MSBuild: %MSBUILD%
echo Found Project: %PROJECT%
echo.
echo Starting build...
echo.

REM Build the project
%MSBUILD% %PROJECT% /p:Configuration=Debug /t:Build /v:minimal

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo BUILD SUCCESSFUL!
    echo ========================================
    echo.
) else (
    echo.
    echo ========================================
    echo BUILD FAILED!
    echo ========================================
    echo.
)

pause
