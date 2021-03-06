rm "bin\release\" -Recurse -Force
dotnet pack --configuration Release --include-symbols -p:SymbolPackageFormat=snupkg
$package=(ls .\bin\Release\*.nupkg).FullName
dotnet nuget push $package --source "https://api.nuget.org/v3/index.json"
$ver = ((ls .\bin\release\*.nupkg)[0].Name -replace '[^\.]*\.[^\.]*\.(\d+(\.\d+){1,3}).*', '$1')
git tag -a "Bitcoin3.Altcoins/v$ver" -m "Bitcoin3.Altcoins/$ver"
git push --tags
