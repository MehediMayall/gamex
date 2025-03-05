dotnet tool install --global dotnet-reportgenerator-globaltool

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=CoverageResults/
reportgenerator -reports:CoverageResults/coverage.cobertura.xml -targetdir:CoverageReports -reporttypes:Html

