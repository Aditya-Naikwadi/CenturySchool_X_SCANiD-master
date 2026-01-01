$ErrorActionPreference = "Continue"

Write-Host "Building Century Rayon School Application..." -ForegroundColor Cyan
Write-Host ""

$msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
$projectPath = "CenturyRayonSchool\CenturyRayonSchool.csproj"

if (-not (Test-Path $msbuildPath)) {
    Write-Host "ERROR: MSBuild not found at $msbuildPath" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $projectPath)) {
    Write-Host "ERROR: Project file not found at $projectPath" -ForegroundColor Red
    exit 1
}

Write-Host "Found MSBuild: $msbuildPath" -ForegroundColor Green
Write-Host "Found Project: $projectPath" -ForegroundColor Green
Write-Host ""
Write-Host "Starting build..." -ForegroundColor Yellow
Write-Host ""

& $msbuildPath $projectPath /p:Configuration=Debug /t:Build /v:minimal

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "========================================"  -ForegroundColor Green
    Write-Host "BUILD SUCCESSFUL!" -ForegroundColor Green
    Write-Host "========================================"  -ForegroundColor Green
    Write-Host ""
} else {
    Write-Host ""
    Write-Host "========================================"  -ForegroundColor Red
    Write-Host "BUILD FAILED!" -ForegroundColor Red
    Write-Host "========================================"  -ForegroundColor Red
    Write-Host ""
    exit 1
}
