FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /build
EXPOSE 3100


COPY ./app .

ENTRYPOINT ["dotnet", "gamex.ApiGateway.dll"]