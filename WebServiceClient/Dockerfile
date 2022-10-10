#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.



FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebServiceClient/WebServiceClient.csproj", "WebServiceClient/"]
RUN dotnet restore "WebServiceClient/WebServiceClient.csproj"
COPY . .
WORKDIR "/src/WebServiceClient"
RUN dotnet build "WebServiceClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebServiceClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebServiceClient.dll"]

