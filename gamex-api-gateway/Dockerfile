FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
EXPOSE 3100


COPY ./app-output .

ENTRYPOINT ["dotnet", "gamex.api.gateway.dll"]