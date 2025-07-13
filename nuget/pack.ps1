# Ensure script fails on errors
$ErrorActionPreference = "Stop"

# Move to the repo root
cd $PSScriptRoot
cd ..

# Read version from version.txt (must be in the same directory as the script or provide path)
$versionFile = Join-Path $PSScriptRoot "version.txt"
if (!(Test-Path $versionFile)) {
    throw "version.txt file not found at $versionFile"
}
$version = Get-Content $versionFile | Select-Object -First 1
if (-not $version) {
    throw "version.txt is empty or invalid"
}

# Define other variables
$nugetServer = "https://api.nuget.org/v3/index.json"
$solutionFile = "Vapolia.KeyValueLite.sln"
$nuspecFile = "Vapolia-KeyValueLite.nuspec"
$packageName = "PlaceMyOrder-Vapolia-KeyValueLite.$version.nupkg"

# Restore NuGet packages
dotnet restore $solutionFile

# Build the solution
dotnet build $solutionFile --configuration Release --no-restore

# Navigate to NuGet folder
cd .\nuget

# Clean up any old packages
Remove-Item *.nupkg -ErrorAction SilentlyContinue

# Pack the NuGet package
nuget pack $nuspecFile -Version $version

# Push the NuGet package using GitHub secret API key
nuget push $packageName -Source $nugetServer -ApiKey $env:NUGET_API_KEY
