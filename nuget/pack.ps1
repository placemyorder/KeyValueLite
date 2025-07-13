$baseDir = (Get-Item $PSScriptRoot).Parent.FullName

$versionFile = [IO.Path]::Combine($baseDir,'version.txt')
$releasefolder = [IO.Path]::Combine($baseDir,'Vapolia.KeyValueLite','bin','Release')

Set-Location $baseDir

$version = Get-Content $versionFile

dotnet build --configuration Release

dotnet test

dotnet pack --configuration Release /p:Version=$version

Set-Location $releasefolder

dotnet nuget push *.nupkg -k $env:NUGET_API_KEY -s https://api.nuget.org/v3/index.json --skip-duplicate --no-symbols
