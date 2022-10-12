#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DatabaseProvider/DatabaseProvider.csproj", "DatabaseProvider/"]
RUN dotnet restore "DatabaseProvider/DatabaseProvider.csproj"
COPY . .
WORKDIR "/src/DatabaseProvider"
RUN dotnet build "DatabaseProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DatabaseProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DatabaseProvider.dll"]