FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

EXPOSE 8080

COPY bin/Debug/net8.0/ .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
