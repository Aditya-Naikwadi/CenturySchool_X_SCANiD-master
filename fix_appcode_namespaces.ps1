# Fix namespace issues in App_Code files

$appCodePath = "CenturyRayonSchool\App_Code"
$csFiles = Get-ChildItem -Path $appCodePath -Filter "*.cs" -File

Write-Host "Fixing namespace references in App_Code files..." -ForegroundColor Cyan
Write-Host ""

$fixedCount = 0

foreach ($file in $csFiles) {
    try {
        $content = Get-Content -Path $file.FullName -Raw -Encoding UTF8
        $modified = $false
        
        # Uncomment Model namespace imports (they're in the same App_Code folder now)
        if ($content -match '//using CenturyRayonSchool\.Model;') {
            $content = $content -replace '//using CenturyRayonSchool\.Model;', 'using CenturyRayonSchool.Model;'
            $modified = $true
        }
        
        if ($content -match '//using CenturyRayonSchool\.FeesModule\.Model;') {
            $content = $content -replace '//using CenturyRayonSchool\.FeesModule\.Model;', 'using CenturyRayonSchool.FeesModule.Model;'
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
Write-Host "Fixed $fixedCount files in App_Code" -ForegroundColor Green
