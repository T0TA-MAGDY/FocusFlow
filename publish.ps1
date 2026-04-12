param(
    [string]$Runtime = "win-x64",
    [switch]$SelfContained
)

$apiProject = "./FocusFlow.API/FocusFlow.API.csproj"
$outputDir = "./publish"

Write-Host "Publishing FocusFlow API..." -ForegroundColor Cyan
dotnet publish $apiProject -c Release -o $outputDir -r $Runtime --self-contained:$($SelfContained.IsPresent)

if ($LASTEXITCODE -ne 0) {
    throw "Publish failed."
}

Write-Host "Publish completed at $outputDir" -ForegroundColor Green
