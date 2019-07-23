dotnet build ./WebAdvertSearchWorker.csproj

dotnet lambda package  --msbuild-parameters "./WebAdvertSearchWorker.csproj" -c Release -o ./bin/SearchWorker.zip --framework netcoreapp2.1

dotnet lambda deploy-function SearchWorker -pac ./bin/SearchWorker.zip -frun dotnetcore2.1