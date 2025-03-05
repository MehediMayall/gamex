# Use the official ASP.NET Core runtime image as a base
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /build
EXPOSE 3300



# Build the application according to the project dependencies
COPY ./app ./

# Health check
HEALTHCHECK CMD curl --fail http://localhost || exit 1

# Run Command for the entrypoint
ENTRYPOINT ["dotnet", "gamex.Auth.Services.dll"]