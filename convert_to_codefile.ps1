# PowerShell script to convert all CodeBehind to CodeFile in .aspx files

$projectPath = "CenturyRayonSchool"
$aspxFiles = Get-ChildItem -Path $projectPath -Filter "*.aspx" -Recurse -File

Write-Host "Found $($aspxFiles.Count) .aspx files to process..." -ForegroundColor Cyan
Write-Host ""

$convertedCount = 0
$skippedCount = 0

foreach ($file in $aspxFiles) {
    $content = Get-Content -Path $file.FullName -Raw
    
    # Check if file contains CodeBehind
    if ($content -match 'CodeBehind=') {
        # Replace CodeBehind with CodeFile
        $newContent = $content -replace 'CodeBehind=', 'CodeFile='
        
        # Write back to file
        Set-Content -Path $file.FullName -Value $newContent -NoNewline
        
        Write-Host "[CONVERTED] $($file.FullName.Replace($PWD, '.'))" -ForegroundColor Green
        $convertedCount++
    }
    else {
        Write-Host "[SKIPPED] $($file.FullName.Replace($PWD, '.')) - No CodeBehind found" -ForegroundColor Yellow
        $skippedCount++
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Conversion Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Converted: $convertedCount files" -ForegroundColor Green
Write-Host "Skipped: $skippedCount files" -ForegroundColor Yellow
Write-Host "Total: $($aspxFiles.Count) files" -ForegroundColor Cyan
