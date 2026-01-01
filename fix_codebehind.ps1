# Fix CodeBehind to CodeFile conversion - more robust version

$projectPath = "CenturyRayonSchool"
$aspxFiles = Get-ChildItem -Path $projectPath -Filter "*.aspx" -Recurse -File

Write-Host "Checking and fixing all .aspx files..." -ForegroundColor Cyan
Write-Host ""

$fixedCount = 0

foreach ($file in $aspxFiles) {
    try {
        # Read file content
        $content = Get-Content -Path $file.FullName -Raw -Encoding UTF8
        
        # Check if file contains CodeBehind (case-insensitive)
        if ($content -match 'CodeBehind\s*=') {
            # Replace CodeBehind with CodeFile (preserve spacing)
            $newContent = $content -replace 'CodeBehind\s*=', 'CodeFile='
            
            # Write back to file with UTF8 encoding
            [System.IO.File]::WriteAllText($file.FullName, $newContent, [System.Text.Encoding]::UTF8)
            
            Write-Host "[FIXED] $($file.Name)" -ForegroundColor Green
            $fixedCount++
        }
    }
    catch {
        Write-Host "[ERROR] $($file.Name): $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "Fixed $fixedCount files with CodeBehind" -ForegroundColor Green
