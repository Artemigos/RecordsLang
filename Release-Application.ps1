dotnet publish src/RecordsLang -o ../../release -c Release
nuget pack build/RecordsLang.nuspec -OutputDirectory build
