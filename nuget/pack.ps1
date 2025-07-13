# Ensure script fails on errors
$ErrorActionPreference = "Stop"

# Get absolute path to the repo root (one level up from script location)
$repoRoot = Resolve-Path "$PSScriptRoot\.."

# Path to version.txt in the root
$versionFile = Join-Path $repoRoot "version.txt"

# Validate and read version
if (!(Test-Path $versionFile)) {
    throw "version.txt file not found at $versionFile"
}
$version = Get-Content $versionFile | Select-Object -First 1
if (-not $version) {
    throw "version.txt is empty or invalid"
}

# Define other variables
$nugetServer = "https://api.nuget.org/v3/index.json"
$solutionFile = Join-Path $repoRoot "Vapolia.KeyValueLite.sln"
$nuspecFile = "Vapolia-KeyValueLite.nuspec"
$packageName = "PlaceMyOrder-Vapolia-KeyValueLite.$version.nupkg"

# Restore NuGet packages
dotnet restore $solutionFile

# Build the solution
dotnet build $solutionFile --configuration Release --no-restore

# Clean old nupkg files
Remove-Item "$PSScriptRoot\*.nupkg" -ErrorAction SilentlyContinue

# Pack the NuGet package (runs from the nuget/ folder where .nuspec lives)
Push-Location $PSScriptRoot
nuget pack $nuspecFile -Version $version
Pop-Location

# Push the NuGet package
nuget push "$PSScriptRoot\$packageName" -Source $nugetServer -ApiKey $env:NUGET_API_KEY
