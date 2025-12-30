$iisExpressPath64 = "C:\Program Files\IIS Express\iisexpress.exe"
$iisExpressPath32 = "C:\Program Files (x86)\IIS Express\iisexpress.exe"
$iisExpress = if (Test-Path $iisExpressPath64) { $iisExpressPath64 } elseif (Test-Path $iisExpressPath32) { $iisExpressPath32 } else { $null }

if (-not $iisExpress) {
    Write-Error "IIS Express executable not found in standard locations."
    Write-Host "Please install IIS Express or verify its location."
    exit 1
}

$sitePath = Resolve-Path ".\CenturyRayonSchool"
$port = 8080
$url = "http://localhost:$port"

Write-Host "Starting IIS Express..."
Write-Host "Path: $sitePath"
Write-Host "Port: $port"

# Start IIS Express in background (or separate window)
Start-Process $iisExpress -ArgumentList "/path:`"$sitePath`" /port:$port" -WindowStyle Minimized

# Wait a moment for it to start
Start-Sleep -Seconds 2

# Open in default browser
Start-Process $url

Write-Host "Application launched. Check your browser."
