#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["secure-ftp-service/secure-ftp-service.csproj", "secure-ftp-service/"]
RUN dotnet restore "secure-ftp-service/secure-ftp-service.csproj"
COPY . .
WORKDIR "/src/secure-ftp-service"
RUN dotnet build "secure-ftp-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "secure-ftp-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "secure-ftp-service.dll"]