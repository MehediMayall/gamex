dotnet publish ./src/gamex.Auth.Services/gamex.Auth.Services.csproj -c Release -o ./app-output
robocopy C:\PROJECTS\gamex\gamex-auth-services\src\gamex.Auth.Services\StaticContents C:\PROJECTS\gamex\gamex.Auth.Services\app-output\StaticContents /E
