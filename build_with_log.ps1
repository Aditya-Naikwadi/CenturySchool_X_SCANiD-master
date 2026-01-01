$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
$project = "CenturyRayonSchool\CenturyRayonSchool.csproj"

Write-Host "Building project..." -ForegroundColor Cyan

$output = & $msbuild $project /p:Configuration=Debug /t:Build /v:detailed 2>&1

$output | Out-File -FilePath "build_log.txt" -Encoding UTF8

Write-Host "Build log saved to build_log.txt" -ForegroundColor Green

# Show last 50 lines
Write-Host "`nLast 50 lines of build output:" -ForegroundColor Yellow
$output | Select-Object -Last 50

exit $LASTEXITCODE
