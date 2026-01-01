# PowerShell script to comment out Model namespace references in all .cs files

$projectPath = "CenturyRayonSchool"
$csFiles = Get-ChildItem -Path $projectPath -Filter "*.cs" -Recurse -File

Write-Host "Fixing Model namespace references in .cs files..." -ForegroundColor Cyan
Write-Host ""

$fixedCount = 0

foreach ($file in $csFiles) {
    try {
        $content = Get-Content -Path $file.FullName -Raw -Encoding UTF8
        $modified = $false
        
        # Comment out Model namespace imports
        if ($content -match 'using CenturyRayonSchool\.Model;') {
            $content = $content -replace 'using CenturyRayonSchool\.Model;', '//using CenturyRayonSchool.Model;'
            $modified = $true
        }
        
        if ($content -match 'using CenturyRayonSchool\.FeesModule\.Model;') {
            $content = $content -replace 'using CenturyRayonSchool\.FeesModule\.Model;', '//using CenturyRayonSchool.FeesModule.Model;'
            $modified = $true
        }
        
        if ($modified) {
            [System.IO.File]::WriteAllText($file.FullName, $content, [System.Text.Encoding]::UTF8)
            Write-Host "[FIXED] $($file.Name)" -ForegroundColor Green
            $fixedCount++
        }
    }
    catch {
        Write-Host "[ERROR] $($file.Name): $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "Fixed $fixedCount .cs files" -ForegroundColor Green
