cd /d %~dp0
cd /d ..\src\Fischless.Configuration
dotnet restore
dotnet build -c Release
dotnet pack -c Release -o ../../build/
cd /d %~dp0
cd /d ..\src\Fischless.Configuration.Json
dotnet restore
dotnet build -c Release
dotnet pack -c Release -o ../../build/
cd /d %~dp0
cd /d ..\src\Fischless.Configuration.Yaml
dotnet restore
dotnet build -c Release
dotnet pack -c Release -o ../../build/
@pause
