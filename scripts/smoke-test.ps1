param(
    [string]$BaseUrl = "http://localhost:5088"
)

$ErrorActionPreference = "Stop"

Write-Host "[1/3] Build..."
dotnet build .\InventoryWarehouseSystem.sln | Out-Host

Write-Host "[2/3] Tests..."
dotnet test .\InventoryWarehouseSystem.sln --no-build | Out-Host

Write-Host "[3/3] Health check..."
$response = Invoke-WebRequest -Uri "$BaseUrl/api/health" -UseBasicParsing -TimeoutSec 10
if ($response.StatusCode -ne 200) {
    throw "Health endpoint returned status $($response.StatusCode)"
}

Write-Host "Smoke test passed."
Write-Host $response.Content
