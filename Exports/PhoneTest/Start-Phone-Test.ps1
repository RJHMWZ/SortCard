$ErrorActionPreference = 'Stop'
Set-Location -LiteralPath $PSScriptRoot
Write-Host 'Little Finds - http://10.80.30.25:8766/LittleFinds.html'
python -m http.server 8766 --bind 0.0.0.0 --directory $PSScriptRoot
